using AutoMapper;
using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.PL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
	[Authorize]

	public class DepartmentController : Controller
	{
		#region Dependency Injection
		// Inheritance: DepartmentController is a Controller
		// Composition: DepartmentController has a IDepartmentServices
		private readonly IDepartmentServices departmentServices;
		private readonly IMapper mapper;
		private readonly ILogger<DepartmentController> logger;
		private readonly IWebHostEnvironment environment;


		public DepartmentController(IDepartmentServices _departmentServices,IMapper mapper, ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
		{
			departmentServices = _departmentServices;
			this.mapper = mapper;
			logger = _logger;
			this.environment = environment;
		} 
		#endregion

		#region Index
		[HttpGet]
		public IActionResult Index()
		{
			var Departments = departmentServices.GetAllDepartments();
			ViewData["Message"] = "Hello from View Data";
			return View(Departments);
		}
		#endregion

		#region Details
		[HttpGet]
		public IActionResult Details(int? id)
		{
			if (id is null)
				return BadRequest();

			var department = departmentServices.GetDepartmentById(id.Value);

			if (department is null)
				return NotFound();

			return View(department);
		}
		#endregion
		  
		#region Create
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(DepartmentVM departmentVM)
		{
			// Server Side Validation	
			if (!ModelState.IsValid)
				return View(departmentVM);

			var Message = string.Empty;


			try
			{
				var departmentDto = mapper.Map<DepartmentVM, CreatedDepartmentDto>(departmentVM);
				//var departmentDto = new CreatedDepartmentDto
				//{
				//	Name = departmentVM.Name,
				//	Code = departmentVM.Code,
				//	Description = departmentVM.Description,
				//	CreationDate = DateTime.Now
				//};


				var Result = departmentServices.CreateDepartment(departmentDto);
				if (Result > 0)
				{
					TempData["Message"] = $"{departmentDto.Name} Department is created";
					return RedirectToAction(nameof(Index));

				}

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

				return View(departmentVM);

		}
		#endregion

		#region Edit
		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (id is null)
				return BadRequest();
			var department = departmentServices.GetDepartmentById(id.Value);
			if (department is null)
				return NotFound();

			var MappedDepartment = mapper.Map<DepartmentDetailsDto, DepartmentVM>(department);

			//var MappedDepartment = new DepartmentVM
			//{
			//	Id = department.Id,
			//	Name = department.Name,
			//	Code = department.Code,
			//	Description = department.Description
			//};

			return View(MappedDepartment);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(DepartmentVM departmentVM)
		{
			if (!ModelState.IsValid)
				return View(departmentVM);

			var Message = string.Empty;
			try
			{
				var departmentDto = mapper.Map<DepartmentVM, UpdatedDepartmentDto>(departmentVM);
				//var departmentDto = new UpdatedDepartmentDto
				//{
				//	Id = departmentVM.Id,
				//	Name = departmentVM.Name,
				//	Code = departmentVM.Code,
				//	Description = departmentVM.Description,
				//	CreationDate = departmentVM.CreationDate
				//};
				var Result = departmentServices.UpdateDepartment(departmentDto);
				if (Result > 0)
					return RedirectToAction(nameof(Index));
				else
				{
					Message = "Department is not updated";

				}
			}

			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				Message = environment.IsDevelopment() ? ex.Message : "An Error Occured at the update operation";
			}

			ModelState.AddModelError(string.Empty, Message);
			return View(departmentVM);

		}
		#endregion

		#region Delete
		[HttpGet]
		public IActionResult Delete(int? id)
		{
			if (id is null)
				return BadRequest();
			var department = departmentServices.GetDepartmentById(id.Value);
			if (department is null)
				return NotFound();
			return View(department);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int deptId)
		{
			var Message = string.Empty;
			try
			{ 
				var Result = departmentServices.DeleteDepartment(deptId);
				if (Result)
					return RedirectToAction(nameof(Index));
				else
				{
					Message = "Department is not deleted";
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				Message = environment.IsDevelopment() ? ex.Message : "An Error Occured at the delete operation";
			}
			ModelState.AddModelError(string.Empty, Message);
			return RedirectToAction(nameof(Delete), new { id = deptId });
		}



		#endregion

	}
}