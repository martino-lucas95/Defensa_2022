using System;

namespace Battleship
{
    /// <summary>
    /// Excepción que se encarga de notificar cuando aún no se ha creado el usuario.
    /// Es específica para los Handlers, ya que necesitan utilizar al usuario para
    /// varios de ellos.
    /// </summary>
    [Serializable]
    internal class UserNotCreatedException : Exception
    {
        public UserNotCreatedException()
        {
        }

    }
}