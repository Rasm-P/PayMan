using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using PayManAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PayManAPI.Security
{
    public class AuthService : IAuthService
    {
        private const string dbName = "PayMan";

        private const string colName = "users";

        //A collection is the way that Mongo db assisiates entities together. It is readonly
        private readonly IMongoCollection<UserModel> userCollection;

        private readonly string key;

        private readonly PasswordAuthentication passAuth;

        //Constructor for injecting a MongoDB client along with configurations containing JwtKey string
        public AuthService(IMongoClient mongoClient, IConfiguration Configuration)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            userCollection = database.GetCollection<UserModel>(colName);

            key = Configuration.GetSection("JwtKey").ToString();

            passAuth = new PasswordAuthentication();
        }

        //Method for user authentication
        public async Task<(UserModel, string)> AuthenticationAsync(string username, string password)
        {
            //Will return null if no match
            var user = await userCollection.Find(user => user.UserName == username).FirstOrDefaultAsync();

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
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                }),

                Expires = DateTime.UtcNow.AddMinutes(30),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDiscriptor);

            return (user, tokenHandler.WriteToken(token));
        }

        public async Task<UserModel> GetAuthenticatedUserAsync(string username, string password)
        {
            var user = await userCollection.Find(user => user.UserName == username).SingleOrDefaultAsync();
            if (!passAuth.verifyPassword(user.Password, password))
            {
                return null;
            }
            return user;
        }

        public string GetUserIdFromToken(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
