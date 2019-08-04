using Shell32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Xml;
using DataFormats = System.Windows.DataFormats;
using DragEventArgs = System.Windows.DragEventArgs;
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
                ShowTrackInfo();
                PlayTrack();
            }
        }

        private void AddItemToPlayList()
        {
            PlayListBox.Items.Clear();
            PlayListBox.Visibility = Visibility.Hidden;
        }

        private void SetPlayTrack(string fileName)
        {
            _currentTrackPath = fileName;
            TrackLabel.Content = _currentTrackPath;
        }


        private void ShowTrackInfo()
        { 
            InfoTextBox.Clear();

            string directoryName = Path.GetDirectoryName(_currentTrackPath);
            string fileName = Path.GetFileName(_currentTrackPath);

            GetFolderIno(directoryName, fileName);
        }

        private void GetFolderIno(string directoryName, string fileName)
        {
            var shell = new Shell();
            Folder folder = shell.NameSpace(directoryName);
            FolderItem folderItem = folder.ParseName(fileName);

            for (int i = 0; i < 315; i++)
            {
                var header = GetHeaderInfo(folder, i);
                var data = GetDataInfo(folder, folderItem, i);

                AppendToInfoBox(i, header, data);
            }
        }
       
        private static string GetHeaderInfo(Folder folder, int i)
        {
            var header = folder.GetDetailsOf(null, i);
            if (string.IsNullOrWhiteSpace(header))
                header = "[Unknown header]";
            return header;
        }
       
        private static string GetDataInfo(Folder folder, FolderItem folderItem, int i)
        {
            var data = folder.GetDetailsOf(folderItem, i);
            if (string.IsNullOrWhiteSpace(data))
                data = "[No data]";
            return data;
        }

        private void AppendToInfoBox(int i, string header, string data)
        {
            InfoTextBox.AppendText($"\n{i} {header} : {data}");
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
        #region PlayList

        private void OpenPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var document = new XmlDocument();

            bool isOk = true;

            try
            {
                document.Load(Settings.CdDataFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading XML Data");
                isOk = false;
            }

            if (isOk)
            {
                PlayListBox.Items.Clear();
                PlayListBox.Visibility = Visibility.Visible;
                var xmlNodes = document.SelectNodes("Playlist/Track");

                foreach (XmlNode node in xmlNodes)
                {
                    string track = GetNodeText(node, "Path");
                    PlayListBox.Items.Add(track);
                }

                PlayListBox.SelectedIndex = 0;
            }
        }

        private static string GetNodeText(XmlNode node, string name)
        {
            var singleNode = node.SelectSingleNode(name);
            string innerText = "-";
            if (singleNode != null)
                innerText = singleNode.InnerText;

            return innerText;
        }

        private void SavePlayList_Click(object sender, RoutedEventArgs e)
        {
            List<string> albumData = GetPlayListTracks();

            if (albumData.Count == 0)
            {
                MessageBox.Show("No tracks to save!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            WritePlayListTracks(albumData);
        }

        private static void WritePlayListTracks(List<string> albumData)
        {
            using (var xmlWriter = XmlWriter.Create(Settings.CdDataFileName, Settings.XmlWriterSettings))
            {
                xmlWriter.WriteStartElement("Playlist");
                foreach (var path in albumData)
                {
                    xmlWriter.WriteStartElement("Track");
                    xmlWriter.WriteElementString("Path", path);
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
            }
        }

        private List<string> GetPlayListTracks()
        {
            var trackPaths = new List<string>();

            foreach (var path in PlayListBox.Items)
            {
                trackPaths.Add(path.ToString());
            }

            return trackPaths;
        }


        private void ShowControlPanel_OnClick(object sender, RoutedEventArgs e)
        {
            var controlPanelWindow = new ControlPanelWindow();
            controlPanelWindow.SpeedChangedEvent += OnControlPanelWindowOnSpeedChangedEvent;
            controlPanelWindow.BalanceChangedEvent += OnControlPanelWindowOnBalanceChangedEvent;
            controlPanelWindow.CloseControlPanelEvent += (senderControl, eControl) =>
                {
                    (senderControl as ControlPanelWindow).SpeedChangedEvent -= OnControlPanelWindowOnSpeedChangedEvent;
                    (senderControl as ControlPanelWindow).BalanceChangedEvent -= OnControlPanelWindowOnBalanceChangedEvent;
                };
            controlPanelWindow.Show();
        }

        private void OnControlPanelWindowOnBalanceChangedEvent(object senderControl, double eControl)
        {
            this.BalanceSlider.Value = eControl;
        }

        private void OnControlPanelWindowOnSpeedChangedEvent(object senderControl, double eControl)
        {
            this.SpeedSlider.Value = eControl;
        }

        #endregion


        #region Dropping down files

        private void Files_OnDrop(object sender, DragEventArgs e)
        {
            string[] trackPaths = e.Data.GetData(DataFormats.FileDrop) as string[];

            SortTrackPaths(trackPaths);

            AddTrackPathsToPlayListBox(trackPaths);

            MakePlayListBoxReady();
        }

        private static void SortTrackPaths(string[] trackPaths)
        {
            if (trackPaths != null)
                Array.Sort(trackPaths, StringComparer.InvariantCulture);
        }

        private void AddTrackPathsToPlayListBox(string[] trackPaths)
        {
            foreach (var path in trackPaths)
                if (IsValidTrack(path, FileType.MP3))
                    PlayListBox.Items.Add(path);
        }

        private bool IsValidTrack(string path, FileType fileType)
        {
            string end = "";
            switch (fileType)
            {
                case FileType.MP3:
                    end = ".mp3";
                    break;
                 default:
                     return false;
            }

            return path.EndsWith(end);
        }

        private void MakePlayListBoxReady()
        {
            if (PlayListBox.Items.Count > 0)
            {
                PlayListBox.Visibility = Visibility.Visible;
                PlayListBox.SelectedIndex = 0;
                _currentTrackPath = PlayListBox.Items[0].ToString();
                TrackLabel.Content = _currentTrackPath;
            }
        }

        #endregion

    }
}
