using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using WebScraperCars.Models;

namespace WebScraperCars.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<CarModel> carModelsItemSource;
        public StartScrapingCommand ButtonScrapingCommand { get; set; }

        public ObservableCollection<CarModel> CarModelsItemSource
        {
            get { return carModelsItemSource; }
            set 
            { 
                carModelsItemSource = value;
                NotifyPropertyChanged("CarModelsItemSource");
            }
        }

        public MainPageViewModel()
        {
            ButtonScrapingCommand = new StartScrapingCommand(StartScraping);
        }

        public void StartScraping(object carName)
        {
            Debug.WriteLine(carName as string);
            CarModelsItemSource = CarManager.GetCars();
        }
    }

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
