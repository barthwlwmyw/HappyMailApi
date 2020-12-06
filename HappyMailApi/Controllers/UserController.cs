using HappyMailApi.Models;
using HappyMailApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyMailApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UsersRepository _usersRepository;

        public UserController(ILogger<UserController> logger, UsersRepository usersRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _usersRepository.Get().ToArray();
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public User Create([FromBody] User user)
        {
            return _usersRepository.Create(user);
        }
    }
}
