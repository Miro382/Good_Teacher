using Good_Teacher.Controls;
using Good_Teacher.Windows.Dialogs;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_Media.xaml
    /// </summary>
    public partial class Value_Media : System.Windows.Controls.Page
    {

        MediaPlayer_Control cont;
        DataStore data;

        public Value_Media(DataStore datas ,MediaPlayer_Control control)
        {
            InitializeComponent();

            cont = control;
            data = datas;

            positionselector.SetData(cont);
            positionselector.LoadData();

            if(cont.Tag!=null)
            TBL_Media.Text = cont.Tag.ToString();
        }

        private void SetMedia_Click(object sender, RoutedEventArgs e)
        {
            DWindow_MediaSelector mediaSelector = new DWindow_MediaSelector();
            mediaSelector.Owner = Window.GetWindow(this);
            mediaSelector.ShowDialog();

            if(mediaSelector.OK)
            {
                cont.Tag = mediaSelector.FileName;
                TBL_Media.Text = mediaSelector.FileName;
            }
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((Canvas)cont.Parent).Children.Remove(cont);
                NavigationService.Content = "";
                ((MainWindow)Window.GetWindow(this)).RemoveSelectedItemEffect();
            }
            catch
            {

            }
        }

    }
}
