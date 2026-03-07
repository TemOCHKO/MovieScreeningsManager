using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MovieScreeningsManager.UI.ViewModel
{
    
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel() { }
        bool isBusy = false;
        string title = string.Empty;

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy == value)
                    return;
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        public string Title
        {
            get => title;
            set
            {
                if (title == value)
                    return;
                title = value;
                OnPropertyChanged(nameof(Title));
            }

        }

        public bool IsNotBusy => !IsBusy;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
