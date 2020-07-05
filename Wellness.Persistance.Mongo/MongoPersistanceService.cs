using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Persistance.Mongo
{
    public class MongoPersistanceService : IPersistanceService
    {
        private readonly MongoClient _mongoClient;
        private readonly string _database;
        private readonly string _collectionName;
        public MongoPersistanceService(string connectionString, string database, string collectionName)
        {
            _mongoClient = new MongoClient(connectionString);
            _database = database;
            _collectionName = collectionName;
        }
        public async Task Create<T>(PersistenceWrapper<T> wrapped)
            where T : IIdentifiable
        {
            if(!BsonClassMap.IsClassMapRegistered(typeof(T)))
            {
                BsonClassMap.RegisterClassMap<T>();
            }
            
            var database = _mongoClient.GetDatabase(_database);                        
            var collection = database.GetCollection<PersistenceWrapper<T>>(_collectionName);
            await collection.InsertOneAsync(wrapped);
        }

        private BsonDocument CreateDocument<T>(PersistenceWrapper<T> wrapped)
            where T : IIdentifiable
        {
            var document = new BsonDocument();
            document.Add("name", "MongoDB");
            document.Add("type", "Database");
            document.Add("count", 1);
            document.Add("CommonInfo", BsonValue.Create(wrapped.Common));
            document.Add("Model", BsonValue.Create(wrapped.Model));

            return document;
        }


    }
}
