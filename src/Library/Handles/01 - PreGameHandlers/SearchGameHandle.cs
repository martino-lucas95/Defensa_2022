using System;
using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot;


namespace Battleship
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "buscar partida".
    /// SearchGam3eHandle cumple con el principio OCP, ya que está abierto a la extensión pero cerrado 
    /// a la modificación. Como es el ejemplo de SearchPredictiveGameHandler, es una clase
    /// heredera de ésta, que se encarga de agregar nuevas funciones, sin necesidad de modificar la 
    /// clase base SearchGame
    /// </summary>
    public class SearchGameHandler : BaseHandler
    {
        protected string gameMode; // Para el modo de juego
        protected IPrinter Printer;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SearchGameHandler"/>. Esta clase procesa el mensaje "buscar partida".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public SearchGameHandler(BaseHandler next, IPrinter printer) : base(next)
        {
            this.Keywords = new string[] {"buscar partida", "/buscar_partida"};
            this.gameMode = "normal";
            this.Printer = printer;
        }

        /// <summary>
        /// Procesa el mensaje "Buscar partida" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(Message message, out string response)
        {
            try
            {
                User user = UserRegister.GetUser(message.From.Id);

                if (user.getStatus() != "start")
                {
                    // Estado de user incorrecto
                    response = $"Comando incorrecto. Estado del usuario = {user.getStatus()}";
                    return;
                }
                else
                {
                    if (Lobby.NumberUsersLobby(gameMode) >= 1)
                    {
                        // Si ya hay otro usuario en el Lobby, se crea la partida
                        User user2 = Lobby.GetAndRemoveUser(gameMode);
                        user.RestartPlayer();
                        user2.RestartPlayer();

                        int idGame = GamesRegister.CreateGame(user, user2);

                        user.GetPlayer().ChangeTurn();

                        user.ChangeGameMode(gameMode);
                        user.ChangeStatus("position ships");
                        user2.ChangeStatus("position ships");

                        response = $"Se ha unido a una partida con id {idGame}";

                        Printer.Print(response, user2.GetID());

                    }
                    else
                    {
                        // Agregando el usuario al Lobby
                        user.ChangeGameMode(gameMode);
                        Lobby.AddUser(user);
                        user.ChangeStatus("lobby");
                        response = $"Entraste a la sala de espera. Modo de juego: {gameMode}";
                    }
                }
            }
            catch(UserNotCreatedException)
            {
                response = "Debe crear un usuario\nIngrese 'Crear Usuario':\n";
            }
            catch
            {
                response = "Sucedió un error, vuelve a intentar";
            }
        }
    }
}