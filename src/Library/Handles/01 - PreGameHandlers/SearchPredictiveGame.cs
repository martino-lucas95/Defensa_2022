using System;
using System.Linq;
using Telegram.Bot.Types;

namespace Battleship
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "buscar partida predictiva".
    /// Como se puede observar, tanto las clases search como attack del tipo de juego predictivo,
    /// heredan de search y attack estandar. De esta forma las clases quedan abierta a la extensión
    /// pero cerradas a la modificación, respetando el OCP.
    /// </summary>
    public class SearchPredictiveGameHandler : SearchGameHandler
    {
        public SearchPredictiveGameHandler(BaseHandler next, IPrinter printer) : base(next, printer)
        {
            this.Keywords = new string[] {"buscar partida predictiva"};
            this.gameMode = "predictive";
        }
    }
}