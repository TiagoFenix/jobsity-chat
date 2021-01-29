using JobsityChat.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsityChat.Controllers
{
    public class StockBotController : BaseController
    {
        private readonly StockBotService _stockBotService;

        public StockBotController(StockBotService stockBotService)
        {
            this._stockBotService = stockBotService;
        }

        [HttpGet("stockCode")]
        public IActionResult GetStockCode([FromRoute] string stockCode)
        {
            return Ok(_stockBotService.GetStockValue(stockCode));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return NotFound();
        }
    }
}
