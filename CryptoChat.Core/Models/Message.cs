namespace CryptoChat.Core.Models;

public class Message
{
    public string Text { get; set; }
    public string From { get; set; }
    public bool Signed { get; set; }
}