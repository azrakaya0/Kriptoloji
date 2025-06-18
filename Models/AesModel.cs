namespace Kriptoloji.Models
{
    public class AesModel
    {
        public string PlainText { get; set; }
        public string Key { get; set; }

        public string EncryptedText { get; set; }
        public string DecryptedText { get; set; }
    }
}
