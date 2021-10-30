using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayManAPI.Models;
using PayManAPI.Dtos;

namespace PayManAPI
{
    //Use of extention method
    public static class Extentions
    {
        //Converts a User object to a UserDto
        public static UserDto AsDto(this UserModel user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Frikort = user.Frikort,
                Hovedkort = user.Hovedkort,
                CreatedAt = user.CreatedAt,
            };
        }
    }
}
