using MvvmHelpers;
using PayManXamarin.Views;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PayManXamarin.ViewModels
{
    class AppShellViewModel : BaseViewModel
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

            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}
