using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ms_pre_agendamiento.DTO;
using ms_pre_agendamiento.Repository;

namespace ms_pre_agendamiento.Controllers
{
    [ApiController, Route("[controller]")]
    public class Login : ControllerBase
    {
        private readonly SymmetricSecurityKey _mySecurityKey =
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaa"));

        private readonly IUserRepository _userRepository;

        public Login(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            Console.WriteLine(user);
            if (!_userRepository.isValid(user)) return Unauthorized();
            const string myIssuer = "http://pre-agendamiento-front.azurewebsites.net";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "fakeUserId"),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = myIssuer,
                SigningCredentials = new SigningCredentials(_mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new ObjectResult(new LoginResponse(tokenHandler.WriteToken(token))) ;
        }
    }
}