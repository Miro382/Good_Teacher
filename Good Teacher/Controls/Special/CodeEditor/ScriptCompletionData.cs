using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System;

namespace Good_Teacher.Controls.Special.CodeEditor
{
    public class ScriptCompletionData : ICompletionData
    {
        public ScriptCompletionData(string text, string description)
        {
            this.Text = text;
            this.DescriptionT = description;
        }

        public System.Windows.Media.ImageSource Image
        {
            get { return null; }
        }

        public string Text { get; private set; }
        public string DescriptionT { get; private set; }

        // Use this property if you want to show a fancy UIElement in the list.
        public object Content
        {
            get { return this.Text; }
        }

        public object Description
        {
            get { return DescriptionT; }
        }

        public double Priority => 1;

        public void Complete(TextArea textArea, ISegment completionSegment,
            EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment, this.Text);
        }

    }
}
