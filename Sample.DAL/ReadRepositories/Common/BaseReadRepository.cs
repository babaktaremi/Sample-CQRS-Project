using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Sample.DAL.ReadRepositories.Common
{
    public class BaseReadRepository<TEntity>
        where TEntity : class, new()
    {
        public IMongoClient MongoClient { get; private set; }
        public IMongoDatabase Db { get; private set; }

        public IMongoCollection<TEntity> Collection { get; }

        
        public BaseReadRepository(string connectionString,string database)
        {
            MongoClient = new MongoClient(connectionString);
            Db = MongoClient.GetDatabase(database);

            var tableName = typeof(TEntity).Name;
            Collection = Db.GetCollection<TEntity>(tableName);
        }

        public async Task<IEnumerable<TEntity>> GetCollection()
        {


            var dataList = await Collection.Find(FilterDefinition<TEntity>.Empty).ToListAsync();
            return dataList;
        }

        public async Task Create(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public async Task<bool> Update(TEntity entity, FilterDefinition<TEntity> filter)
        {
            var result = await Collection.ReplaceOneAsync(filter, entity);

            return result.IsAcknowledged;
        }

        public async Task<List<TEntity>> GetWithFilter(FilterDefinition<TEntity> filter)
        {
            var result = await Collection.Find(filter).ToListAsync();
            return result;
        }

        public async Task<TEntity> GetSingleWithFilter(FilterDefinition<TEntity> filter)
        {
            return await (await Collection.FindAsync(filter)).FirstOrDefaultAsync();
        }

        public async Task<bool> Delete(FilterDefinition<TEntity> filter)
        {
            var result = await Collection.DeleteOneAsync(filter);
            return result.IsAcknowledged;
        }
    }
}