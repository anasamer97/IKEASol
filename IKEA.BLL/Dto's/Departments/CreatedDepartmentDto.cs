﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Dto_s.Departments
{
	public class CreatedDepartmentDto
	{
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = "Code is required")]
		public string Code { get; set; } = null!;

		public string? Description { get; set; }

		[Display(Name = "Date of Creation")]
		public DateTime CreationDate { get; set; }
	}
}