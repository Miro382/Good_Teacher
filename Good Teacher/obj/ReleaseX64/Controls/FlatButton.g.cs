﻿#pragma checksum "..\..\..\Controls\FlatButton.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BE59F2BE4AA3D7D2E6666C671FD7A2DF6DADA26D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Good_Teacher.Controls;
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


namespace Good_Teacher.Controls {
    
    
    /// <summary>
    /// FlatButton
    /// </summary>
    public partial class FlatButton : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\Controls\FlatButton.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.FlatButton flatbutton;
        
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
            System.Uri resourceLocater = new System.Uri("/Good Teacher;component/controls/flatbutton.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\FlatButton.xaml"
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
            this.flatbutton = ((Good_Teacher.Controls.FlatButton)(target));
            
            #line 8 "..\..\..\Controls\FlatButton.xaml"
            this.flatbutton.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.flatbutton_MouseDown);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\Controls\FlatButton.xaml"
            this.flatbutton.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.flatbutton_MouseUp);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\Controls\FlatButton.xaml"
            this.flatbutton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.flatbutton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\Controls\FlatButton.xaml"
            this.flatbutton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.flatbutton_MouseLeave);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

