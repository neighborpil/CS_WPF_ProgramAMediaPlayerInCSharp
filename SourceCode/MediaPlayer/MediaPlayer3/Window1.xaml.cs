using System;
using System.IO; 
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using System.Collections;

namespace Player2 {
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window {

		DispatcherTimer timer = new DispatcherTimer();
		bool posSliderDragging = false;
		bool trackPaused = false;
		String trackPath = ""; // full path to file of currently playing track	
		private string datafile = "default.playlist";


		public Window1() {
			InitializeComponent();
			timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
			timer.Tick += new EventHandler(Timer_Tick);
		}

		private void Timer_Tick( object sender, EventArgs e ) {
			if( !posSliderDragging ) {
				posSlider.Value = media.Position.TotalMilliseconds;
			}
		}

		private void ContinuePlaying() {
			trackPaused = false;
			// assign the defaults (from slider positions) when a track starts playing
			media.SpeedRatio = speedSlider.Value;
			media.Volume = volSlider.Value;
			media.Balance = balanceSlider.Value;
			media.Play();
			timer.Start();
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
					media.Source = src;
					ContinuePlaying();
				}
			}
		}

		private void StartBtn_Click( object sender, RoutedEventArgs e ) {
			if( trackPaused ) {
				ContinuePlaying();
			} else {
				if( playlistBox.Items.Count > 0 ) {
					PlayPlaylist();
				} else {
					PlayTrack();
				}
			}
		}

		private void StopBtn_Click( object sender, RoutedEventArgs e ) {
			media.Stop();
			timer.Stop();
		}

		private void PauseBtn_Click( object sender, RoutedEventArgs e ) {
			media.Pause();
			trackPaused = true;
		}

		private void Media_MediaOpened( object sender, RoutedEventArgs e ) {
			posSlider.Maximum = media.NaturalDuration.TimeSpan.TotalMilliseconds;
			speedSlider.Value = 1;
		}

		// Stop payback when position is changed so that you don't get any
		// 'seek-jiggle' as you move the slider
		private void PosSlider_PreviewMouseDown( object sender, MouseButtonEventArgs e ) {
			posSliderDragging = true;
			media.Stop();
		}

		// and start again when you've finished
		private void PosSlider_PreviewMouseUp( object sender, MouseButtonEventArgs e ) {
			posSliderDragging = false;
			media.Play();
		}

		private void PosSlider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e ) {
			int SliderValue = (int)posSlider.Value;
			if( posSliderDragging ) {
				media.Position = TimeSpan.FromMilliseconds(SliderValue);
			}
		}

		private void VolSlider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e ) {
			media.Volume = volSlider.Value;
		}

		private void SpeedSlider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e ) {
			media.SpeedRatio = speedSlider.Value;
		}


		private void BalanceSlider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e ) {
			media.Balance = balanceSlider.Value;
		}


		private void Media_MediaEnded( object sender, RoutedEventArgs e ) {
			int nextTrackIndex = -1;
			int numberOfTracks = -1;
			media.Stop();
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
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result;
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


		private ArrayList PlaylistTracks() {
			int i = 0;
			string trackname = "";
			int playListSize = playlistBox.Items.Count;
			ArrayList tracks = new ArrayList();
			if( playListSize > 0 ) {
				for( i = 0; i < playListSize; i++ ) {
					trackname = playlistBox.Items[i].ToString();
					tracks.Add(trackname);
				}
			}
			return tracks;
		}

		private void OpenPlaylist_Click( object sender, RoutedEventArgs e ) {
			XmlDocument xdoc = new XmlDocument();
			XmlNodeList xmlNodes;
			XmlNode pathNode;
			string track;
			bool ok = true;
			try {
				xdoc.Load(datafile);
			} catch( Exception ex ) {
				MessageBox.Show(ex.Message, "Error Loading XML Data");
				ok = false;
			}
			if( ok ) {
				playlistBox.Items.Clear();
				playlistBox.Visibility = Visibility.Visible;
				xmlNodes = xdoc.SelectNodes("Playlist/track");
				foreach( XmlNode node in xmlNodes ) {
					pathNode = node.SelectSingleNode("path");
					track = pathNode.InnerText;
					playlistBox.Items.Add(track);
				}
				playlistBox.SelectedIndex = 0;
			}
		}

        private void SavePlaylist_Click(object sender, RoutedEventArgs e) {
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter xmlwr;
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            xmlwr = XmlWriter.Create(datafile, settings);
            ArrayList albumData = PlaylistTracks();
            if (albumData.Count == 0) {
                MessageBox.Show("No tracks to save!", "Error");
            } else {
                xmlwr.WriteStartElement("Playlist");
                foreach (string s in albumData) {
                    xmlwr.WriteStartElement("track");
                    xmlwr.WriteElementString("path", s);
                    xmlwr.WriteEndElement();
                }
                xmlwr.WriteEndElement();
                xmlwr.Close();
            }
        }

        private void CloseApp_Click( object sender, RoutedEventArgs e ) {
			this.Close();
		}

		private void Media_MediaFailed( object sender, ExceptionRoutedEventArgs e ) {
			// This fires if the media.Source can't be found or can't be played
			MessageBox.Show("Unable to play " + trackPath + " [" + e.ErrorException.Message + "]");
		}


		private bool IsValidTrack( string aTrack ) {
			return (aTrack.EndsWith(".mp3"));
		}

		private void Files_Drop( object sender, DragEventArgs e ) {
			string[] trackpaths = e.Data.GetData(DataFormats.FileDrop) as string[];
			foreach( string s in trackpaths ) {
				if( IsValidTrack(s) ) {
					playlistBox.Items.Add(s);
				}
			}
			if( playlistBox.Items.Count > 0 ) {
				playlistBox.Visibility = Visibility.Visible;
				playlistBox.SelectedIndex = 0;
			}
		}








	}
}
