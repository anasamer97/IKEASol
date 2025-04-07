using IKEA.DAL.Models;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories._Generics
{
    public interface IGenericRepository<T> where T : ModelBase
    {
		public IQueryable<T> GetAll(bool WithNoTracking = true);

		T? Get(int id);
		 
		void Add(T entity);

		void Update(T entity);

		void Delete(T entity); 
	}
}
