using JobsityChat.Models;
using System.Collections.Generic;

namespace JobsityChat.Services
{
    public interface IChatMessageService
    {
        ChatMessage Create(ChatMessage chatMessage);
        ChatMessage FindById(long id);
        List<ChatMessage> FindAll();
        List<ChatMessage> FindLast(int qty);
        ChatMessage Update(ChatMessage chatMessage);
        void Delete(long id);
    }
}
