using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManApi.Models
{
    interface AccountInterface
    {
        long id
        {
            get;
            set;
        }

        String userName
        {
            get;
            set;
        }

        String password
        {
            get;
            set;
        }
    }
}
