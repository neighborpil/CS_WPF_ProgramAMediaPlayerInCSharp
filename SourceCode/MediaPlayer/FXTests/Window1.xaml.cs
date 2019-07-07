using System.Windows;
using System.Windows.Media.Effects;

namespace FXTests {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {
		public Window1() {
			InitializeComponent();
		}


        private void Fx1( object sender, RoutedEventArgs e ) {
			BlurEffect myBlurEffect = new BlurEffect();
			myBlurEffect.Radius = 0;			
			myBlurEffect.KernelType = KernelType.Box;
			button1.Effect = myBlurEffect;

			for( int i = 0; i <= 20; i++ ) {
				myBlurEffect.Radius = i;
				System.Threading.Thread.Sleep(50);			// delay
				System.Windows.Forms.Application.DoEvents();// requires reference to Windows.Forms                
			}

			for( int i = 20; i >= 0; i-- ) {
				myBlurEffect.Radius = i;
				System.Threading.Thread.Sleep(50);
				System.Windows.Forms.Application.DoEvents();
			}
			
		}
	}
}
