namespace PayManXamarin.Models
{
    public class LoginModel
    {
        public string userName { get; set; }
        public string password { get; set; }
        public LoginModel(string username, string pass)
        {
            userName = username;
            password = pass;
        }
    }
}
