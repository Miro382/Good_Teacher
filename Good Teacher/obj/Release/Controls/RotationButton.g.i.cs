﻿#pragma checksum "..\..\..\Controls\RotationButton.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "06F72174B6E700ED829F457B062F22ABFAA237E7A733FC94D233DBC8287AB284"
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
    /// RotationButton
    /// </summary>
    public partial class RotationButton : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\Controls\RotationButton.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse ellipse;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Controls\RotationButton.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid RotatePointer;
        
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
            System.Uri resourceLocater = new System.Uri("/Good Teacher;component/controls/rotationbutton.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\RotationButton.xaml"
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
            this.ellipse = ((System.Windows.Shapes.Ellipse)(target));
            
            #line 12 "..\..\..\Controls\RotationButton.xaml"
            this.ellipse.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Ellipse_PreviewMouseDown);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\Controls\RotationButton.xaml"
            this.ellipse.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.ellipse_PreviewMouseUp);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\Controls\RotationButton.xaml"
            this.ellipse.PreviewMouseMove += new System.Windows.Input.MouseEventHandler(this.ellipse_PreviewMouseMove);
            
            #line default
            #line hidden
            return;
            case 2:
            this.RotatePointer = ((System.Windows.Controls.Grid)(target));
            
            #line 14 "..\..\..\Controls\RotationButton.xaml"
            this.RotatePointer.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Ellipse_PreviewMouseDown);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\Controls\RotationButton.xaml"
            this.RotatePointer.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.ellipse_PreviewMouseUp);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\Controls\RotationButton.xaml"
            this.RotatePointer.PreviewMouseMove += new System.Windows.Input.MouseEventHandler(this.ellipse_PreviewMouseMove);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

