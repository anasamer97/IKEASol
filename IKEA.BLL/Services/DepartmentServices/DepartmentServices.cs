using IKEA.DAL.Persistance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentServices
{
    public class DepartmentServices : IDepartmentServices
	{
        private IDepartmentRepository departmentRepository;

		public DepartmentServices(IDepartmentRepository _repository)
		{
			departmentRepository = _repository;
		}
	}
}
