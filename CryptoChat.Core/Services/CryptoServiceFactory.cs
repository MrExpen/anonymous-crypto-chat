using System.Data;
using System.Security.Cryptography;
using CryptoChat.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace CryptoChat.Core.Services;

public static class CryptoServiceFactory
{
    private static readonly IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());
    
    public static ILowLevelCryptoService GetLowLevelCryptoService(int version) =>
        version switch
        {
            1 => new LowLevelCryptoService(),
            _ => throw new VersionNotFoundException()
        };

    private static ICryptoService _GetCryptoService(int version, string? publicKey = null, string? privateKey = null)
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
    
    public static ICryptoService GetCryptoService(int version, string? publicKey = null, string? privateKey = null)
    {
        return _memoryCache.GetOrCreate(version + publicKey + privateKey, entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromHours(1);

            return _GetCryptoService(version, publicKey, privateKey);
        })!;
    }

    public static ICryptoService GenerateCryptoService(int keySize)
    {
        var rsaCryptoServiceProvider = new RSACryptoServiceProvider(keySize);

        return GetCryptoService(
            Config.CURRENT_VERSION,
            Convert.ToBase64String(rsaCryptoServiceProvider.ExportRSAPublicKey()),
            Convert.ToBase64String(rsaCryptoServiceProvider.ExportRSAPrivateKey())
        );
    }
}