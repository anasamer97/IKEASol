using IKEA.BLL.Dto_s.Employees;
using IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.EmployeeServices
{
    public interface IEmployeeServices
    {
		// DTO: Data Transfer Object  
		IEnumerable<EmployeeDto> GetAllEmployees();

		EmployeeDetailsDto? GetEmployeeById(int id);

		int CreateEmployee(CreatedEmployeeDto employeeDto);

		int UpdatedEmployee(UpdatedEmployeeDto employeeDto);

		bool DeleteEmployee(int id);
	}
}
