using CryptoChat.Core.Interfaces;
using CryptoChat.Core.Models;
using CryptoChat.Core.Services;

namespace CryptoChat.Core.Controllers;

public class BasicMessageController : IMessageController
{
    public BasicMessageController(ICryptoService cryptoService)
    {
        CryptoService = cryptoService;
    }

    public ICryptoService CryptoService { get; set; }

    public EncryptedSignedMessage PrepareMessage(string message, string publicKey, string encodingName,
        string hashAlgorithmName)
    {
        var encryptedMessage =
            CryptoServiceFactory.GetCryptoService(Config.CURRENT_VERSION, publicKey).EncryptMessage(message, encodingName);
        return new EncryptedSignedMessage
        {
            Encoding = encodingName,
            HashAlgorithm = hashAlgorithmName,
            PublicKeyTo = publicKey,
            PublicKeyFrom = CryptoService.PublicKey,
            Message = encryptedMessage,
            Signature = CryptoService.SignMessage(encryptedMessage, hashAlgorithmName),
            ProtocolVersion = Config.CURRENT_VERSION
        };
    }

    public Message DecryptAndVerifyMessage(EncryptedSignedMessage encryptedMessage) =>
        new()
        {
            From = encryptedMessage.PublicKeyFrom,
            Text = CryptoService.DecryptMessage(encryptedMessage),
            Signed = CryptoServiceFactory.GetCryptoService(encryptedMessage.ProtocolVersion, encryptedMessage.PublicKeyFrom)
                .VerifyFromMe(encryptedMessage)
        };

    public static bool VerifyMessage(EncryptedSignedMessage encryptedSignedMessage) =>
        CryptoServiceFactory.GetCryptoService(encryptedSignedMessage.ProtocolVersion, encryptedSignedMessage.PublicKeyFrom)
            .VerifyFromMe(encryptedSignedMessage);
}