using System.Collections.Generic;
using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using ms_pre_agendamiento.Configuration;
using ms_pre_agendamiento.Dto;
using ms_pre_agendamiento.Models;
using ms_pre_agendamiento.Repository;

namespace ms_pre_agendamiento.Controllers
{
    [ApiController, Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        
        public UserController(IUserRepository userRepository, IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _userRepository = userRepository;
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        [Route("{id}")]
        [HttpGet("{id}")]
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

        [Route("{userId}/appointment")]
        [HttpGet("{userId}/appointment")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [FeatureGate(FeatureManagement.IsUserAppointmentsFeatureEnabled)]
        public IActionResult GetUserAppointments(int userId)
        {
            var user = _userRepository.GetUserAppointmentsById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserResponse>(user).Appointments);
        }
        
        [Route("{userId}/appointment/center/{centerId}")]
        [HttpGet("{userId}/appointment/center/{centerId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserAndCenterAppointments(int userId, int centerId)
        {
            var user = _userRepository.GetById(userId);

            if (user == null)
            {
                return NotFound();
            }
            List<Appointment> appointments = _appointmentRepository.GetUserAndCenterAppointments(userId,centerId);

            return Ok(_mapper.Map<List<AppointmentResponse>>(appointments));
        }
    }
}