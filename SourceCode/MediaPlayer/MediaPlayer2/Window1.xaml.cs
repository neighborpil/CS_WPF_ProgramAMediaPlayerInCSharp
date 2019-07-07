using System;
using System.IO; 
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading; // needed for DispatcherTimer

namespace Player2 {
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window {

		DispatcherTimer timer = new DispatcherTimer();
		bool posSliderDragging = false;
        
        String trackPath = ""; // full path to file of currently playing track		


		public Window1() {
			InitializeComponent();
			timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
			timer.Tick += new EventHandler(Timer_Tick);
		}

		private void Timer_Tick( object sender, EventArgs e ) {
			if( !posSliderDragging ) {
				PosSlider.Value = Me.Position.TotalMilliseconds;
			}
		}

		private void PlayPlaylist() {
			int selectedItemIndex = -1;
			if( playlistBox.Items.Count > 0 ) {
				selectedItemIndex = playlistBox.SelectedIndex;
				if( selectedItemIndex > -1 ) {
					trackPath = playlistBox.Items[selectedItemIndex].ToString();
					trackLabel.Content = trackPath;
					PlayTrack();
				}
			}

		}

		private void PlayTrack() {
			bool ok = true;
			FileInfo fi = null;
            Uri src;            
            try {
				fi = new FileInfo(trackPath);
			} catch( Exception ex ) {
				MessageBox.Show(ex.Message);
				ok = false;
			}
			if( ok ) {
				// check that the file actually exists
				if( !fi.Exists ) {
					MessageBox.Show("Cannot find " + trackPath);
				} else {
					// trackPath += "xxx"; // uncomment to fake a failed file path!
					// if the MediaElement can find its Source the MediFailed event-handler should take over..
					src = new Uri(trackPath);
					Me.Source = src;
					// assign the defaults (from slider positions) when a track starts playing
					Me.SpeedRatio = SpeedSlider.Value;
					Me.Volume = VolSlider.Value;
					Me.Balance = BalanceSlider.Value;
					Me.Play();
					timer.Start();
				}
			}
		}

		private void StartBtn_Click( object sender, RoutedEventArgs e ) {
			if( playlistBox.Items.Count > 0 ) {
				PlayPlaylist();
			} else {
				PlayTrack();
			}
		}

		private void StopBtn_Click( object sender, RoutedEventArgs e ) {
			Me.Stop();
			timer.Stop();
		}

		private void PauseBtn_Click( object sender, RoutedEventArgs e ) {
			Me.Pause();
		}

		private void Media_MediaOpened( object sender, RoutedEventArgs e ) {
			PosSlider.Maximum = Me.NaturalDuration.TimeSpan.TotalMilliseconds;
			SpeedSlider.Value = 1;
		}

		// Stop payback when position is changed so that you don't get any
		// 'seek-jiggle' as you move the slider
		private void PosSlider_PreviewMouseDown( object sender, MouseButtonEventArgs e ) {
			posSliderDragging = true;
			Me.Stop();
		}

		// and start again when you've finished
		private void PosSlider_PreviewMouseUp( object sender, MouseButtonEventArgs e ) {
			posSliderDragging = false;
			Me.Play();
		}

		private void PosSlider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e ) {
			int SliderValue = (int)PosSlider.Value;
			if( posSliderDragging ) {
				Me.Position = TimeSpan.FromMilliseconds(SliderValue);
			}
		}

		private void VolSlider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e ) {
			Me.Volume = VolSlider.Value;
		}


		private void SpeedSlider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e ) {
			Me.SpeedRatio = SpeedSlider.Value;
		}


		private void BalanceSlider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e ) {
			Me.Balance = BalanceSlider.Value;
		}


		private void Media_MediaEnded( object sender, RoutedEventArgs e ) {
			int nextTrackIndex = -1;
			int numberOfTracks = -1;
			Me.Stop();
			numberOfTracks = playlistBox.Items.Count;
			if( numberOfTracks > 0 ) {
				nextTrackIndex = playlistBox.SelectedIndex + 1;
				if( nextTrackIndex >= numberOfTracks ) {
					nextTrackIndex = 0;
				}
				playlistBox.SelectedIndex = nextTrackIndex;
				PlayPlaylist();
			}
		}


		// File processing		

		private void OpenFile_Click( object sender, RoutedEventArgs e ) {
            Nullable<bool> result;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.FileName = "";
			dlg.DefaultExt = ".mp3";
			dlg.Filter = ".mp3|*.mp3|.mpg|*.mpg|.wmv|*.wmv|All files (*.*)|*.*";
			dlg.CheckFileExists = true;
			result = dlg.ShowDialog();
			if( result == true ) {
				playlistBox.Items.Clear();
				playlistBox.Visibility = Visibility.Hidden;
				// Open document
				trackPath = dlg.FileName;
				trackLabel.Content = trackPath;
				PlayTrack();
			}

		}

		private void OpenFolder_Click( object sender, RoutedEventArgs e ) {
			String folderpath = "";
            string[] files;
            // Note: You must browse to add a reference to System.Windows.Forms
            // in Solution Explorer in order to have access to the FolderBrowserDialog			
            System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();
			System.Windows.Forms.DialogResult result = fd.ShowDialog();
			if( result == System.Windows.Forms.DialogResult.OK ) {
				folderpath = fd.SelectedPath;
			}
			if( folderpath != "" ) {
				playlistBox.Items.Clear();
				playlistBox.Visibility = Visibility.Visible;
				files = Directory.GetFiles(folderpath, "*.mp3");
				foreach( string fn in files ) {
					playlistBox.Items.Add(fn);
				}
				playlistBox.SelectedIndex = 0;
			}
		}

		private void CloseApp_Click( object sender, RoutedEventArgs e ) {
			this.Close();
		}

		private void Media_MediaFailed( object sender, ExceptionRoutedEventArgs e ) {
			// This fires if the media.Source can't be found or can't be played
			MessageBox.Show("Unable to play " + trackPath + " [" + e.ErrorException.Message + "]");
		}

	}
}
