using System.Collections.Generic;
using System.Web.Http;
using DataAccess.Model;
using System;

namespace WebAPI
{
  public class CardsController : ApiController
	{
		private ICardService cardService;

		public CardsController(ICardService CardService)
		{
			this.cardService = CardService;
		}
		// GET api/cards 
		public IEnumerable<Card> Get()
		{
			return cardService.GetAll();
		}

		// GET api/cards/5 
		public Card Get(Guid id)
		{
			return cardService.GetById(id);
			//return ItemList.Instance.Items.FirstOrDefault(i => i.Name == name);
		}

		// POST api/cards 
		public void Post([FromBody] Card Card)
		{
			//ItemList.Instance.Items.Add(Card);

			cardService.AddCardAsync(Card);
		}

		// PUT api/cards/5 
		public void Put([FromBody] Card Card)
		{
			cardService.Update(Card);
		}

		// DELETE api/cards/5 
		public void Delete([FromBody] Guid id)
		{
			//ItemList.Instance.Items.RemoveAll(p=>p.Id == id);
			cardService.DeleteCardAsync(id);
		}
	}
}
