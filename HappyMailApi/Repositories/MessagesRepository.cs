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

        public async Task<List<Message>> GetByRecipient(string username)
        {
            var messages = await _messages.FindAsync(message => message.RecipientUsername == username);

            return messages.ToList();
        }
            


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

        public void Delete(string messageId)
        {
            _messages.DeleteOne(msg => msg.Id == messageId);
        }
    }
}
