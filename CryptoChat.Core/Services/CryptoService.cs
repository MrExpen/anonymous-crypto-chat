using System.Security.Cryptography;
using System.Text;
using CryptoChat.Core.Interfaces;
using CryptoChat.Core.Models;

namespace CryptoChat.Core.Services;

public class CryptoService : ICryptoService
{
    private readonly ILowLevelCryptoService _lCryptoService;
    private ICryptoService _cryptoServiceImplementation;

    public CryptoService(ILowLevelCryptoService lCryptoService)
    {
        _lCryptoService = lCryptoService;
    }

    public int ImportPublicKey(string publicKey) =>
        _lCryptoService.ImportPublicKey(Convert.FromBase64String(publicKey));

    public int ImportPrivateKey(string privateKey) =>
        _lCryptoService.ImportPrivateKey(Convert.FromBase64String(privateKey));

    public string PublicKey => Convert.ToBase64String(_lCryptoService.PublicKey);

    public string EncryptMessage(string message, string encodingName) =>
        Convert.ToBase64String(_lCryptoService.EncryptData(Encoding.GetEncoding(encodingName).GetBytes(message)));

    public string DecryptMessage(string encryptedMessage, string encodingName) =>
        Encoding.GetEncoding(encodingName).GetString(_lCryptoService.DecryptData(Convert.FromBase64String(encryptedMessage)));

    public string SignMessage(string data, string hashAlgorithmName) =>
        Convert.ToBase64String(_lCryptoService.SignData(Encoding.ASCII.GetBytes(data + PublicKey),
            new HashAlgorithmName(hashAlgorithmName)));

    public bool VerifyMessage(EncryptedSignedMessage message) =>
        _lCryptoService.VerifyData(Encoding.ASCII.GetBytes(message.Message + message.PublicKeyFrom),
            Convert.FromBase64String(message.Signature), new HashAlgorithmName(message.HashAlgorithm));
}