using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Good_Teacher.Pages.Pages;

namespace Good_Teacher
{
    /// <summary>
    /// Interaction logic for Page_Form.xaml
    /// </summary>
    public partial class Page_Form : System.Windows.Controls.Page, Page_Interface
    {


        public event Pages.Pages.AddControlDelegate AddControlEvent;

        public Page_Form()
        {
            InitializeComponent();
        }

        private void AddControl_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = ((TextBlock)this.Resources["CursorAdd"]).Cursor;
            AddControlEvent(int.Parse(((MenuItem)sender).Tag.ToString()));
        }


    }
}
