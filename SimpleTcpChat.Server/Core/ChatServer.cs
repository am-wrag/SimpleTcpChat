using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using SimpleTcpChat.CommonData.Interfaces;
using SimpleTcpChat.CommonData.Models;
using SimpleTcpChat.Server.Interfaces;

namespace SimpleTcpChat.Server.Core
{
    public class ChatServer : IChatServer, IDisposable
    {
        private readonly IChatBot _chatBot;
        private readonly IChatStreamOperator _streamOperator;
        private readonly TcpListener _listener;
        private bool _doWork = true;

        public ChatServer(IChatBot chatBot, IChatStreamOperator streamOperator, ServerSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            _chatBot = chatBot ?? throw new ArgumentNullException(nameof(chatBot));
            _streamOperator = streamOperator ?? throw new ArgumentNullException(nameof(streamOperator));
            _listener = new TcpListener(settings.EndPoint);
        }

        public async Task Start()
        {
            _listener.Start();
            Console.WriteLine("Server started.");
            try
            {
                while (_doWork)
                {
                    var clientConnect = await _listener.AcceptTcpClientAsync();
                    ProcessClientConnect(clientConnect);
                }
            }
            finally
            {
                _listener.Stop();
            }
        }

        public void Stop()
        {
            _doWork = false;
        }
        public void Dispose()
        {
            Stop();
        }

        private void ProcessClientConnect(TcpClient clientConnect)
        {
            Task.Run(async () =>
            {
                try
                {
                    var messageStream = clientConnect.GetStream();
                    while (true)
                    {
                        if (!clientConnect.Connected)
                        {
                            break;
                        }
                        var request = await _streamOperator.ReadAsync(messageStream);

                        var response = await _chatBot.HandleRequest(request, clientConnect);
                        if (string.IsNullOrEmpty(response))
                        {
                            continue;
                        }
                        await _streamOperator.WriteAsync(messageStream, response);

                        if (response.Contains("Chat closed"))
                        {
                            break;
                        }
                    }
                }
                finally
                {
                    clientConnect.Dispose();
                }
            });
        }
    }
}