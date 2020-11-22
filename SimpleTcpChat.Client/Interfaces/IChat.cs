using System.Threading.Tasks;

namespace SimpleTcpChat.Client.Interfaces
{
    public interface IChat
    {
        string GeUserMessage();
        Task WriteAnotherUserMessage(string userName, string message);
        Task WriteNormanServerMessage(string message);
        Task WriteErrorMessage(string message);
        Task CompleteChat();
    }
}