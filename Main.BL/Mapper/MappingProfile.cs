using AutoMapper;
using Main.BL.DTOs;
using Main.Domain.Entities;

namespace Main.BL.Mapper
{
    public class MainMappingProfile : Profile
    {
        public MainMappingProfile()
        {
            // Map for creating an employee
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Projects.Select(p => new Project
                {
                    Name = p.Name,
                    Description = p.Description,
                    StartDate = p.StartDate, 
                    EndDate = p.EndDate,
                    
                })));

            // Map from Employee to EmployeeDto (including projects)
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Projects)).ReverseMap();

            // Map for projects between Employee and EmployeeDto
            CreateMap<Project, ProjectDto>().ReverseMap();
        }
    }
}
