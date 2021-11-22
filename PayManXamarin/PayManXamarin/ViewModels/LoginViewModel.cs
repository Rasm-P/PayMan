using PayManXamarin.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayManXamarin.ViewModels
{
    public class LoginViewModel
    {
        private readonly LoginRepository _loginRepository = new LoginRepository();

        public async void Login(string username, string password)
        {
            var authenticated = await _loginRepository.AuthenticateLogin(username, password);
        }

    }
}
