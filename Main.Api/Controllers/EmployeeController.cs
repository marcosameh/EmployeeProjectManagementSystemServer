using App.Shared.Models;
using Main.BL.DTOs;
using Main.BL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Log.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] CreateEmployeeDto employeeDto)
        {
            var response = await _employeeService.AddEmployeeAsync(employeeDto);

            return Ok(response);

        }
        [HttpPut]
        public async Task<ActionResult<ApiResponse<int>>> Update([FromBody] EmployeeDto employeeDto)
        {
            var response = await _employeeService.UpdateEmployeeAsync(employeeDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> SoftDelete(int id)
        {
            var response = await _employeeService.SoftDeleteEmployeeWithAuditAsync(id);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> GetById(int id)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<EmployeeDto>>>> GetAll()
        {
            var response = await _employeeService.GetAllEmployeesAsync();
            return Ok(response);
        }
        [HttpGet("IsEmailUnique")]
        public async Task<ActionResult<ApiResponse<IEnumerable<EmployeeDto>>>> IsEmailUnique(string email, int? employeeId)
        {
            var response = await _employeeService.IsEmailUniqueAsync(email, employeeId);
            return Ok(response);
        }
       
    }
}
