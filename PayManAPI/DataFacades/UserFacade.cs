using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PayManAPI.Models;
using PayManAPI.Data;

namespace PayManAPI.DataFacades
{
    public class UserFacade : UserFacadeInterface
    {

        private MyDBContext context;

        public UserFacade(MyDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList();
        }

        public User Getuser(Guid id)
        {
            return context.Users.Find(id);
        }

        public void CreateUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
