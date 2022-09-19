using System.Windows.Navigation;

namespace NClicker.Views
{
    /// <summary>
    /// Interaction logic for LicenseView.xaml
    /// </summary>
    public partial class LicenseView
    {
        public LicenseView()
        {
            InitializeComponent();
        }

        private void LinkOnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }
}