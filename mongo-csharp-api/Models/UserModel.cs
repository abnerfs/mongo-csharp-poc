using mongo_csharp_api.Services;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongo_csharp_api.Models
{
    [BsonIgnoreExtraElements]
    public class UserAuth
    {
        [BsonElement("_id")]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class UserCreate : UserModel
    {
        public string Password { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class UserModel
    {
        [BsonElement("_id")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDay { get; set; } = null;

        public static T GetUserByEmail<T>(string Email, MongoHelper db)
        {
            try
            {
                return db.LoadRecords("Users", Builders<T>.Filter.Eq("Email", Email)).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
