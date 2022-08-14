
using CryptoChat.Core;
using CryptoChat.Core.Interfaces;
using CryptoChat.Core.Services;

var myCryptoService = CryptoServiceFactory.GenerateCryptoService(1024 * 8);
var friendService = CryptoServiceFactory.GenerateCryptoService(1024 * 8);

IMessageController myMessageController = new BasicMessageController(myCryptoService);
IMessageController friendMessageController = new BasicMessageController(friendService);

var encryptedMessage = myMessageController.PrepareMessage("Hello world!", friendService.PublicKey);
var decryptedMessage = friendMessageController.DecryptAndVerifyMessage(encryptedMessage);

Console.WriteLine();


