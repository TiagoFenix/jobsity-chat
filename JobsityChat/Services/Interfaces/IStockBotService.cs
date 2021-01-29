using JobsityChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsityChat.Services
{
    public interface IStockBotService
    {
        String GetStockValue(string stockCode);
        ChatMessage CreateStockMessage(string stockCode);
    }
}
