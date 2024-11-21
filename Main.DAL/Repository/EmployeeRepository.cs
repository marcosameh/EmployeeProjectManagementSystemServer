using Main.DAL.Context;
using Main.DAL.IRepository;
using Main.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Main.DAL.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MainDbContext _context;

        public EmployeeRepository(MainDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                                 .Include(e => e.Projects)
                                 .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees
                                 .Include(e => e.Projects)
                                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            employee.IsDeleted = false;
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee newEmployeedData)
        {
            var existingEmployee = await _context.Employees
                                                 .Include(e => e.Projects)
                                                 .FirstOrDefaultAsync(e => e.Id == newEmployeedData.Id);

            if (existingEmployee == null)
                throw new ArgumentException("Employee not found");


            existingEmployee.Name = newEmployeedData.Name;
            existingEmployee.Email = newEmployeedData.Email;


            foreach (var project in newEmployeedData.Projects)
            {
                var existingProject = existingEmployee.Projects
                    .FirstOrDefault(p => p.Id == project.Id);

                if (existingProject != null)
                {

                    existingProject.Name = project.Name;
                    existingProject.Description = project.Description;
                }
                else
                {

                    existingEmployee.Projects.Add(project);
                }
            }


            foreach (var project in existingEmployee.Projects.ToList())
            {
                if (!newEmployeedData.Projects.Any(p => p.Id == project.Id))
                {
                    project.IsDeleted = true;
                }
            }

            await _context.SaveChangesAsync();
            return existingEmployee;
        }


        public async Task<bool> SoftDeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
                return false;

            employee.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsEmailUniqueAsync(string email, int? employeeId)
        {

            email = email.ToLower().Trim();

            if (employeeId.HasValue && employeeId > 0)
            {

                var existingEmployee = await _context.Employees
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == employeeId.Value);

                if (existingEmployee == null)
                {
                    throw new ArgumentException("Employee not found.", nameof(employeeId));
                }

                // If the email hasn't changed, it's valid
                if (existingEmployee.Email.ToLower().Trim() == email)
                {
                    return true;
                }


                return !await _context.Employees.AnyAsync(e => e.Email == email);
            }


            return !await _context.Employees.AnyAsync(e => e.Email == email);
        }

        public async Task<bool> IsProjectAssignedAsync(int employeeId, string projectName, int? projectId)
        {
            projectName = projectName.ToLower().Trim();

            if (projectId.HasValue && projectId > 0)
            {
                var existingProject = await _context.Projects
                   .AsNoTracking()
                   .FirstOrDefaultAsync(p => p.Id == projectId.Value && p.EmployeeId == employeeId);

                if (existingProject.Name.ToLower().Trim() == projectName)
                {
                    return true;
                }
                return !await _context.Projects.AnyAsync(e => e.EmployeeId == employeeId && e.Name == projectName);
            }
            return !await _context.Projects.AnyAsync(e => e.EmployeeId == employeeId && e.Name == projectName);



        }
    }

}
