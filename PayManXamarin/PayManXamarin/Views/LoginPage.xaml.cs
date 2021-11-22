using PayManXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PayManXamarin.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel loginViewModel;

        public LoginPage()
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel();
        }

        private async void Login_Clicked(Object sender, EventArgs e)
        {
            // Using // removes the backstack, so that the user cant go back to login again
            //await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            loginViewModel.Login("New2", "1234");
        }

        private async void TapGesture_Tapped(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
        }
    }
}