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
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(Object sender, EventArgs e)
        {
            // Using // removes the backstack, so that the user cant go back to login again
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }

        private async void TapGesture_Tapped(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
        }
    }
}