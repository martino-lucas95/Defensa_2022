using System.IO;
using System.Collections.Generic;

namespace Battleship
{
    /// <summary>
    /// La clase Games es una clase que representa las partidas creadas.
    /// Contiene una Id que identifica cada juego, dicha id es guardada con persistencia,
    /// por si en el futuro alguien quiere ver las partidas jugadas. Solamente debe recordar
    /// la id de la partida que jugó, la cual se le indica en pantalla antes de iniciar la misma.
    /// </summary>
    public class Game
    {
        private int Id {get; set;} // Id de la partida

        private User User1 {get; set;}  // Usuario que jugará
        private User User2 {get; set;}  // Usuario que jugará

        private User UserWinner;    // Usuario ganador

        public Game(User user1, User user2)
        {
            this.Id = CounterId();  // Se le asigna un id con el método CounterId

            this.User1 = user1;

            this.User2 = user2;
        }

        public int GetId()
        {
            return this.Id;
        }

        /// <summary>
        /// Método para tener una permanencia de la id de los juegos
        /// </summary>
        /// <returns>Retorna la id para ser utilizada</returns>
        private int CounterId()
        {
            int counterId;
            try
            {
                counterId = int.Parse(File.ReadAllText("CounterIdGame.txt"));
            }
            catch
            {
                counterId = 0;
            }

            int newCounterId = counterId + 1;

            StreamWriter writetext = new StreamWriter("CounterIdGame.txt", false);
            writetext.WriteLine($"{newCounterId}");
            writetext.Close();

            return counterId;
        }

        /// <summary>
        /// Se ingresa la id de un usuario, y se retorna el otro usuario
        /// Sirve para cuando conocemos un usuario y el juego, 
        /// pero queremos modificar al otro usuario
        /// </summary>
        /// <param name="id">id del usuario</param>
        /// <returns>El otro usuario</returns>
        public User GetOtherUserById(long id)
        {
            if (User1.GetID() == id)
            {
                return User2;
            }
            else if (User2.GetID() == id)
            {
                return User1;
            }
            else
            {
                return null;    
            }
        }

        /// <summary>
        /// Se declará cuales el usuario ganador
        /// </summary>
        public void AddUserWinner(User user)
        {
            this.UserWinner = user;
        }


        /// <summary>
        /// Se toman los datos más importantes de la partida, y se convierten en una string
        /// </summary>
        /// <returns>string con el resúmen de la partida</returns>
        public string GameInString()
        {
            string summary = $"Game id = {this.Id}\n";
            summary += $"User 1 id= {User1.GetID()}";
            summary += $" - Ships alive = {User1.GetPlayer().GetShipsAlive()}\n";
            summary += $"User 2 id= {User2.GetID()}";
            summary += $" - Ships alive = {User2.GetPlayer().GetShipsAlive()}\n";
            summary += $"Winner = {UserWinner.GetID()}\n";
            return summary;
        }

        /// <summary>
        /// Retorna una lista con las id de ambos usuarios
        /// </summary>
        public List<long> GetUsersId()
        {
            List<long> usersId = new List<long>{this.User1.GetID(), this.User2.GetID()};

            return usersId;
        }
    }
}