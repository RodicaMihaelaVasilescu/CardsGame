using DataAccess.Model;
using System;

namespace DataAccess.Dto
{
  public class CardDto
  {
    public CardDto()
    {
    }

    public CardDto(Card obj)
    {
      Id = obj.Id;
      Value = obj.Value;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Value { get; set; }

  }
}
