using System;
using System.Collections.Generic;

namespace Battleship
{
    // Board es un array de strings de dos dimensiones, cada elemento contiene una de las 4 string posibles.
    // - = lugar sin disparar
    // o = agua
    // x = tocado
    // # = hundido
    // BARCOS:
    // C = Carrier (Size: 5)
    // B = Battleship (Size: 4)
    // S = Submarine (Size: 3)
    // D = Destroyer (Size: 2)

    /// <summary>
    /// Board es el experto de información de la ubicación de los barcos en el tablero,
    /// por lo tanto es el ideal para conocer, crear y posicionar los barcos.
    /// También por lo tanto, es el encargado de retornarse a el mismo representado
    /// como una string para poder ser impreso por un IPrinter
    /// </summary>
    public class Board
    {
        private string[,] board = new string[10,10]; //Tablero, El tamaño del tablero es fijo, de 10x10.

        private List<Ship> ShipsList = new List<Ship>{}; // Lista con los barcos colocados

        static List<string> Orientations = new List<string>{"UP", "DOWN", "LEFT", "RIGHT"}; //Orientaciones de colocación disponibles

        private List<int> ShipsSize = new List<int>{5,4,3,2}; // Los tamaños de los barcos

        public Board()
        {
            // lleno el tablero de "-"
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board[i,j] = "-";
                }
            }
        }

        /// <summary>
        /// Retorna el número de barcos que hay en el tablero
        /// </summary>
        public int GetShipsAlive()
        {
            return this.ShipsList.Count;
        }

        /// <summary>
        /// Retorna el tablero
        /// </summary>
        /// <returns>tablero</returns>
        public string[,] GetBoard()
        {
            return this.board;
        }

        /// <summary>
        /// Retorna una lista con los barcos colocados
        /// </summary>
        /// <returns>Listas con los barcos colocados</returns>
        public List<Ship> GetShipsList()
        {
            return this.ShipsList;
        }

        /// <summary>
        /// Se ingresa un barco y se elimina al mismo de la lista que contiene a los barcos
        /// </summary>
        /// <param name="ship">Barco a eliminar</param>
        public void RemoveShip(Ship ship)
        {
            ship.DecreaseHealth();
            this.ShipsList.Remove(ship);
        }


        /// <summary>
        /// Controla si los datos de coordenada y orientación ingresados son correctos, de ser asi los envia a PositionShip
        /// </summary>
        /// <param name="coordinate">coordenada</param>
        /// <param name="direction">orientacion (Izquierda, derecha, arriba, abajo)</param>
        public void ControlCoordinates(string coordinate, string direction)
        {
            if (this.ShipsList.Count >= 4)
            {
                throw new FullBoardException();
            }
            
            List<int> coordinateList = Logic.FixCoordinate(coordinate);
            if (coordinateList == (new List<int>{}))
            {
                throw new IncorrectCoordinateFormatException();
            }

            direction = direction.ToUpper();
            if (!Orientations.Contains(direction))
            {
                throw new IncorrectOrientationException();
            }                

            int size = ShipsSize[0];
            

            if (Position_Ship(size, coordinateList, direction) == false)
            {
                // no se posicionó el barco
                throw new Exception();
            }     
            else
            {
                // Se posiciono el barco
                ShipsSize.RemoveAt(0);
            }
        }

        /// <summary>
        /// Posiciona los barcos en la coordenada y orientación indicada
        /// </summary>
        /// <param name="size">tamaño del barco</param>
        /// <param name="coordinateList">Lista de coordenadas</param>
        /// <param name="direction">orientacion del barco</param>
        private bool Position_Ship(int size, List<int> coordinateList, string direction)
        {
            int c1 = coordinateList[0];
            int c2 = coordinateList[1];

            try
            {
                // Para la orientación horizontal
                if(direction == "UP")
                {
                    // Verifico que las coordenadas esten dentro del tablero y no haya otro barco
                    for (int i = c1; i > (c1-size); i -= 1)
                    {
                        if (this.board[i-1, c2-1] != "-")
                        {
                            return false;
                            throw new Exception();
                        }
                    }

                    // Una vez verificado, coloco el barco

                    Ship ship = new Ship(size);
                    for (int i = c1; i > (c1-size); i -= 1)
                    {
                        this.board[i-1, c2-1] = $"{ship.GetCharacter()}";
                        ship.AddCoordinate(i, c2);
                    }
                    ShipsList.Add(ship);
                }
                else if(direction == "DOWN")
                {
                    // Verifico que las coordenadas esten dentro del tablero y no haya otro barco
                    for (int i = c1; i < (c1+size); i += 1)
                    {
                        if (this.board[i-1, c2-1] != "-")
                        {
                            return false;
                            throw new Exception();
                        }
                    }

                    // Una vez verificado, coloco el barco

                    Ship ship = new Ship(size);
                    for (int i = c1; i < (c1+size); i += 1)
                    {
                        this.board[i-1, c2-1] = $"{ship.GetCharacter()}";
                        ship.AddCoordinate(i, c2);
                    }
                    ShipsList.Add(ship);
                }
                
                // Para la orientación vertical
                else if (direction == "RIGHT")
                {
                    // Verifico que las coordenadas esten dentro del tablero y no haya otro barco
                    for (int i = c2; i < (c2+size); i += 1)
                    {
                        if (this.board[c1-1, i-1] != "-")
                        {
                            return false;
                            throw new Exception();
                        }
                    }

                    // Una vez verificado, coloco el barco

                    Ship ship = new Ship(size);
                    for (int i = c2; i < (c2+size); i += 1)
                    {
                        this.board[c1-1, i-1] = $"{ship.GetCharacter()}";
                        ship.AddCoordinate(c1, i);
                    }
                    ShipsList.Add(ship);
                }
                else
                {
                    // Verifico que las coordenadas esten dentro del tablero y no haya otro barco
                    for (int i = c2; i > (c2-size); i -= 1)
                    {
                        if (this.board[c1-1, i-1] != "-")
                        {
                            return false;
                            throw new Exception();
                        }
                    }

                    // Una vez verificado, coloco el barco

                    Ship ship = new Ship(size);
                    for (int i = c2; i > (c2-size); i -= 1)
                    {
                        this.board[c1-1, i-1] = $"{ship.GetCharacter()}";
                        ship.AddCoordinate(c1, i);
                    }
                    ShipsList.Add(ship);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Devuelve el tablero (board) como una string, para que luego se pueda imprimir a travez de un IPrinter
        /// </summary>
        /// <returns>string con la representación del tablero</returns>
        public string BoardToString()
        {
            List<string> Lyrics = new List<string>{"A", "B", "C", "D", "E", "F", "G", "H", "I  ", "J "};
            
            string stringBoard = "";

            stringBoard += "     |  1  |  2  |  3  |  4  |  5  |  6  |  7  |  8  |  9  | 10 |\n";
            stringBoard += "-------------------------------------------------------------------\n";

            for (int i = 0; i < 10; i++)
            {   
                stringBoard += $"{Lyrics[i]}  |";
                for (int j = 0; j < 10; j++)
                {
                    if (this.board[i,j] == "-")
                    {
                        stringBoard += $"  --  |";
                    }
                    else
                    {
                        stringBoard += $"  {this.board[i,j]}  |";
                    }
                }
                stringBoard += "\n";
                stringBoard += "-------------------------------------------------------------------";
                stringBoard += "\n";
            }

            return stringBoard;
        }
    }
}