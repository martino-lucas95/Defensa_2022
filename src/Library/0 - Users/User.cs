
namespace Battleship
{
    /// <summary>
    /// Cuando una persona quiere jugar en consola, o desea jugar desde Telegram,
    /// debe crear un usuario, cada usuario tiene una id que lo identifica
    /// </summary>
    public class User
    {
        private long Id {get; set; } // id del usuario
        private string Status; // Estado del usuario, se usa para los handles

        /// Player se utiliza para jugar el juego de la batalla naval.
        /// Cada vez que un User se une a la partida, Player se reinicia con el 
        /// método RestartPlayer()
        private Player player = new Player();

        private string GameMode; // Modo de juego que el jugador selecciona para buscar partida

        public User(long id)
        {
            this.Id = id;

            this.Status = "start"; // El jugador inicia en el estado "start"
        }
       
        /// <summary>
        /// Retorna la id del usuario
        /// </summary>
        public long GetID()
        {
            return this.Id;
        } 

        /// <summary>
        /// Retorna el estado del usuario
        /// </summary>
        public string getStatus()
        {
            return this.Status;
        }

        /// <summary>
        /// Retorna el Player del usuario
        /// </summary>
        public Player GetPlayer()
        {
            return this.player;
        }

        /// <summary>
        /// Retorna el modo de juego en el cual el jugador está buscando partida
        /// o está jugando
        /// </summary>
        public string GetGameMode()
        {
            return this.GameMode;
        }


        /// <summary>
        /// Cambia el estado del usuario
        /// User Status: start | lobby | position ships | in {GameMode} game
        /// start: Antes de buscar una partida
        /// Lobby: En la sala de espera, esperando otro usuario para iniciar la partida 
        /// Position ships: Se creó la partida, y hay que posicionar los barcos
        /// In {GameMode} game: Jugando partida con modo de juego GameMode
        /// </summary>
        /// <param name="status">string que con el nuevo estado del usuario</param>
        public void ChangeStatus(string status)
        {
            this.Status = status;
        }


        /// <summary>
        /// Reinicia el Player para un nuevo juego.
        /// </summary>
        public void RestartPlayer()
        {
            this.player = new Player();
        }


        /// <summary>
        /// Cambiar el modo de juego
        /// </summary>
        /// <param name="gameMode">El modo de juego en el que el usuario buscará o estará jugando</param>
        public void ChangeGameMode(string gameMode)
        {
            this.GameMode = gameMode;
        }

    }
}