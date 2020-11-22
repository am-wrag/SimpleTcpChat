using System;

namespace SimpleTcpChat.CommonData.Models
{
    public class SendMessageResult
    {
        public bool Success { get;}
        public string Text { get; }
        public string ErrorText { get; }
        public bool ChatIsClosed { get; private set; }

        private SendMessageResult(bool success, string text, string errorText)
        {
            Success = success;
            Text = text;
            ErrorText = errorText;
        }

        public static SendMessageResult CreateSuccess(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            
            var result = new SendMessageResult(true, text, null);
            if (text.Contains("Chat closed"))
            {
                result.ChatIsClosed = true;
            }
            return result;
        }
        public static SendMessageResult CreateFailure(string errorText)
        {
            if (errorText == null) throw new ArgumentNullException(nameof(errorText));

            return new SendMessageResult(false, null, errorText);
        }
    }
}