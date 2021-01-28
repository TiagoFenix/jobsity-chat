using JobsityChat.Models;
using JobsityChat.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
            if (chatMessage.Message.StartsWith("/stock="))
            {

                var values = readCSV((byte[])GetStock(chatMessage.Message.Replace("/stock=", "")).Result);

                if(values != null)
                    chatMessage.Message = $"{values[0]} quote is ${values[6]} per share.";
            }

            return _repository.Create(chatMessage);
        }

        private async Task<byte[]> GetStock(string value)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://stooq.com/q/l/?s={value}&f=sd2t2ohlcv&h&e=csv"),
                Headers =
                    {
                        
                    },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync();
            }
        }

        private string[] readCSV(byte[] file)
        {
            using (var reader = new StreamReader(new MemoryStream(file)))
            {
                bool isHeader = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    
                    if(!isHeader)
                        return values;

                    isHeader = false;
                }

                return null;
            }
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
