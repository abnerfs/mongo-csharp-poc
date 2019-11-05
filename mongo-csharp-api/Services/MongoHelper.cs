using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongo_csharp_api.Services
{
    public class MongoHelper
    {
        private IMongoDatabase db;

        public MongoHelper()
        {

        }

        public MongoHelper(string Database)
        {
            var Client = new MongoClient();
            db = Client.GetDatabase(Database);
        }

        public void InsertRecord<T>(string table, T Record)
        {
            try
            {
                var collection = db.GetCollection<T>(table);
                collection.InsertOne(Record);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<T> LoadRecords<T>(string table, FilterDefinition<T> filter)
        {
            try
            {
                var collection = db.GetCollection<T>(table);
                return collection.Find(filter).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<T> LoadRecords<T>(string table)
        {
            try
            {
                return LoadRecords<T>(table, new BsonDocument());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public T LoadRecordById<T>(string table, string id)
        {
            try
            {
                var collection = db.GetCollection<T>(table);
                var filter = Builders<T>.Filter.Eq("Id", id);

                return collection.Find(filter).First();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            try
            {
                var collection = db.GetCollection<T>(table);
                var result = collection.ReplaceOne(new BsonDocument("_id", id), record, new UpdateOptions { IsUpsert = true });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteRecord<T>(string table, Guid id)
        {
            try
            {
                var collection = db.GetCollection<T>(table);
                var Filter = Builders<T>.Filter.Eq("Id", id);
                collection.DeleteOne(Filter);
            }
            catch (Exception)
            {

                throw;
            }
        }





    }
}
