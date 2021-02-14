using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Sample.DAL.ReadRepositories.Common
{
    public class BaseReadRepository<TEntity>
        where TEntity : class, new()
    {
        public IMongoClient MongoClient { get; private set; }
        public IMongoDatabase Db { get; private set; }

        public IMongoCollection<TEntity> Collection { get; }

        public BaseReadRepository(string connectionString, string database)
        {
            MongoClient = new MongoClient(connectionString);
            Db = MongoClient.GetDatabase(database);

            var tableName = typeof(TEntity).Name;
            Collection = Db.GetCollection<TEntity>(tableName);
        }

        public async Task<IEnumerable<TEntity>> GetCollectionAsync(CancellationToken cancellationToken = default)
        {
            var dataList = await Collection.Find(FilterDefinition<TEntity>.Empty).ToListAsync(cancellationToken);
            return dataList;
        }

        public Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            var result = await Collection.ReplaceOneAsync(new ExpressionFilterDefinition<TEntity>(filter), entity, cancellationToken: cancellationToken);

            if (!result.IsAcknowledged)
                throw new Exception($"Could Not update the entity {entity.GetType().Name}");
        }

        public Task<List<TEntity>> GetWithFilterAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            return Collection.Find(filter).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetSingleWithFilterAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await (await Collection.FindAsync(new ExpressionFilterDefinition<TEntity>(filter), cancellationToken: cancellationToken)).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            var result = await Collection.DeleteOneAsync(new ExpressionFilterDefinition<TEntity>(filter), cancellationToken);

            if (!result.IsAcknowledged)
                throw new Exception($"Could Not Delete The Entity {typeof(TEntity).Name}");
        }
    }
}