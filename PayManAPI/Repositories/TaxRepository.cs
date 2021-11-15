using MongoDB.Driver;
using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Repositories
{
    public class TaxRepository : ITaxRepository
    {
        private const string dbName = "PayMan";

        private const string colName = "taxes";

        //A collection is the way that Mongo db assisiates entities together. It is readonly
        private readonly IMongoCollection<TaxModel> taxCollection;

        //Filter builder
        private readonly FilterDefinitionBuilder<TaxModel> fBuilder = Builders<TaxModel>.Filter;

        //Constructor for injecting a MongoDB client
        public TaxRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dbName);
            taxCollection = database.GetCollection<TaxModel>(colName);
        }

        public async Task CreateTaxAsync(TaxModel tax)
        {
            await taxCollection.InsertOneAsync(tax);
        }

        public async Task DeleteTaxAsync(Guid id)
        {
            var filter = fBuilder.Eq(x => x.Id, id);
            await taxCollection.DeleteOneAsync(filter);
        }

        public async Task<TaxModel> GetTaxAsync(Guid id)
        {
            var filter = fBuilder.Eq(x => x.Id, id);
            return await taxCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TaxModel>> GetTaxsAsync(List<Guid> idList)
        {
            var filter = fBuilder.In(x => x.Id, idList);
            return await taxCollection.Find(filter).ToListAsync();
        }

        public async Task UpdateTaxAsync(TaxModel tax)
        {
            var filter = fBuilder.Eq(x => x.Id, tax.Id);
            await taxCollection.ReplaceOneAsync(filter, tax);
        }
    }
}
