using System.Collections.Concurrent;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using SimpleTcpChat.Server.Interfaces;

namespace SimpleTcpChat.Server.Core
{
    public class ChatBot : IChatBot
    {
        private readonly ConcurrentDictionary<TcpClient, UserInfo> _activeUsers =
            new ConcurrentDictionary<TcpClient, UserInfo>();

        public async Task<string> HandleRequest(string request, TcpClient client)
        {
            if (!_activeUsers.ContainsKey(client))
            {
                _activeUsers.TryAdd(client, new UserInfo());
                return "Welcome to chat server. Please enter your name.";
            }

            if (_activeUsers[client].IsNewUser)
            {
                _activeUsers[client].SetName(request);
                return $"Hello {request}. To get chat commands enter GetChatCommands";
            }

            return request switch
            {
                Commands.GetActiveUsers => await GetActiveUsersList(),
                Commands.GetChatCommands => await GetCommandList(),
                Commands.Leave => await ProcessLeaveCommand(client),
                Commands.HowAreYou => "Хорошо",
                _ => string.Empty
            };
        }

        private Task<string> ProcessLeaveCommand(TcpClient client)
        {
            return Task.Run(() =>
            {
                _activeUsers.TryRemove(client, out _);
                return "Chat closed. Good bay.";
            });
        }

        private Task<string> GetCommandList()
        {
            return Task.Run(() =>
                Commands.All.Aggregate("\n", (current, command) => current + $"{command}\n").TrimEnd('\n')); 
        }
        private Task<string> GetActiveUsersList()
        {
            return Task.Run(() => 
            {
                var users = _activeUsers.Where(v => v.Key.Connected).Select(u => u.Value.Name);
                var result = users.Aggregate("\n", (current, user) => current + $"{user}\n");
                return result.TrimEnd('\n');
            });
        }
    }

    internal class UserInfo
    {
        public bool IsNewUser { get; private set; }
        public string Name { get; private set; }

        public UserInfo()
        {
            IsNewUser = true;
        }

        public void SetName(string name)
        {
            Name = name;
            IsNewUser = false;
        }

    }
}