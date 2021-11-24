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

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => SetProperty(ref _isLoggedIn, value);
        }

        public async Task Login(string username, string password)
        {
            var authenticate = await _loginRepository.AuthenticateLogin(username, password);
            if (!authenticate.IsError)
            {
                await SecureStorage.SetAsync("accessToken", authenticate.AccessToken);
                IsLoggedIn = true;

                // Using dubble backslash removes the backstack, so that the user cant go back to login again
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            } else
            {

            }
        }

    }
}
