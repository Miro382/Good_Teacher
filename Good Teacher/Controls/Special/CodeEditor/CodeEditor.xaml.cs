using ICSharpCode.AvalonEdit.CodeCompletion;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Good_Teacher.Controls.Special.CodeEditor
{
    /// <summary>
    /// Interaction logic for CodeEditor.xaml
    /// </summary>
    public partial class CodeEditor : UserControl
    {

        CompletionWindow completionWindow;

        public CodeEditor()
        {
            InitializeComponent();

            textEditor.TextArea.Margin = new Thickness(10,15,0,0);
            textEditor.ShowLineNumbers = true;
            textEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
            textEditor.TextArea.TextEntered += textEditor_TextArea_TextEntered;
        }


        void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "\n" || e.Text == " " || e.Text == "=" || e.Text == "(")
            {
                completionWindow = new CompletionWindow(textEditor.TextArea);
                IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
                data.Add(new ScriptCompletionData("Canvas", "Canvas."));
                data.Add(new ScriptCompletionData("Alert","Alert(string caption, string text)"));
                data.Add(new ScriptCompletionData("alert", "alert(string text)"));
                data.Add(new ScriptCompletionData("Element","Element[]"));
                data.Add(new ScriptCompletionData("Visible", "Visibility - visible"));
                data.Add(new ScriptCompletionData("Collapsed", "Visibility - collapsed"));
                data.Add(new ScriptCompletionData("CurrentPage()", "CurrentPage() return (int) - Current page"));
                data.Add(new ScriptCompletionData("SetX", "SetX(element, double posX)"));
                data.Add(new ScriptCompletionData("SetY", "SetY(element, double posY)"));
                data.Add(new ScriptCompletionData("SetW", "SetW(element, double width)"));
                data.Add(new ScriptCompletionData("SetH", "SetH(element, double height)"));
                data.Add(new ScriptCompletionData("GetX", "(double)GetX(element)"));
                data.Add(new ScriptCompletionData("GetY", "(double)GetY(element)"));
                data.Add(new ScriptCompletionData("GetW", "(double)GetW(element)"));
                data.Add(new ScriptCompletionData("GetH", "(double)GetH(element)"));
                data.Add(new ScriptCompletionData("FindByID", "(FrameworkElement)FindByID(long id, FrameworkElement parent [use Canvas for example])"));
                completionWindow.Show();
                completionWindow.Closed += delegate {
                    completionWindow = null;
                };
            }
            else if (e.Text == ".")
            {
                // Open code completion after the user has pressed dot:
                completionWindow = new CompletionWindow(textEditor.TextArea);
                IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
                data.Add(new ScriptCompletionData("Visibility", "element visibility"));
                data.Add(new ScriptCompletionData("RemoveAt", "RemoveAt(int elementID)"));
                data.Add(new ScriptCompletionData("Content", "Special controls like Label (Set text)"));
                data.Add(new ScriptCompletionData("add_MouseDown(function(sender, eventArgs) { /*...*/ });", "Mouse button down event"));
                data.Add(new ScriptCompletionData("add_MouseUp(function(sender, eventArgs) { /*...*/ });", "Mouse button up event"));
                data.Add(new ScriptCompletionData("add_MouseLeftButtonDown(function(sender, eventArgs) { /*...*/ });", "Left mouse button down event"));
                data.Add(new ScriptCompletionData("add_MouseLeftButtonUp(function(sender, eventArgs) { /*...*/ });", "Left mouse button up event"));
                data.Add(new ScriptCompletionData("add_MouseRightButtonDown(function(sender, eventArgs) { /*...*/ });", "Right mouse button down event"));
                data.Add(new ScriptCompletionData("add_MouseRightButtonUp(function(sender, eventArgs) { /*...*/ });", "Right mouse button up event"));
                data.Add(new ScriptCompletionData("add_MouseEnter(function(sender, eventArgs) { /*...*/ });", "Mouse enter event"));
                data.Add(new ScriptCompletionData("add_MouseLeave(function(sender, eventArgs) { /*...*/ });", "Mouse leave event"));
                completionWindow.Show();
                completionWindow.Closed += delegate {
                    completionWindow = null;
                };
            }
        }

        void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && completionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    // Whenever a non-letter is typed while the completion window is open,
                    // insert the currently selected element.
                    completionWindow.CompletionList.RequestInsertion(e);
                }
            }
            // Do not set e.Handled=true.
            // We still want to insert the character that was typed.
        }

    }
}
