using System;
using System.Windows;
using System.Windows.Threading;

namespace WPF_MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _positionSliderDragging;
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Tick += (sender, e) =>
            {
                if (!_positionSliderDragging)
                    PositionSlider.Value = MainMediaElement.Position.TotalMilliseconds;
            };
        }

        private void MainMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            PositionSlider.Maximum = MainMediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            SpeedSlider.Value = 1;
        }

        private void MainMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            MainMediaElement.Stop();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            MainMediaElement.SpeedRatio = SpeedSlider.Value;
            MainMediaElement.Volume = VolumeSlider.Value;
            MainMediaElement.Balance = BalanceSlider.Value;
            MainMediaElement.Play();
            _timer.Start();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            MainMediaElement.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            MainMediaElement.Stop();
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainMediaElement.Volume = VolumeSlider.Value;
        }

        private void PositionSlider_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _positionSliderDragging = true;
            MainMediaElement.Stop();
        }

        private void PositionSlider_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _positionSliderDragging = false;
            MainMediaElement.Play();

        }

        private void PositionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_positionSliderDragging)
                MainMediaElement.Position = TimeSpan.FromMilliseconds(PositionSlider.Value);
        }

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainMediaElement.SpeedRatio = SpeedSlider.Value;
        }

        private void BalanceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainMediaElement.Balance = BalanceSlider.Value;
        }

    }
}
