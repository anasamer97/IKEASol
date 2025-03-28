using Azure;
using IKEA.BLL.Dto_s.Employees;
using IKEA.DAL.Common.Enums;
using IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeServices : IEmployeeServices
	{
		private readonly IEmployeeRepository repository;
		public EmployeeServices(IEmployeeRepository employeeRepository)
		{
			repository = employeeRepository;
		}

		public IEnumerable<EmployeeDto> GetAllEmployees()
		{
			var employees = repository.GetAll();
			var FilteredEmployees = employees.Where(E => E.IsDeleted == false);

			var AfterFiltration = FilteredEmployees.Select(E => new EmployeeDto()
			{
				Id = E.Id,
				Name = E.Name,
				Age = E.Age,
				Salary = E.Salary,  
				IsActive = E.IsActive,
				Email = E.Email,
				Gender = E.Gender,
				EmployeeType = E.EmployeeType
			});
				
			
			return AfterFiltration.ToList();

		}

		public EmployeeDetailsDto? GetEmployeeById(int id)
		{
			var employee = repository.GetById(id);

			if(employee is not null)
			{
				return new EmployeeDetailsDto
				{
					Id = employee.Id,
					Name = employee.Name,
					Age = employee.Age,
					Address = employee.Address,
					IsActive = employee.IsActive,
					Salary = employee.Salary,
					Email = employee.Email,
					PhoneNumber = employee.PhoneNumber,
					Gender = employee.Gender,
					EmployeeType = employee.EmployeeType,
					LastModifiedBy = employee.LastModifiedBy,
					CreatedBy = employee.CreatedBy,
					CreatedOn = employee.CreatedOn,
					LastModifiedOn = employee.LastModifiedOn

				};


			}

			return null;
		}

		public int CreateEmployee(CreatedEmployeeDto employeeDto)
		{
			var Employee = new DAL.Models.Employees.Employee()
			{
				Name = employeeDto.Name,
				Age = employeeDto.Age,
				Address = employeeDto.Address,
				Salary = employeeDto.Salary,
				IsActive = employeeDto.IsActive,
				Email = employeeDto.Email,
				PhoneNumber = employeeDto.PhoneNumber,
				HiringDate = employeeDto.HiringDate,
				Gender = employeeDto.Gender,
				EmployeeType = employeeDto.EmployeeType,
				CreatedBy = 1,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.Now,
				CreatedOn = DateTime.Now
			};

			return repository.Add(Employee); 
		}

		public int UpdatedEmployee(UpdatedEmployeeDto employeeDto)
		{
			var Employee = new DAL.Models.Employees.Employee()
			{
				Id = employeeDto.Id,
				Name = employeeDto.Name,
				Age = employeeDto.Age,
				Address = employeeDto.Address,
				Salary = employeeDto.Salary,
				IsActive = employeeDto.IsActive,
				Email = employeeDto.Email,
				PhoneNumber = employeeDto.PhoneNumber,
				HiringDate = employeeDto.HiringDate,
				Gender = employeeDto.Gender,
				EmployeeType = employeeDto.EmployeeType,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.Now,
			};

			return repository.Update(Employee);
		}

		public bool DeleteEmployee(int id)
		{
			var employee = repository.GetById(id);
			if (employee is not null)
				return repository.Delete(employee) > 0;

			else
				return false;
		}

		

		

		
	}
}
