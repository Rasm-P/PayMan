using MongoDB.Bson;
using MongoDB.Driver;
using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Repositories
{
    public class JobRepository : IJobRepository
    {
        private const string dbName = "PayMan";

        private const string colName = "jobs";

        //A collection is the way that Mongo db assisiates entities together. It is readonly
        private readonly IMongoCollection<JobModel> jobCollection;

        //Filter builder
        private readonly FilterDefinitionBuilder<JobModel> fBuilder = Builders<JobModel>.Filter;

        //Constructor for injecting a MongoDB client
        public JobRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            jobCollection = database.GetCollection<JobModel>(colName);
        }

        public async Task CreateJobAsync(JobModel job)
        {
            await jobCollection.InsertOneAsync(job);
        }

        public async Task DeleteJobAsync(Guid id)
        {
            var filter = fBuilder.Eq(job => job.Id, id);
            await jobCollection.DeleteOneAsync(filter);
        }

        public async Task<JobModel> GetJobAsync(Guid id)
        {
            var filter = fBuilder.Eq(job => job.Id, id);
            return await jobCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<JobModel>> GetJobsAsync()
        {
            return await jobCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateJobAsync(JobModel job)
        {
            var filter = fBuilder.Eq(job => job.Id, job.Id);
            await jobCollection.ReplaceOneAsync(filter, job);
        }
    }
}
