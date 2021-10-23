using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using _888BonusGame.Services;

namespace _888BonusGame.ViewModels
{
  public class GameBoardViewModel : INotifyPropertyChanged
  {
    #region Private Members
    private int _score;
    private int _occurrencesOf8s;
    private HttpClient client = new HttpClient();
    private CardService _cardService;
    private ObservableCollection<CardViewModel> _cardsList;
    #endregion

    #region Public Members
    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<CardViewModel> CardsList
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

    public int OccurrencesOf8s
    {
      get
      {
        return _occurrencesOf8s;
      }
      set
      {
        _occurrencesOf8s = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OccurrencesOf8s"));
      }
    }
    #endregion

    #region Constructor

    public GameBoardViewModel()
    {
      _cardService = new CardService(client);
      _cardService.ShuffledCards += ShuffleCards;
      Task loadCommand = LoadCommandExecute();
    }

    #endregion

    #region Private Methods
    private void ShuffleCards(List<DataAccess.Model.Card> cards)
    {
      ResetOccurrenciesNumber();
      CardsList = InitializeCardsList(cards);
    }

    private void ResetOccurrenciesNumber()
    {
      OccurrencesOf8s = 0;
    }

    private void CheckOccurencies()
    {
      OccurrencesOf8s = GetOccurrencesNumberOf8(CardsList);
      if (OccurrencesOf8s > 1)
      {
        Score += 8;
      }

    }

    private int GetOccurrencesNumberOf8(ObservableCollection<CardViewModel> cardsList)
    {
      var cardsOf8 = 0;
      foreach (var item in CardsList)
      {
        if (item.Card.Value.Contains("8"))
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

    private ObservableCollection<CardViewModel> InitializeCardsList(List<DataAccess.Model.Card> allItems)
    {
      var cardsList = new ObservableCollection<CardViewModel>();
      foreach (var item in allItems)
      {
        var cardViewModel = new CardViewModel(item);
        cardsList.Add(cardViewModel);
        cardViewModel.Flipped += CardFlipped;
      }
      return cardsList;
    }

    private void CardFlipped(object sender, EventArgs e)
    {
      if (CardsList.Count(c => c.Card.HasBeenFlipped == true) == CardsList.Count())
      {
        CheckOccurencies();
      }
    }
    #endregion
  }
}
