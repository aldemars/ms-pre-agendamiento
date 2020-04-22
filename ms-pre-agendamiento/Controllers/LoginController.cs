using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ms_pre_agendamiento.Dto;
using ms_pre_agendamiento.Repository;

namespace ms_pre_agendamiento.Controllers
{
    [AllowAnonymous]
    [ApiController, Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly SymmetricSecurityKey _mySecurityKey;

        private readonly IUserRepository _userRepository;
        
        private readonly string _issuer;


        public LoginController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            var secret = configuration.GetSection("AppSettings")["Secret"];
            _issuer = configuration.GetSection("AppSettings")["Issuer"];
            _mySecurityKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest loginRequestDto)
        {
            Models.User user = _userRepository.GetUser(loginRequestDto);
            if (user == null) return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Name),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(_mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, null, new AuthenticationProperties());
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userResponse = user.MapToUserResponse(tokenHandler.WriteToken(token));
            return new ObjectResult(userResponse);
        }
    }
}