using System.Security.Cryptography;
using System.Text;

namespace TTV.Infrastructure;
public static class MD5StringExtensions
{
    public static string ToMD5Hash(this string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = MD5.HashData(inputBytes);

        StringBuilder sb = new();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("x2"));
        }

        return sb.ToString();
    }
}
