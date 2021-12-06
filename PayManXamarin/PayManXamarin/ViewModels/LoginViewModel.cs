using MvvmHelpers;
using PayManXamarin.Repositories;
using PayManXamarin.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PayManXamarin.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly LoginRepository _loginRepository = new LoginRepository();

        public async Task Login(string username, string password)
        {
            Authentication.AuthenticationResult authenticate = await _loginRepository.AuthenticateLogin(username, password);
            if (!authenticate.IsError)
            {
                await SecureStorage.SetAsync("accessToken", authenticate.AccessToken);
                Application.Current.Properties.Add("IsLoggedIn", true);
                Application.Current.Properties.Add("user", authenticate.User);
                await Application.Current.SavePropertiesAsync();

                Application.Current.MainPage = new AppShell();
            } else
            {
                await Application.Current.MainPage.DisplayAlert("Login Error",authenticate.Error, "Ok");
            }
        }

    }
}
