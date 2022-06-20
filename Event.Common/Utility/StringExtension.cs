using System.Security.Cryptography;
using System.Text;

namespace Event.Common.Utility
{
    public static class StringExtension
    {
        public static string ToMd5Hash(this string stringData)
        {
            var md5 = MD5.Create();
            var data = md5.ComputeHash(Encoding.UTF8.GetBytes(stringData));
            var stringBuilder = new StringBuilder();

            foreach (var t in data)
            {
                stringBuilder.Append(t.ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
}
