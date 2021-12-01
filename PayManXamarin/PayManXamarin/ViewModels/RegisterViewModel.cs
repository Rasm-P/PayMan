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
    class RegisterViewModel : BaseViewModel
    {
        private readonly RegisterRepository _registerRepository = new RegisterRepository();

        public async Task Register(string username, string password)
        {
            var register = await _registerRepository.RegisterUser(username, password);
            if (!register.IsError)
            {
                await SecureStorage.SetAsync("accessToken", register.AccessToken);
                Application.Current.Properties.Add("IsLoggedIn", true);
                Application.Current.Properties.Add("user", register.User);
                await Application.Current.SavePropertiesAsync();

                Application.Current.MainPage = new AppShell();
            } else
            {
                await Application.Current.MainPage.DisplayAlert("Register Error", register.Error, "Ok");
            }
        }
    }
}
