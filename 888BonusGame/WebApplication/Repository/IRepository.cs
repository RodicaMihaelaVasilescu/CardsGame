using System;
using System.Collections.Generic;

namespace WebAPI
{
  public interface IRepository<T> where T : class
	{
		bool Add(T entity);

		IEnumerable<T> GetAll();

		T GetById(Guid id);

		bool Delete(Guid id);

		void Update(T entity);
	}
}