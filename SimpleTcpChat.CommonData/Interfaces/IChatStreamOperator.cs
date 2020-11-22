using System.Net.Sockets;
using System.Threading.Tasks;

namespace SimpleTcpChat.CommonData.Interfaces
{
    public interface IChatStreamOperator
    {
        Task<string> ReadAsync(NetworkStream stream);
        Task WriteAsync(NetworkStream stream, string response);
    }
}