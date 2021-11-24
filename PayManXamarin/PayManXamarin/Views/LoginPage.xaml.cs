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
            await loginViewModel.Login(EntryUsername.Text, EntryPassword.Text);
        }

        private async void TapGesture_Tapped(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
        }
    }
}