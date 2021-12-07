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
            if (EntryUsername.Text != null || EntryPassword.Text != null)
            {
                await loginViewModel.Login(EntryUsername.Text, EntryPassword.Text);
                EntryUsername.Text = null;
                EntryPassword.Text = null;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login Error", "Username or password cannot be empty", "Ok");
            }
        }

        private async void TapGesture_Tapped(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}