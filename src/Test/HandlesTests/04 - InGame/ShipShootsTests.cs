using NUnit.Framework;
using Battleship;
using Telegram.Bot.Types;
using System;

namespace Library.Tests
{
    // Una vez en el juego, el jugador puede atacar, para ello debe indicar la coordenada
    // de ataque en el mensaje
    [TestFixture]
    public class ShipShootsTests
    {
        private AttackHandle attackHandler;
        private ShipShootsHandle handler;
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
            
            attackHandler = new AttackHandle(null, Printer);

            handler = new ShipShootsHandle(null, Printer);

            sgameh = new SearchGameHandler(null, Printer);

            pshiph = new PositionShipsHandle(null, Printer);

            message = new Message();

            UserRegister.CreateUser(7);
            UserRegister.CreateUser(8);
            
            user1 = UserRegister.GetUser(7);
            user2 = UserRegister.GetUser(8);

            string response;
            IHandler result;

            userTelegram1 = new Telegram.Bot.Types.User();
            userTelegram1.Id = 7;

            userTelegram2 = new Telegram.Bot.Types.User();
            userTelegram2.Id = 8;

            message.From = userTelegram1;
            message.Text = "buscar partida";
            
            sgameh.Handle(message, out response);

            message.From = userTelegram2;
            message.Text = "buscar partida";

            sgameh.Handle(message, out response);

            for (int i = 1; i < 5; i++)
            {
                message.Text = $"posicionar barco a{i} down";
                result = pshiph.Handle(message, out response);
            }  

            message.From = userTelegram1;

            for (int i = 1; i < 5; i++)
            {
                message.Text = $"posicionar barco a{i} down";
                result = pshiph.Handle(message, out response);
            }              

            message.From = userTelegram2;
            message.Text = "atacar b1";
            
            attackHandler.Handle(message, out response);

          
        }

        [Test]
        public void TestShipShootHandle()
        {
            message.Text = "Disparos acertados";
            message.From = userTelegram2;
            

            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("1"));

        }
    }
}