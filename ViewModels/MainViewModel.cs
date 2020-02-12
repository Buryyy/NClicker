using Autofac;
using NClicker.Models;
using NClicker.Services;
using NClicker.Storage;
using NClicker.Views;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;

namespace NClicker.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly PresetStorage _presetStorage;
        private readonly PresetService _presetService;
        private readonly KeyboardControllerService _keyboardController;

        #region Backing fields

        private ObservableCollection<RunConfiguration> _presets;
        private RunConfiguration _selectedConfig;

        private int _seconds;
        private int _milliseconds;
        private int _randomSeconds;
        private int _randomMilliseconds;
        private string _presetsHeader;
        private bool _blockKeys;
        private RunConfiguration _lastSelectedConfiguration;

        #endregion Backing fields

        public MainViewModel(PresetStorage presetStorage, PresetService presetService,
            KeyboardControllerService keyboardController)
        {
            _presets = new ObservableCollection<RunConfiguration>();
            _presetStorage = presetStorage;
            _presetService = presetService;
            _keyboardController = keyboardController;
            SetDefaultConfiguration();
            _presetService.SharedPresetCollection.CollectionChanged += CollectionChanged;
        }

        #region Commands

        public ICommand DisplayDonateCommand => new Command(() =>
        {
            var donate = new DonateView();
            donate.Show();
        }, () => true);

        public ICommand DisplayLicenseCommand => new Command(() =>
        {
            var license = new LicenseView();
            license.Show();
        }, () => true);

        public ICommand StartClickCommand => new Command(() =>
        {
            try
            {
                if (_seconds <= 0 && _milliseconds <= 0)
                {
                    MessageBox.Show(
                        $"NClicker cannot be started due to invalid seconds or milliseconds, the values cannot be zero.",
                        "Invalid delay settings");
                    return;
                }

                App.Context.Resolve<MouseControllerService>()
                    .LoopClick(_seconds, _milliseconds, _randomSeconds, _randomMilliseconds);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Unable to start auto clicker: {exception.Message}", "StartClickCommand error");
                if (exception is ArithmeticException) throw;
            }
        }, () => true);

        public ICommand StopClickCommand => new Command(() =>
        {
            App.Context.Resolve<MouseControllerService>().Stop();
        }, () => true);

        public ICommand CreatePresetCommand => new Command(() =>
        {
            var addPresetView = new CreatePresetView(new RunConfiguration
            {
                BlockInput = _blockKeys,
                CreationDate = DateTime.Now,
                Milliseconds = _milliseconds,
                RandomSeconds = _randomSeconds,
                RandomMilliseconds = _randomMilliseconds,
                Seconds = _seconds,
                Name = null
            });
            addPresetView.Show();
        }, () => true);

        public ICommand RemovePresetCommand => new Command(() =>
        {
            if (SelectedConfiguration == null)
            {
                return;
            }
            _presetService.RemovePreset(SelectedConfiguration);
            SelectedConfiguration = null;
        }, () => true);

        public ICommand ResetPresetCommand => new Command(() =>
        {
            SetDefaultConfiguration();

            if (SelectedConfiguration == null
                || SelectedConfiguration.Name == _lastSelectedConfiguration?.Name)
            {
                return;
            }
            _presetService.ResetPreset(SelectedConfiguration);
            _lastSelectedConfiguration = SelectedConfiguration;
            SelectedConfiguration = null;
        }, () => true);

        #endregion Commands

        #region Binding properties

        public RunConfiguration SelectedConfiguration
        {
            get => _selectedConfig;
            set
            {
                if (value == null || value == _selectedConfig) return;

                LoadRunConfiguration(value);
                SetPropertyValue(ref _selectedConfig, value);
            }
        }

        public ObservableCollection<RunConfiguration> Presets
        {
            get => _presetService.SharedPresetCollection;
            set => SetPropertyValue(ref _presets, value);
        }

        public string Seconds
        {
            get => _seconds.ToString();
            set => SetPropertyValue(ref _seconds, int.Parse(value));
        }

        public string RandomSeconds
        {
            get => _randomSeconds.ToString();
            set => SetPropertyValue(ref _randomSeconds, int.Parse(value));
        }

        public string RandomMilliseconds
        {
            get => _randomMilliseconds.ToString();
            set => SetPropertyValue(ref _randomMilliseconds, int.Parse(value));
        }

        public string PresetsHeader
        {
            get => _presetsHeader;
            set => SetPropertyValue(ref _presetsHeader, value);
        }

        public string Milliseconds
        {
            get => _milliseconds.ToString();
            set => SetPropertyValue(ref _milliseconds, int.Parse(value));
        }

        public bool BlockKeys
        {
            get => _blockKeys;
            set
            {
                SetPropertyValue(ref _blockKeys, value);
                App.Context.Resolve<KeyboardControllerService>().BlockInput(value);
            }
        }

        #endregion Binding properties

        private void RefreshUIPresets()
        {
            Presets = _presetService.SharedPresetCollection;
            PresetsHeader = $"Presets ({_presetStorage.TotalPresets})";
        }

        private void LoadRunConfiguration(RunConfiguration config)
        {
            BlockKeys = config.BlockInput;
            RandomSeconds = config.RandomSeconds.ToString();
            RandomMilliseconds = config.RandomMilliseconds.ToString();
            Seconds = config.Seconds.ToString();
            Milliseconds = config.Milliseconds.ToString();
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshUIPresets();
        }

        private void SetDefaultConfiguration()
        {
            BlockKeys = Constants.DefaultConfig.BlockInput;
            Seconds = Constants.DefaultConfig.Seconds.ToString();
            Milliseconds = Constants.DefaultConfig.Milliseconds.ToString();
            PresetsHeader = $"Presets ({_presetStorage.TotalPresets})";
            RandomSeconds = Constants.DefaultConfig.RandomSeconds.ToString();
            RandomMilliseconds = Constants.DefaultConfig.RandomMilliseconds.ToString();
        }
    }
}