﻿#pragma checksum "..\..\..\Pages\Value_Gallery.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8CDE86E3CB0F168AF06C340D94E29340EDE1975F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    /// Value_Gallery
    /// </summary>
    public partial class Value_Gallery : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\Pages\Value_Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Label_SettingsName;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Pages\Value_Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.Special.PositionSelector positionselector;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Pages\Value_Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CB_TextVis;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Pages\Value_Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CB_CircleVis;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Pages\Value_Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CB_ControlVis;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Pages\Value_Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_RestTime;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Pages\Value_Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_TransitionSpeed;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Pages\Value_Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox_Stretch;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\Pages\Value_Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.Special.BrushSelector brushselectorFor;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\Pages\Value_Gallery.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.Special.FontEditorPanel fontEditorPanel;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Pages\Value_Gallery.xaml"
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
            System.Uri resourceLocater = new System.Uri("/Good Teacher;component/pages/value_gallery.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Value_Gallery.xaml"
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
            this.Label_SettingsName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.positionselector = ((Good_Teacher.Controls.Special.PositionSelector)(target));
            return;
            case 3:
            
            #line 33 "..\..\..\Pages\Value_Gallery.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ImagesGallery_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.CB_TextVis = ((System.Windows.Controls.CheckBox)(target));
            
            #line 40 "..\..\..\Pages\Value_Gallery.xaml"
            this.CB_TextVis.Checked += new System.Windows.RoutedEventHandler(this.CB_TextVis_Checked);
            
            #line default
            #line hidden
            
            #line 40 "..\..\..\Pages\Value_Gallery.xaml"
            this.CB_TextVis.Unchecked += new System.Windows.RoutedEventHandler(this.CB_TextVis_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CB_CircleVis = ((System.Windows.Controls.CheckBox)(target));
            
            #line 44 "..\..\..\Pages\Value_Gallery.xaml"
            this.CB_CircleVis.Checked += new System.Windows.RoutedEventHandler(this.CB_CircleVis_Checked);
            
            #line default
            #line hidden
            
            #line 44 "..\..\..\Pages\Value_Gallery.xaml"
            this.CB_CircleVis.Unchecked += new System.Windows.RoutedEventHandler(this.CB_CircleVis_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CB_ControlVis = ((System.Windows.Controls.CheckBox)(target));
            
            #line 48 "..\..\..\Pages\Value_Gallery.xaml"
            this.CB_ControlVis.Checked += new System.Windows.RoutedEventHandler(this.CB_ControlVis_Checked);
            
            #line default
            #line hidden
            
            #line 48 "..\..\..\Pages\Value_Gallery.xaml"
            this.CB_ControlVis.Unchecked += new System.Windows.RoutedEventHandler(this.CB_ControlVis_Checked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TB_RestTime = ((System.Windows.Controls.TextBox)(target));
            
            #line 53 "..\..\..\Pages\Value_Gallery.xaml"
            this.TB_RestTime.KeyUp += new System.Windows.Input.KeyEventHandler(this.TB_RestTime_KeyUp);
            
            #line default
            #line hidden
            return;
            case 8:
            this.TB_TransitionSpeed = ((System.Windows.Controls.TextBox)(target));
            
            #line 56 "..\..\..\Pages\Value_Gallery.xaml"
            this.TB_TransitionSpeed.LostFocus += new System.Windows.RoutedEventHandler(this.TB_TransitionSpeed_LostFocus);
            
            #line default
            #line hidden
            
            #line 56 "..\..\..\Pages\Value_Gallery.xaml"
            this.TB_TransitionSpeed.KeyDown += new System.Windows.Input.KeyEventHandler(this.TB_TransitionSpeed_KeyDown);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ComboBox_Stretch = ((System.Windows.Controls.ComboBox)(target));
            
            #line 59 "..\..\..\Pages\Value_Gallery.xaml"
            this.ComboBox_Stretch.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.brushselectorFor = ((Good_Teacher.Controls.Special.BrushSelector)(target));
            return;
            case 11:
            this.fontEditorPanel = ((Good_Teacher.Controls.Special.FontEditorPanel)(target));
            return;
            case 12:
            this.effectselector = ((Good_Teacher.Controls.Special.EffectSelector)(target));
            return;
            case 13:
            
            #line 80 "..\..\..\Pages\Value_Gallery.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Delete_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
