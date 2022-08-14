using System.Security.Cryptography;

namespace CryptoChat.Core.Interfaces;

public interface ILowLevelCryptoService
{
    int ImportPublicKey(byte[] publicKey);
    int ImportPrivateKey(byte[] privateKey);

    byte[] PublicKey { get; }
    
    byte[] EncryptData(byte[] data);
    byte[] DecryptData(byte[] data);
    byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithmName);
    bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithmName);
}