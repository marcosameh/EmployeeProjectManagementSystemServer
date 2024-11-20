using AutoMapper;
using Log.BL.DTOs;
using Log.Domain.Entities;

namespace Log.BL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<AuditLogWithEmployee, AuditLogWithEmployeeDto>().ReverseMap();
        }
    }
}
