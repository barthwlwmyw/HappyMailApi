using HappyMailApi.Jwt;
using HappyMailApi.Models;
using HappyMailApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace HappyMailApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtAuthManager _jwtAuthManager;

        public AuthController(IUserService userService, IJwtAuthManager jwtAuthManager)
        {
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] User user)
        {
           if (!ModelState.IsValid) return BadRequest();
           return Ok(_userService.Create(user));
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] User user)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (!_userService.IsValidUserCredentials(user.Username, user.Password)) return Unauthorized();

            var jwtResult = _jwtAuthManager.GenerateTokens(user.Username, GetUserClaims(user), DateTime.Now);

            return Ok(new LoginResult
            {
                UserName = user.Username,
                Role = _userService.GetUserRole(user.Username),
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            });
        }

        [HttpPost("logout")]
        [Authorize]
        public ActionResult Logout()
        {
            var userName = User.Identity.Name;
            _jwtAuthManager.RemoveRefreshTokenByUserName(userName); 
            return Ok();
        }

        private Claim[] GetUserClaims(User user)
        {
            var role = _userService.GetUserRole(user.Username);
            return new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, role)
            };
        }

        public class SignInResult
        {
            [JsonPropertyName("username")]
            public string UserName { get; set; }

            [JsonPropertyName("role")]
            public string Role { get; set; }

            [JsonPropertyName("originalUserName")]
            public string OriginalUserName { get; set; }

            [JsonPropertyName("accessToken")]
            public string AccessToken { get; set; }

            [JsonPropertyName("refreshToken")]
            public string RefreshToken { get; set; }
        }
    }
}
