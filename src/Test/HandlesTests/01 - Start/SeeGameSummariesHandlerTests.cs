using NUnit.Framework;
using Battleship;
using System.IO;
using Telegram.Bot.Types;
using System;

namespace Library.Tests
{
    // El handler para ver resumenes del juego, se encarga de mostrar en pantalla todos 
    // los juegos finalizados hasta el momento, cada juego tiene una id propia.
    [TestFixture]
    public class SeeGameSummariesHandlerTests
    {
        private SeeGameSummariesHandler handler;
        private Message message;
        private Battleship.User user1;
        private Battleship.User user2;
        private Telegram.Bot.Types.User userTelegram1;
        private Telegram.Bot.Types.User userTelegram2;
        private Battleship.Game game;

        [SetUp]
        public void Setup()
        {
            handler = new SeeGameSummariesHandler(null);
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
            game = GamesRegister.GetGameByUserId(1);
        }

        [Test]
        public void TestSeeGameSummariesHandler()
        {
            game.AddUserWinner(user1);

            message.Text = handler.Keywords[0];
            string response;
            user1.ChangeStatus("start");

            StreamWriter writetext = new StreamWriter("GameSummaries.txt", false);
            writetext.WriteLine(game.GameInString());
            writetext.Close();

            // El resumen del juego se puede ver en la siguiente ubicación:
            //          ./src/Test/bin/Debug/net5.0/GameSummaries.txt

            string summaries = System.IO.File.ReadAllText("GameSummaries.txt");
            

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo(summaries));
        }

    }
}