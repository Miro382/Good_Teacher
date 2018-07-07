using System.Windows;

namespace Good_Teacher.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for DWindow_AddFromClipboard.xaml
    /// </summary>
    public partial class DWindow_AddFromClipboard : Window
    {
        public bool AddImage = false;

        public DWindow_AddFromClipboard()
        {
            InitializeComponent();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            AddImage = false;
            Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddImage = true;
            Close();
        }

    }
}
