using NUnit.Framework;
using Battleship;
using System;

namespace Library.Tests
{   
    /// <summary>
    /// Se testea User
    /// </summary>
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void UserTest()
        {
            User user1 = new User(11);

            user1.ChangeGameMode("normal");

            user1.ChangeStatus($"in {user1.GetGameMode()} game");

            Assert.AreEqual("in normal game", user1.getStatus());
        }
    }
}