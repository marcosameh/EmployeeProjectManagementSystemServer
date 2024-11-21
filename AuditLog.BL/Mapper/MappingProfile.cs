using AutoMapper;
using Log.BL.DTOs;
using Log.Domain.Entities;

namespace Log.BL.Mapper
{
    public class LogMappingProfile : Profile
    {
        public LogMappingProfile()
        {
           
            CreateMap<AuditLogWithEmployee, AuditLogWithEmployeeDto>().ReverseMap();
        }
    }
}
