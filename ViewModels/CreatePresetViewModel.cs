using Autofac;
using NClicker.Models;
using NClicker.Services;
using NClicker.Storage;
using System.Windows.Input;

namespace NClicker.ViewModels
{
    public class CreatePresetViewModel : BaseViewModel
    {
        private readonly RunConfiguration _configuration;
        private readonly PresetStorage _presetStorage;

        private string _presetName;

        public CreatePresetViewModel(RunConfiguration configuration)
        {
            _configuration = configuration;
            _presetStorage = App.Context.Resolve<PresetStorage>();
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
            App.Context.Resolve<PresetService>().AddPreset(_configuration);
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