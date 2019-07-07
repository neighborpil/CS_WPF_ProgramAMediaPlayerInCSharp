using System.Windows;
using System.Windows.Input;

namespace Player2 {
    /// <summary>
    /// Interaction logic for ControlPanel.xaml
    /// </summary>
    public partial class ControlPanel : Window {


        public ControlPanel(double balance, double speed) {
            InitializeComponent();
            balanceSlider.Value = balance;
            speedSlider.Value = speed;
        }


        // SpeedSlider

        private void SpeedSlider_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            Window1.mainwin.SaveMediaPos();
            Window1.mainwin.StopMedia();            
        }

        private void SpeedSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e) {
            Window1.mainwin.RestoreMediaPos();
            Window1.mainwin.PlayMedia();
        }

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            Window1.mainwin.SetSpeed(speedSlider.Value);
        }

        // BalanceSlider

        private void BalanceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            Window1.mainwin.SetBalance(balanceSlider.Value);
        }

        public double BalanceValue() {
            return balanceSlider.Value;
        }

        public double SpeedValue() {
            return speedSlider.Value;
        }
    }
}
