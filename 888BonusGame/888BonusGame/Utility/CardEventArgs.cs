using System;
using DataAccess.Model;

namespace _888BonusGame.Utility
{
  class CardEventArgs : EventArgs

	{
		public Card Product { get; set; }

		public CardEventArgs(Card product)
		{
			Product = product;
		}

	}
}
