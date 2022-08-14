using CryptoChat.Core.Models;

namespace CryptoChat.Core.Interfaces;

public interface IMessageController
{
    ICryptoService MyCryptoService { get; set; }

    EncryptedSignedMessage PrepareMessage(string message, string publicKey,
        string encodingName = Config.DEFAULT_ENCODING,
        string hashAlgorithmName = Config.DEFAULT_HASH_ALGORITHM);

    Message DecryptAndVerifyMessage(EncryptedSignedMessage encryptedMessage);
}