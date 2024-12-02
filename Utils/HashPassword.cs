using System.Security.Cryptography;
using System.Text;

namespace A3System.Utils.ValidatorHasher
{
    public class HashPassword
    {
        public static string GerarHash(string senha)
        {
            var sha256 = SHA256.Create();
            var bytesSenha = Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytesSenha);
            return Convert.ToBase64String(hash);
        }
    }
}