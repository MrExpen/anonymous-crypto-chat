using CryptoChat.Core.Models;

namespace CryptoChat.Core.Interfaces;

public interface ICryptoService
{
    int ImportPublicKey(string publicKey);
    int ImportPrivateKey(string privateKey);

    string PublicKey { get; }
    
    string EncryptMessage(string message, string encodingName);
    string DecryptMessage(string encryptedMessage, string encodingName);
    string SignMessage(string message, string hashAlgorithmName);
    bool VerifyMessage(EncryptedSignedMessage message);
}