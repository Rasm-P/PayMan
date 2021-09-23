using System;
using System.Collections.Generic;
using System.Linq;
using PayManAPI.Models;

namespace PayManAPI.DataFacades
{
    public class UserFacade : UserFacadeInterface
    {
        private readonly List<User> users = new()
        {
            new User { Id = Guid.NewGuid(), UserName = "Rasmus", Frikort = 45555.50, Hovedkort = 0.0, CreatedAt = DateTimeOffset.Now },
            new User { Id = Guid.NewGuid(), UserName = "Susan", Frikort = 0.0, Hovedkort = 44444.44, CreatedAt = DateTimeOffset.Now },
            new User { Id = Guid.NewGuid(), UserName = "Anders", Frikort = 15555.55, Hovedkort = 0.0, CreatedAt = DateTimeOffset.Now }
        };

        public IEnumerable<User> GetUsers()
        {
            return users;
        }

        public User Getuser(Guid id)
        {
            return users.Where(User => User.Id == id).SingleOrDefault();
        }

        public void CreateUser(User user)
        {
            users.Add(user);
        }

        public void UpdateUser(User user)
        {
            var index = users.FindIndex(userToUpdate => userToUpdate.Id == user.Id);
            users[index] = user;
        }

        public void DeleteUser(Guid id)
        {
            var index = users.FindIndex(userToDelete => userToDelete.Id == id);
            users.RemoveAt(index);
        }
    }
}
