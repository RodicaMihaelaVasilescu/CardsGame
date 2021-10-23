using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Model;

namespace WebAPI
{
  public interface ICardService : IService<Card>
  {
    IEnumerable<Card> GetAllBySuit(string suit);

    Task<bool> AddCardAsync(Card Card);

    Task<bool> DeleteCardAsync(Guid id);
  }
}