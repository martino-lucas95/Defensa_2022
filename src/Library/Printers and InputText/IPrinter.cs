
namespace Battleship
{
    /// <summary>
    /// Se crea interfaz en caso de que en el futuro haya una nueva forma de imprimir
    /// información. Respetando el SRP en las clases que contienen la información, ya que 
    /// no son las responsables de imprimir, sino que los responsables de imprimir son los
    /// IPrinters.
    /// 
    /// La interfaz también permite que se respete el LSP, ya que se pueden utilizar los distintos
    /// IPrinter en los programas, y la sustitución de uno u otro, no cambiará el comportamiento de dichos programas
    /// o clases
    /// </summary>
    public interface IPrinter
    {
        public void Print(string textToPrint, long id);
    }
}