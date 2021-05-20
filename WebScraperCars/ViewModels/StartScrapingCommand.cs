using System;
using System.Windows.Input;

namespace WebScraperCars.ViewModels
{
    class StartScrapingCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> _execute;

        public StartScrapingCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            if (string.IsNullOrEmpty(parameter as string))
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
