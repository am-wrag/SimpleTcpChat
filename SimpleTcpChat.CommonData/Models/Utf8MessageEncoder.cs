using System.Text;
using System.Threading.Tasks;
using SimpleTcpChat.CommonData.Interfaces;

namespace SimpleTcpChat.CommonData.Models
{
    public class Utf8MessageEncoder : IMessageEncoder
    {
        public Task<byte[]> Encode(string text)
        {
            return Task.Run(() => Encoding.UTF8.GetBytes(text));
        }

        public Task<string> Decode(byte[] bytes)
        {
            return Task.Run(() => Encoding.UTF8.GetString(bytes).TrimEnd('\0'));
        }
    }
}