using System;
using SimpleTcpChat.CommonData.Models;
using SimpleTcpChat.Server.Core;
using SimpleTcpChat.Server.Interfaces;

namespace SimpleTcpChat.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var charServer = CreateTestChatServer();
            charServer.Start();
            Console.WriteLine("Enter eny key to stop server...");
            Console.ReadLine();
            charServer.Stop();
        }

        private static IChatServer CreateTestChatServer()
        {
            var chatBot = new ChatBot();
            var encoder = new Utf8MessageEncoder();
            var streamOperator = new ChatStreamOperator(encoder);
            return new ChatServer(chatBot, streamOperator, ServerSettings.CreateDefault());
        }
    }
}
