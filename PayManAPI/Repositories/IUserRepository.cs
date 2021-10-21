using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayManAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetuserAsync(Guid id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
        Task CreateUserAsync(User user);
    }
}