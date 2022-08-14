namespace CryptoChat.Core.Models;

public class EncryptedSignedMessage
{
    public string Message { get; init; }
    public string PublicKeyTo { get; init; }
    public string PublicKeyFrom { get; init; }
    public string Encoding { get; init; }
    public string HashAlgorithm { get; init; }
    public string Signature { get; init; }
    public int ProtocolVersion { get; init; }
}