using JobsityChat.Models;
using JobsityChat.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace JobsityChat.Services
{
    public class StockBotService : IStockBotService
    {
        private readonly IOptions<AppServiceSettings> _appServiceSettings;
        private readonly IChatMessageRepository _repository;

        public StockBotService(IOptions<AppServiceSettings> appServiceSettings, IChatMessageRepository repository)
        {
            this._repository = repository;
            this._appServiceSettings = appServiceSettings;
        }

        public ChatMessage CreateStockMessage(string stockCode)
        {
            try
            {
                if(string.IsNullOrEmpty(stockCode))
                    throw new Exception("stock code cannot be empty");

                ChatMessage chatMessage = new ChatMessage()
                {
                    CreatedBy = "BOT",
                    Created = DateTime.Now,
                    Message = GetStockValue(stockCode)
                };
                return _repository.Create(chatMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetStockValue(string stockCode)
        {
            var values = readCSV((byte[])GetStock(stockCode).Result);
            return $"{values[0]} quote is ${values[6]} per share.";
        }

        private async Task<byte[]> GetStock(string stockCode)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(String.Format(_appServiceSettings.Value.StooqUri,stockCode) ),
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

                    if (!isHeader)
                        return values;

                    isHeader = false;
                }

                return null;
            }
        }
    }
}
