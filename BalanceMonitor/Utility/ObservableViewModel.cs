using System.ComponentModel;

namespace BalanceMonitor.Utility
{
  public abstract class ObservableViewModel : INotifyPropertyChanged
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
