using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Model;
using WebAPI.Hub;

using Microsoft.AspNet.SignalR;
using WebAPI.Repository;

namespace WebAPI
{
	public class CardService : ICardService
	{
		private readonly ICardRepository _cardRepository;

		private readonly IHubContext CardsHub;

		public CardService(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
			CardsHub = GlobalHost.ConnectionManager.GetHubContext<ItemHub>();
		}

		public async Task<bool> AddCardAsync(Card entity)
		{
			if (_cardRepository.Add(entity))
			{
				await CardsHub.Clients.All.AddNewCardToPage(entity);

				return true;
			}

			return false;
		}

		public async Task<bool> AddNewItemCardAsync()
		{
			await CardsHub.Clients.All.AddNewCardToPage();

			return true;
		}

		public void Add(Card entity)
		{
			_cardRepository.Add(entity);
		}

		public IEnumerable<Card> GetAll()
		{
			return _cardRepository.GetAll();
		}

		public IEnumerable<Card> GetAllBySuit(string category)
		{
			return _cardRepository.GetAllByValue(category);
		}

		public Card GetById(Guid id)
		{
			return _cardRepository.GetById(id);
		}

		public void Delete(Guid id)
		{
			_cardRepository.Delete(id);
		}

		public async Task<bool> DeleteCardAsync(Guid id)
		{
			if (_cardRepository.Delete(id))
			{
				await CardsHub.Clients.All.DeleteCardFromPage(id);

				return true;
			}

			return false;
		}

		public async Task Update(Card entity)
		{
			_cardRepository.Update(entity);
			await CardsHub.Clients.All.PutCardToPage(entity);
		}
	}
}