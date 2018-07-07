using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Good_Teacher.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for DWindow_MoveTo.xaml
    /// </summary>
    public partial class DWindow_MoveTo : Window
    {
        public bool isOK { get; private set; } = false;
        public int MoveTo { get; private set; } = 0;

        int PageCount = 0;

        public DWindow_MoveTo(int pagecount)
        {
            InitializeComponent();

            PageCount = pagecount;
            TB_Number.Focus();
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



        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            uint number = 0;


            if(uint.TryParse(TB_Number.Text,out number))
            {

                if(number>PageCount)
                {
                    number = (uint)PageCount;
                }

                if(number<1)
                {
                    number = 1;
                }

                number--;

                isOK = true;
                MoveTo = (int)number;
                Close();
            }
            else
            {
                TB_Number.Text = "1";
                MessageBox.Show(Strings.ResStrings.WrongInputData, Strings.ResStrings.Error);
            }
        }

    }
}
