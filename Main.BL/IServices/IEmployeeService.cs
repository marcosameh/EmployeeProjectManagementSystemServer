using App.Shared.Models;
using Main.BL.DTOs;

namespace Main.BL.IServices
{
    public interface IEmployeeService
    {
        Task<ApiResponse<int>> AddEmployeeWithAuditAsync(CreateEmployeeDto createEmployeeDto);
        Task<ApiResponse<EmployeeDto>> GetEmployeeByIdAsync(int id);
        Task<ApiResponse<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync();
        Task<ApiResponse<int>> UpdateEmployeeAsync(EmployeeDto employeeDto);
        Task<ApiResponse<bool>> SoftDeleteEmployeeWithAuditAsync(int employeeId);
    }
}
