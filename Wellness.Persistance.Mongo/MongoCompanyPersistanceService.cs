using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Persistance.Mongo
{
    public class MongoCompanyPersistanceService<T> : IPersistanceService<T> where T : IIdentifiable
    {
        private readonly MongoClient _mongoClient;
        private readonly string _database;
        private readonly string _collectionName;

        private readonly DatabaseSettings _databaseSettings;
        public MongoCompanyPersistanceService(IOptions<DatabaseSettings> databaseSettings)
        {
            _databaseSettings = databaseSettings.Value;
            _mongoClient = new MongoClient(_databaseSettings.ConnectionString);
            _database = _databaseSettings.DatabaseName;
            _collectionName = typeof(T).Name;
            Initialize();
        }

        private void Initialize()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(PersistenceWrapper<T>)))
            {
                BsonClassMap.RegisterClassMap<PersistenceWrapper<T>>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(c => c.Id);                    
                });
            }
        }

        public ICompanyModelQueryable<PersistenceWrapper<T>> Query(Guid companyId)
        {
            var database = _mongoClient.GetDatabase(_database);
            var collection = database.GetCollection<PersistenceWrapper<T>>(_collectionName);
            return new CompanyModelQueryable<PersistenceWrapper<T>>(collection.AsQueryable(), companyId);

        }

        public async Task Create(PersistenceWrapper<T> wrapped, CancellationToken cancellationToken)
        {
            var database = _mongoClient.GetDatabase(_database);
            var collection = database.GetCollection<PersistenceWrapper<T>>(_collectionName);
            var options = new InsertOneOptions();
            await collection.InsertOneAsync(wrapped, options, cancellationToken);
        }

        public async Task<IEnumerable<PersistenceWrapper<T>>> GetAll(Guid companyId, CancellationToken cancellationToken)
        {
            var database = _mongoClient.GetDatabase(_database);
            var collection = database.GetCollection<PersistenceWrapper<T>>(_collectionName);
            return await collection.AsQueryable().Where(i => i.Common.CompanyId == companyId).ToListAsync(cancellationToken);
        }

        public async Task Update(PersistenceWrapper<T> wrapped, CancellationToken cancellationToken)
        {
            var database = _mongoClient.GetDatabase(_database);
            var collection = database.GetCollection<PersistenceWrapper<T>>(_collectionName);

            var updateDefinitionBuilder = new UpdateDefinitionBuilder<PersistenceWrapper<T>>();
            var updateDefinition = updateDefinitionBuilder
                .Set(i => i.Model, wrapped.Model)
                .Set(i => i.Common, wrapped.Common);

            var filter = new FilterDefinitionBuilder<PersistenceWrapper<T>>();
            await collection.UpdateOneAsync(filter.Eq(i => i.Model.Id, wrapped.Model.Id), updateDefinition, cancellationToken: cancellationToken);
        }

        public async Task<PersistenceWrapper<T>> Get(Guid id, Guid companyId, CancellationToken cancellationToken)
        {
            var database = _mongoClient.GetDatabase(_database);
            var collection = database.GetCollection<PersistenceWrapper<T>>(_collectionName);
            var filter = new FilterDefinitionBuilder<PersistenceWrapper<T>>();
            return await collection.AsQueryable().Where(i => i.Common.CompanyId == companyId).Where(i => i.Model.Id == id).SingleAsync(cancellationToken);
        }

        public async Task<IEnumerable<PersistenceWrapper<T>>> Get(Guid companyId, CancellationToken cancellationToken)
        {
            var database = _mongoClient.GetDatabase(_database);
            var collection = database.GetCollection<PersistenceWrapper<T>>(_collectionName);
            return await collection.AsQueryable().Where(i => i.Common.CompanyId == companyId).ToListAsync(cancellationToken);
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var database = _mongoClient.GetDatabase(_database);
            var collection = database.GetCollection<PersistenceWrapper<T>>(_collectionName);
            var filter = new FilterDefinitionBuilder<PersistenceWrapper<T>>();
            await collection.DeleteOneAsync(filter.Eq(i => i.Model.Id, id), cancellationToken);
        }
    }
}
