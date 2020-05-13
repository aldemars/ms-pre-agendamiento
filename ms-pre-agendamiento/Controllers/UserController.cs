using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ms_pre_agendamiento.Dto;
using ms_pre_agendamiento.Repository;

namespace ms_pre_agendamiento.Controllers
{
    [ApiController, Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [Route("{id:int}")]
        [HttpGet(Name = "GetUser")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Route("{id:int}/appointment")]
        [HttpGet(Name = "GetUserApointments")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserAppointments(int id)
        {
            var user = _userRepository.GetUserAppointmentsById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserResponse>(user));
        }
    }
}