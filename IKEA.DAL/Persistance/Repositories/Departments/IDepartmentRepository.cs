using IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Departments
{
	// GetAll GetById Add Update Delete
	public interface IDepartmentRepository
    {
		public IEnumerable<Department> GetAll(bool WithNoTracking = true);
		Department? GetById(int id);

		int Add(Department deparment);

		int Update(Department department);

		int Delete(Department department);
	}
}
