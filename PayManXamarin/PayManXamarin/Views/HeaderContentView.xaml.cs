using PayManXamarin.ViewModels;
using Xamarin.Forms;

namespace PayManXamarin.Views
{
    public partial class HeaderContentView : ContentView
    {
        public HeaderContentView()
        {
            InitializeComponent();
            BindingContext = new HeaderContentViewmodel();
        }
    }
}