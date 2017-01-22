using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceMonitor.ViewModels
{
  public abstract class ViewModelBase : INotifyPropertyChanged
  {
    protected void RaisePropertyChangedEvent(string propertyName)
    {
      var propChanged = this.PropertyChanged;
      if (propChanged != null)
      {
        propChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
