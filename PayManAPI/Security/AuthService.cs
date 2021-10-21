using MongoDB.Bson;
using MongoDB.Driver;
using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using PayManAPI.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using PayManAPI.Dtos;

namespace PayManAPI.Security
{
    public class AuthService : IAuthService
    {
        private const string dbName = "PayMan";

        private const string colName = "users";

        //A collection is the way that Mongo db assisiates entities together. It is readonly
        private readonly IMongoCollection<User> userCollection;

        private readonly string key;

        private readonly PasswordAuthentication passAuth;

        //Constructor for injecting a MongoDB client
        public AuthService(IMongoClient mongoClient, IConfiguration Configuration)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            userCollection = database.GetCollection<User>(colName);

            key = Configuration.GetSection("JwtKey").ToString();

            passAuth = new PasswordAuthentication();
        }

        //Method for user authentication
        public (User, string) Authentication(string username, string password)
        {
            //Will return null if no match
            var user = userCollection.Find(user => user.UserName == username).FirstOrDefault();

            if (user == null || !passAuth.verifyPassword(user.Password, password))
            {
                return (null, null);
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDiscriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                }),

                Expires = DateTime.UtcNow.AddMinutes(30),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDiscriptor);

            return (user, tokenHandler.WriteToken(token));
        }

        public User GetAuthenticatedUser(string username, string password)
        {
            var user = userCollection.Find(user => user.UserName == username).SingleOrDefault();
            if (!passAuth.verifyPassword(user.Password, password))
            {
                return null;
            }
            return user;
        }
    }
}
