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
        Task<(User, string)> AuthenticationAsync(string username, string password);
        Task<User> GetAuthenticatedUserAsync(string username, string password);
    }
}
