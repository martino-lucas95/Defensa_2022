using System;

namespace Battleship
{
    /// <summary>
    /// Excepción que se encarga de notificar cuando una orientación de coordenada ingresada
    /// es incorrecta
    /// </summary>
    [Serializable]
    internal class IncorrectOrientationException : Exception
    {
        public IncorrectOrientationException()
        {
        }

    }
}