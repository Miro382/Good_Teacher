﻿#pragma checksum "..\..\..\..\Pages\Content\ContentEditor_Image.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D07C1828BAD57502955C96DB681C974E72C1C90EDBB8C484A0C0FA307F531AF1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Good_Teacher.Pages.Content;
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


namespace Good_Teacher.Pages.Content {
    
    
    /// <summary>
    /// ContentEditor_Image
    /// </summary>
    public partial class ContentEditor_Image : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\..\Pages\Content\ContentEditor_Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle R_ImageFill;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Pages\Content\ContentEditor_Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_Width;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Pages\Content\ContentEditor_Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_Height;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Pages\Content\ContentEditor_Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_MarginLeft;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Pages\Content\ContentEditor_Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox_Stretch;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\Pages\Content\ContentEditor_Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox_Quality;
        
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
            System.Uri resourceLocater = new System.Uri("/Good Teacher;component/pages/content/contenteditor_image.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\Content\ContentEditor_Image.xaml"
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
            
            #line 16 "..\..\..\..\Pages\Content\ContentEditor_Image.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TB_Width = ((System.Windows.Controls.TextBox)(target));
            
            #line 23 "..\..\..\..\Pages\Content\ContentEditor_Image.xaml"
            this.TB_Width.KeyUp += new System.Windows.Input.KeyEventHandler(this.TB_Width_KeyUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TB_Height = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\..\..\Pages\Content\ContentEditor_Image.xaml"
            this.TB_Height.KeyUp += new System.Windows.Input.KeyEventHandler(this.TB_Height_KeyUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TB_MarginLeft = ((System.Windows.Controls.TextBox)(target));
            
            #line 33 "..\..\..\..\Pages\Content\ContentEditor_Image.xaml"
            this.TB_MarginLeft.KeyUp += new System.Windows.Input.KeyEventHandler(this.TB_MarginLeft_KeyUp);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ComboBox_Stretch = ((System.Windows.Controls.ComboBox)(target));
            
            #line 38 "..\..\..\..\Pages\Content\ContentEditor_Image.xaml"
            this.ComboBox_Stretch.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBoxStretch_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ComboBox_Quality = ((System.Windows.Controls.ComboBox)(target));
            
            #line 48 "..\..\..\..\Pages\Content\ContentEditor_Image.xaml"
            this.ComboBox_Quality.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

