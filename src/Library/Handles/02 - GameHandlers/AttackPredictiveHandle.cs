using System;
using System.Linq;
using Telegram.Bot.Types;

namespace Battleship
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "atacar p".
    /// Como se puede observar, tanto las clases search como attack del tipo de juego predictivo,
    /// heredan de search y attack estandar. De esta forma las clases quedan abierta a la extensión
    /// pero cerradas a la modificación, respetando el OCP.
    /// </summary>
    public class AttackPredictiveHandler : AttackHandle
    {
        public AttackPredictiveHandler(BaseHandler next, IPrinter printer) : base(next, printer)
        {
            this.Keywords = new string[] {"predictivo ataque", "p ataque"};

            gameMode = "predictive";
        }

        protected override string Attack(User user, User userAttacked, Message message)
        {
            string[] coordinates = message.Text.Split(' ');

            string stringCoordinate = coordinates[2];

            string response = "";
            try
            {
                response = Logic.AttackPredictive(stringCoordinate, user, userAttacked);
            }
            catch(IncorrectCoordinateFormatException)
            {
                response = "Las coordenadas ingresadas son incorrectas";
            }
            catch(CoordinateAttackedTwiceException)
            {
                response = "Ya se atacó en dicha coordenada";
            }
            
            return response;
        }

        protected override bool CanHandle(Message message)
        {
            try
            {
                string[] words = message.Text.Split(' ');


                if (this.Keywords.Contains(words[0].ToLower()+" "+words[1].ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            
        }
    }
}