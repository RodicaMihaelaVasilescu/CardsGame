using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
  public class Filter : INotifyPropertyChanged
  {
    private string _selectedProduct;
    public event EventHandler SelectedProductChanged;

    public string Title { get; set; }
    public List<string> FilterListItem { get; set; } = new List<string>();
    public string SelectedProduct
    {
      get { return _selectedProduct; }
      set
      {
        if (_selectedProduct == value) return;
        _selectedProduct = value;
        SelectedProductChanged(this, new EventArgs());
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedProduct"));
      }
    }


    public event PropertyChangedEventHandler PropertyChanged;
  }
}
