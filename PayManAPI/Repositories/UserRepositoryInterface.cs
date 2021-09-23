using PayManAPI.Models;
using System;
using System.Collections.Generic;

namespace PayManAPI.Repositories
{
    public interface UserRepositoryInterface
    {
        User Getuser(Guid id);
        IEnumerable<User> GetUsers();
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(Guid id);
    }
}