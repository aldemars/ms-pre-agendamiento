using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ms_pre_agendamiento.Dto;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Repository;

namespace ms_pre_agendamiento.Controllers
{
    [AllowAnonymous]
    [ApiController, Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly SymmetricSecurityKey _mySecurityKey;

        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            var secret = configuration.GetSection("AppSettings")["Secret"];
            _mySecurityKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] LoginRequest loginRequestDto)
        {
            User user = _userRepository.GetUser(loginRequestDto);
            if (user == null) return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(_mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userResponse = user.MapToUserResponse(tokenHandler.WriteToken(token));
            return Ok(userResponse);
        }
    }
}