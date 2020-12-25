using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyMailApi.Models;
using HappyMailApi.Repositories;

namespace HappyMailApi.Services
{
    public interface IMessageService
    {
        Message Send(Message message);
        List<Message> Get(string username);
    }

    public class MessageService: IMessageService
    {
        private readonly MessagesRepository _messagesRepository;

        public MessageService(MessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }

        public Message Send(Message message)
        {
            return _messagesRepository.Create(message);
        }

        public List<Message> Get(string username)
        {
            return _messagesRepository.GetByRecipient(username);
        }
    }
}
