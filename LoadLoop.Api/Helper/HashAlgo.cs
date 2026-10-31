using System.Security.Cryptography;
using System.Text;

namespace LoadLoop.api.Helper;
public class HashAlgo
{
    public static string hashPassword(string password)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = md5.ComputeHash(bytes);

            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}