using PayManAPI.Dtos;
using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Security
{
    public interface IAuthService
    {
        string Authentication(string username, string password);
        User GetAuthoriceduser(string username, string password);
        void CreateUser(User user);
    }
}
