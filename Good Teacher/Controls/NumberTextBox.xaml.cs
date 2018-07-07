using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Good_Teacher.Controls.Special
{
    /// <summary>
    /// Interaction logic for NumberTextBox.xaml
    /// </summary>
    public partial class NumberTextBox : UserControl
    {

        public string Text
        {
            get { return TB_Number.Text; }
            set { TB_Number.Text = value; }
        }

        public NumberTextBox()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }


        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9,-]+");
            return !regex.IsMatch(text);
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {

            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

    }
}
