using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Manager;
using DataAccess.Model;

namespace WebAPI.Repository
{
  class CardRepository : ICardRepository
  {
    private readonly IDictionary<Guid, Card> _cards;

    private static CardRepository instance = null;

    public CardRepository()
    {
      _cards = new Dictionary<Guid, Card>();

      var cards = CardsManager.GetRandomCards();
      foreach(var card in cards)
      {
        _cards.Add(card.Id, card);
      }
    }
    public static CardRepository Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new CardRepository();
        }
        return instance;
      }
    }

    public bool Add(Card entity)
    {
      Instance._cards.Add(entity.Id, entity);
      return Instance._cards.TryGetValue(entity.Id, out entity);
    }

    public IEnumerable<Card> GetAll()
    {
      return Instance._cards.Values;
    }

    public IEnumerable<Card> GetAllByValue(string suit)
    {
      return Instance._cards.Values.Where(x => x.Value == suit);
    }

    public Card GetById(Guid id)
    {
      return Instance._cards[id];
    }

    public bool Delete(Guid id)
    {
      var result = Instance._cards.Remove(id);

      if (result)
      {
        return true;
      }

      return false;
    }

    public void Update(Card entity)
    {
      Instance._cards.Remove(entity.Id);
      Instance._cards.Add(entity.Id, entity);
    }
  }
}