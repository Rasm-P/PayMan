using MongoDB.Driver;
using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Repositories
{
    public class MongoDbUserRepository : UserRepositoryInterface
    {
        private const string dbName = "PayMan";

        private const string colName = "users";

        //A collection is the way that Mongo db assisiates entities together. It is readonly
        private readonly IMongoCollection<User> userCollection;

        //Constructor for injecting a MongoDB client
        public MongoDbUserRepository(IMongoClient mongoClient)
        {
            //docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
            //https://youtu.be/ZXdFisA_hOY?t=5920
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            userCollection = database.GetCollection<User>(colName);
        }

        public void CreateUser(User user)
        {
            userCollection.InsertOne(user);
        }

        public void DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public User Getuser(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
