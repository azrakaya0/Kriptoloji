using Microsoft.AspNetCore.Mvc;
using Kriptoloji.Models;
using System.Security.Cryptography;
using System.Text;

namespace Kriptoloji.Controllers
{
    public class AesController : Controller
    {
        public IActionResult Index()
        {
            return View(new AesModel());
        }

        [HttpPost]
        public IActionResult Index(AesModel model, string action)
        {
            if (action == "encrypt" && !string.IsNullOrEmpty(model.PlainText) && !string.IsNullOrEmpty(model.Key))
            {
                model.EncryptedText = EncryptText(model.PlainText, model.Key);
            }
            else if (action == "decrypt" && !string.IsNullOrEmpty(model.EncryptedText) && !string.IsNullOrEmpty(model.Key))
            {
                model.DecryptedText = DecryptText(model.EncryptedText, model.Key);
            }

            return View(model);
        }

        private string EncryptText(string plainText, string key)
        {
            using Aes aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(key));
            aes.IV = new byte[16]; // sabit IV

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

            return Convert.ToBase64String(encrypted);
        }

        private string DecryptText(string encryptedText, string key)
        {
            using Aes aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(key));
            aes.IV = new byte[16]; // sabit IV

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] decrypted = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
