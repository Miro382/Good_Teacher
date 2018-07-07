using System.Windows;

namespace Good_Teacher.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for DWindow_AddImage.xaml
    /// </summary>
    public partial class DWindow_AddImage : Window
    {
        public string Path = "";
        public bool OK = false;

        public DWindow_AddImage()
        {
            InitializeComponent();
        }


        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            if(sender == WebSearch)
            {
                DWindow_AddFromWeb addw = new DWindow_AddFromWeb();
                addw.Owner = this;
                addw.ShowDialog();

                if (addw.OK)
                {
                    PathTextBox.Text = addw.Address;
                    CloseWindow();
                }
            }
        }


        void CloseWindow()
        {
            if (!string.IsNullOrWhiteSpace(PathTextBox.Text))
                OK = true;

            Path = PathTextBox.Text;

            Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

    }

}
