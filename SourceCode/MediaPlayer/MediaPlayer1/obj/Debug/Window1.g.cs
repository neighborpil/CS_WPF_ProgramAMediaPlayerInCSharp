﻿#pragma checksum "..\..\Window1.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F6873A66357CB3C9C465CF9BA455FA056975483E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Player2 {
    
    
    /// <summary>
    /// Window1
    /// </summary>
    public partial class Window1 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StopBtn;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PauseBtn;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StartBtn;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement Me;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider VolumeSlider;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider PosSlider;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label1;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label2;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider SpeedSlider;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label3;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider BalanceSlider;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label4;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Player2;component/window1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Window1.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.StopBtn = ((System.Windows.Controls.Button)(target));
            
            #line 6 "..\..\Window1.xaml"
            this.StopBtn.Click += new System.Windows.RoutedEventHandler(this.StopBtn_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PauseBtn = ((System.Windows.Controls.Button)(target));
            
            #line 7 "..\..\Window1.xaml"
            this.PauseBtn.Click += new System.Windows.RoutedEventHandler(this.PauseBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.StartBtn = ((System.Windows.Controls.Button)(target));
            
            #line 8 "..\..\Window1.xaml"
            this.StartBtn.Click += new System.Windows.RoutedEventHandler(this.StartBtn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Me = ((System.Windows.Controls.MediaElement)(target));
            
            #line 9 "..\..\Window1.xaml"
            this.Me.MediaOpened += new System.Windows.RoutedEventHandler(this.Me_MediaOpened);
            
            #line default
            #line hidden
            
            #line 9 "..\..\Window1.xaml"
            this.Me.MediaEnded += new System.Windows.RoutedEventHandler(this.Me_MediaEnded);
            
            #line default
            #line hidden
            return;
            case 5:
            this.VolumeSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 10 "..\..\Window1.xaml"
            this.VolumeSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.VolSlider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.PosSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 11 "..\..\Window1.xaml"
            this.PosSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.PosSlider_ValueChanged);
            
            #line default
            #line hidden
            
            #line 11 "..\..\Window1.xaml"
            this.PosSlider.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.PosSlider_PreviewMouseDown);
            
            #line default
            #line hidden
            
            #line 11 "..\..\Window1.xaml"
            this.PosSlider.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.PosSlider_PreviewMouseUp);
            
            #line default
            #line hidden
            return;
            case 7:
            this.label1 = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.label2 = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.SpeedSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 14 "..\..\Window1.xaml"
            this.SpeedSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SpeedSlider_ValueChanged);
            
            #line default
            #line hidden
            
            #line 14 "..\..\Window1.xaml"
            this.SpeedSlider.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.SpeedSlider_PreviewMouseDown);
            
            #line default
            #line hidden
            
            #line 14 "..\..\Window1.xaml"
            this.SpeedSlider.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SpeedSlider_PreviewMouseUp);
            
            #line default
            #line hidden
            return;
            case 10:
            this.label3 = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.BalanceSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 16 "..\..\Window1.xaml"
            this.BalanceSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.BalanceSlider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.label4 = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
