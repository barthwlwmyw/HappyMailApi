using System.Collections.Generic;
using System.Threading.Tasks;
using HappyMailApi.Models;
using HappyMailApi.Repositories;

namespace HappyMailApi.Services
{
    public interface IMessageService
    {
        Message Send(Message message);
        Task<List<Message>> Get(string username);
        void Delete(string messageId);
    }

    public class MessageService: IMessageService
    {
        private readonly MessagesRepository _messagesRepository;
        private readonly ISentimentAnalysisService _sentimentAnalysisService;

        public MessageService(MessagesRepository messagesRepository, ISentimentAnalysisService sentimentAnalysisService)
        {
            _messagesRepository = messagesRepository;
            _sentimentAnalysisService = sentimentAnalysisService;
        }

        public Message Send(Message message)
        {
            message.IsToxic = _sentimentAnalysisService.IsToxic(message.Content);
            return _messagesRepository.Create(message);
        }

        public async Task<List<Message>> Get(string username) => await _messagesRepository.GetByRecipient(username);

        public void Delete(string messageId) => _messagesRepository.Delete(messageId);
    }
}
