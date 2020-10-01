using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NClicker.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetPropertyValue<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (value == null ? field == null : value.Equals(field))
            {
                return false;
            }

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }
}