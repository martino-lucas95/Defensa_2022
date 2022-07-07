using System;

namespace Battleship
{
    /// <summary>
    /// Excepción que se encarga de tratar notificar que ya se atacó en una coordinada determinada.
    /// </summary>
    [Serializable]
    internal class CoordinateAttackedTwiceException : Exception
    {
        public CoordinateAttackedTwiceException()
        {
        }

    }
}