using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace WPF_MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _positionSliderDragging;
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        private string _currentTrackPath = "";


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

        private void MainMediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show($"Unable to play {_currentTrackPath} [{e.ErrorException.Message}]");
        }

        private void MainMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            MainMediaElement.Stop();

            PlayNextTrack();
        }

        private void PlayNextTrack()
        {
            var numberOfTracks = GetNumberOfTracks();
            if (numberOfTracks > 0)
            {
                var nextTrackIndex = GetNextTrackIndex(numberOfTracks);
                PlayListBox.SelectedIndex = nextTrackIndex;
                PlayPlayList();
            }
        }

        private int GetNumberOfTracks()
        {
            int numberOfTracks = -1;
            numberOfTracks = PlayListBox.Items.Count;
            return numberOfTracks;
        }

        private int GetNextTrackIndex(int numberOfTracks)
        {
            int nextTrackIndex = -1;
            nextTrackIndex = PlayListBox.SelectedIndex + 1;
            if (nextTrackIndex >= numberOfTracks)
                nextTrackIndex = 0;

            return nextTrackIndex;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayListBox.Items.Count > 0)
            {
                PlayPlayList();
            }
            else
            {
                PlayTrack();
            }
        }

        private void PlayPlayList()
        {
            if (PlayListBox.Items.Count > 0)
            {
                int selectedItemIndex = -1;
                selectedItemIndex = PlayListBox.SelectedIndex;

                if (selectedItemIndex > -1)
                {
                    _currentTrackPath = PlayListBox.Items[selectedItemIndex].ToString();
                    TrackLabel.Content = _currentTrackPath;
                    PlayTrack();
                }
            }
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

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            bool? result;

            var dialog = new OpenFileDialog
            {
                FileName = "",
                DefaultExt = "All files(*.*)",
                Filter = ".mp3|*.mp3|.mpg|*.mpg|.wmv|*.wmv|All files(*.*)|*.*",
                CheckFileExists = true
            };
            result = dialog.ShowDialog();

            if (result == true)
            {
                AddItemToPlayList();
                SetPlayTrack(dialog.FileName);
                PlayTrack();
            }
        }

        private void SetPlayTrack(string fileName)
        {
            _currentTrackPath = fileName;
            TrackLabel.Content = _currentTrackPath;
        }

        private void AddItemToPlayList()
        {
            PlayListBox.Items.Clear();
            PlayListBox.Visibility = Visibility.Hidden;
        }

        private void PlayTrack()
        {
            FileInfo info = null;

            try
            {
                info = new FileInfo(_currentTrackPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (info != null && info.Exists)
            {
                var source = new Uri(_currentTrackPath);
                MainMediaElement.Source = source;
                MainMediaElement.SpeedRatio = SpeedSlider.Value;
                MainMediaElement.Volume = VolumeSlider.Value;
                MainMediaElement.Balance = BalanceSlider.Value;
                MainMediaElement.Play();

                _timer.Start();
            }
            else
                MessageBox.Show($"Cannot find {_currentTrackPath}");

        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = null;

            folderPath = GetDirectoryOfTracks(folderPath);

            if (!string.IsNullOrWhiteSpace(folderPath))
                AddTracksToPlayListBox(folderPath);

        }
        
        private string GetDirectoryOfTracks(string folderPath)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                var dialogResult = dialog.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                    folderPath = dialog.SelectedPath;
            }

            return folderPath;
        }

        private void AddTracksToPlayListBox(string folderPath)
        {
            PlayListBox.Items.Clear();
            PlayListBox.Visibility = Visibility.Visible;
            var files = Directory.GetFiles(folderPath, "*.mp3");

            foreach (var file in files)
                PlayListBox.Items.Add(file);

            PlayListBox.SelectedIndex = 0;
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
