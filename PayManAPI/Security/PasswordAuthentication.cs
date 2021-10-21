using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PayManAPI.Dtos;
using PayManAPI.Models;

namespace PayManAPI.Security
{
    public class PasswordAuthentication : IPasswordAuthentication
    {
        private int saltBytes = 128 / 8;
        private KeyDerivationPrf prf = KeyDerivationPrf.HMACSHA256;
        private int iterationCount = 100000;
        private int subKeyBytes = 256 / 8;

        public byte[] generateSalt()
        {
            // 128-bit salt using a cryptographically strong random sequence of nonzero values
            var salt = new byte[saltBytes];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }

        public string hashPassword(string password, byte[] salt)
        {
            // 256-bit subkey (use HMACSHA256 with 100000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: prf,
                iterationCount: iterationCount,
                numBytesRequested: subKeyBytes));
            return hashed;
        }

        public string generatePassword(string password)
        {
            byte[] salt = generateSalt();
            string hashedPassword = Convert.ToBase64String(salt) + "." + hashPassword(password, salt);
            return hashedPassword;
        }

        public bool verifyPassword(string hashedPassword, string passwordToVerify)
        {
            string[] passwordString = hashedPassword.Split(".");
            byte[] salt = Convert.FromBase64String(passwordString[0]);
            string password = passwordString[1];

            string hashToVerify = hashPassword(passwordToVerify, salt);
            Console.WriteLine(passwordToVerify);
            Console.WriteLine(hashToVerify);
            Console.WriteLine(hashedPassword);
            return password == hashToVerify;
        }
    }
}
