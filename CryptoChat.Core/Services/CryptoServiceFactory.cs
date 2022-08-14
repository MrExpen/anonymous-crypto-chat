using System.Data;
using CryptoChat.Core.Interfaces;

namespace CryptoChat.Core.Services;

public static class CryptoServiceFactory
{
    public static ILowLevelCryptoService GetLowLevelCryptoService(int version) =>
        version switch
        {
            1 => new LowLevelCryptoService(),
            _ => throw new VersionNotFoundException()
        };

    public static ICryptoService GetCryptoService(int version, string? publicKey = null, string? privateKey = null)
    {
        var service = version switch
        {
            1 => new CryptoService(GetLowLevelCryptoService(1)),
            _ => throw new VersionNotFoundException()
        };
        
        if (!string.IsNullOrEmpty(publicKey))
            service.ImportPublicKey(publicKey);
        

        if (!string.IsNullOrEmpty(privateKey))
            service.ImportPrivateKey(privateKey);

        return service;
    }
}