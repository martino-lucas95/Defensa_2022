using System;
using System.Linq;
using Telegram.Bot.Types;

namespace Battleship
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "buscar partida".
    /// Se encarga de sacar a un usuario de la sala de espera
    /// </summary>
    public class ExitLobbyHandle : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ExitLobbyHandle"/>. Esta clase procesa el mensaje "salir lobby".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public ExitLobbyHandle(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"salir lobby", "/salir_lobby"};
        }

        /// <summary>
        /// Procesa el mensaje "salir lobby" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(Message message, out string response)
        {
            try
            {
                User user = UserRegister.GetUser(message.From.Id);

                if (user.getStatus() != "lobby")
                {
                    // Estado de user incorrecto
                    response = $"Comando incorrecto. Estado del usuario = {user.getStatus()}";
                    return;
                }
                else
                {
                    Lobby.RemoveUser(user);
                    user.ChangeStatus("start");
                    response = "Has salido de la sala de espera";
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