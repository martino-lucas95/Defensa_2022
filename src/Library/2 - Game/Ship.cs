using System.Collections.Generic;

namespace Battleship
{
    /// <summary>
    /// Ship representa a los barcos en los tableros
    /// </summary>
    public class Ship
    {
        private int Size{get; set;} // El tamaño del barco

        private int Health{get; set;}  // Contiene la cantidad de casillas sin disparar del barco

        private List<List<int>> Coordinates{get; set;} // Coordenadas sin disparar del barco

        private string Character{get; set;} //Character correspondiente al barco

        public Ship(int size)
        {
            this.Size = size;

            this.Health = size;

            // En base al tamaño, se le asigna un caracter al barco, que lo representa
            if (size == 2)
            {
                this.Character = "D";
            }
            if (size == 3)
            {
                this.Character = "S";
            }
            if (size == 4)
            {
                this.Character = "B";
            }
            if (size == 5)
            {
                this.Character = "C";
            }

            Coordinates = new List<List<int>>{};
        }

        /// <summary>
        /// Retorna el caracter que lo representa
        /// </summary>
        /// <returns>Caracter del barco</returns>
        public string GetCharacter()
        {
            return this.Character;
        }

        /// <summary>
        /// Retorna una lista con las coordenadas del barco
        /// </summary>
        /// <returns>Lista con coordenadas</returns>
        public List<List<int>> GetCoordinates()
        {
            return this.Coordinates;
        }

        /// <summary>
        /// Retorna la cantidad de casillas que el barco posee sin se disparadas
        /// </summary>
        /// <returns>Vida (Nro de casillas sin disparar)</returns>
        public int GetHealth()
        {
            return this.Health;
        }

        /// <summary>
        /// Retorna el tamaño del barco
        /// </summary>
        /// <returns></returns>
        public int GetSize()
        {
            return this.Size;
        }

        /// <summary>
        /// Se ingresa una coordenada, indicando columna y fila, y se agrega la misma a
        /// la lisat con las coordenadas del barco
        /// </summary>
        /// <param name="column">Columna</param>
        /// <param name="row">Fila</param>
        public void AddCoordinate(int column, int row)
        {
            List<int> coordinate = new List<int>{column, row};
            this.Coordinates.Add(coordinate);
        }

        /// <summary>
        /// Se decrementa en 1 el número de casillas que el barco posee sin disparar (vida)
        /// </summary>
        public void DecreaseHealth()
        {
            this.Health --;
        }

    }
}