using System.Net.Sockets;
using System.Threading.Tasks;

namespace SimpleTcpChat.Server.Interfaces
{
    public interface IChatBot
    {
        Task<string> HandleRequest(string request, TcpClient client);
    }
}