﻿using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Dto_s.Employees;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.BLL.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
		#region Services - Dependency Injection
		private readonly IEmployeeServices employeeServices;
		private readonly ILogger<EmployeeController> logger;
		private readonly IWebHostEnvironment environment;

		public EmployeeController(IEmployeeServices employeeServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
		{
			this.employeeServices = employeeServices;
			this.logger = logger;
			this.environment = environment;
		}
		#endregion


		#region Indeex
		[HttpGet]
		public IActionResult Index()
		{
			var employees = employeeServices.GetAllEmployees();
			return View(employees);
		} 
		#endregion

		#region Create

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreatedEmployeeDto employeeDto)
		{
			// Server Side Validation 
			if (!ModelState.IsValid)
				return View(employeeDto);

			var Message = string.Empty;


			try
			{
				var Result = employeeServices.CreateEmployee(employeeDto);
				if (Result > 0)
					return RedirectToAction(nameof(Index));

				else
					Message = "Department is not created";

			}

			catch (Exception ex)
			{
				// Log the exception (Kestral)
				logger.LogError(ex, ex.Message);

				// Display the error message to the user

				if (environment.IsDevelopment())
					Message = ex.Message;

				else
					Message = "An Error Occured at the creation operation";

			}

			ModelState.AddModelError(string.Empty, Message);
			return View(employeeDto);

		}
		#endregion

		#region Details
		[HttpGet]
		public IActionResult Details(int? id)
		{
			if (id is null)
				return BadRequest();

			var employee = employeeServices.GetEmployeeById(id.Value);

			if (employee is null)
				return NotFound();

			return View(employee);
		}
		#endregion

		#region Edit
		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (id is null)
				return BadRequest();
			var employee = employeeServices.GetEmployeeById(id.Value);
			if (employee is null)
				return NotFound();

			var MappedEmployee = new UpdatedEmployeeDto
			{
				Id = employee.Id,
				Name = employee.Name,
				Age = employee.Age,
				Address = employee.Address,
				Salary = employee.Salary,
				IsActive = employee.IsActive,
				Email = employee.Email,
				PhoneNumber = employee.PhoneNumber,
				EmployeeType = employee.EmployeeType,
				Gender = employee.Gender,
				HiringDate = employee.HiringDate

			};

			return View(MappedEmployee);
		}

		[HttpPost]
		public IActionResult Edit(UpdatedEmployeeDto employeeDto)
		{
			if (!ModelState.IsValid)
				return View(employeeDto);

			var Message = string.Empty;
			try
			{
				var Result = employeeServices.UpdatedEmployee(employeeDto);
				if (Result > 0)
					return RedirectToAction(nameof(Index));
				else
				{
					Message = "Employee is not updated";

				}
			}

			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				Message = environment.IsDevelopment() ? ex.Message : "An Error Occured at the update operation";
			}

			ModelState.AddModelError(string.Empty, Message);
			return View(employeeDto);

		}
		#endregion

		#region Delete
		[HttpGet]
		public IActionResult Delete(int? id)
		{
			if (id is null)
				return BadRequest();
			var employee = employeeServices.GetEmployeeById(id.Value);
			if (employee is null)
				return NotFound();
			return View(employee);
		}


		[HttpPost]
		public IActionResult Delete(int empId)
		{
			var Message = string.Empty;
			try
			{
				var Result = employeeServices.DeleteEmployee(empId);
				if (Result)
					return RedirectToAction(nameof(Index));
				else
				{
					Message = "Employee is not deleted";
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				Message = environment.IsDevelopment() ? ex.Message : "An Error Occured at the delete operation";
			}
			ModelState.AddModelError(string.Empty, Message);
			return RedirectToAction(nameof(Delete), new { id = empId });
		}

		#endregion
	}
}
