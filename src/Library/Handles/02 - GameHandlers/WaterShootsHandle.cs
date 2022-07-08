using System;
using System.Linq;
using Telegram.Bot.Types;

namespace Battleship
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "atacar".
    /// AttackHandle cumple con el principio OCP, ya que está abierto a la extensión pero cerrado 
    /// a la modificación. Como es el ejemplo de AttackPredictiveHandle, es una clase
    /// heredera de ésta, que se encarga de agregar nuevas funciones, sin necesidad de modificar la 
    /// clase base AttackHandle
    /// </summary>
    public class WaterShootsHandle : BaseHandler
    {
        protected string gameMode; // Para el modo de juego

        protected IPrinter Printer;


        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AttackHandle"/>. Esta clase procesa el mensaje "atacar".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public WaterShootsHandle(BaseHandler next, IPrinter printer) : base(next)
        {
            this.Keywords = new string[] {"disparos al agua", "Disparos al agua", "Disparos errados"};

            gameMode = "normal";

            this.Printer = printer;
        }

        /// <summary>
        /// Procesa el mensaje "disparos errados" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(Message message, out string response)
        {
            try
            {
                User user = UserRegister.GetUser(message.From.Id);

                Game game = GamesRegister.GetGameByUserId(user.GetID());


                if (user.getStatus() != $"in {gameMode} game")
                {
                    // Estado de user incorrecto
                    response = $"Comando incorrecto. Estado del usuario = {user.getStatus()}";
                    return;
                }
                else
                {                 
                    response = game.GetGameCounter().GetWaterShoots().ToString();
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
        protected override bool CanHandle(Message message)
        {
            return true;
        }
    }
}