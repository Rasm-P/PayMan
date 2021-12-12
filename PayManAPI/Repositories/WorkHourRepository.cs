using MongoDB.Driver;
using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayManAPI.Repositories
{
    public class WorkHourRepository : IWorkHourRepository
    {
        private const string dbName = "PayMan";

        private const string colName = "workHours";

        //A collection is the way that Mongo db assisiates entities together. It is readonly
        private readonly IMongoCollection<WorkHourModel> workHourCollection;

        //Filter builder
        private readonly FilterDefinitionBuilder<WorkHourModel> fBuilder = Builders<WorkHourModel>.Filter;

        //Constructor for injecting a MongoDB client
        public WorkHourRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            workHourCollection = database.GetCollection<WorkHourModel>(colName);
        }

        public async Task CreateWorkHourAsync(WorkHourModel workHour)
        {
            await workHourCollection.InsertOneAsync(workHour);
        }

        public async Task DeleteWorkHourAsync(Guid id)
        {
            var filter = fBuilder.Eq(x => x.Id, id);
            await workHourCollection.DeleteOneAsync(filter);
        }

        public async Task<WorkHourModel> GetWorkHourAsync(Guid id)
        {
            var filter = fBuilder.Eq(x => x.Id, id);
            return await workHourCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<WorkHourModel>> GetWorkHoursAsync(List<Guid> idList)
        {
            var filter = fBuilder.In(x => x.Id, idList);
            return await workHourCollection.Find(filter).ToListAsync();
        }

        public async Task UpdateWorkHourAsync(WorkHourModel workHour)
        {
            var filter = fBuilder.Eq(x => x.Id, workHour.Id);
            await workHourCollection.ReplaceOneAsync(filter, workHour);
        }
    }
}
