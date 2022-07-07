
namespace Battleship
{
    /// <summary>
    /// Se crea la interfaz en caso de que en el futuro haya una nueva forma de ingresar
    /// información. Respetando el SRP
    /// </summary>
    public interface IInputText
    {
        public string Input();
    }
}