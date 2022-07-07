using System;

namespace Battleship
{
    /// <summary>
    /// Implementa la Interfaz IInputText
    /// </summary>
    public class ConsoleInputText: IInputText
    {
        public string Input()
        {
            return Console.ReadLine();
        }
    }
}