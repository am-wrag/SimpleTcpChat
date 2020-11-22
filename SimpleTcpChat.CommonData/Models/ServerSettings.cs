using System.Net;

namespace SimpleTcpChat.CommonData.Models
{
    public class ServerSettings
    {
        public int Port { get; }
        public string ServerAddress { get; }
        public IPEndPoint EndPoint => IPEndPoint.Parse($"{ServerAddress}:{Port}");

        public ServerSettings(string serverAddress, int port)
        {
            ServerAddress = serverAddress;
            Port = port;
        }

        public static ServerSettings CreateDefault()
        {
            return new ServerSettings("127.0.0.1", 11111);
        }
    }
}