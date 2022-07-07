using NUnit.Framework;
using Battleship;
using System;
using System.IO;

namespace Library.Tests
{   
    /// <summary>
    /// Se testea el Lobby
    /// </summary>
    [TestFixture]
    public class LobbyTests
    {
        [Test]
        public void LobbyTest()
        {
            User user1 = new User(17);
            User user2 = new User(18);

            user1.ChangeGameMode("normal");
            user2.ChangeGameMode("predictive");
            
            Lobby.AddUser(user1);
            Lobby.AddUser(user2);

            Assert.AreEqual(1, Lobby.NumberUsersLobby("normal"));
            Assert.AreEqual(1, Lobby.NumberUsersLobby("predictive"));

            Lobby.GetAndRemoveUser("normal");
            Assert.AreEqual(0, Lobby.NumberUsersLobby("normal"));
            Assert.AreEqual(1, Lobby.NumberUsersLobby("predictive"));

            Lobby.GetAndRemoveUser("predictive");
            Assert.AreEqual(0, Lobby.NumberUsersLobby("normal"));
            Assert.AreEqual(0, Lobby.NumberUsersLobby("predictive"));
        }
    }
}