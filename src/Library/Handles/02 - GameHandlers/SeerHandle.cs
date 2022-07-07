using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;

namespace Battleship
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa los comandos "vidente".
    /// Utiliza la habilidad Vidente
    /// </summary>
    public class SeerHandler : BaseHandler
    {
        protected IPrinter Printer;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SeerHandler"/>. Esta clase procesa el mensaje "vidente".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public SeerHandler(BaseHandler next, IPrinter printer) : base(next)
        {
            this.Keywords = new string[] {"vidente"};

            this.Printer = printer;
        }

        /// <summary>
        /// Procesa los mensajes "vidente" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(Message message, out string response)
        {
            try
            {
                User user = UserRegister.GetUser(message.From.Id);

                if (user.GetPlayer().GetTurn() == false)
                {
                    response = "Aún no es tu turno";
                    return;
                }

                if (user.getStatus() != $"in {user.GetGameMode()} game")
                {
                    // Estado de user incorrecto
                    response = $"Comando incorrecto. Estado del usuario = {user.getStatus()}";
                    return;
                }
                else
                {
                    Game game = null;
                    User userAttacked = null;
                    try
                    {
                        // Accediendo al otro usuario(player)
                        long IdUser1 = user.GetID();
                        game = GamesRegister.GetGameByUserId(IdUser1);

                        userAttacked = game.GetOtherUserById(IdUser1);

                        if (userAttacked.getStatus() != $"in {user.GetGameMode()} game")
                        {
                            response = "El contricante no ha posicionado los barcos.";
                            return;
                        }
                    }
                    catch
                    {
                        response = "Error - No se encontró al otro usuario.";
                        return;
                    }
                    
                    try
                    {
                        if (!user.GetPlayer().GetSpecialHabilities().GetSpecialsHabilities().Contains("seer"))
                        {
                            response = "Ya has utilizado la habilidad vidente";
                            return;
                        }

                        response = user.GetPlayer().GetSpecialHabilities().UseSeer(userAttacked);

                        response += "\n\n\n\n------Turno cambiado------\n\n"; 
                        user.GetPlayer().ChangeTurn();
                        userAttacked.GetPlayer().ChangeTurn();
                        Printer.Print("El contricante utilizó vidente", userAttacked.GetID());
                    }
                    catch
                    {
                        response = "Sucedió un error";
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