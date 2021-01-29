using JobsityChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsityChat.Repository

{
    public interface IChatMessageRepository
    {
        ChatMessage Create(ChatMessage chatMessage);
        ChatMessage FindById(long id);
        List<ChatMessage> FindAll();
        List<ChatMessage> FindLast(int qty);
        ChatMessage Update(ChatMessage chatMessage);
        void Delete(long id);

        bool Exists(long? id);
    }
}
