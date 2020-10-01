using NClicker.Models;
using NClicker.Storage;
using NClicker.Utils;
using System.Collections.ObjectModel;

namespace NClicker.Services
{
    public class PresetService : IPresetService
    {
        public ObservableCollection<RunConfiguration> SharedPresetCollection { get; set; }

        private readonly IPresetStorage _storage;

        public PresetService(IPresetStorage presetStorage)
        {
            _storage = presetStorage;
            SharedPresetCollection = new ObservableCollection<RunConfiguration>(_storage.Presets);
        }

        public void AddPreset(RunConfiguration configuration)
        {
            SharedPresetCollection.Add(configuration);
            _storage.Add(configuration);
            RefreshSharedPresets();
        }

        public void RemovePreset(RunConfiguration configuration)
        {
            SharedPresetCollection.Remove(configuration);
            _storage.Remove(configuration.Name);
            RefreshSharedPresets();
        }

        public void ResetPreset(RunConfiguration configuration)
        {
            var defaultPreset = Constants.DefaultConfig;

            configuration.Seconds = defaultPreset.Seconds;
            configuration.Milliseconds = defaultPreset.Milliseconds;
            configuration.RandomSeconds = defaultPreset.RandomSeconds;
            configuration.RandomMilliseconds = defaultPreset.RandomMilliseconds;
            configuration.BlockInput = defaultPreset.BlockInput;
            _storage.Update(configuration);
        }

        private void RefreshSharedPresets()
        {
            SharedPresetCollection.Clear();

            var savedPresets = _storage.Presets;
            savedPresets.ForEach(preset => SharedPresetCollection.Add(preset));
#if Debug
            SharedPresetCollection.ForEach(item => Debug.WriteLine(item.Name));
#endif
        }
    }
}