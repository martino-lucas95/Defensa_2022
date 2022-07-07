using NUnit.Framework;
using Battleship;
using Telegram.Bot.Types;
using System;

namespace Library.Tests
{
    // Si un jugador esta en el lobby, y aún no ha conseguido pareja, tiene la posibilidad
    // de salir del mismo cunado lo desee. Este handler se encarga de eso.
    [TestFixture]
    public class ExitLobbyHandleTests
    {
        private ExitLobbyHandle handler;
        private Message message;
        private Battleship.User user1;
        private Battleship.User user2;
        private Telegram.Bot.Types.User userTelegram1;
        private Telegram.Bot.Types.User userTelegram2;

        IPrinter Printer;

        [SetUp]
        public void Setup()
        {
            Printer = new ConsolePrinter();
            
            handler = new ExitLobbyHandle(null);
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

            int idGame = GamesRegister.CreateGame(user1, user2);

        }

        [Test]
        public void TestExitLobbyHandleNormalMode()
        {
            SearchGameHandler handler1 = new SearchGameHandler(null, Printer);

            message.Text = handler1.Keywords[0];
            string response;
            user1.ChangeStatus("start");

            IHandler result = handler1.Handle(message, out response);
            
            message.Text = handler.Keywords[0];
            user1.ChangeStatus("lobby");

            result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Has salido de la sala de espera"));
            Assert.AreEqual("start", user1.getStatus());  
        }

        [Test]
        public void TestExitLobbyHandlePredictiveMode()
        {
            SearchPredictiveGameHandler handler1 = new SearchPredictiveGameHandler(null, Printer);

            message.Text = handler1.Keywords[0];
            string response;
            user1.ChangeStatus("start");

            IHandler result = handler1.Handle(message, out response);
            
            message.Text = handler.Keywords[0];
            user1.ChangeStatus("lobby");

            result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Has salido de la sala de espera"));
            Assert.AreEqual("start", user1.getStatus());  
        }

    }
}