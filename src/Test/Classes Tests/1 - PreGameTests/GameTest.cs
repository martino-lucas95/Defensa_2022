using NUnit.Framework;
using Battleship;
using System;
using System.IO;

namespace Library.Tests
{   
    /// <summary>
    /// Se testea Game
    /// </summary>
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void GameTest()
        {
            User user1 = new User(13);
            User user2 = new User(14);
            
            Game game1 = new Game(user1, user2);

            game1.AddUserWinner(user1);

            int GameId = int.Parse(System.IO.File.ReadAllText("CounterIdGame.txt")) - 1;
            Assert.AreEqual(GameId, game1.GetId());
            Assert.AreEqual(user2, game1.GetOtherUserById(user1.GetID()));
        }
    }
}