using MongoDB.Bson;
using MongoDB.Driver;
using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayManAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string dbName = "PayMan";

        private const string colName = "users";

        //A collection is the way that Mongo db assisiates entities together. It is readonly
        private readonly IMongoCollection<UserModel> userCollection;

        //Filter builder
        private readonly FilterDefinitionBuilder<UserModel> fBuilder = Builders<UserModel>.Filter;

        //Constructor for injecting a MongoDB client
        public UserRepository(IMongoClient mongoClient)
        {
            //docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=mongodbadmin -e MONGO_INITDB_ROOT_PASSWORD=ax2 mongo
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            userCollection = database.GetCollection<UserModel>(colName);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var filter = fBuilder.Eq(user => user.Id, id);
            await userCollection.DeleteOneAsync(filter);
        }

        public async Task<UserModel> GetuserAsync(Guid id)
        {
            var filter = fBuilder.Eq(user => user.Id, id);
            return await userCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            return await userCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateUserAsync(UserModel user)
        {
            var filter = fBuilder.Eq(user => user.Id, user.Id);
            await userCollection.ReplaceOneAsync(filter, user);
        }

        public async Task CreateUserAsync(UserModel user)
        {
            await userCollection.InsertOneAsync(user);
        }

        public async Task<Boolean> IsUsernameTaken(string username)
        {
            var filter = fBuilder.Eq(user => user.UserName, username);
            var user = await userCollection.Find(filter).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            } else
            {
                return true;
            }
        }
    }
}
