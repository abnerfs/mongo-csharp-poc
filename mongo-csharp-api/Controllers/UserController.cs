using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mongo_csharp_api.Models;
using mongo_csharp_api.Services;
using mongo_csharp_api.WebObjects;
using MongoDB.Driver;

namespace mongo_csharp_api.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        [Route("/register")]
        public UserModel Register([FromBody] UserCreate User)
        {
            try
            {
                var db = new MongoHelper("MongoPoc");

                if (string.IsNullOrEmpty(User.Email) || User.Email.Length < 5)
                    throw new ArgumentException("Invalid email");

                if(string.IsNullOrEmpty(User.Password))
                    throw new ArgumentException("Invalid password");

                if (UserModel.GetUserByEmail<UserModel>(User.Email, db) != null)
                    throw new ArgumentException("Email already taken");

                //HASH PASSWORD 

                db.InsertRecord("Users", User);
                var UserResponse =  UserModel.GetUserByEmail<UserModel>(User.Email, db);
                
                return UserResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("login")]
        public UserModel Login(string Email, string Password)
        {
            var db = new MongoHelper("MongoPoc");

            var User = UserModel.GetUserByEmail<UserAuth>(Email, db);

            //HASH
            if(User.Password == Password)
                return UserModel.GetUserByEmail<UserModel>(Email, db);


            return null;
        }
    }
}