using PayManXamarin.ViewModels;
using PayManXamarin.Views;
using Xamarin.Forms;

namespace PayManXamarin
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel();

            Routing.RegisterRoute(nameof(JobsPage), typeof(JobsPage));
            Routing.RegisterRoute(nameof(TaxesPage), typeof(TaxesPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(WorkHoursPage), typeof(WorkHoursPage));
        }
    }
}