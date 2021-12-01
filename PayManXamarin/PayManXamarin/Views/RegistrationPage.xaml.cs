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
            if (EntryPassword.Text == EntryRepeatPassword.Text)
            {
                await registerViewModel.Register(EntryUsername.Text, EntryPassword.Text);
            } else
            {
                await Application.Current.MainPage.DisplayAlert("Register Error", "The passwords were not the same", "Ok");
            }
            EntryUsername.Text = "";
            EntryPassword.Text = "";
            EntryRepeatPassword.Text = "";
        }

        private async void TapGesture_Tapped(Object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}