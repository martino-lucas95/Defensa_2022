using System;
using System.Linq;
using Telegram.Bot.Types;

namespace Battleship
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que se encarga de aceptar
    /// cualquier mensaje, en caso que ningún handle anterior lo haya aceptado,
    /// y devuelve el mensaje aclarando que no se entiende el mensaje recibido.
    /// </summary>
    public class FinalHandle : BaseHandler
    {

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FinalHandle"/>.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public FinalHandle(BaseHandler next) : base(next)
        {

        }

        /// <summary>
        /// Procesa cualquier mensaje
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(Message message, out string response)
        {
            response = "No entiendo, vuelva a escribir\n    (o escriba comandos para verlos)";
        }

        /// <summary>
        /// Sobrescribimos el CAnHandle para que prosece todos los mensajes enviados
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected override bool CanHandle(Message message)
        {
            return true;
        }
    }
}