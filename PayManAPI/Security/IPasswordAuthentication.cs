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
