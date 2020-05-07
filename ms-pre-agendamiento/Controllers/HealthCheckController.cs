using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ms_pre_agendamiento.Repository;

namespace ms_pre_agendamiento.Controllers
{
    [AllowAnonymous]
    [ApiController, Route("[controller]")]
    public class HealthCheckController  : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly string _env; 
        
        public HealthCheckController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            var secret = configuration.GetSection("AppSettings")["Secret"];
            _env = configuration.GetSection("AppSettings")["Env"];
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public IActionResult CoreServices()
        {
            try
            {
                _userRepository.HealthCheck();
                
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception in basic health check: {0}", ex.Message);
                return StatusCode(503);
            }

            return Ok(new string[] {"HealthCheck OK", "version 1.0 Preagendamiento", $"Environment <{_env}>"});
        }

        
    }
}