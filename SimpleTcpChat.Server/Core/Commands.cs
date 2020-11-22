using System.Collections.Generic;

namespace SimpleTcpChat.Server.Core
{
    public static class Commands
    {
        public const string GetActiveUsers = nameof(GetActiveUsers);
        public const string GetChatCommands = nameof(GetChatCommands);
        public const string Leave = "Пока";
        public const string HowAreYou = "Как дела?";

        public static IEnumerable<string> All => new List<string>
        {
            GetActiveUsers , GetChatCommands , Leave, HowAreYou
        };
    }
}