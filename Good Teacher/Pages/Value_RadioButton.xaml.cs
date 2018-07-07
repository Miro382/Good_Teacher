using Good_Teacher.Controls.Special;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_RadioButton.xaml
    /// </summary>
    public partial class Value_RadioButton : System.Windows.Controls.Page
    {

        RadioButton cont;
        DataStore data;

        public Value_RadioButton(DataStore datas, RadioButton addcont)
        {
            InitializeComponent();

            cont = addcont;

            data = datas;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();

            brushselector.SetData(cont, data, true);
            brushselector.foreground = true;
            brushselector.LoadData(cont.Foreground);
            brushselector.ChangedBrush -= Brushselector_ChangedBrush;
            brushselector.ChangedBrush += Brushselector_ChangedBrush;


            fontpanel.Load(cont,data);

            CheckBox_Ch.IsChecked = cont.IsChecked;

            if (cont.Content != null)
                TB_Text.Text = cont.Content.ToString();

            TB_Group.Text = cont.GroupName;
        }


        private void Brushselector_ChangedBrush(BrushSelector brushSelector, Brush Sbrush)
        {
            cont.Foreground = Sbrush;
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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cont.IsChecked = CheckBox_Ch.IsChecked;
        }


        private void TB_Text_KeyUp(object sender, KeyEventArgs e)
        {
            cont.Content = TB_Text.Text;
        }


        private void TB_Group_KeyUp(object sender, KeyEventArgs e)
        {
            cont.GroupName = TB_Group.Text;
        }

    }
}
