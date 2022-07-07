using NUnit.Framework;
using Battleship;
using Telegram.Bot.Types;
using System;

namespace Library.Tests
{   // El handle se encargad de crear usuarios
    [TestFixture]
    public class CreateUserHandleTest
    {
        private CreateUserHandle handler;
        private Message message;

        private Telegram.Bot.Types.User userTelegram1;

        [SetUp]
        public void Setup()
        {
            handler = new CreateUserHandle(null);
            message = new Message();

            userTelegram1 = new Telegram.Bot.Types.User();
            userTelegram1.Id = 20;

            message.From = userTelegram1;
        }

        [Test]
        public void TestCreateUserHandle()
        {
            message.Text = handler.Keywords[0];
            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("El usuario se ha creado correctamente"));
        }
    }
}