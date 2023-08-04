using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Auth2.Helpers
{
    public static class Base64Helper
    {
        public static string ComputeHash(string codeVerifier)
        {
            using var sha256 = SHA256.Create();
            var hash = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier))).Replace("-", string.Empty, StringComparison.InvariantCulture);

            return hash.ToLower(CultureInfo.InvariantCulture);
        }
    }
}
