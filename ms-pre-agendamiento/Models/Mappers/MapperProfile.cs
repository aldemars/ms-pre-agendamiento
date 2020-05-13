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
                    => opt.MapFrom(src => src.Hour))
                .ForMember("Date", opt 
                    => opt.ConvertUsing(new DateTimeToStringConverter()));
            
            CreateMap<User, UserResponse>()
                .ForMember(dto => dto.Appointments, opt 
                    => opt.MapFrom(src => src.Appointments));
            CreateMap<User, LoginResponse>();
        }
        
        public class DateTimeToStringConverter : IValueConverter<DateTime, string>
        {
            public string Convert(DateTime datetime, ResolutionContext context)
            {
                return datetime.ToString("dd/M/yyyy");
            }
        }
    }
}


