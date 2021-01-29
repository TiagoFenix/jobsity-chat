using JobsityChat.Models;
using JobsityChat.Repository;
using System.Collections.Generic;

namespace JobsityChat.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IChatMessageRepository _repository;
        public ChatMessageService(IChatMessageRepository repository)
        {
            _repository = repository;
        }

        public ChatMessage Create(ChatMessage chatMessage)
        {
            return _repository.Create(chatMessage);
        }

        public ChatMessage FindById(long id)
        {
            return _repository.FindById(id);
        }

        public List<ChatMessage> FindAll()
        {
            return _repository.FindAll();
        }

        public List<ChatMessage> FindLast(int qty)
        {
            return _repository.FindLast(qty);
        }

        public ChatMessage Update(ChatMessage ChatMessage)
        {
            return _repository.Update(ChatMessage);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }
    }
}
