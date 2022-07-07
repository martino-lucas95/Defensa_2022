using System.Collections.Generic;
using System.IO;

namespace Battleship
{
    /// <summary>
    /// GamesRegister es una clase estática que funciona como base de datos de los Game. 
    /// Se accede a los Game a travez del id. En el momento que se inicia un partida, 
    /// se crea un Game, y en el momento que finaliza, se elimina su referencia desde
    /// la clase GamesRegister, dejando a voluntad del Garbage Collector para que lo elimine
    /// cuando lo considere conveniente. Ese es el ciclo de vida de los Game.
    /// 
    /// UserGame cumple con el patrón creator, ya que es la misma los agrega, contiene y guarda intancias de Game
    /// Por ende, debe ser la clase creadora de los Game
    /// </summary>
    public static class GamesRegister
    {
        private static List<Game> GamesInPlay = new List<Game>{};   //List con los juegos en partida

        /// <summary>
        /// Se ingresan dos usuarios y se crea el juego con ambos
        /// </summary>
        /// <param name="user1">Primer Usuario</param>
        /// <param name="user2">Segundo Usuario</param>
        /// <returns>Se retorna la id del juego</returns>
        public static int CreateGame(User user1, User user2)
        {
            Game game = new Game(user1, user2);
            GamesInPlay.Add(game);
            
            return game.GetId();
        }


        /// <summary>
        /// Se ingresa un Game, y se elimina de la lista de juegos
        /// </summary>
        /// <param name="game">Game a eliminar</param>
        public static void RemoveGame(Game game)
        {
            foreach (Game game1 in GamesInPlay)
            {
                if (game1 == game)
                {
                    GamesInPlay.Remove(game1);
                    break;
                }
            }  
        }
        

        /// <summary>
        /// Recibe un game, guarda un resumen del mismo en forma de string, y lo elimina
        /// de la lista de GamesInPlay
        /// </summary>
        /// <param name="game">Juego a guardar resumen y eliminar</param>
        public static void SaveGame(Game game)
        {
            string gameToSave = game.GameInString();
            
            StreamWriter writetext = new StreamWriter("GameSummaries.txt", true);
            writetext.WriteLine(gameToSave);
            writetext.Close();

            foreach (Game game1 in GamesInPlay)
            {
                if (game1 == game)
                {
                    GamesInPlay.Remove(game1);
                    break;
                }
            }  
        }

        /// <summary>
        /// Se ingresa la id de un User, se busca el Game que contiene a dicho
        /// usuario y se retorna el Game.
        /// </summary>
        /// <param name="id">id de uno de los usuarios</param>
        /// <returns>Game que contiene a dicho usuario</returns>
        public static Game GetGameByUserId(long id)
        {
            foreach (Game game in GamesInPlay)
            {
                List<long> listWithUserId= game.GetUsersId();
                if (listWithUserId[0] == id || listWithUserId[1] == id)
                {
                    return game;
                }
            }

            return null;
        }
    }
}