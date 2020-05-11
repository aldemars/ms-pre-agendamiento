using System;
using System.Collections.Generic;
using AutoMapper;
using ms_pre_agendamiento.Dto;
using ms_pre_agendamiento.Models;

namespace ms_pre_agendamiento.Models.Mappers
{
    public class MapperProfile : Profile    
    {
        public MapperProfile()
        {
            CreateMap<TimeSpan, AppointmentTime>();
            CreateMap<Appointment, AppointmentResponse>()
                .ForMember(dto => dto.Time, opt 
                    => opt.MapFrom(src => src.Hour));
            
            CreateMap<User, UserResponse>()
                .ForMember(dto => dto.Appointments, opt 
                    => opt.MapFrom(src => src.Appointments));
            CreateMap<User, LoginResponse>();
        }
    }
}


