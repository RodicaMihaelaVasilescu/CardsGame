using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using _888BonusGame.Services;
using Card = _888BonusGame.Model.Card;

namespace _888BonusGame.ViewModels
{
  public class GameBoardViewModel : INotifyPropertyChanged
  {
    private HttpClient client = new HttpClient();
    private CardService _cardService;
    private ObservableCollection<Card> _cardsList;
    private int _score;
    private int _occurenciesOf8s;

    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<Card> CardsList
    {
      get
      {
        return _cardsList;
      }
      set
      {
        _cardsList = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CardsList"));
      }
    }

    public int Score
    {
      get
      {
        return _score;
      }
      set
      {
        _score = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Score"));
      }
    }

    public int OccurenciesOf8s
    {
      get
      {
        return _occurenciesOf8s;
      }
      set
      {
        _occurenciesOf8s = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OccurenciesOf8s"));
      }
    }


    public GameBoardViewModel()
    {
      _cardService = new CardService(client);
      _cardService.ShuffledCards += ShuffleCards;
      Task loadCommand = LoadCommandExecute();

    }

    private void ShuffleCards(List<DataAccess.Model.Card> cards)
    {
      OccurenciesOf8s = 0;
      CardsList = InitializeCardsList(cards);
    }

    private void CheckOccurencies()
    {
      OccurenciesOf8s = GetOccurenciesNumber(CardsList);
      if (OccurenciesOf8s > 1)
      {
        Score += 8;
      }

    }

    private int GetOccurenciesNumber(ObservableCollection<Card> cardsList)
    {
      var cardsOf8 = 0;
      foreach (var item in CardsList)
      {
        if (item.Value.Contains("8"))
        {
          cardsOf8++;
        }
      }
      return cardsOf8;
    }

    private async Task LoadCommandExecute()
    {
      var allItems = new List<DataAccess.Model.Card>();
      await Application.Current.Dispatcher.Invoke(async () =>
      {
        allItems = await _cardService.GetAllItems();
      });

      CardsList = InitializeCardsList(allItems);
    }

    private ObservableCollection<Card> InitializeCardsList(List<DataAccess.Model.Card> allItems)
    {
      var cardsList = new ObservableCollection<Card>();
      foreach (var item in allItems)
      {
        var card = new Card(item);
        cardsList.Add(card);
        card.Flipped += CardFlipped;
      }
      return cardsList;
    }

    private void CardFlipped(object sender, EventArgs e)
    {
      if (CardsList.Count(c => c.HasBeenFlipped == true) == CardsList.Count())
      {
        CheckOccurencies();
      }
    }
  }
}
