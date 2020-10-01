using NClicker.Models;
using System.Collections.ObjectModel;

namespace NClicker.Services
{
    public interface IPresetService
    {
        ObservableCollection<RunConfiguration> SharedPresetCollection { get; }

        void AddPreset(RunConfiguration configuration);

        void RemovePreset(RunConfiguration configuration);

        void ResetPreset(RunConfiguration configuration);
    }
}