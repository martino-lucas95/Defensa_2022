using System;
using System.Linq;
using Telegram.Bot.Types;

namespace Battleship
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "posicionar barco".
    /// Se encarga de posicionar los barcos en el tablero con naves
    /// </summary>
    public class PositionShipsHandle : BaseHandler
    {
        protected IPrinter Printer;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PositionShipsHandle"/>. Esta clase procesa el mensaje "posicionar barcos".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public PositionShipsHandle(BaseHandler next, IPrinter printer) : base(next)
        {
            this.Keywords = new string[] {"posicionar barco", "posicionar nave"};
            this.Printer = printer;
        }

        /// <summary>
        /// Procesa el mensaje "posicionar barcos" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(Message message, out string response)
        {
            try
            {
                User user = UserRegister.GetUser(message.From.Id);

                if (user.getStatus() != "position ships")
                {
                    // Estado de user incorrecto
                    response = $"Comando incorrecto Estado del usuario = {user.getStatus()}";
                    return;
                }
                else
                {
                    Game game = null;
                    User user2 = null;
                    try
                    {
                        // Accediendo al otro usuario(player)
                        long IdUser1 = user.GetID();
                        game = GamesRegister.GetGameByUserId(IdUser1);

                        user2 = game.GetOtherUserById(IdUser1);
                    }
                    catch
                    {
                        response = "Error - No se encontró al otro usuario.";
                        return;
                    }

                    // Posicionando naves
                    try
                    {
                        string[] coordinates = message.Text.Split(' ');

                        try
                        {
                            user.GetPlayer().GetShipsBoard().ControlCoordinates(coordinates[2], coordinates[3]);
                            response = "El barco se creó correctamente";
                        }
                        catch (FullBoardException)
                        {
                            response = "No se puede agregar más barcos (El tablero ya esta lleno).";
                        }
                        catch (IncorrectCoordinateFormatException)
                        {
                            response = "La coordenada indicada no es correcta. Por favor ingrese una coordenda del tipo 'LetraNumero' (ej: A1).";
                        }
                        catch (IncorrectOrientationException)
                        {
                            response = "Dirección incorrecta, ingrese una de las siguientes: \nUp\nDown\nLeft\nRight";
                        }
                        catch
                        {
                            response = "La Coordenada ingresada es incorrecta.";
                            return;
                        }
                        
                        if (user.GetPlayer().GetShipsBoard().GetShipsAlive() >= 4)
                        {
                            user.ChangeStatus($"in {user.GetGameMode()} game");
                            response = "Los barcos estan listos";

                            Printer.Print("El contricante ya ha posicionado los barcos", user2.GetID());
                        }
                    }
                    catch
                    {
                        response = ("Coordenadas ingresadas incorrectas.");
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

                if (this.Keywords.Contains(words[0].ToLower() +" "+words[1].ToLower()))
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