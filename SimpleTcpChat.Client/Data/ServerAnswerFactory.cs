using System;
using SimpleTcpChat.Client.Interfaces;
using SimpleTcpChat.CommonData.Models;

namespace SimpleTcpChat.Client.Data
{
    public class ServerAnswerFactory : IServerAnswerFactory
    {
        public SendMessageResult CreateSandMessageResult(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                return SendMessageResult.CreateFailure("No server Response");
            }

            if (response.Contains("Error"))
            {
                return SendMessageResult.CreateFailure(response);
            }

            return SendMessageResult.CreateSuccess(response);
        }

        public SendMessageResult CreateSandMessageResult(Exception ex)
        {
            return SendMessageResult.CreateFailure(ex?.Message);
        }
    }
}