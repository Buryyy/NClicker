using NClicker.Models;
using NClicker.ViewModels;
using System.Windows;

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
            DataContext = new CreatePresetViewModel(configuration);
        }
    }
}