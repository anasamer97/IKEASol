using IKEA.DAL.Models.Employees;
using IKEA.BLL.Dto_s.Employees;
using IKEA.DAL.Common.Enums;
using IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using IKEA.BLL.Common.Services.Attachments;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeServices : IEmployeeServices
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IAttachmentServices attachmentServices;

		public EmployeeServices(IUnitOfWork unitOfWork, IAttachmentServices attachmentServices)
		{
			this.unitOfWork = unitOfWork;
			this.attachmentServices = attachmentServices;
		}


		public IEnumerable<EmployeeDto> GetAllEmployees(string search)
		{
			var employees = unitOfWork.EmployeeRepository.GetAll();

			var QueryEmployees = employees.Where(E=>!E.IsDeleted && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower()))).Include(E=>E.Department).Select(E => new EmployeeDto()
			{
				Id = E.Id,
				Name = E.Name,
				Age = E.Age,
				Salary = E.Salary,  
				IsActive = E.IsActive,
				Email = E.Email,
				Gender = E.Gender,
				EmployeeType = E.EmployeeType,
				Department = E.Department.Name ?? "N/A"
			}).ToList();
				
			return QueryEmployees;

		}

		public EmployeeDetailsDto? GetEmployeeById(int id)
		{
			var employee = unitOfWork.EmployeeRepository.Get(id);

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
					HiringDate = employee.HiringDate,
					Gender = employee.Gender,
					Department = employee.Department?.Name ?? "N/A",
					EmployeeType = employee.EmployeeType,
					LastModifiedBy = employee.LastModifiedBy,
					CreatedBy = employee.CreatedBy,
					LastModifiedOn = employee.LastModifiedOn,
					CreatedOn = employee.CreatedOn,
					ImageName = employee.ImageName

				};


			}

			return null;
		}

		public int CreateEmployee(CreatedEmployeeDto employeeDto)
		{
			var Employee = new Employee()
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
				DepartmentID = employeeDto.DepartmentID,
				CreatedBy = 1,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.Now,
				CreatedOn = DateTime.Now,
			};

			if(employeeDto.Image is not null)
			{
			  Employee.ImageName = attachmentServices.UploadImage(employeeDto.Image , "images");
			}

			unitOfWork.EmployeeRepository.Add(Employee);
			return unitOfWork.Complete();
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
				DepartmentID = employeeDto.DepartmentID,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.Now,
				ImageName = employeeDto.ImageName
			};

			if (employeeDto.Image is not null)
			{
				if (employeeDto.ImageName is not null)
				{
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images", employeeDto.ImageName);
					attachmentServices.DeleteImage(filePath);
				}

				Employee.ImageName = attachmentServices.UploadImage(employeeDto.Image, "images");
			}

			unitOfWork.EmployeeRepository.Update(Employee);
			return unitOfWork.Complete();
		}

		public bool DeleteEmployee(int id)
		{
			var employee = unitOfWork.EmployeeRepository.Get(id);

			if (employee is not null)
			{
				if(employee.ImageName is not null)
				{
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images", employee.ImageName);
					attachmentServices.DeleteImage(filePath);
				}
				 unitOfWork.EmployeeRepository.Delete(employee);
			}

			var result = unitOfWork.Complete();
			if (result > 0)
				return true;

			else
				return false;
		}

		

		

		
	}
}
