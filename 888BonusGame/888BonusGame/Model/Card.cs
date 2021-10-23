using Prism.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace _888BonusGame.Model
{
  public class Card : INotifyPropertyChanged
  {
    private string _displayedImage;

    public Guid Id { get; set; }

    public string Value { get; set; }

    public bool HasBeenFlipped { get; set; } = false;

    public string FrontImage
    {
      get
      {
        return string.Format(@"pack://application:,,,/Resources/Cards/{0}.png", Value);
      }
    }

    public string DisplayedImage
    {
      get
      {
        return _displayedImage;
      }
      set
      {
        _displayedImage = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisplayedImage"));
      }
    }

    public string BackImage => @"pack://application:,,,/Resources/Cards/red_back.png";


    public event PropertyChangedEventHandler PropertyChanged;
  }
}
