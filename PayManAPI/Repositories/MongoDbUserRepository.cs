using MongoDB.Bson;
using MongoDB.Driver;
using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Repositories
{
    public class MongoDbUserRepository : IUserRepository
    {
        private const string dbName = "PayMan";

        private const string colName = "users";

        //A collection is the way that Mongo db assisiates entities together. It is readonly
        private readonly IMongoCollection<User> userCollection;

        //Filter builder
        private readonly FilterDefinitionBuilder<User> fBuilder = Builders<User>.Filter;

        //Constructor for injecting a MongoDB client
        public MongoDbUserRepository(IMongoClient mongoClient)
        {
            //docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            userCollection = database.GetCollection<User>(colName);
        }

        public void DeleteUser(Guid id)
        {
            var filter = fBuilder.Eq(user => user.Id, id);
            userCollection.DeleteOne(filter);
        }

        public User Getuser(Guid id)
        {
            var filter = fBuilder.Eq(user => user.Id, id);
            return userCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return userCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateUser(User user)
        {
            var filter = fBuilder.Eq(user => user.Id, user.Id);
            userCollection.ReplaceOne(filter, user);
        }
    }
}
