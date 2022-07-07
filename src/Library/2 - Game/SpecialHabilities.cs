using System.Collections.Generic;

namespace Battleship
{
/// <summary>
/// SpecialHabilities es la clase que contiene las habilidades especiales del jugador.
/// Las contiene en una lista representadas como string.
/// Anteriormente, el Player contenia dicha lista, pero creamos una nueva clase por si en un futuro
/// se quieren agregar nuevas habilidades, y así evitamos que Player tenga otra causa de modificación.
/// La logica de las habilidades se encuentran en Logic, esta clase solamente es utilizada como control
/// de que habilidades todavia no ha utilizado el Player, ya que solamente se pueden utilizar una vez 
/// en el juego.
/// </summary>
    public class SpecialHabilities
    {

        /// <summary>
        /// Lista con las habilidades del player representadas como string, solo se pueden usar una vez.
        /// Cuandos se utiliza una habilidad determinada, la misma se elimina de la lista 
        /// </summary>
        /// <value></value>
        private List<string> SpecialHabilitiesList = new List<string>{"air attack", "seer", "satellite photo"};
        

        /// <summary>
        /// Utiliza la habilidad especial Seer. Seer puede averiguar cual zona (superior o inferior)
        /// el usuario contricante posee más puntos de impacto.
        /// </summary>
        /// <param name="userAttacked">Usuario contricante</param>
        /// <returns>string con la información del uso de Seer</returns>
        public string UseSeer(User userAttacked)
        {
            SpecialHabilitiesList.Remove("seer");
            return Logic.Seer(userAttacked);
        }

        /// <summary>
        /// Utiliza la habilidad especial AirAttack. AirAttack realiza disparos en toda una fila en un
        /// solo turno.
        /// </summary>
        /// <param name="row">Fila de ataque</param>
        /// <param name="user">Usuario que ataca</param>
        /// <param name="userAttacked">Usuario atacado</param>
        public void UseAirAttack(string row, User user, User userAttacked)
        {
            SpecialHabilitiesList.Remove("air attack");
            Logic.AirAttack(row, user, userAttacked);
        }


        /// <summary>
        /// Utiliza la habilidad especial "Satelitte" (Vista satelital).
        /// Donde un usuario ingresa una columna, y puede obtener una foto satelital de la dicha columna
        /// del tablero del enemigo
        /// </summary>
        /// <param name="column">Columna para foto satelital</param>
        /// <param name="board">Tablero al que se sacará la foto</param>
        /// <returns>string, información brindada por la foto</returns>
        public string UseSatellite(int column, string[,] board)
        {
            SpecialHabilitiesList.Remove("satellite photo");
            return Logic.Satelitte(column, board);
        }

        /// <summary>
        /// Retorna la lista con las string que representa las habilidades especiales del usuario
        /// </summary>
        /// <returns>Lista con string que representan las habilidades especiales</returns>
        public List<string> GetSpecialsHabilities()
        {
           return SpecialHabilitiesList;
        }
    }
}