using CryptoChat.Core.Interfaces;
using CryptoChat.Core.Models;
using CryptoChat.Core.Services;

namespace CryptoChat.Core;

public class BasicMessageController : IMessageController
{
    public BasicMessageController(ICryptoService cryptoService)
    {
        CryptoService = cryptoService;
    }

    public ICryptoService CryptoService { get; set; }

    protected virtual ICryptoService GetCryptoServiceFor(int version, string publicKey, string? privateKey = null) =>
        CryptoServiceFactory.GetCryptoService(version, publicKey, privateKey);

    public EncryptedSignedMessage PrepareMessage(string message, string publicKey, string encodingName,
        string hashAlgorithmName)
    {
        var encryptedMessage =
            GetCryptoServiceFor(Config.CURRENT_VERSION, publicKey).EncryptMessage(message, encodingName);
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
            Signed = GetCryptoServiceFor(encryptedMessage.ProtocolVersion, encryptedMessage.PublicKeyFrom)
                .VerifyMessage(encryptedMessage)
        };
}