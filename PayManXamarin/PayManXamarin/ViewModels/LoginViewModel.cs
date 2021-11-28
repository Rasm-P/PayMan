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
            var authenticate = await _loginRepository.AuthenticateLogin(username, password);
            if (!authenticate.IsError)
            {
                await SecureStorage.SetAsync("accessToken", authenticate.AccessToken);
                Application.Current.Properties.Add("IsLoggedIn", true);
                Application.Current.Properties.Add("user", authenticate.User);
                await Application.Current.SavePropertiesAsync();

                // Using dubble backslash removes the backstack, so that the user cant go back to login again
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            } else
            {
                await Application.Current.MainPage.DisplayAlert("Login Error",authenticate.Error, "Ok");
            }
        }

    }
}
