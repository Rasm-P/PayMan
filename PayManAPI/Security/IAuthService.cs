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
        Task<(UserModel, string)> AuthenticationAsync(string username, string password);
        Task<UserModel> GetAuthenticatedUserAsync(string username, string password);
    }
}
