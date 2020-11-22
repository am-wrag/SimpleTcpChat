using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SimpleTcpChat.CommonData.Interfaces;

namespace SimpleTcpChat.CommonData.Models
{
    public class ChatStreamOperator : IChatStreamOperator
    {
        private readonly IMessageEncoder _encoder;
        private const int bufferSize = 64;

        public ChatStreamOperator(IMessageEncoder encoder)
        {
            _encoder = encoder;
        }

        public async Task<string> ReadAsync(NetworkStream stream)
        { 
            var buffer = new byte[bufferSize];
            var builder = new StringBuilder();

            do
            {
                await stream.ReadAsync(buffer, 0, buffer.Length);
                var request = await _encoder.Decode(buffer);
                builder.Append(request);

            } while (stream.DataAvailable);

            return builder.ToString();     
        }

        public async Task WriteAsync(NetworkStream stream, string responseText)
        {
            var response = await _encoder.Encode(responseText);
            await stream.WriteAsync(response, 0, response.Length);
        }
    }
}