using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using SimpleTcpChat.Client.Interfaces;
using SimpleTcpChat.CommonData.Interfaces;
using SimpleTcpChat.CommonData.Models;

namespace SimpleTcpChat.Client.Core
{
    public class ExchangeClient : IExchangeClient
    {
        private readonly ServerSettings _settings;
        private readonly IChatStreamOperator _streamOperator;
        private readonly IServerAnswerFactory _answerFactory;

        private bool _needToConnect = true;
        private NetworkStream stream;
        private readonly TcpClient client = new TcpClient();

        public ExchangeClient(ServerSettings settings, IChatStreamOperator streamOperator, IServerAnswerFactory answerFactory)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _streamOperator = streamOperator ?? throw new ArgumentNullException(nameof(streamOperator));
            _answerFactory = answerFactory ?? throw new ArgumentNullException(nameof(answerFactory));
        }

        public async Task<SendMessageResult> SendMessage(string message)
        {
            try
            {
                if (_needToConnect)
                {
                    client.Connect(_settings.ServerAddress, _settings.Port);
                    stream = client.GetStream();
                    _needToConnect = false;
                }

                await _streamOperator.WriteAsync(stream, message);
                var responseText = await _streamOperator.ReadAsync(stream);

                return _answerFactory.CreateSandMessageResult(responseText);
            }
            catch (Exception e)
            {
                return _answerFactory.CreateSandMessageResult(e);
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}