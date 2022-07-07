using System;

namespace Battleship
{
    /// <summary>
    /// Implementa la interfaz IPrinter, en esta ocación para imprimir en consola
    /// </summary>
    public class ConsolePrinter: IPrinter
    {
        public void Print(string textToPrint, long id)
        {
            Console.WriteLine(textToPrint);
        }
    }
}