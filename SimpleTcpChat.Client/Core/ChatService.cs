using SimpleTcpChat.Client.Interfaces;

namespace SimpleTcpChat.Client.Core
{
    public class ChatService : IChatClientService
    {
        private readonly IExchangeClient _exchangeClient;
        private readonly IChat _chat;
        private bool _doWork;

        public ChatService(IExchangeClient exchangeClient, IChat chat)
        {
            _exchangeClient = exchangeClient;
            _chat = chat;
        }

        public void Run()
        {
            _doWork = true;
            try
            {
                while (_doWork)
                {
                    var userMessage = _chat.GeUserMessage();
                    ProcessMessage(userMessage);
                }
            }
            finally
            {
                _exchangeClient.Dispose();
            }
        }

        private async void ProcessMessage(string message)
        {
            if(!_doWork) return;

            var answer = await _exchangeClient.SendMessage(message);
            if (answer.Success)
            {
                await _chat.WriteNormanServerMessage(answer.Text);
            }
            else
            {
                await _chat.WriteErrorMessage(answer.ErrorText);
            }

            if (answer.ChatIsClosed)
            {
                await _chat.CompleteChat();
                _doWork = false;
            }
        }

        public void Stop()
        {
            _doWork = false;
        }
    }
}