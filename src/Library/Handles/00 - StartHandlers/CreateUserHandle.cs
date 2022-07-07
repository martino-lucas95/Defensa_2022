using System;
using System.Linq;
using Telegram.Bot.Types;

namespace Battleship
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "crear usuario".}
    /// Se encarga de crear un usuario
    /// </summary>
    public class CreateUserHandle : BaseHandler
    {

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CreateUserHandle"/>. Esta clase procesa el mensaje "crear usuario".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public CreateUserHandle(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"crear usuario", "/crear_usuario"};
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
                long id = message.From.Id;
                User user;
                response = "";

                try
                {
                    user = UserRegister.GetUser(id);
                    if (user != null)
                    {
                        response = "Ya has creado un usuario";
                    }
                }
                catch(UserNotCreatedException)
                {
                    // En el caso de que salte la excepción de que no se ha creado usuario
                    // se crea el mismo
                    UserRegister.CreateUser(message.From.Id);
                    response = "El usuario se ha creado correctamente";
                }

            }
            catch
            {
                response = "Sucedió un error, vuelve a intentar";
            }
        }

    }
}