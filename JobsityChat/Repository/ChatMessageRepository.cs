using JobsityChat.Data;
using JobsityChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsityChat.Repository
{
    public class ChatMessageRepository : IChatMessageRepository

    {
        private readonly ApplicationDbContext _context;

        public ChatMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Metodo responsável por criar uma nova pessoa
        // nesse momento adicionamos o objeto ao contexto
        // e finalmente salvamos as mudanças no contexto
        // na base de dados
        public virtual ChatMessage Create(ChatMessage chatMessage)
        {
            try
            {
                _context.Add(chatMessage);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return chatMessage;
        }

        // Método responsável por retornar uma pessoa
        public ChatMessage FindById(long id)
        {
            return _context.ChatMessages.SingleOrDefault(p => p.Id.Equals(id));
        }

        // Método responsável por retornar todas as pessoas
        public List<ChatMessage> FindAll()
        {
            return _context.ChatMessages.ToList();
        }

        public List<ChatMessage> FindLast(int qty)
        {
            return _context.ChatMessages.OrderByDescending(x => x.Created).Take(qty).ToList();
        }

        // Método responsável por atualizar uma pessoa
        public ChatMessage Update(ChatMessage ChatMessage)
        {
            // Verificamos se a pessoa existe na base
            // Se não existir retornamos uma instancia vazia de pessoa
            if (!Exists(ChatMessage.Id)) return null;

            // Pega o estado atual do registro no banco
            // seta as alterações e salva
            var result = _context.ChatMessages.SingleOrDefault(b => b.Id == ChatMessage.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(ChatMessage);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        // Método responsável por deletar
        // uma pessoa a partir de um ID
        public void Delete(long id)
        {
            var result = _context.ChatMessages.SingleOrDefault(i => i.Id.Equals(id));
            try
            {
                if (result != null) _context.ChatMessages.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(long? id)
        {
            return _context.ChatMessages.Any(b => b.Id.Equals(id));
        }

    }
}
