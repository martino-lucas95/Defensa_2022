using NUnit.Framework;
using Battleship;
using System;
using System.IO;

namespace Library.Tests
{   
    /// <summary>
    /// Se testean las clases que se utilizan durante el juego:
    ///     - Player
    ///     - Board
    ///     - Ship
    ///     - Logic
    ///     - 
    /// </summary>
    [TestFixture]
    public class ClassesInGameTest
    {
        private User user1;
        private Player player1;
        private Board shipBoard;
        private Board registerBoard;

        [SetUp]
        public void Setup()
        {
            user1 = new User(19);
            player1 = user1.GetPlayer();
            shipBoard = player1.GetShipsBoard();
            registerBoard = player1.GetShipsBoard();
        }

        [TestCase("a1", "down")]
        [TestCase("j10", "up")]
        [TestCase("j1", "right")]
        [TestCase("a10", "left")]
        public void ValidCoordinates(string coordinate, string orientation)
        {
            shipBoard.ControlCoordinates(coordinate, orientation);
            // Si la coordenada es incorrecta, devuelve una excepción
        }

        [TestCase]
        public void RepeatCoordinate()
        {
            bool exception = false;
            shipBoard.ControlCoordinates("a1", "down");

            try
            {
                shipBoard.ControlCoordinates("a1", "down");
            }
            catch
            {
                exception = true;
            }

            Assert.AreEqual(exception, true);
        }

        [TestCase("a1", "up")]
        [TestCase("j10", "down")]
        [TestCase("j1", "left")]
        [TestCase("a10", "right")]
        public void InvalidCoordinates1(string coordinate, string orientation)
        {
            bool exception = false;

            try
            {
                shipBoard.ControlCoordinates(coordinate, orientation);
            }
            catch
            {
                exception = true;
            }

            Assert.AreEqual(exception, true);
        }

        [TestCase("aa", "up")]
        [TestCase("232", "down")]
        [TestCase("p3", "left")]
        [TestCase("{", "right")]
        public void InvalidCoordinates2(string coordinate, string orientation)
        {
            bool exception = false;

            try
            {
                shipBoard.ControlCoordinates(coordinate, orientation);
            }
            catch
            {
                exception = true;
            }

            Assert.AreEqual(exception, true);
        }

        [TestCase("a1", "sds")]
        public void InvalidCoordinates3(string coordinate, string orientation)
        {
            bool exception = false;

            try
            {
                shipBoard.ControlCoordinates(coordinate, orientation);
            }
            catch
            {
                exception = true;
            }

            Assert.AreEqual(exception, true);
        }

        [TestCase]
        public void AttackTest()
        {
            shipBoard.ControlCoordinates("a1", "down");

            string response = Logic.Attack("a1", user1, user1);

            Assert.AreEqual("Tocado", response);

            response = Logic.Attack("j10", user1, user1);

            Assert.AreEqual("Agua", response);
        }

        [TestCase]
        public void AttackPredictiveTest()
        {
            shipBoard.ControlCoordinates("a1", "down");

            string response = Logic.AttackPredictive("a2", user1, user1);

            Assert.AreEqual("Agua casi tocado", response);
        }

        [TestCase]
        public void SpecialHabilities()
        {
            shipBoard.ControlCoordinates("a1", "down");

            string response = Logic.Seer(user1);

            Assert.AreEqual("La zona superior contiene más puntos de impacto", response);

            Logic.AirAttack("a", user1, user1);
            Logic.Satelitte(1, shipBoard.GetBoard());

            user1.GetPlayer().GetSpecialHabilities().UseAirAttack("b", user1, user1);
            user1.GetPlayer().GetSpecialHabilities().UseSatellite(1, user1.GetPlayer().GetShipsBoard().GetBoard());
            user1.GetPlayer().GetSpecialHabilities().UseSeer(user1);

            Assert.AreEqual(0, user1.GetPlayer().GetSpecialHabilities().GetSpecialsHabilities().Count);
        }
    }
}