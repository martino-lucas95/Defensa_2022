using NUnit.Framework;
using Battleship;
using System;

namespace Library.Tests
{   
    /// <summary>
    /// Se testea UserRegister
    /// </summary>
    [TestFixture]
    public class UserRegisterTests
    {
        [Test]
        public void UserRegisterTest()
        {
            UserRegister.CreateUser(12);

            User user1 = UserRegister.GetUser(12);

            Assert.AreEqual(12, user1.GetID());
            Assert.AreEqual("start", user1.getStatus());
        }
    }
}