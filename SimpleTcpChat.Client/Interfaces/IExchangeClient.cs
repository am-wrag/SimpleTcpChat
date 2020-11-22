using System;
using System.Threading.Tasks;
using SimpleTcpChat.CommonData.Models;

namespace SimpleTcpChat.Client.Interfaces
{
    public interface IExchangeClient: IDisposable
    {
        Task<SendMessageResult> SendMessage(string message);
    }
}