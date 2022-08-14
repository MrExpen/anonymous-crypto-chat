using CryptoChat.Core.Controllers;
using CryptoChat.Core.Interfaces;
using CryptoChat.Core.Services;
using Newtonsoft.Json;

var myCryptoService = CryptoServiceFactory.GenerateCryptoService(1024 * 1);
var friendService = CryptoServiceFactory.GenerateCryptoService(1024 * 1);

IMessageController myMessageController = new BasicMessageController(myCryptoService);
IMessageController friendMessageController = new BasicMessageController(friendService);

var encryptedMessage = myMessageController.PrepareMessage("Hello world!", friendService.PublicKey);
var decryptedMessage = friendMessageController.DecryptAndVerifyMessage(encryptedMessage);

Console.WriteLine(JsonConvert.SerializeObject(decryptedMessage, Formatting.Indented));
