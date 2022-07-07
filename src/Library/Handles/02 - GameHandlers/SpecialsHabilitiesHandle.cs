using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;

namespace Battleship
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa los comandos "ataque aereo","satelite".
    /// Utiliza las habilidades Ataque Aereo y Foto Satelital
    /// </summary>
    public class SpecialHabilitiesHandler : BaseHandler
    {
        protected IPrinter Printer;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SpecialHabilitiesHandler"/>. Esta clase procesa el mensaje "aereo","satelite".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public SpecialHabilitiesHandler(BaseHandler next, IPrinter printer) : base(next)
        {
            this.Keywords = new string[] {"aereo", "aéreo", "satelite", "satélite"};

            this.Printer = printer;
        }

        /// <summary>
        /// Procesa los mensajes "aereo", "vidente", "satelite" y retorna true; retorna false en caso contrario.
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
                        string[] direction = message.Text.Split(' ');

                        if (message.Text == $"aereo {direction[1]}" || message.Text == $"Aereo {direction[1]}" || message.Text == $"aéreo {direction[1]}" || message.Text == $"Aéreo {direction[1]}")
                        {
                            if (!user.GetPlayer().GetSpecialHabilities().GetSpecialsHabilities().Contains("air attack"))
                            {
                                response = "Ya has utilizado la habilidad ataque aereo";
                                return;
                            }

                            string theRow = direction[1];

                            if (!Logic.GetRow().Contains(theRow.ToUpper()))
                            {
                                response = "Fila ingresada incorrecta";
                                return;
                            }

                            user.GetPlayer().GetSpecialHabilities().UseAirAttack(theRow, user, userAttacked);

                            response = "Fila atacada con exito";
                            Printer.Print("El contricante utilizó ataque aéreo", userAttacked.GetID());
                        }
                        else if (message.Text == $"satelite {direction[1]}" || message.Text == $"Satelite {direction[1]}" || message.Text == $"satélite {direction[1]}" || message.Text == $"Satélite {direction[1]}")
                        {
                            if (!user.GetPlayer().GetSpecialHabilities().GetSpecialsHabilities().Contains("satellite photo"))
                            {
                                response = "Ya has utilizado la habilidad satelite";
                                return;
                            }

                            string theColumn = direction[1];

                            List<string> validColumns = new List<string>{"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};

                            if (!validColumns.Contains(theColumn))
                            {
                                response = "Columna ingresada incorrecta";
                                return;
                            }

                            int columnInt = int.Parse(theColumn);

                            response = "Foto satelital recibida:\n";

                            response += user.GetPlayer().GetSpecialHabilities().UseSatellite(columnInt, userAttacked.GetPlayer().GetShipsBoard().GetBoard());

                            Printer.Print("El contricante utilizó foto satelital", userAttacked.GetID());
                        }
                        else
                        {
                            throw new Exception();
                        }

                        response += "\n\n\n\n------Turno cambiado------\n\n"; 
                        user.GetPlayer().ChangeTurn();
                        userAttacked.GetPlayer().ChangeTurn();
                    }
                    catch
                    {
                        response = "Sucedió un error, vuelve a intentar";
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

        protected override bool CanHandle(Message message)
        {
            try
            {
                string[] words = message.Text.Split(' ');


                if (this.Keywords.Contains(words[0].ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            
        }
        
    }
}