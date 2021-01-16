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

        [AllowAnonymous]
        [HttpPost("mltest")]
        public IActionResult Mltest()
        {
            try
            {
                var input = new ModelInput();

                var userInput = "I'm a bloody fuc*ing bastard toxic comment";

                input.SentimentText = userInput;


                ModelOutput result = ModelConsumer.Predict(input);


                return Ok($"Prediction result: ${result.Prediction}");

            }
            catch(Exception e)
            {
                return Ok(e.Message);
            }
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
