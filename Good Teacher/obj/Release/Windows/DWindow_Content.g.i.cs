﻿#pragma checksum "..\..\..\Windows\DWindow_Content.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "52F7BC18076F634C81DCC19C8EAC64011B1B7EB7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Good_Teacher.Strings;
using Good_Teacher.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using System.Windows.Shell;


namespace Good_Teacher.Windows {
    
    
    /// <summary>
    /// DWindow_Content
    /// </summary>
    public partial class DWindow_Content : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\Windows\DWindow_Content.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame editor;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Windows\DWindow_Content.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ContentPanel;
        
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
            System.Uri resourceLocater = new System.Uri("/Good Teacher;component/windows/dwindow_content.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\DWindow_Content.xaml"
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
            this.editor = ((System.Windows.Controls.Frame)(target));
            return;
            case 2:
            this.ContentPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            
            #line 56 "..\..\..\Windows\DWindow_Content.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonAddImage_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 60 "..\..\..\Windows\DWindow_Content.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonAddText_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 64 "..\..\..\Windows\DWindow_Content.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonAddSplitter_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 68 "..\..\..\Windows\DWindow_Content.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonAddPageNumber_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 72 "..\..\..\Windows\DWindow_Content.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonAddAnswersCount_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 76 "..\..\..\Windows\DWindow_Content.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonAddDateCount_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 83 "..\..\..\Windows\DWindow_Content.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonLeft_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 87 "..\..\..\Windows\DWindow_Content.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonRight_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 91 "..\..\..\Windows\DWindow_Content.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonDelete_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 96 "..\..\..\Windows\DWindow_Content.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonOK_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

