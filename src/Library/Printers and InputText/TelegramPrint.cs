using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Battleship
{
    /// <summary>
    /// Implementa la interfaz IPrinter, en esta ocación para imprimir en la pantalla de Telegram de un usuario
    /// en específico, al cual se accede a travez de su id
    /// </summary>
    public class TelegramPrint : IPrinter
    {
        private ITelegramBotClient Client;

        public TelegramPrint(ITelegramBotClient client)
        {
            this.Client = client;
        }

        /// <summary>
        /// Se ingresa una string y una id y se envía el mensaje al usuario con la 
        /// id correspondiente
        /// </summary>
        public void Print(string textToPrint, long id)
        {
            this.Client.SendTextMessageAsync(id, textToPrint);
        }

    }
}