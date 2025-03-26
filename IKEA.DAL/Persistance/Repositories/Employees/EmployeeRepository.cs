﻿using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Repositories._Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Employees
{
    public class EmployeeRepository:GenericRepository<Employee>, IEmployeeRepository
    {
		private readonly ApplicationDbContext dbContext;

		public EmployeeRepository(ApplicationDbContext context): base(context)
		{
			dbContext = context;
		}

		public string NameOfEmployees { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	}
}
