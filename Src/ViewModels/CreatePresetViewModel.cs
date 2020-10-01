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
        private readonly IPresetStorage _presetStorage;

        private string _presetName;

        public CreatePresetViewModel(RunConfiguration configuration, IPresetStorage presetStorage,
            IPresetService presetService)
        {
            _configuration = configuration;
            _presetStorage = presetStorage;
            _presetService = presetService;
            PresetName = "Default";
        }

        #region Commands

        public ICommand AddPresetCommand => new Command(() =>
        {
            _configuration.Name = PresetName;
            if (_presetStorage.Contains(_presetName))
            {
                int presetNumber = GetNextAvailableNumber();
                _configuration.Name = PresetName + presetNumber; //Now the output is like 'Default1', 'Default2' etc.
            }

            _presetService.AddPreset(_configuration);
#if Debug
                Debug.WriteLine("Created preset " + _configuration.Name);
#endif
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
            while (value < int.MaxValue)
            {
                if (_presetStorage.Contains(_presetName + value))
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