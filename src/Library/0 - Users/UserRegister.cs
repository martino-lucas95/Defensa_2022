using System.Collections.Generic;

namespace Battleship
{
    /// <summary>
    /// UserRegister es una clase estática que funciona como base de datos de los usuarios. 
    /// Se accede a los usuarios a travez del id, en el caso que no existe, se devuelve un valor null.
    /// 
    /// UserRegister cumple con el patrón creator, ya que es la misma los agrega, contiene y guarda intancias de User
    /// Por ende, debe ser la clase creadora de User
    /// </summary>
    public static class UserRegister
    {
        private static List<User> ListOfUsers = new List<User> (); // Lista con los usuarios creados

        /// <summary>
        /// Se ingresa una Id, y se crea un nuevo usuario
        /// </summary>
        /// <param name="user">id del usuario a crear</param>
        public static void CreateUser(long id)
        {
            User user = new User(id);
            ListOfUsers.Add(user);
        }

        /// <summary>
        /// Se ingresa una id, y se retoran el usuario con dicha id, o null si no existe
        /// </summary>
        /// <param name="UserId">id del usuario</param>
        /// <returns>El usuario con dicha Id</returns>
        public static User GetUser(long UserId)
        {
            foreach (User user in ListOfUsers)
                {
                    if (user.GetID() == UserId)
                    {
                        return user;
                    }
                }
            
            throw new UserNotCreatedException();
        }
    }
}