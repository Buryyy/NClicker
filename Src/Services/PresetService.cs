using NClicker.Models;
using NClicker.Storage;
using System.Collections.ObjectModel;

namespace NClicker.Services
{
    public class PresetService : IPresetService
    {
        public ObservableCollection<RunConfiguration> SharedPresetCollection { get; set; }

        private readonly IPresetRepository _presetRepository;

        public PresetService(IPresetRepository presetPresetRepository)
        {
            _presetRepository = presetPresetRepository;
            SharedPresetCollection = new ObservableCollection<RunConfiguration>(_presetRepository.GetAllPresetsOrdered());
        }

        public void AddPreset(RunConfiguration configuration)
        {
            _presetRepository.Upsert(configuration);
            SharedPresetCollection.Add(configuration);

        }

        public void RemovePreset(RunConfiguration configuration)
        {
            _presetRepository.Remove(configuration.Name);
            SharedPresetCollection.Remove(configuration);
        }

        public void ResetPreset(RunConfiguration configuration)
        {
            var defaultPreset = Constants.DefaultConfig;

            configuration.Seconds = defaultPreset.Seconds;
            configuration.Milliseconds = defaultPreset.Milliseconds;
            configuration.RandomSeconds = defaultPreset.RandomSeconds;
            configuration.RandomMilliseconds = defaultPreset.RandomMilliseconds;
            configuration.BlockInput = defaultPreset.BlockInput;
            _presetRepository.Upsert(configuration);
        }

        public int GetTotalPresets() => _presetRepository.GetPresetsCount();

    }
}