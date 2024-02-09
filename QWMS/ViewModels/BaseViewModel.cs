using CommunityToolkit.Maui.Core;
using QWMS.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set 
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(IsNotBusy));
            }
        }

        //todo: Add XAML Converter
        public bool IsNotBusy => !IsBusy;
        
        protected void Set<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            NotifyPropertyChanged(propertyName);
        }        

        public void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
