using CryptoChat.Core.Interfaces;
using CryptoChat.Core.Models;

namespace CryptoChat.Core.Services;

public static class CryptoServiceExtensions
{
    public static string DecryptMessage(this ICryptoService service, EncryptedSignedMessage message) => 
        service.DecryptMessage(message.Message, message.Encoding);
}