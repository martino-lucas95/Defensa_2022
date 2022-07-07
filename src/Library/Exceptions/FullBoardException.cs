using System;

namespace Battleship
{
    /// <summary>
    /// Excepción que se encarga de notificar el caso de que el tablero este lleno.
    /// </summary>
    [Serializable]
    internal class FullBoardException : Exception
    {
        public FullBoardException()
        {
        }

    }
}