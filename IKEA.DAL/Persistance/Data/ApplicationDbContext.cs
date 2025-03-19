using IKEA.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Data
{
    public class ApplicationDbContext:DbContext
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
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());	
		}
		public DbSet<Department> Departments { get; set; }

	}
}
