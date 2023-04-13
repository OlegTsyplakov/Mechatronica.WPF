

namespace Mechatronica.WPF.Interfaces
{
    public interface IModel
    {
        string? Name {get; set;}
        string? Date {get; set;}
        abstract string ToString();
    }
}
