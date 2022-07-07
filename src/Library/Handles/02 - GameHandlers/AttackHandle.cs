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
    public class AttackHandle : BaseHandler
    {
        protected string gameMode; // Para el modo de juego

        protected IPrinter Printer;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AttackHandle"/>. Esta clase procesa el mensaje "atacar".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public AttackHandle(BaseHandler next, IPrinter printer) : base(next)
        {
            this.Keywords = new string[] {"atacar"};

            gameMode = "normal";

            this.Printer = printer;
        }

        /// <summary>
        /// Procesa el mensaje "atacar" y retorna true; retorna false en caso contrario.
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

                if (user.getStatus() != $"in {gameMode} game")
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

                        if (userAttacked.getStatus() != $"in {gameMode} game")
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
                    
                    // Ataque
                    response = Attack(user, userAttacked, message);

                    // Juego terminado
                    if (userAttacked.GetPlayer().GetShipsAlive() == 0)
                    {
                        // Mensajes para los jugadores
                        response = "¡Felicitaciones!. Has hundido todos los barcos ¡Ganaste!.";
                        User loserUser = game.GetOtherUserById(user.GetID());
                        Printer.Print("El enemigo ha hundido todos tus barcos. Has perdido.", loserUser.GetID());
                        game.AddUserWinner(user);

                        // Cambio de estado y turno
                        user.ChangeStatus("start");
                        userAttacked.ChangeStatus("start"); 
                        response += "\n\n------Turno cambiado------\n\n";   
                        user.GetPlayer().ChangeTurn();
                        userAttacked.GetPlayer().ChangeTurn();

                        // Elimina el juego de la lista de juegos
                        GamesRegister.RemoveGame(game);
                    
                        // Guardando juego
                        GamesRegister.SaveGame(game);
                    }

                    // Cambio de turno (Agua casi tocado es para el modo de juego predictivo)
                    if(response == "Agua" || response == "Hundido" || response == "Tocado" || response == "Agua casi tocado")
                    {
                        Printer.Print($"El contricante ha atacado, {response}", userAttacked.GetID());
                        response += "\n\n\n\n------Turno cambiado------\n\n"; 
                        user.GetPlayer().ChangeTurn();
                        userAttacked.GetPlayer().ChangeTurn();
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

        // Método para que las clases herederas lo sobrescriban, cambiando el método con el cual se ataca
        protected virtual string Attack(User user, User userAttacked, Message message)
        {
            string[] coordinates = message.Text.Split(' ');

            string stringCoordinate = coordinates[1];

            string response = "";
            try
            {
                response = Logic.Attack(stringCoordinate, user, userAttacked);
            }
            catch(IncorrectCoordinateFormatException)
            {
                response = "Las coordenadas ingresadas son incorrectas";
            }
            catch(CoordinateAttackedTwiceException)
            {
                response = "Ya se atacó en dicha coordenada";
            }

            return response;
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