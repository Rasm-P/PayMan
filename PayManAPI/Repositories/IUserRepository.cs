using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayManAPI.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> GetuserAsync(Guid id);
        Task<IEnumerable<UserModel>> GetUsersAsync();
        Task UpdateUserAsync(UserModel user);
        Task DeleteUserAsync(Guid id);
        Task CreateUserAsync(UserModel user);
    }
}