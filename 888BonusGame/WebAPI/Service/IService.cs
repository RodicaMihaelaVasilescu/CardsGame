using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;

namespace WebAPI
{
	public interface IService<T> where T : class
	{
		void Add(T entity);

		IEnumerable<T> GetAll();

		T GetById(Guid id);

		void Delete(Guid entity);

		Task Update(T entity);
	}
}