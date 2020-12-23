using HappyMailApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace HappyMailApi.Repositories
{
    public class UsersRepository
    {
        private readonly IMongoCollection<User> _users;

        public UsersRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _users = database.GetCollection<User>(databaseSettings.UsersCollectionName);
        }

        public List<User> Get() => 
            _users.Find(user => true).ToList();

        public User Get(string username) => 
            _users.Find(user => user.Username == username).FirstOrDefault();

        public bool Contains(string username) =>
            Get(username) != null;

        public User Create(User user)
        {
            try
            {
                _users.InsertOne(user);
                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}
