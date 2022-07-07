using System;
using System.Linq;
using Telegram.Bot.Types;

namespace Battleship
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "comandos".
    /// Dependiendo del estado en que se encuentre el usuario, se encarga de imprimir los comandos
    /// disponibles para el mismo
    /// </summary>
    public class CommandsHandle : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CommandsHandle"/>. Esta clase procesa el mensaje "Comandos".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public CommandsHandle(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"comandos", "comando", "/comandos"};
        }

        /// <summary>
        /// Procesa el mensaje "comandos" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(Message message, out string response)
        {
            try
            {
                User user = UserRegister.GetUser(message.From.Id);

                if (user == null)
                {
                    response = $"\nIngrese el siguiente comando para crear su usuario:\ncrear usuario\n";
                    return;
                }

                string forInGame = "\n- ver tableros\n- aereo <fila> (ejemplo:aereo A)\n- vidente\n- satelite <columna (ejemplo: satelite 1)";

                if (user.getStatus() == "start")
                {
                    response = $"\nComandos en estado '{user.getStatus()}':\n\n- buscar partida\n- buscar partida predictiva\n- ver partidas jugadas";
                }
                else if(user.getStatus() == "lobby")
                {
                    response = $"\nComandos en estado '{user.getStatus()}':\n\n- salir lobby";
                } 
                else if(user.getStatus() == "position ships")
                {
                    response = $"\nComandos en estado '{user.getStatus()}:'\n\n- posicionar barco <coordenada> <orientación> (ejemplo: 'posicionar barco a1 down')\n      Orientaciones = up, down, left, right\n- ver tableros";
                } 
                else if(user.getStatus() == $"in normal game")
                {
                    response = $"\nComandos en estado '{user.getStatus()}:'\n\n- atacar <coordenada> (ejemplo: 'atacar A1'){forInGame}";
                }
                else if(user.getStatus() == $"in predictive game")
                {
                    response = $"\nComandos en estado '{user.getStatus()}:'\n\n- p ataque <coordenada> (ejemplo: 'p ataque A1'){forInGame}";
                }
                else
                {
                    throw new Exception();
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