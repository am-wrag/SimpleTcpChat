using System;
using SimpleTcpChat.CommonData.Models;

namespace SimpleTcpChat.Client.Interfaces
{
    public interface IServerAnswerFactory
    {
        SendMessageResult CreateSandMessageResult(string response);
        SendMessageResult CreateSandMessageResult(Exception ex);
    }
}