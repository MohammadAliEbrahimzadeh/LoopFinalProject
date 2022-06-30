using System.Security.Cryptography;
using System.Text;



namespace Shop.Application.Extentions
{
    public static class HashGenerator
    {
        public static string GenerateHash(string plainText)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(plainText);
            byte[] hash = HashAlgorithm.Create("SHA512").ComputeHash(byteArray);
            return Convert.ToBase64String(hash);
        }

        public static string GenerateSalt()
        {
            const int size = 7;

            Random random = new Random();
            var builder = new StringBuilder();

            char letter;


            for (int i = 0; i < size; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                builder.Append(letter);
            }

            return builder.ToString();
        }

        public static bool CheckPassword(string hashedPassword, string plainText)
        {
            var getSalt = hashedPassword.Chunk(7);
            var builder = new StringBuilder();

            foreach (var item in getSalt)
            {
                var salt = item;
                builder.Append(salt);
                break;
            }

            var generatedPassword = GenerateHash(plainText + builder.ToString());
            generatedPassword = builder.ToString() + generatedPassword;

            if (generatedPassword == hashedPassword)
            {
                return true;
            }

            return false;
        }

        public static string GeneratePassword(string plainText)
        {
            var createSalt = GenerateSalt();
            plainText = plainText + createSalt;
            var hashed = GenerateHash(plainText);
            return createSalt + hashed;
        }
    }
}