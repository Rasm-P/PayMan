using PayManAPI.Models;
using System;
using System.Collections.Generic;

namespace PayManAPI.DataFacades
{
    public interface UserFacadeInterface
    {
        User Getuser(Guid id);
        IEnumerable<User> GetUsers();
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User id);
    }
}