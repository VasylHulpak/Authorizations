using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Auth2.Helpers
{
    public class Base64Helper
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        
        public static string ComputeHash(string codeVerifier)
        {
            using var sha256 = new SHA256Managed();
            var hash = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier))).Replace("-", string.Empty, StringComparison.InvariantCulture);

            return hash.ToLower(CultureInfo.InvariantCulture);
        }
    }
}
