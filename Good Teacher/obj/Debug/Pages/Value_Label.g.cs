﻿#pragma checksum "..\..\..\Pages\Value_Label.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CA92289203A392056BCD3E1D39B22D4A6A2EA31A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Good_Teacher;
using Good_Teacher.Controls;
using Good_Teacher.Controls.Special;
using Good_Teacher.Strings;
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


namespace Good_Teacher {
    
    
    /// <summary>
    /// Value_Label
    /// </summary>
    public partial class Value_Label : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\Pages\Value_Label.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Box_text;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Pages\Value_Label.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.Special.PositionSelector positionselector;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Pages\Value_Label.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.Special.FontEditorPanel fontEditorPanel;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Pages\Value_Label.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.Special.BrushSelector brushselectorBack;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Pages\Value_Label.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.Special.BrushSelector brushselectorFor;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Pages\Value_Label.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.Special.EffectSelector effectselector;
        
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
            System.Uri resourceLocater = new System.Uri("/Good Teacher;component/pages/value_label.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Value_Label.xaml"
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
            this.Box_text = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\..\Pages\Value_Label.xaml"
            this.Box_text.KeyUp += new System.Windows.Input.KeyEventHandler(this.Box_text_KeyUp);
            
            #line default
            #line hidden
            
            #line 28 "..\..\..\Pages\Value_Label.xaml"
            this.Box_text.KeyDown += new System.Windows.Input.KeyEventHandler(this.Control_KeyDown);
            
            #line default
            #line hidden
            
            #line 28 "..\..\..\Pages\Value_Label.xaml"
            this.Box_text.LostFocus += new System.Windows.RoutedEventHandler(this.Control_LostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 30 "..\..\..\Pages\Value_Label.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.WriteText_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.positionselector = ((Good_Teacher.Controls.Special.PositionSelector)(target));
            return;
            case 4:
            this.fontEditorPanel = ((Good_Teacher.Controls.Special.FontEditorPanel)(target));
            return;
            case 5:
            this.brushselectorBack = ((Good_Teacher.Controls.Special.BrushSelector)(target));
            return;
            case 6:
            this.brushselectorFor = ((Good_Teacher.Controls.Special.BrushSelector)(target));
            return;
            case 7:
            this.effectselector = ((Good_Teacher.Controls.Special.EffectSelector)(target));
            return;
            case 8:
            
            #line 61 "..\..\..\Pages\Value_Label.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Delete_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
