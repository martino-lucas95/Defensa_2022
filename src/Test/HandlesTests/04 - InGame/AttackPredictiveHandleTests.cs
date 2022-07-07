using NUnit.Framework;
using Battleship;
using Telegram.Bot.Types;
using System;

namespace Library.Tests
{
    // Una vez en el juego, el jugador puede atacar, para ello debe indicar la coordenada
    // de ataque en el mensaje
    [TestFixture]
    public class AttackPredictiveHandlerTest
    {
        private AttackPredictiveHandler handler;
        private Message message;
        private Battleship.User user1;
        private Battleship.User user2;
        private Telegram.Bot.Types.User userTelegram1;
        private Telegram.Bot.Types.User userTelegram2;

        private SearchGameHandler sgameh;

        private PositionShipsHandle pshiph;

        private IPrinter Printer;

        [SetUp]
        public void Setup()
        {
            Printer = new ConsolePrinter();
            
            handler = new AttackPredictiveHandler(null, Printer);

            sgameh = new SearchPredictiveGameHandler(null, Printer);

            pshiph = new PositionShipsHandle(null, Printer);

            message = new Message();

            UserRegister.CreateUser(9);
            UserRegister.CreateUser(10);
            
            user1 = UserRegister.GetUser(9);
            user2 = UserRegister.GetUser(10);

            string response;
            IHandler result;

            userTelegram1 = new Telegram.Bot.Types.User();
            userTelegram1.Id = 9;

            userTelegram2 = new Telegram.Bot.Types.User();
            userTelegram2.Id = 10;

            message.From = userTelegram1;
            message.Text = "buscar partida predictiva";
            
            sgameh.Handle(message, out response);

            message.From = userTelegram2;
            message.Text = "buscar partida predictiva";

            sgameh.Handle(message, out response);

            for (int i = 1; i < 6; i++)
            {
                message.Text = $"posicionar barco a{i} down";
                result = pshiph.Handle(message, out response);
            }  

            message.From = userTelegram1;

            for (int i = 1; i < 6; i++)
            {
                message.Text = $"posicionar barco a{i} down";
                result = pshiph.Handle(message, out response);
            }  
        }

        [Test]
        public void TestAttackHandle()
        {
            message.Text = "p ataque a1";
            message.From = userTelegram2;
            if (user2.GetPlayer().GetTurn() == false)
            {
                user2.GetPlayer().ChangeTurn();
            }

            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo($"Tocado\n\n\n\n------Turno cambiado------\n\n"));

        }


        [TestCase("a0")]
        [TestCase("k4")]
        public void InvalidCoordinates(string coor)
        {
            message.Text = $"p ataque {coor}";
            message.From = userTelegram2;

            if (user2.GetPlayer().GetTurn() == false)
            {
                user2.GetPlayer().ChangeTurn();
            }

            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Sucedió un error, vuelve a intentar"));

        }

    }
}