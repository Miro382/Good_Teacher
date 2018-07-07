using System;
using System.Windows;

namespace Good_Teacher.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for DWindow_AddFromWeb.xaml
    /// </summary>
    public partial class DWindow_AddFromWeb : Window
    {

        public bool OK = false;
        public string Address = "";

        public DWindow_AddFromWeb()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Uri.IsWellFormedUriString(TextBox_Address.Text, UriKind.Absolute))
            {
                Address = TextBox_Address.Text;
                OK = true;
                Close();
            }
            else
            {
                MessageBox.Show(Strings.ResStrings.WrongUrl);
            }
        }

    }
}
