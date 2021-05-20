using System;
using System.Windows.Input;

namespace WebScraperCars.ViewModels
{
    class StartScrapingCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<string> _execute;

        public StartScrapingCommand(Action<string> execute)
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
            _execute.Invoke(parameter as string);
        }
    }
}
