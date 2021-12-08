using MvvmHelpers;
using Newtonsoft.Json;
using PayManXamarin.Models;
using System;
using Xamarin.Forms;

namespace PayManXamarin.ViewModels
{
    class HeaderContentViewmodel : BaseViewModel
    {
        public string Username { get; set; }
        public string Frikort { get; set; }
        public string Hovedkort { get; set; }

        public HeaderContentViewmodel()
        {
            string jsonString = Convert.ToString(Application.Current.Properties["user"]);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(jsonString);
            Username = user.UserName;
            Frikort = user.Frikort.ToString();
            Hovedkort = user.Hovedkort.ToString();
        }
    }
}
