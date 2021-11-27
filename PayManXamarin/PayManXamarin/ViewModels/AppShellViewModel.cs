using PayManXamarin.Views;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PayManXamarin.ViewModels
{
    class AppShellViewModel
    {

        private Command logout;

        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new Command(PerformLogout);
                }

                return logout;
            }
        }

        private void PerformLogout()
        {
            SecureStorage.RemoveAll();
            Application.Current.Properties.Clear();
            Application.Current.SavePropertiesAsync();

            Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
