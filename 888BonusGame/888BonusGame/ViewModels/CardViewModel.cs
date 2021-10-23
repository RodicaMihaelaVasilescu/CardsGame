using _888BonusGame.Model;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace _888BonusGame.ViewModels
{
  public class CardViewModel : INotifyPropertyChanged
  {
    public Card Card { get; set; }

    public ICommand FlipCommand { get; set; }

    public event EventHandler Flipped;

    public CardViewModel(DataAccess.Model.Card obj)
    {
      Card = new Card();
      Card.Id = obj.Id;
      Card.Value = obj.Value;
      Card.DisplayedImage = Card.BackImage;
      FlipCommand = new DelegateCommand(FlipCommandExecute);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void FlipCommandExecute()
    {
      if (Card.DisplayedImage == Card.BackImage)
      {
        Card.DisplayedImage = Card.FrontImage;
        Card.HasBeenFlipped = true;
        Flipped?.Invoke(this, new EventArgs());
      }
      else
      {
        Card.DisplayedImage = Card.BackImage;
      }
    }
  }
}
