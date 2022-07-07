using System.Collections.Generic;

namespace Battleship
{
    /// <summary>
    /// Player se utiliza como una extensión de User, contiene métodos y atributos
    /// particulares para jugar al juego Batalla Naval. Se creó para respetar el SRP, 
    /// de forma que no se sobrecarga a User de responsabilidades.
    /// Player está contenido en el User. Se crea cuando se crea un User y se reinicia
    /// su información cada vez que se inicia un juego.
    /// </summary>
    public class Player
    {
        private Board RegisterBoard = new Board();  // Tablero con el registro de ataques

        private Board ShipsBoard = new Board(); // Tablero con las naves del Player
        
        private SpecialHabilities specialHabilities = new SpecialHabilities();  // Habilidades especiales del jugados

        private bool Turn = false; // Representa si es o no el turno del Player

        /// <summary>
        /// Retorna el tablero con los barcos
        /// </summary>
        /// <returns>tablero con las naves</returns>
        public Board GetShipsBoard()
        {
            return this.ShipsBoard;
        }


        /// <summary>
        /// Retorna el tablero con el registro de disparos
        /// </summary>
        /// <returns>Tablero con el registro de disparos</returns>
        public Board GetRegisterBoard()
        {
            return this.RegisterBoard;
        }

        /// <summary>
        /// Retorna las habilidades especiales del jugador
        /// </summary>
        /// <returns>SpecialHabilities</returns>
        public SpecialHabilities GetSpecialHabilities()
        {
            return this.specialHabilities;
        }

        /// <summary>
        /// El método accede a ambos tableros de los barcos, los cuales contienen un método que
        /// retorna el tablero representado como una string (BoardToString()), luego une ambas string
        /// en una sola y la retorna.
        /// </summary>
        /// <returns>string con las dos representaciones de los tableros como string</returns>
        public string GetBoardsToPrint()
        {
            string stringBoard = "Tablero con tus naves:\n";
            stringBoard += $"{this.ShipsBoard.BoardToString()}\n\n";
            stringBoard += "Tablero de registro de disparos:\n";
            stringBoard += $"{this.RegisterBoard.BoardToString()}\n";

            return stringBoard;
        }


        /// <summary>
        /// El método accede al tablero con las naves a trávez del método GetShipsAlive,
        /// y retorna la información brindada por el método.
        /// Decidimos no acceder directamente al método del tablero para que el mismo
        /// tenga una mejor privacidad, y evitar que un Handler tenga acceso directo
        /// al tablero.
        /// </summary>
        /// <returns>El número de barcos vivos en el tablero con naves</returns>
        public int GetShipsAlive()
        {
            return ShipsBoard.GetShipsAlive();
        }

        /// <summary>
        /// Retorna un bool representado si es o no el turno del Player
        /// </summary>
        /// <returns>bool, Turno</returns>
        public bool GetTurn()
        {
            return this.Turn;
        }

        /// <summary>
        /// Cambia el turno del jugador
        /// </summary>
        public void ChangeTurn()
        {
            if (this.Turn == false)
            {
                this.Turn = true;
            }
            else
            {
                this.Turn = false;
            }
        }
    }
}