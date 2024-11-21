using App.Shared.Models;
using Main.BL.DTOs;

namespace Main.BL.IServices
{
    public interface IEmployeeService
    {
        Task<ApiResponse<int>> AddEmployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task<ApiResponse<EmployeeDto>> GetEmployeeByIdAsync(int id);
        Task<ApiResponse<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync();
        Task<ApiResponse<int>> UpdateEmployeeAsync(EmployeeDto employeeDto);
        Task<ApiResponse<bool>> SoftDeleteEmployeeWithAuditAsync(int employeeId);
        Task<ApiResponse<bool>> IsEmailUniqueAsync(string email,int? employeeId);

    }
}
