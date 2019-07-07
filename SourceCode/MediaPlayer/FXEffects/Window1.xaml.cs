using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace EffectSample {
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window {
		public Window1() {
			InitializeComponent();
		}

		private void button1_Click( object sender, RoutedEventArgs e ) {
			// Animate the Button's Width
			DoubleAnimation widthAnimation = new DoubleAnimation(75, 300, TimeSpan.FromSeconds(5));
			widthAnimation.RepeatBehavior = RepeatBehavior.Forever;
			widthAnimation.AutoReverse = true;
			button1.BeginAnimation(Button.WidthProperty, widthAnimation);
			// Animate the Button's Colour
			SolidColorBrush myBrush = new SolidColorBrush();			
			button1.Background = myBrush;
			ColorAnimation myColorAnimation = new ColorAnimation();
			myColorAnimation.From = Colors.Lavender;
			myColorAnimation.To = Colors.Red;
			myColorAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(5000));
			myColorAnimation.AutoReverse = true;
			myColorAnimation.RepeatBehavior = RepeatBehavior.Forever;			
			myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
		
		}

		private void button2_Click( object sender, RoutedEventArgs e ) {
			// Animate Blur effect
			DoubleAnimation blurAnimation = new DoubleAnimation(0, 10, TimeSpan.FromMilliseconds(500));
			blurAnimation.RepeatBehavior = RepeatBehavior.Forever;
			blurAnimation.AutoReverse = true;
			BlurEffect myBlurEffect = new BlurEffect();
			myBlurEffect.Radius = 0;
			myBlurEffect.KernelType = KernelType.Box;
			button2.Effect = myBlurEffect;
			button2.Effect.BeginAnimation(BlurEffect.RadiusProperty, blurAnimation);

		}

        /* OuterGlowBitmapEffect is deprecated. Try substituting one of the glow 'drop shadow' alternatives
         * I used in the FXEffects project...
         * */
		private void button3_Click( object sender, RoutedEventArgs e ) {
			// Animate Glow effect
			DoubleAnimation glowAnimation = new DoubleAnimation(0, 50, TimeSpan.FromMilliseconds(2000));
			glowAnimation.RepeatBehavior = RepeatBehavior.Forever;
			glowAnimation.AutoReverse = true;
			OuterGlowBitmapEffect myGlowEffect = new OuterGlowBitmapEffect();			
			myGlowEffect.GlowColor = Colors.Violet;			
			myGlowEffect.GlowSize = 0;
			button3.BitmapEffect = myGlowEffect;
			button3.BitmapEffect.BeginAnimation(OuterGlowBitmapEffect.GlowSizeProperty, glowAnimation);
		}

		private void ellipse1_MouseEnter( object sender, System.Windows.Input.MouseEventArgs e ) {
			// Animate Glow effect
			DoubleAnimation glowAnimation = new DoubleAnimation(0, 20, TimeSpan.FromMilliseconds(500));
			glowAnimation.RepeatBehavior = new RepeatBehavior(2);
			glowAnimation.AutoReverse = true;
			OuterGlowBitmapEffect myGlowEffect = new OuterGlowBitmapEffect();
			myGlowEffect.GlowColor = Colors.Aquamarine;
			myGlowEffect.GlowSize = 0;
			ellipse1.BitmapEffect = myGlowEffect;
			ellipse1.BitmapEffect.BeginAnimation(OuterGlowBitmapEffect.GlowSizeProperty, glowAnimation);
			ellipse1.Fill = new SolidColorBrush( Colors.Cyan );
		}

		private void ellipse1_MouseLeave( object sender, System.Windows.Input.MouseEventArgs e ) {
			ellipse1.Fill = new SolidColorBrush(Colors.CornflowerBlue);
		}
	}
}
