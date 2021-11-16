using System;
namespace PayManMobileApp
{
    public class PayManItem
    {
        public string PayManText { get; set;  }
        public bool Complete { get; set; }

        public PayManItem(string PayManText, bool Complete)
        {
            this.PayManText = PayManText;
            this.Complete = Complete;
        }
    }
}
