﻿#pragma checksum "..\..\..\Pages\Value_TextBox.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "80F145AAC4EDA1D4464E8724BED9652A802AEBC4"
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
using Good_Teacher.Controls.Special;
using Good_Teacher.Pages;
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


namespace Good_Teacher.Pages {
    
    
    /// <summary>
    /// Value_TextBox
    /// </summary>
    public partial class Value_TextBox : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\Pages\Value_TextBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Box_ID;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Pages\Value_TextBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.Special.PositionSelector positionselector;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Pages\Value_TextBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.Special.FontEditorPanel fontEditorPanel;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Pages\Value_TextBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CB_TextWrapping;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Pages\Value_TextBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CB_MultiLine;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Pages\Value_TextBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_MaxLength;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Pages\Value_TextBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.Special.BrushSelector brushselector;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Pages\Value_TextBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle Rect_ForColor;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Pages\Value_TextBox.xaml"
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
            System.Uri resourceLocater = new System.Uri("/Good Teacher;component/pages/value_textbox.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Value_TextBox.xaml"
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
            this.Box_ID = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\..\Pages\Value_TextBox.xaml"
            this.Box_ID.KeyDown += new System.Windows.Input.KeyEventHandler(this.Control_KeyDown);
            
            #line default
            #line hidden
            
            #line 28 "..\..\..\Pages\Value_TextBox.xaml"
            this.Box_ID.LostFocus += new System.Windows.RoutedEventHandler(this.Control_LostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.positionselector = ((Good_Teacher.Controls.Special.PositionSelector)(target));
            return;
            case 3:
            this.fontEditorPanel = ((Good_Teacher.Controls.Special.FontEditorPanel)(target));
            return;
            case 4:
            this.CB_TextWrapping = ((System.Windows.Controls.CheckBox)(target));
            
            #line 38 "..\..\..\Pages\Value_TextBox.xaml"
            this.CB_TextWrapping.Checked += new System.Windows.RoutedEventHandler(this.Checkbox_Checked);
            
            #line default
            #line hidden
            
            #line 38 "..\..\..\Pages\Value_TextBox.xaml"
            this.CB_TextWrapping.Unchecked += new System.Windows.RoutedEventHandler(this.Checkbox_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CB_MultiLine = ((System.Windows.Controls.CheckBox)(target));
            
            #line 39 "..\..\..\Pages\Value_TextBox.xaml"
            this.CB_MultiLine.Checked += new System.Windows.RoutedEventHandler(this.Checkbox_Checked);
            
            #line default
            #line hidden
            
            #line 39 "..\..\..\Pages\Value_TextBox.xaml"
            this.CB_MultiLine.Unchecked += new System.Windows.RoutedEventHandler(this.Checkbox_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TB_MaxLength = ((System.Windows.Controls.TextBox)(target));
            
            #line 43 "..\..\..\Pages\Value_TextBox.xaml"
            this.TB_MaxLength.KeyDown += new System.Windows.Input.KeyEventHandler(this.Control_KeyDown);
            
            #line default
            #line hidden
            
            #line 43 "..\..\..\Pages\Value_TextBox.xaml"
            this.TB_MaxLength.LostFocus += new System.Windows.RoutedEventHandler(this.Control_LostFocus);
            
            #line default
            #line hidden
            
            #line 43 "..\..\..\Pages\Value_TextBox.xaml"
            this.TB_MaxLength.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextBox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 43 "..\..\..\Pages\Value_TextBox.xaml"
            this.TB_MaxLength.AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.TextBox_Pasting));
            
            #line default
            #line hidden
            return;
            case 7:
            this.brushselector = ((Good_Teacher.Controls.Special.BrushSelector)(target));
            return;
            case 8:
            this.Rect_ForColor = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 9:
            
            #line 53 "..\..\..\Pages\Value_TextBox.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonColorFont_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.effectselector = ((Good_Teacher.Controls.Special.EffectSelector)(target));
            return;
            case 11:
            
            #line 65 "..\..\..\Pages\Value_TextBox.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Delete_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
