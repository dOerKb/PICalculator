using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PICalculator.App.Abstracts
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
        {
            bool returnValue = false;

            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                returnValue = true;
            }

            return returnValue;
        }
    }
}
