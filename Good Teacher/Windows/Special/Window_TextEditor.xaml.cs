using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_TextEditor.xaml
    /// </summary>
    public partial class Window_TextEditor : Window
    {
        TextBox text;

        public Window_TextEditor(TextBox textbox)
        {
            InitializeComponent();
            text = textbox;
            TB_TextEditor.Text = text.Text;
        }


        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            text.Text = TB_TextEditor.Text;
            Close();
        }

    }
}
