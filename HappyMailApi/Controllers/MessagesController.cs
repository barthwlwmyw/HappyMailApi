using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HappyMailApi.Models;
using HappyMailApi.Services;
using Microsoft.AspNetCore.Authorization;
using HappyMailApi.SentimentAnalysis;
using Microsoft.Extensions.Configuration;

namespace HappyMailApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public MessagesController(IMessageService messageService, IUserService userService, IConfiguration configuration)
        {
            _messageService = messageService;
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_messageService.Get(User.Identity.Name));
        }

        [HttpPost("send")]
        public IActionResult Send(MessageToSend message)
        {
            var isToxic = false;

            try
            {
                var input = new ModelInput
                {
                    SentimentText = message.Content
                };

                ModelOutput result = ModelConsumer.Predict(input);

                if(result.Prediction == "1")
                {
                    isToxic = true;
                }

            }
            catch (Exception){}

            var res = _messageService.Send(new Message
            {
                SenderUsername = User.Identity.Name,
                RecipientUsername = message.RecipientUsername,
                CreatedAt = DateTime.UtcNow,
                Content = message.Content,
                IsToxic = isToxic
            });

            return Ok(res);
        }
    }

    public class MessageToSend
    {
        [Required]
        public string RecipientUsername { get; set; }
        [Required]
        public string Content { get; set; }
    }

}
