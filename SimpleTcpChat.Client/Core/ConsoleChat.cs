using System;
using System.Threading.Tasks;
using SimpleTcpChat.Client.Interfaces;

namespace SimpleTcpChat.Client.Core
{
    public class ConsoleChat : IChat
    {
        public string GeUserMessage()
        {
            return Console.ReadLine();
        }

        public Task WriteAnotherUserMessage(string userName, string message)
        {
            return Task.Run(() =>
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"{userName}: {message}");
                Console.ForegroundColor = ConsoleColor.White;
            });
        }

        public Task WriteNormanServerMessage(string message)
        {
            return Task.Run(() =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Server message: {message}");
                Console.ForegroundColor = ConsoleColor.White;
            });
        }

        public Task WriteErrorMessage(string message)
        {
            return Task.Run(() =>
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error!\n{message}");
                Console.ForegroundColor = ConsoleColor.White;
            });
        }

        public Task CompleteChat()
        {
            return Task.Run(() =>
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Chat completed. Enter eny key to exit.");
                Console.ForegroundColor = ConsoleColor.White;
            });
        }
    }
}