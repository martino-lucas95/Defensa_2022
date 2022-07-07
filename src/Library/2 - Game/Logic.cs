using System;
using System.Collections.Generic;

namespace Battleship
{
    public static class Logic
    {
        // Lista con las filas posibles
        static private List<string> Row = new List<string>{"A", "B", "C", "D", "E", "F", "G", "H", "I", "J"};
        
        // Lista con las columnas posibles
        static private List<string> Column = new List<string>{"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};

        /// <summary>
        /// Recibe dos usuarios y una coordenada.
        /// En caso de que la coordenada sea correcta, realiza el ataque.
        /// En caso de que sea incorrecta, retorna el mensaje correspondiente
        /// </summary>
        /// <param name="coordinate">string con la coordenada</param>
        /// <param name="user">Usuario que ataca</param>
        /// <param name="userAttacked">Usuario atacado</param>
        /// <returns>Mensaje</returns>
        public static string Attack(string coordinate, User user, User userAttacked)
        {
            Board boardWithShips = userAttacked.GetPlayer().GetShipsBoard();
            Board registerBoard = user.GetPlayer().GetRegisterBoard();

            string response = "";

            List<int> coordinateList = FixCoordinate(coordinate);
            if (coordinateList == new List<int>{})
            {
                throw new IncorrectCoordinateFormatException();
            }

            string coordinateInBoard = boardWithShips.GetBoard()[coordinateList[0]-1, coordinateList[1]-1];
            if (coordinateInBoard == "x" || coordinateInBoard == "#" || coordinateInBoard == "o")
            {
                throw new CoordinateAttackedTwiceException();
            }
            else if(coordinateInBoard == "-")
            {
                response = "Agua";
                boardWithShips.GetBoard()[coordinateList[0]-1, coordinateList[1]-1] = "o";
                registerBoard.GetBoard()[coordinateList[0]-1, coordinateList[1]-1] = "o";
                return response;
            }
            else
            {
                response = TocadoHundido(boardWithShips, registerBoard, coordinateList);
                return response;
            }
        }


        /// <summary>
        /// La misma función que el método Attack, pero con la diferencia que cuando se ataca,
        /// si el disparo dió al lado de un barco, el mensaje a retornar es "agua casi tocado" en lugar
        /// de solamente "agua"
        /// </summary>
        /// <param name="coordinate">string con la coordenada/param>
        /// <param name="user">Usuario que ataca</param>
        /// <param name="userAttacked">Usuario Atacado</param>
        /// <returns></returns>
        public static string AttackPredictive(string coordinate, User user, User userAttacked)
        {
            Board boardWithShips = userAttacked.GetPlayer().GetShipsBoard();
            Board registerBoard = user.GetPlayer().GetRegisterBoard();
            string response = "";

            List<int> coordinateList = FixCoordinate(coordinate);
            if (coordinateList == new List<int>{})
            {
                throw new IncorrectCoordinateFormatException();
            }

            string coordinateInBoard = boardWithShips.GetBoard()[coordinateList[0]-1, coordinateList[1]-1];
            if (coordinateInBoard == "x" || coordinateInBoard == "#" || coordinateInBoard == "o")
            {
                throw new CoordinateAttackedTwiceException();
            }
            else if(coordinateInBoard == "-")
            {
                response = "Agua";
                boardWithShips.GetBoard()[coordinateList[0]-1, coordinateList[1]-1] = "o";
                registerBoard.GetBoard()[coordinateList[0]-1, coordinateList[1]-1] = "o";

                // Coloco los números de las coordenadas en la siguiente lista para verificar que no salen del tablero
                List<List<int>> listBoardCoordinate = new List<List<int>>{};
                listBoardCoordinate.Add(new List<int>{coordinateList[0], coordinateList[1]-1});
                listBoardCoordinate.Add(new List<int>{coordinateList[0]-2, coordinateList[1]-1});
                listBoardCoordinate.Add(new List<int>{coordinateList[0]-1, coordinateList[1]});
                listBoardCoordinate.Add(new List<int>{coordinateList[0]-1, coordinateList[1]-2});

                List<string> charactersShips = new List<string>{"D", "S", "B", "C"};

                foreach (List<int> coor in listBoardCoordinate)
                {
                    try
                    {
                        if (charactersShips.Contains(boardWithShips.GetBoard()[coor[0], coor[1]]))
                        {
                            response = "Agua casi tocado";
                        }
                    }
                    catch{}
                }
                

                return response;
            }
            else
            {
                response = TocadoHundido(boardWithShips, registerBoard, coordinateList);
                return response;
            }
        }


        /// <summary>
        /// Es utilizado en el Método Attack y AttackPredictive.
        /// Se encarga de diferenciar a cuando un disparo toca o hunde un barco.
        /// Según haya sido tocado o hundido, realiza las correspondientes acciones.
        /// </summary>
        /// <param name="boardWithShips">Tablero con el barco (del usuario atacado)</param>
        /// <param name="registerBoard">Tablero con registros (del usuario atacante)</param>
        /// <param name="coordinateList">Coordinada</param>
        /// <returns></returns>
        private static string TocadoHundido(Board boardWithShips, Board registerBoard, List<int> coordinateList)
        {
            string coordinateInBoard = boardWithShips.GetBoard()[coordinateList[0]-1, coordinateList[1]-1];
            string response = "";
            
            foreach (Ship ship in boardWithShips.GetShipsList())
                {
                    if (ship.GetCharacter() == coordinateInBoard)
                    {
                        if (ship.GetHealth() == 1)
                        {
                            boardWithShips.RemoveShip(ship);
                            response = "Hundido";
                            List<List<int>> shipCoordinates = ship.GetCoordinates();
                            for (int i = 0; i < ship.GetSize(); i++)
                            {
                                boardWithShips.GetBoard()[shipCoordinates[i][0]-1, shipCoordinates[i][1]-1] = "#";
                                registerBoard.GetBoard()[shipCoordinates[i][0]-1, shipCoordinates[i][1]-1] = "#";
                            }
                            break;
                        }
                        else
                        {
                            ship.DecreaseHealth();
                            response = "Tocado";
                            boardWithShips.GetBoard()[coordinateList[0]-1, coordinateList[1]-1] = "x";
                            registerBoard.GetBoard()[coordinateList[0]-1, coordinateList[1]-1] = "x";
                            break;
                        }
                    }
                }

            return response;
        }


        /// <summary>
        /// Se utiliza para la habilidad especial AirAttack (Ataque Aereo)
        /// Donde se ataca una fila completa en un turno
        /// </summary>
        /// <param name="row">Fila</param>
        /// <param name="user">Usuario que usa la habilidad</param>
        /// <param name="userAttacked">Usuario perjudicado</param>
        public static void AirAttack(string row, User user, User userAttacked)
        {
            for (int i = 1; i < 11; i++)
            {
                Attack($"{row}{i}", user, userAttacked);
            }
        }


        /// <summary>
        /// Se utilizar para la habilidad especial "seer" (Vidente).
        /// Donde se retorna una string donde se expresa en cual zona hay mas posibilidades de impacto.
        /// Las zonas son superior (mitad superior) e inferior (mitad inferior)
        /// </summary>
        /// <param name="userAttacked">Usuario perjudicado</param>
        /// <returns>Mensaje con información</returns>
        public static string Seer(User userAttacked)
        {
            string[,] board = userAttacked.GetPlayer().GetShipsBoard().GetBoard();
            List<string> charactersShips = new List<string>{"D", "S", "B", "C"};

            int HighPart = 0;
            int LowPart = 0;
                
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (charactersShips.Contains(board[i, j]))
                    {
                        HighPart++;
                    }
                }
            }

            for (int i = 5; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (charactersShips.Contains(board[i, j]))
                    {
                        LowPart++;
                    }
                }
            }        

            if (HighPart > LowPart)
            {
                return "La zona superior contiene más puntos de impacto";
            }    
            else if (HighPart < LowPart)
            {
                return "La zona inferior tiene más puntos de impacto";
            }
            else
            {
                return "Ambas zonas tienen la misma cantidad de puntos de impacto";
            }
        }

        /// <summary>
        /// Se utiliza para la habilidad especial "Satelitte" (Vista satelital).
        /// Donde un usuario ingresa una columna, y puede obtener una foto satelital de la dicha columna
        /// del tablero del enemigo
        /// </summary>
        /// <param name="column">columna</param>
        /// <param name="board">tablero del usuario perjudicado</param>
        /// <returns></returns>
        public static string Satelitte(int column, string[,] board)
        {
            List<string> Row2 = new List<string>{"A ", "B ", "C ", "D ", "E ", "F ", "G ", "H ", "I  ", "J "};

            string photo = $"     | {column} |\n";
            column--;
            for (int i = 0; i < 10; i++)
            {
                if (board[i,column] == "-")
                {
                    photo += $"{Row2[i]} | -- |\n";
                }
                else
                {
                    photo += $"{Row2[i]} | {board[i,column]} |\n";
                }
            } 

            return photo;
        }



        /// <summary>
        /// FixCoordinate recibe una coordenada en forma de string (ej "A3") y la transforma en una lista de dos int.
        /// En caso de ser incorrectas, retorna una lista vacia
        /// </summary>
        /// <param name="coordinate">Coordinada</param>
        /// <returns>Lista con las coordenadas</returns>
        public static List<int> FixCoordinate(string coordinate)
        {
            List<int> coordinateList = new List<int>();
            string coordinateString;
            int coordinate1;
            int coordinate2;

            try
            {
                coordinateString = coordinate.Substring(0, 1);
                coordinate1 = Row.IndexOf(coordinateString.ToUpper()) + 1;

                if (coordinate1 == 0) //Si la letra no se encuentra entre la A y la J, IndexOf devuelve -1 (-1+1=0)
                {
                    return coordinateList;
                }
                
                try
                {
                    if (coordinate.Substring(1, 2) == "10")
                    {
                        coordinate2 = 10;
                    }
                    else
                    {
                        throw new Exception();
                    }  
                }
                catch
                {
                    coordinateString = coordinate.Substring(1, 1);
                    coordinate2 = Column.IndexOf(coordinateString) + 1;

                    if (coordinate2 < 1)
                    {
                        return coordinateList;
                    }
                }

                coordinateList.Add(coordinate1);
                coordinateList.Add(coordinate2);

                return coordinateList;

                
            }
            catch
            {
                    return coordinateList;
            }
        }


        /// <summary>
        /// El método retorna una lista con las posibles filas del tablero
        /// </summary>
        /// <returns>Lista con posibles filas del tablero</returns>
        public static List<string> GetRow()
        {
            return Row;
        }
    }
}