﻿#pragma checksum "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B9A718EF6BA7C3633CFD64CB6995E42C2267FA5E77EB4D9939B3C54DDEC0F7D7"
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
    /// ContentEditor_Answers
    /// </summary>
    public partial class ContentEditor_Answers : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RB_GoodAns;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RB_WrongAns;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox_FontSize;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_MarginLeft;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle Rect_BackColor;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_font;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox_FontName;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.SelectButton SButton_Bold;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Good_Teacher.Controls.SelectButton SButton_Italic;
        
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
            System.Uri resourceLocater = new System.Uri("/Good Teacher;component/pages/content/contenteditor_answers.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.RB_GoodAns = ((System.Windows.Controls.RadioButton)(target));
            
            #line 16 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
            this.RB_GoodAns.Checked += new System.Windows.RoutedEventHandler(this.RadioButton_Checked);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
            this.RB_GoodAns.Unchecked += new System.Windows.RoutedEventHandler(this.RadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 2:
            this.RB_WrongAns = ((System.Windows.Controls.RadioButton)(target));
            
            #line 17 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
            this.RB_WrongAns.Checked += new System.Windows.RoutedEventHandler(this.RadioButton_Checked);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
            this.RB_WrongAns.Unchecked += new System.Windows.RoutedEventHandler(this.RadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ComboBox_FontSize = ((System.Windows.Controls.ComboBox)(target));
            
            #line 22 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
            this.ComboBox_FontSize.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(this.ComboBox_FontSize_TextChanged));
            
            #line default
            #line hidden
            return;
            case 4:
            this.TB_MarginLeft = ((System.Windows.Controls.TextBox)(target));
            
            #line 55 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
            this.TB_MarginLeft.KeyUp += new System.Windows.Input.KeyEventHandler(this.TB_MarginLeft_KeyUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Rect_BackColor = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 6:
            
            #line 61 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonColor_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 66 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonBrush_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Label_font = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.ComboBox_FontName = ((System.Windows.Controls.ComboBox)(target));
            
            #line 73 "..\..\..\..\Pages\Content\ContentEditor_Answers.xaml"
            this.ComboBox_FontName.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_FontName_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.SButton_Bold = ((Good_Teacher.Controls.SelectButton)(target));
            return;
            case 11:
            this.SButton_Italic = ((Good_Teacher.Controls.SelectButton)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

