using System.Threading.Tasks;

namespace Mechatronica.WPF.Interfaces
{
    public interface ISignalRConnection
    {
        Task Start();
        Task Send(string message);
        void Receive();
        Task Stop();
    }
}
