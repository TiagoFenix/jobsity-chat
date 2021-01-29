using JobsityChat.Models;
using JobsityChat.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace JobsityChat.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class ChatMessageController : BaseController
    {
        private IChatMessageService _chatMessageService;
        private readonly IStockBotService _stockBotService;

        public ChatMessageController(IChatMessageService chatMessageService, IStockBotService stockBotService)
        {
            this._chatMessageService = chatMessageService;
            this._stockBotService = stockBotService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_chatMessageService.FindAll());
        }

        [HttpGet("last")]
        public IActionResult GetLast()
        {
            return Ok(_chatMessageService.FindLast(50).OrderBy(x => x.Created));
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var chatMessage = _chatMessageService.FindById(id);
            if (chatMessage == null) return NotFound();
            return Ok(chatMessage);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ChatMessage chatMessage)
        {
            if (chatMessage == null) return BadRequest();


            if (chatMessage.Message.StartsWith("/stock="))
            {
                return Ok(_stockBotService.CreateStockMessage(chatMessage.Message.Replace("/stock=", "").Trim()));
            }

            //TODO: Not sure why this is getting null value. UI is sending this value meanwhile;
            chatMessage.CreatedBy ??= (this.User.Identity.Name ?? "Anonymous");
            chatMessage.Created = DateTime.Now;
            return new ObjectResult(_chatMessageService.Create(chatMessage));
        }

        [HttpPut]
        public IActionResult Put([FromBody] ChatMessage chatMessage)
        {
            if (chatMessage == null) return BadRequest();
            var updatedChatMessage = _chatMessageService.Update(chatMessage);
            if (updatedChatMessage == null) return BadRequest();
            return new ObjectResult(updatedChatMessage);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _chatMessageService.Delete(id);
            return NoContent();
        }
    }
}
