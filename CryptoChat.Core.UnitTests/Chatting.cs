using CryptoChat.Core.Controllers;
using CryptoChat.Core.Interfaces;
using CryptoChat.Core.Services;
using FluentAssertions;

namespace CryptoChat.Core.UnitTests;

public class Chatting
{
    private readonly IMessageController _firstMessageController;
    private readonly IMessageController _secondMessageController;
    private readonly string _text = "Hello world!";

    public Chatting()
    {
        _firstMessageController = new BasicMessageController(CryptoServiceFactory.GenerateCryptoService(1024));
        _secondMessageController = new BasicMessageController(CryptoServiceFactory.GenerateCryptoService(2048));
    }

    [Fact]
    public void TestChatting()
    {
        var encryptedMessage = _firstMessageController.PrepareMessage(_text, _secondMessageController.CryptoService.PublicKey);
        var decryptedMessage = _secondMessageController.DecryptAndVerifyMessage(encryptedMessage);

        decryptedMessage.Signed.Should().BeTrue();
        decryptedMessage.Text.Should().Be(_text);
        decryptedMessage.From.Should().Be(_firstMessageController.CryptoService.PublicKey);

        encryptedMessage.PublicKeyFrom.Should().Be(_firstMessageController.CryptoService.PublicKey);
        encryptedMessage.PublicKeyTo.Should().Be(_secondMessageController.CryptoService.PublicKey);
        encryptedMessage.HashAlgorithm.Should().Be(Config.DEFAULT_HASH_ALGORITHM);
        encryptedMessage.Encoding.Should().Be(Config.DEFAULT_ENCODING);
    }
}