using System;
using System.Windows;

namespace WPF_MediaPlayer
{
    /// <summary>
    /// Interaction logic for ControlPanelWindow.xaml
    /// </summary>
    public partial class ControlPanelWindow : Window
    {
        public event EventHandler<double> SpeedChangedEvent;

        public event EventHandler<double> BalanceChangedEvent;

        public event EventHandler CloseControlPanelEvent;


        public ControlPanelWindow()
        {
            InitializeComponent();
        }

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SpeedChangedEvent?.Invoke(this, SpeedSlider.Value);
        }

        private void BalanceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BalanceChangedEvent?.Invoke(this, BalanceSlider.Value);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseControlPanelEvent?.Invoke(this, null);
        }
    }
}
