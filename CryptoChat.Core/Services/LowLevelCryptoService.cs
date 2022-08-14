using System.Security.Cryptography;
using CryptoChat.Core.Interfaces;

namespace CryptoChat.Core.Services;

public class LowLevelCryptoService : ILowLevelCryptoService
{
    private readonly RSACryptoServiceProvider _rsaCryptoServiceProvider = new();
    
    public int ImportPublicKey(byte[] publicKey)
    {
        _rsaCryptoServiceProvider.ImportRSAPublicKey(publicKey, out var bytesRead);
        return bytesRead;
    }

    public int ImportPrivateKey(byte[] privateKey)
    {
        _rsaCryptoServiceProvider.ImportRSAPrivateKey(privateKey, out var bytesRead);
        return bytesRead;
    }

    public byte[] PublicKey => _rsaCryptoServiceProvider.ExportRSAPublicKey();

    public byte[] EncryptData(byte[] data) => _rsaCryptoServiceProvider.Encrypt(data, RSAEncryptionPadding.Pkcs1);

    public byte[] DecryptData(byte[] data) => _rsaCryptoServiceProvider.Decrypt(data, RSAEncryptionPadding.Pkcs1);

    public byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithmName) =>
        _rsaCryptoServiceProvider.SignData(data, hashAlgorithmName, RSASignaturePadding.Pkcs1);

    public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithmName) =>
        _rsaCryptoServiceProvider.VerifyData(data, signature, hashAlgorithmName, RSASignaturePadding.Pkcs1);
}