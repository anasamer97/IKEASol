using IKEA.BLL.Dto_s.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentServices
{
	public class DepartmentServices : IDepartmentServices
	{
		private readonly IUnitOfWork unitOfWork;

		public DepartmentServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public IEnumerable<DepartmentDto> GetAllDepartments()
		{
			var Departments = unitOfWork.DepartmentRepository.GetAll().Select(dept => new DepartmentDto()
			{
				Id = dept.Id,
				Name = dept.Name,
				Code = dept.Code,
				CreationDate = dept.CreationDate

			}).ToList();

			return Departments;
		}

		public DepartmentDetailsDto? GetDepartmentById(int id)
		{
			var Department = unitOfWork.DepartmentRepository.Get(id);
			if (Department is not null)
				return new DepartmentDetailsDto()
				{
					Id = Department.Id,
					Name = Department.Name, 
					Code = Department.Code,
					Description = Department.Description,
					CreationDate = Department.CreationDate,
					IsDeleted = Department.IsDeleted,
					LastModfiedOn = Department.LastModifiedBy,
					LastModifiedOn = Department.LastModifiedOn,
					CreatedBy = Department.CreatedBy,
					CreatedOn = Department.CreatedOn
				};

			return null;
		}

		public int CreateDepartment(CreatedDepartmentDto departmentDto)
		{
			var CreatedDepartment = new Department()
			{
				Code = departmentDto.Code,
				Name = departmentDto.Name,
				Description = departmentDto.Description,
				CreationDate = departmentDto.CreationDate,
				CreatedBy = 1,
				CreatedOn = DateTime.Now,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.Now,
			};
			 unitOfWork.DepartmentRepository.Add(CreatedDepartment);
			return unitOfWork.Complete();
		}

		public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
		{
			var UpdatedDepartment = new Department()
			{
				Id = departmentDto.Id,
				Code = departmentDto.Code,
				Name = departmentDto.Name,
				Description = departmentDto.Description,
				CreationDate = departmentDto.CreationDate,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.Now,
			};

			 unitOfWork.DepartmentRepository.Update(UpdatedDepartment);
			 return unitOfWork.Complete();
		}

		public bool DeleteDepartment(int id)
		{
			var department = unitOfWork.DepartmentRepository.Get(id);
			if (department is not null)
				unitOfWork.DepartmentRepository.Delete(department) ;

			var Result = unitOfWork.Complete();

			if (Result > 0)
				return true;

			else
				return false;

		}









	}
}