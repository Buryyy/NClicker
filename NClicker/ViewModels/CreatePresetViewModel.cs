using NClicker.Models;
using NClicker.Services;
using NClicker.Storage;
using System.Windows.Input;

namespace NClicker.ViewModels
{
    public class CreatePresetViewModel : BaseViewModel
    {
        private readonly IPresetService _presetService;
        private readonly RunConfiguration _configuration;
        private readonly IPresetRepository _presetRepository;

        private string _presetName;

        public CreatePresetViewModel(RunConfiguration configuration, IPresetRepository presetRepository,
            IPresetService presetService)
        {
            _configuration = configuration;
            _presetRepository = presetRepository;
            _presetService = presetService;
            PresetName = "Default";
        }

        #region Commands

        public ICommand AddPresetCommand => new Command(() =>
        {
            _configuration.Name = PresetName;
            if (_presetRepository.Contains(_presetName))
            {
                int presetNr = GetNextAvailableNumber();
                _configuration.Name = $"{_presetName} {presetNr}"; //Now the output is like 'Default 1', 'Default 2' etc.
            }
            _presetService.AddPreset(_configuration);

        }, () => true);

        #endregion Commands

        public string PresetName
        {
            get => _presetName;
            set => SetPropertyValue(ref _presetName, value);
        }

        //This will get the next avaible number since the name of the preset exists.
        private int GetNextAvailableNumber()
        {
            int value = 1;
            while (value < ushort.MaxValue)
            {
                if (_presetRepository.Contains($"{_presetName} {value}"))
                {
                    value++;
                }
                else
                {
                    return value;
                }
            }

            return value;
        }
    }
}