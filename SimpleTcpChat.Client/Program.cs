using System;
using SimpleTcpChat.Client.Core;
using SimpleTcpChat.Client.Data;
using SimpleTcpChat.Client.Interfaces;
using SimpleTcpChat.CommonData.Models;

namespace SimpleTcpChat.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var chatService = CreateChatService();
            Console.WriteLine("Enter any key to connect chat server...");
            chatService.Run();
        }

        private static IChatClientService CreateChatService()
        {
            var client = CreateExchangeClient();
            var chat = new ConsoleChat();

            return new ChatService(client, chat);
        }

        private static IExchangeClient CreateExchangeClient()
        {
            var settings = ServerSettings.CreateDefault();
            var encoder = new Utf8MessageEncoder();
            var streamOperator = new ChatStreamOperator(encoder);
            var answerFactory = new ServerAnswerFactory();

            return new ExchangeClient(settings, streamOperator, answerFactory);
        }
    }
}
