using System;
using System.Windows.Input;

namespace BalanceMonitor.Utility
{
  public class DelegateCommand : ICommand
  {
    private readonly Action<object> execution;
    private readonly Predicate<object> canExecute;

    public DelegateCommand(Action<object> execution, Predicate<object> canExecute)
    {
      this.execution = execution;
      this.canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
      return this.canExecute(parameter);
    }

    public void Execute(object parameter)
    {
      this.execution(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
      add
      {
        CommandManager.RequerySuggested += value;
      }
      remove
      {
        CommandManager.RequerySuggested -= value;
      }
    }
  }
}
