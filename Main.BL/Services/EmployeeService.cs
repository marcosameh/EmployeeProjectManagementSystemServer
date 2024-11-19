using App.Shared.Models;
using AutoMapper;
using Log.BL.IServices;
using Main.BL.DTOs;
using Main.BL.IServices;
using Main.DAL.IRepository;
using Main.Domain.Entities;
using Newtonsoft.Json;
using System.Transactions;

namespace Main.BL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAuditLogService _auditAuditService;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IAuditLogService auditAuditService, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _auditAuditService = auditAuditService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<int>> AddEmployeeWithAuditAsync(CreateEmployeeDto createEmployeeDto)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (!await _employeeRepository.IsEmailUniqueAsync(createEmployeeDto.Email))
                    {
                        return new ApiResponse<int>(false, new List<string> { "employee already exisit" }, default(int));
                    }

                    var employee = _mapper.Map<Employee>(createEmployeeDto);

                    // Add employee
                    var addedEmployee = await _employeeRepository.AddEmployeeAsync(employee);

                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };
                    //log action

                   var newData = JsonConvert.SerializeObject(addedEmployee, settings);
                    
                    await _auditAuditService.LogAddition(addedEmployee.Id, null, newData);


                    scope.Complete();


                    return new ApiResponse<int>(true, new List<string>(), addedEmployee.Id);
                }
                catch (Exception ex)
                {

                    return new ApiResponse<int>(false, new List<string> { ex.InnerException?.Message }, default(int));
                }
            }
        }
        public async Task<ApiResponse<EmployeeDto>> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

                if (employee == null)
                {
                    return new ApiResponse<EmployeeDto>(false, new List<string> { "Employee not found" }, null);
                }

                var employeeDto = _mapper.Map<EmployeeDto>(employee);

                return new ApiResponse<EmployeeDto>(true, new List<string>(), employeeDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<EmployeeDto>(false, new List<string> { ex.Message }, null);
            }
        }

        public async Task<ApiResponse<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployeesAsync();

                var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

                return new ApiResponse<IEnumerable<EmployeeDto>>(true, new List<string>(), employeeDtos);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<EmployeeDto>>(false, new List<string> { ex.Message }, null);
            }
        }

        public async Task<ApiResponse<int>> UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(employeeDto.Id);
                    if (existingEmployee == null)
                    {
                        return new ApiResponse<int>(false, new List<string> { "Employee not found" }, default(int));
                    }
                    if (existingEmployee.Email != employeeDto.Email)
                    {
                        if (!await _employeeRepository.IsEmailUniqueAsync(employeeDto.Email))
                        {
                            return new ApiResponse<int>(false, new List<string> { "Email already exisit" }, default(int));

                        }

                    }
                    var oldData = JsonConvert.SerializeObject(existingEmployee, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                    var newEmployeeData = _mapper.Map<Employee>(employeeDto);

                    var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(newEmployeeData);

                    var newData = JsonConvert.SerializeObject(updatedEmployee, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                   

                    await _auditAuditService.LogUpdate(updatedEmployee.Id, oldData, newData);

                    scope.Complete();

                    return new ApiResponse<int>(true, new List<string>(), updatedEmployee.Id);
                }
                catch (Exception ex)
                {
                    return new ApiResponse<int>(false, new List<string> { ex.Message }, default(int));
                }
            }
        }

        public async Task<ApiResponse<bool>> SoftDeleteEmployeeWithAuditAsync(int employeeId)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
                    if (employee == null)
                    {
                        return new ApiResponse<bool>(false, new List<string> { "Employee not found" }, false);
                    }


                    var oldData = JsonConvert.SerializeObject(employee, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });


                    var success = await _employeeRepository.SoftDeleteEmployeeAsync(employeeId);

                    if (!success)
                    {
                        return new ApiResponse<bool>(false, new List<string> { "Failed to delete employee" }, false);
                    }

                  

                    await _auditAuditService.LogDeletion(employee.Id, oldData, null);

                    scope.Complete();

                    return new ApiResponse<bool>(true, new List<string>(), true);
                }
                catch (Exception ex)
                {
                    return new ApiResponse<bool>(false, new List<string> { ex.Message }, false);
                }
            }
        }



    }
}
