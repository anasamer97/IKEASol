using IKEA.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Data.Configurations.DepartmentConfigurations
{

	// This class is used to configure the Department entity
	public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
	{
		// This method is used to configure the Department entity
		public void Configure(EntityTypeBuilder<Department> builder)
		{ 

			builder.Property(D=>D.Id).UseIdentityColumn(10, 10);
			builder.Property(D => D.Name).HasColumnName("varchar(50)").IsRequired();
			builder.Property(D => D.Code).HasColumnName("varchar(20)").IsRequired();

			builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()");
			builder.Property(D=>D.LastModifiedOn).HasComputedColumnSql("GetDate()");


			builder.HasMany(D => D.Employees).WithOne(E => E.Department).HasForeignKey(E => E.DepartmentID).OnDelete(DeleteBehavior.SetNull);
		}
	}
}
