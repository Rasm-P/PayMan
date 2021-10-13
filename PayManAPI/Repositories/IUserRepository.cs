using PayManAPI.Models;
using System;
using System.Collections.Generic;

namespace PayManAPI.Repositories
{
    public interface IUserRepository
    {
        User Getuser(Guid id);
        IEnumerable<User> GetUsers();
        void UpdateUser(User user);
        void DeleteUser(Guid id);
        public void CreateUser(User user);
    }
}