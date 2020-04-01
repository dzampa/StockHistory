using System.Security.Cryptography;
using System.Text;

namespace Util
{
    public class HashMd5Generator
    {
        public static string Generate(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Convert the String to an array of bytes, which is how the library works.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // A StringBuilder is created to recompose the string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
