﻿#pragma checksum "..\..\..\Pages\Value_Image.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0563BAE6EDE41F6BD69F232080750D0711217CC4"
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
    /// Value_Image
    /// </summary>
    public partial class Value_Image : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\Pages\Value_Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle R_ImageFill;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Pages\Value_Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_imageWH;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Pages\Value_Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.Special.PositionSelector positionselector;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Pages\Value_Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox_Stretch;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Pages\Value_Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox_Quality;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\Pages\Value_Image.xaml"
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
            System.Uri resourceLocater = new System.Uri("/Good Teacher;component/pages/value_image.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Value_Image.xaml"
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
            this.R_ImageFill = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 2:
            
            #line 33 "..\..\..\Pages\Value_Image.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Label_imageWH = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.positionselector = ((Good_Teacher.Controls.Special.PositionSelector)(target));
            return;
            case 5:
            this.ComboBox_Stretch = ((System.Windows.Controls.ComboBox)(target));
            
            #line 44 "..\..\..\Pages\Value_Image.xaml"
            this.ComboBox_Stretch.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ComboBox_Quality = ((System.Windows.Controls.ComboBox)(target));
            
            #line 53 "..\..\..\Pages\Value_Image.xaml"
            this.ComboBox_Quality.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.effectselector = ((Good_Teacher.Controls.Special.EffectSelector)(target));
            return;
            case 8:
            
            #line 67 "..\..\..\Pages\Value_Image.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Delete_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
