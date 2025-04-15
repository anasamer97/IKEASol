using IKEA.DAL.Identity;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks; 

namespace IKEA.DAL.Persistance.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
		// Department => Context => Options
		// This constructor is used to initialize the ApplicationDbContext class
		public ApplicationDbContext(DbContextOptions options):base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=.;Database=IKEA;Trusted_Connection=True;TrustServerCertificate=true");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());	
		}
		public DbSet<Department> Departments { get; set; }

		public DbSet<Employee> Employees { get; set; }


	}
}
