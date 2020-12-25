using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyMailApi.Models;
using MongoDB.Driver;

namespace HappyMailApi.Repositories
{
    public class MessagesRepository
    {
        private readonly IMongoCollection<Message> _messages;

        public MessagesRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _messages = database.GetCollection<Message>(databaseSettings.MessagesCollectionName);
        }
        public List<Message> Get() =>
            _messages.Find(user => true).ToList();

        public List<Message> GetBySender(string username) =>
            _messages.Find(message => message.SenderUsername == username).ToList();

        public List<Message> GetByRecipient(string username) =>
            _messages.Find(message => message.RecipientUsername == username).ToList();

        public Message Create(Message message)
        {
            try
            {
                _messages.InsertOne(message);
                return message;
            }
            catch
            {
                return null;
            }
        }
    }
}
