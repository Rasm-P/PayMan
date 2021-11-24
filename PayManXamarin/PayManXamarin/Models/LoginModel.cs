using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
