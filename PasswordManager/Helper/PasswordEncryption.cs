using System.Text;

namespace PasswordManager.Helper
{
    public static class PasswordEncryption
    {
        public static string GetEncryptedPassword(string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var encodedPassword = Convert.ToBase64String(passwordBytes);

            return encodedPassword;
        }

        public static string GetDecryptedPassword(string encryptedPassword)
        {
            var decodedBytes = Convert.FromBase64String(encryptedPassword);
            var decodedPassword = Encoding.UTF8.GetString(decodedBytes);

            return decodedPassword;
        }
    }
}
