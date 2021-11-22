using System;
using System.Collections.Generic;
using System.Text;

namespace PayManXamarin.Models
{
    class LoginModel
    {
        private string userName;
        private string password;

        public LoginModel(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }
    }
}
