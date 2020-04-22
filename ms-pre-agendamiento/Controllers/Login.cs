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
using ms_pre_agendamiento.DTO;
using ms_pre_agendamiento.Repository;

namespace ms_pre_agendamiento.Controllers
{
    [AllowAnonymous]
    [ApiController, Route("[controller]")]
    public class Login : ControllerBase
    {
        private readonly SymmetricSecurityKey _mySecurityKey;

        private readonly IUserRepository _userRepository;
        
        private readonly string _issuer;


        public Login(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            var secret = configuration.GetSection("AppSettings")["Secret"];
            _issuer = configuration.GetSection("AppSettings")["Issuer"];
            _mySecurityKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }

        [HttpPost]
        public IActionResult Post([FromBody] User userDto)
        {
            Models.User user = _userRepository.getUser(userDto);
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
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(_mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, null, new AuthenticationProperties());
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return new ObjectResult(user);
        }
    }
}