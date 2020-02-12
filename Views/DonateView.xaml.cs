using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace NClicker.Views
{
    /// <summary>
    /// Interaction logic for DonateView.xaml
    /// </summary>
    public partial class DonateView
    {
        public DonateView()
        {
            InitializeComponent();
        }

        private static void LinkOnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        private void Paypal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Constants.PaypalUrl);
        }
    }
}