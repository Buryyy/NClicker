using Autofac;
using NClicker.Models;
using NClicker.Services;
using NClicker.Storage;
using NClicker.ViewModels;

namespace NClicker.Views
{
    /// <summary>
    /// Interaction logic for CreatePresetView.xaml
    /// </summary>
    public partial class CreatePresetView
    {
        public CreatePresetView(RunConfiguration configuration)
        {
            InitializeComponent();
            var presetStorage = App.Context.Resolve<IPresetStorage>();
            var presetService = App.Context.Resolve<IPresetService>();
            DataContext = new CreatePresetViewModel(configuration, presetStorage, presetService);
        }
    }
}