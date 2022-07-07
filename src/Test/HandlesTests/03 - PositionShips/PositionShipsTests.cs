using NUnit.Framework;
using Battleship;
using Telegram.Bot.Types;
using System;

namespace Library.Tests
{
    // El siguiente Handler es el encargado de posicionar los barcos, se le indica la 
    // coordenada y la orientación del mismo. Por convensión se coloca del mas grande
    // al mas pequeño. En total hay 4 barcos que varian el tamaño desde 2 a 5 lugares c/u
    [TestFixture]
    public class PositionShipsHandleTest
    {
        private PositionShipsHandle handler;
        private Message message;
        private Battleship.User user1;
        private Battleship.User user2;
        private Telegram.Bot.Types.User userTelegram1;
        private Telegram.Bot.Types.User userTelegram2;

        private Battleship.Game game;

        private IPrinter Printer;

        [SetUp]
        public void Setup()
        {
            Printer = new ConsolePrinter();

            handler = new PositionShipsHandle(null, Printer);
            message = new Message();

            UserRegister.CreateUser(5);
            UserRegister.CreateUser(6);
            
            user1 = UserRegister.GetUser(5);
            user2 = UserRegister.GetUser(6);

            userTelegram1 = new Telegram.Bot.Types.User();
            userTelegram1.Id = 5;

            userTelegram2 = new Telegram.Bot.Types.User();
            userTelegram2.Id = 6;

            message.From = userTelegram1;

            int idGame = GamesRegister.CreateGame(user1, user2);
            game = GamesRegister.GetGameByUserId(5);
        }

        [Test]
        public void TestPositionShipsHandle()
        {
            message.Text = "posicionar barco a1 down";
            string response;
            user1.ChangeStatus("position ships");

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo($"El barco se creó correctamente"));

            for (int i = 2; i < 5; i++)
            {
                message.Text = $"posicionar barco a{i} down";
                result = handler.Handle(message, out response);
            }

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo($"Los barcos estan listos"));
            
        }


        [TestCase("a1 up")]
        [TestCase("j10 down")]
        public void InvalidCoordinates(string coor)
        {
            message.Text = $"posicionar barco {coor}";
            string response;
            user1.ChangeStatus("position ships");

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("La Coordenada ingresada es incorrecta."));
            
        }

    }
}