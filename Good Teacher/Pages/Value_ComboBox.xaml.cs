using Good_Teacher.Controls;
using Good_Teacher.Windows.Dialogs;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_ComboBox.xaml
    /// </summary>
    public partial class Value_ComboBox : System.Windows.Controls.Page
    {

        ComboBox_Control cont;

        DataStore data;


        public Value_ComboBox(DataStore datas, ComboBox_Control AControl)
        {
            InitializeComponent();

            cont = AControl;

            data = datas;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();

            alignmentSelector.SetData(cont.combobox.HorizontalContentAlignment, cont.combobox.VerticalContentAlignment);
            alignmentSelector.ChangedAlign += AlignmentSelector_ChangedAlign;
        }


        private void AlignmentSelector_ChangedAlign(HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            cont.combobox.HorizontalContentAlignment = horizontal;
            cont.combobox.VerticalContentAlignment = vertical;
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

        private void Content_Click(object sender, RoutedEventArgs e)
        {
            DWindow_ComboBox comboboxW = new DWindow_ComboBox(data, cont.contents);
            comboboxW.ShowDialog();

            if(comboboxW.isOK)
            {
                cont.contents = comboboxW.contents;
                cont.Create(data);
            }
        }

    }
}
