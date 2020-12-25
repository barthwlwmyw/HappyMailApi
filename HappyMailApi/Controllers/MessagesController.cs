using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HappyMailApi.Models;
using HappyMailApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace HappyMailApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public MessagesController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_messageService.Get(User.Identity.Name));
        }

        [HttpPost("send")]
        public IActionResult Send(MessageToSend message)
        {
            var res = _messageService.Send(new Message
            {
                SenderUsername = User.Identity.Name,
                RecipientUsername = message.RecipientUsername,
                Content = message.Content,
                IsToxic = false
            });

            return Ok(res);
        }
    }

    public class MessageToSend
    {
        public string RecipientUsername { get; set; }
        public string Content { get; set; }
    }

}
