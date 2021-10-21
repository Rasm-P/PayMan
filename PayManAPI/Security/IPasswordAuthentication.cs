using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Security
{
    public interface IPasswordAuthentication
    {
        public byte[] generateSalt();

        public string hashPassword(string password, byte[] salt);

        public string generatePassword(string password);

        public bool verifyPassword(string hashedPassword, string passwordToVerify);
    }
}
