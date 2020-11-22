using System.Threading.Tasks;

namespace SimpleTcpChat.Server.Interfaces
{
    public interface IChatServer
    {
        Task Start();
        void Stop();
    }
}