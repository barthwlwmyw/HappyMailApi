using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using HappyMailApi.Models;
using HappyMailApi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace HappyMailApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _messageService.Get(User.Identity.Name));
        }

        [HttpPost("send")]
        public IActionResult Send(MessageToSend message)
        {
            var res = _messageService.Send(new Message
            {
                SenderUsername = User.Identity.Name,
                RecipientUsername = message.RecipientUsername,
                CreatedAt = DateTime.UtcNow,
                Content = message.Content,
                IsToxic = false
            });

            return Ok(res);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(MessageToDelete message)
        {
            _messageService.Delete(message.MessageId);

            return Ok(new { messageId = message.MessageId });
        }
    }

    public class MessageToSend
    {
        [Required]
        public string RecipientUsername { get; set; }

        [Required]
        public string Content { get; set; }
    }

    public class MessageToDelete
    {
        [Required]
        public string MessageId { get; set; }
    }
}
