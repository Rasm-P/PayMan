using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PayManMobileApp
{
    public class PayManViewModel
    {

        public ObservableCollection<PayManItem> PayManItems { get; set; }

        public PayManViewModel()
        {
            PayManItems = new ObservableCollection<PayManItem>();

            PayManItems.Add(new PayManItem("PayMan Job 1", false));
            PayManItems.Add(new PayManItem("PayMan Job 2", false));
            PayManItems.Add(new PayManItem("PayMan Job 3", false));
        }

        public ICommand AddPayManCommand => new Command(AddPayManItem);
        public string NewPayManInputValue { get; set; }
        void AddPayManItem()
        {
            PayManItems.Add(new PayManItem(NewPayManInputValue, false));
        }
    }
}
