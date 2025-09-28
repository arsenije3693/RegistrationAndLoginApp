//Arsenije Brajovic
// cst350
//9/27/2025
// Registration and Login - Hashing Helper
namespace RegistrationAndLoginApp.Services.Utils
{
    // Static class for hashing helper methods
    public class HashingHelper
    {

        /// <summary>
        /// Hash a plain-text password using BCrypt with a generated salt.
        /// </summary>
        /// <param name="plainTextPassword">The password to hash</param>
        /// <returns>Hashed password</returns>
        public static string HashPassword(string plainTextPassword)
        {
            // Generate a salt to ensure unique password hashes
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the password using the generated salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainTextPassword, salt);

            // Return the hashed password
            return hashedPassword;
        }

        public static bool VerifyPassword(string plainTextPassword, string hashedPassword)
        {
            // Verify the plain-text password against the stored hashed password
            return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
        }





    }
}
