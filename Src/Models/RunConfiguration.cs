using NClicker.Properties;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NClicker.Models
{
    public sealed class RunConfiguration : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public int Seconds { get; set; }

        public int Milliseconds { get; set; }

        public int RandomSeconds { get; set; }

        public int RandomMilliseconds { get; set; }

        public bool BlockInput { get; set; }

        public DateTime CreationDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}