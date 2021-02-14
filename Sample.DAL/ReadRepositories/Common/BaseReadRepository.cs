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
        public IMongoClient MongoClient { get; }
        public IMongoDatabase Db { get; }

        public IMongoCollection<TEntity> Collection { get; }

        public BaseReadRepository(IMongoDatabase db)
        {
            Db = db;
            MongoClient = db.Client;

            var tableName = typeof(TEntity).Name;
            Collection = Db.GetCollection<TEntity>(tableName);
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Collection.Find(FilterDefinition<TEntity>.Empty).ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> GetWithFilterAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            return Collection.Find(filter).ToListAsync(cancellationToken);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            return Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            var result = await Collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);

            if (!result.IsAcknowledged)
                throw new Exception($"Could not update the entity {entity.GetType().Name}");
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            var result = await Collection.DeleteOneAsync(filter, cancellationToken);

            if (!result.IsAcknowledged)
                throw new Exception($"Could not delete the entity {typeof(TEntity).Name}");
        }
    }
}