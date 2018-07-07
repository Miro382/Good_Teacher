using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Good_Teacher.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for DWindow_Brush.xaml
    /// </summary>
    public partial class DWindow_Brush : Window
    {

        public delegate void ChangedBrushDelegate(string Key);
        public event ChangedBrushDelegate ChangedBrush;

        Control cont;

        public DWindow_Brush(Control element, DataStore data)
        {
            InitializeComponent();

            cont = element;

            brushselector.SetData(element, data, false);
            brushselector.LoadData(element.Foreground);
            brushselector.ChangedBrush += Brushselector_ChangedBrush;
        }

        private void Brushselector_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            cont.Foreground = Sbrush;

            if (ChangedBrush != null)
                ChangedBrush(brushselector.LastSelectedImageKey);
        }

    }
}
