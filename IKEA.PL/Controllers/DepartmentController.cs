using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
		// Inheritance: DepartmentController is a Controller
		// Composition: DepartmentController has a IDepartmentServices
		private readonly IDepartmentServices departmentServices;
		private readonly ILogger<DepartmentController> logger;
		private readonly IWebHostEnvironment environment;


		public DepartmentController(IDepartmentServices _departmentServices, ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
		{
			departmentServices = _departmentServices;
			logger = _logger;
			this.environment = environment;
		}



		#region Index
		[HttpGet]
		public IActionResult Index()
		{
			var Departments = departmentServices.GetAllDepartments();
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
		public IActionResult Create(CreatedDepartmentDto departmentDto)
		{
			// Server Side Validation 
			if (!ModelState.IsValid)
				return View(departmentDto);

			var Message = string.Empty;


			try
			{
				var Result = departmentServices.CreateDepartment(departmentDto);
				if (Result > 0)
					return RedirectToAction(nameof(Index));

				else
				{
					Message = "Department is not created";
					ModelState.AddModelError(string.Empty, Message);
					return View(departmentDto);
				}
			}

			catch (Exception ex)
			{
				// Log the exception (Kestral)
				logger.LogError(ex, ex.Message);

				// Display the error message to the user

				if (environment.IsDevelopment())
				{
					Message = ex.Message;
					ModelState.AddModelError(string.Empty, Message);
					return View(departmentDto);
				}

				else
				{
					Message = "An Error Occured at the creation operation";
				}
				return View(departmentDto);
			}





		} 
		#endregion

	}
}
