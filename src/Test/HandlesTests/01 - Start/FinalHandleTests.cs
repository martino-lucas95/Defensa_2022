using NUnit.Framework;
using Battleship;
using Telegram.Bot.Types;
using System;

namespace Library.Tests
{   // El handle se encarga de aceptar cualquier comando y responder que no se entiende el comando
    // Se utiliza para controlar los casos en el que el usuario ingresa un comando incorrecto
    [TestFixture]
    public class FinalHandleTest
    {
        private FinalHandle handler;
        private Message message;

        private Telegram.Bot.Types.User userTelegram1;

        [SetUp]
        public void Setup()
        {
            handler = new FinalHandle(null);
            message = new Message();

            userTelegram1 = new Telegram.Bot.Types.User();
            userTelegram1.Id = 21;

            message.From = userTelegram1;
        }

        [Test]
        public void TestFinalHandler()
        {
            message.Text = "fuwerhfewf78";
            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("No entiendo, vuelva a escribir\n    (o escriba comandos para verlos)"));
        }
    }
}