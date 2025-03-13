using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Departments
{
	public class DepartmentRepository : IDepartmentRepository
	{

		private readonly ApplicationDbContext dbContext;

		public DepartmentRepository(ApplicationDbContext context)
		{
			dbContext = context;
		}

		public IEnumerable<Department> GetAll(bool WithNoTracking = true)
		{
			if (WithNoTracking)
				return dbContext.Departments.AsNoTracking().ToList();
			else
				return dbContext.Departments.ToList();
		}
		 

		public Department? GetById(int id)
		{
			var Department = dbContext.Departments.Find(id);
			return Department;
		}

		public int Add(Department deparment)
		{
			dbContext.Departments.Add(deparment);
			return dbContext.SaveChanges();
		}

		public int Update(Department department)
		{
			dbContext.Departments.Update(department);
			return dbContext.SaveChanges();
		}

		public int Delete(Department department)
		{
			dbContext.Departments.Remove(department);
			return dbContext.SaveChanges();
		}

		

		
	}
}
