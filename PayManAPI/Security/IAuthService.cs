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
        (User, string) Authentication(string username, string password);
        User GetAuthenticatedUser(string username, string password);
    }
}
