using Good_Teacher.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Good_Teacher.Controls.Special;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_InkCanvas.xaml
    /// </summary>
    public partial class Value_InkCanvas : System.Windows.Controls.Page
    {

        InkCanvas_Control cont;

        DataStore data;


        public Value_InkCanvas(DataStore datas, InkCanvas_Control inkCanvas_Control)
        {
            InitializeComponent();

            cont = inkCanvas_Control;

            data = datas;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();

            brushselector.SetData(cont, data, true);
            brushselector.LoadData(cont.inkCanvas.Background);
            brushselector.ChangedBrush -= Brushselector_ChangedBrush;
            brushselector.ChangedBrush += Brushselector_ChangedBrush;

            BS_ControlPanel.SetData(cont, data, false, cont.PathToCPImage);
            BS_ControlPanel.LoadData(cont.ControlPanelBack);
            BS_ControlPanel.ChangedBrush -= ControlBackground_ChangedBrush;
            BS_ControlPanel.ChangedBrush += ControlBackground_ChangedBrush;
        }

        private void ControlBackground_ChangedBrush(BrushSelector brushSelector, Brush Sbrush)
        {
            cont.ControlPanelBack = Sbrush;

            if (Sbrush is ImageBrush)
            {
                cont.PathToCPImage = brushSelector.LastSelectedImageKey;
                cont.CPStretch = ((ImageBrush)Sbrush).Stretch;
            }
        }

        private void Brushselector_ChangedBrush(BrushSelector brushSelector, Brush Sbrush)
        {
            cont.inkCanvas.Background = Sbrush;
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
