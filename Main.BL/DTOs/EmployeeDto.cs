using Main.BL.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace Main.BL.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "At least one project is required.")]
        [MinLength(1, ErrorMessage = "At least one project is required.")]
        public List<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
    }
    public class ProjectDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(100, ErrorMessage = "Project name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Project description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DateGreaterThan(nameof(StartDate), ErrorMessage = "End date must be greater than start date.")]
        public DateTime EndDate { get; set; }
    }
}
