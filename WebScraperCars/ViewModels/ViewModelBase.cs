using System.ComponentModel;

namespace WebScraperCars
{
    class ViewModelBase : INotifyPropertyChanged
    {
        #region Property Changed Event Handler 
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
