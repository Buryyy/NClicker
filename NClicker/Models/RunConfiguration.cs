using LiteDB;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NClicker.Models
{
    public sealed class RunConfiguration : INotifyPropertyChanged
    {
        [BsonId] public string Name { get; set; }

        public int Seconds { get; set; }

        public int Milliseconds { get; set; }

        public int RandomSeconds { get; set; }

        public int RandomMilliseconds { get; set; }

        public bool BlockInput { get; set; }

        public DateTime CreationDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}