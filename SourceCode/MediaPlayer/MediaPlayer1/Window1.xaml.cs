using System;
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

		private void StartBtn_Click( object sender, RoutedEventArgs e ) {
			// assign the defaults (from slider positions) when a track starts playing
			Me.SpeedRatio = SpeedSlider.Value;
			Me.Volume = VolumeSlider.Value;	
			Me.Balance = BalanceSlider.Value;
			Me.Play();			
		    timer.Start();
		}

		private void StopBtn_Click( object sender, RoutedEventArgs e ) {
			Me.Stop();
			timer.Stop();
		}

		private void PauseBtn_Click( object sender, RoutedEventArgs e ) {
			Me.Pause();
		}
		
		private void Me_MediaOpened( object sender, RoutedEventArgs e ) {
			PosSlider.Maximum = Me.NaturalDuration.TimeSpan.TotalMilliseconds;
			SpeedSlider.Value = 1;
		}

		// Stop payback when position is changed 		
		private void PosSlider_PreviewMouseDown( object sender, MouseButtonEventArgs e ) {
			posSliderDragging = true;
			Me.Stop();			// to stop 'fast forward' effect when dragging position
		}

		// and start again when you've finished
		private void PosSlider_PreviewMouseUp( object sender, MouseButtonEventArgs e ) {
			posSliderDragging = false;
			Me.Play();			
		}

		private void PosSlider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e ) {			
			if( posSliderDragging ) {                
                Me.Position = TimeSpan.FromMilliseconds(PosSlider.Value);
           }
		}

		private void VolSlider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e ) {
			Me.Volume = VolumeSlider.Value;
		}		

		private void SpeedSlider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e ) {
			Me.SpeedRatio = SpeedSlider.Value;
		}		

		private void BalanceSlider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e ) {
			Me.Balance = BalanceSlider.Value;
		}

		private void Me_MediaEnded( object sender, RoutedEventArgs e ) {			
			Me.Stop();
		}

        private void SpeedSlider_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            Me.Pause();
        }

        private void SpeedSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e) {
            Me.Play();
        }
    }
}
