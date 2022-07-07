using System.Collections.Generic;

namespace Battleship
{
    /// <summary>
    /// Sala de espera para concretar un juego. Existen dos posibles modos de juego.
    /// Un usuario entra a la sala de espera (Lista UsersInLobby), y si hay otro usuario
    /// que busca jugar la misma partida, se saca al usuario que está esperando y se crea
    /// un Game con ambos
    /// </summary>
    public static class Lobby
    {
        static List<User> UsersInLobby = new List<User>();  //Listas con los usuarios en espera


        /// <summary>
        /// Agrega un usuario a la sala de espera
        /// </summary>
        /// <param name="user">Usuario a agregar a la sala de espera</param>
        static public void AddUser(User user)
        {
            UsersInLobby.Add(user);
        }

        /// <summary>
        /// Se ingresa el modo de juego, y se retorna al primer usuario que este esperando para jugar
        /// en dicho tipo de juego
        /// </summary>
        /// <param name="gameMode">Modo de juego</param>
        static public User GetAndRemoveUser(string gameMode)
        {
            foreach (User user in UsersInLobby)
            {
                if (user.GetGameMode() == gameMode)
                {
                    UsersInLobby.Remove(user);
                    return user;
                }  
            }

            return null;
        }  

        /// <summary>
        /// Se ingresa un usuario y se lo elimina de la lista de usuarios en espera
        /// </summary>
        /// <param name="user">usuario a eliminar</param>
        static public void RemoveUser(User user)
        {
            UsersInLobby.Remove(user);
        }

        /// <summary>
        /// Retorna el número de usuarios que están buscando una partida
        /// con un modo de juego determinado. Puede haber 1 o 0 usuarios.
        /// Posibles modos de juego:
        ///      normal = Clásico
        ///      predictive = Predictivo
        /// </summary>
        /// <param name="gameMode"></param>
        /// <returns>int, número de usuarios</returns>
        static public int NumberUsersLobby(string gameMode)
        {
            int numberOfUser = 0;

            foreach (User user in UsersInLobby)
            {
                if (user.GetGameMode() == gameMode)
                {
                    numberOfUser ++;
                }  
            }

            return numberOfUser;
        }

    }
}