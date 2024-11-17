using System.Windows.Input;

public class RelayCommand<T> : ICommand
{
    private readonly Action<T> _execute;
    private readonly Func<bool> _canExecute;

    public RelayCommand(Action<T> execute, Func<bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute();
    }

    public void Execute(object parameter)
    {
        if (parameter is T param)
        {
            _execute(param);
        }
        else if (parameter == null && typeof(T).IsClass)
        {
            _execute(default);
        }
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}