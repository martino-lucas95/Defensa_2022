using NUnit.Framework;
using Battleship;
using Telegram.Bot.Types;
using System;

namespace Library.Tests
{
    // El handle se encarga de buscar partida en modo predictivo.
    // El primer usuario que busca, se manda al lobby (sala de espera).
    // Si ya hay un usuario en el lobby, y este busca el mismo modo de juego, 
    // entonces se los matchea y se inicia el juego.
    [TestFixture]
    public class SearchPredictiveGameHandlerTests
    {
        private SearchPredictiveGameHandler handler;
        private Message message;
        private Battleship.User user1;
        private Battleship.User user2;
        private Telegram.Bot.Types.User userTelegram1;
        private Telegram.Bot.Types.User userTelegram2;

        private IPrinter Printer;

        [SetUp]
        public void Setup()
        {
            Printer = new ConsolePrinter();

            handler = new SearchPredictiveGameHandler(null, Printer);
            message = new Message();

            UserRegister.CreateUser(3);
            UserRegister.CreateUser(4);
            
            user1 = UserRegister.GetUser(3);
            user2 = UserRegister.GetUser(4);

            userTelegram1 = new Telegram.Bot.Types.User();
            userTelegram1.Id = 3;

            userTelegram2 = new Telegram.Bot.Types.User();
            userTelegram2.Id = 4;

            message.From = userTelegram1;

            user1.ChangeGameMode("predictive");
            user2.ChangeGameMode("predictive");
        }

        [Test]
        public void TestSearchPredictiveGameHandler()
        {
            message.Text = handler.Keywords[0];
            string response;
            user1.ChangeStatus("start");

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo($"Entraste a la sala de espera. Modo de juego: predictive"));
            Assert.AreEqual("lobby", user1.getStatus());
   
        }

        [Test]
        public void AddOtherUser()
        {
            message.Text = handler.Keywords[0];
            string response;
            user1.ChangeStatus("start");

            handler.Handle(message, out response);

            user2.ChangeStatus("start");
            message.From = userTelegram2;

            IHandler result = handler.Handle(message, out response);

            int GameId = GamesRegister.GetGameByUserId(user2.GetID()).GetId();

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo($"Se ha unido a una partida con id {GameId}"));
            Assert.AreEqual("position ships", user1.getStatus());
            Assert.AreEqual("position ships", user2.getStatus());
        }
    }
}