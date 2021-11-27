using PayManXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayManXamarin.Authentication
{
    public class AuthenticationResult
    {
        public string AccessToken { get; set; }

        public string User { get; set; }

        public bool IsError { get; set; }

        public string Error { get; set; }

    }
}
