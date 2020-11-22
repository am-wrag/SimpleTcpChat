using System.Threading.Tasks;

namespace SimpleTcpChat.CommonData.Interfaces
{
    public interface IMessageEncoder
    {
        Task<byte[]> Encode(string text);
        Task<string> Decode(byte[] bytes);
    }
}