using Microsoft.AspNetCore.Mvc;
using Kriptoloji.Models;
using System.Security.Cryptography;
using System.Text;

namespace Kriptoloji.Controllers
{
    public class Sha256Controller : Controller
    {
        public IActionResult Index()
        {
            return View(new Sha256Model());
        }

        [HttpPost]
        public IActionResult Index(Sha256Model model)
        {
            if (!string.IsNullOrEmpty(model.InputText))
            {
                using SHA256 sha256 = SHA256.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(model.InputText);
                byte[] hash = sha256.ComputeHash(bytes);
                model.HashedText = BitConverter.ToString(hash).Replace("-", "").ToLower();
            }

            return View(model);
        }
    }
}
