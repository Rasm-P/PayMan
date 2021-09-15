using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayManApi.Models
{
    public class Account
    {
        //To do: Random genereate id
        private long id;
        private string userName;
        private string password;

        public Account(String userName, String password)
        {
            this.userName = userName;
            this.password = password;
        }

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

    }
}