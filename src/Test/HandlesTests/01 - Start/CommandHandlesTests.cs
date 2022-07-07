using NUnit.Framework;
using Battleship;
using Telegram.Bot.Types;
using System;

namespace Library.Tests
{   // El handle se comandos se encarga de retornar los posibles comandos en base
    // al estado actual del user
    [TestFixture]
    public class CommandsHandleTests
    {
        private CommandsHandle handler;
        private Message message;
        private Battleship.User user1;
        private Battleship.User user2;
        private Telegram.Bot.Types.User userTelegram1;
        private Telegram.Bot.Types.User userTelegram2;

        [SetUp]
        public void Setup()
        {
            handler = new CommandsHandle(null);
            message = new Message();

            UserRegister.CreateUser(1);
            UserRegister.CreateUser(2);

            user1 = UserRegister.GetUser(1);
            user2 = UserRegister.GetUser(2);

            userTelegram1 = new Telegram.Bot.Types.User();
            userTelegram1.Id = 1;

            userTelegram2 = new Telegram.Bot.Types.User();
            userTelegram2.Id = 2;

            message.From = userTelegram1;
        }

        [Test]
        public void TestCommandsHandle()
        {
            message.Text = handler.Keywords[0];
            string response;
            user1.ChangeStatus("start");

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo($"\nComandos en estado '{user1.getStatus()}':\n\n- buscar partida\n- buscar partida predictiva\n- ver partidas jugadas"));

            user1.ChangeGameMode("normal");
            user1.ChangeStatus($"in {user1.GetGameMode()} game");
            string forInGame = "\n- ver tableros\n- aereo <fila> (ejemplo:aereo A)\n- vidente\n- satelite <columna (ejemplo: satelite 1)";

            result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo($"\nComandos en estado '{user1.getStatus()}:'\n\n- atacar <coordenada> (ejemplo: 'atacar A1'){forInGame}"));
        }
    }
}