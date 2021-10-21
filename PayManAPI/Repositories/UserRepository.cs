using MongoDB.Bson;
using MongoDB.Driver;
using PayManAPI.Models;
using PayManAPI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string dbName = "PayMan";

        private const string colName = "users";

        //A collection is the way that Mongo db assisiates entities together. It is readonly
        private readonly IMongoCollection<User> userCollection;

        //Filter builder
        private readonly FilterDefinitionBuilder<User> fBuilder = Builders<User>.Filter;

        //Constructor for injecting a MongoDB client
        public UserRepository(IMongoClient mongoClient)
        {
            //docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            userCollection = database.GetCollection<User>(colName);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var filter = fBuilder.Eq(user => user.Id, id);
            await userCollection.DeleteOneAsync(filter);
        }

        public async Task<User> GetuserAsync(Guid id)
        {
            var filter = fBuilder.Eq(user => user.Id, id);
            return await userCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await userCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var filter = fBuilder.Eq(user => user.Id, user.Id);
            await userCollection.ReplaceOneAsync(filter, user);
        }

        public async Task CreateUserAsync(User user)
        {
            await userCollection.InsertOneAsync(user);
        }
    }
}
