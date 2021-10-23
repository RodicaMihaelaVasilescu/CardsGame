using System.Collections.Generic;
using DataAccess.Model;

namespace WebAPI.Repository
{
  public interface ICardRepository : IRepository<Card>
	{
		IEnumerable<Card> GetAllByValue(string category);
	}
}
