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
    public partial class RegistrationPage : ContentPage
    {
        private readonly RegisterViewModel registerViewModel;

        public RegistrationPage()
        {
            InitializeComponent();
            registerViewModel = new RegisterViewModel();
        }

        private async void Register_Clicked(Object sender, EventArgs e)
        {
            if (EntryUsername.Text == null || EntryPassword.Text == null)
            {
                await Application.Current.MainPage.DisplayAlert("Register Error", "Username or password cannot be empty", "Ok");
            }
            else if (EntryPassword.Text != EntryRepeatPassword.Text)
            {
                await Application.Current.MainPage.DisplayAlert("Register Error", "The passwords were not the same", "Ok");
            } 
            else
            {
                await registerViewModel.Register(EntryUsername.Text, EntryPassword.Text);
            }
            EntryUsername.Text = null;
            EntryPassword.Text = null;
            EntryRepeatPassword.Text = null;
        }

        private async void TapGesture_Tapped(Object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}