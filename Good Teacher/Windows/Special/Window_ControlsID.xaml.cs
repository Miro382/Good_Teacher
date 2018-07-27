using Good_Teacher.Class.Workers;
using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_ControlsID.xaml
    /// </summary>
    public partial class Window_ControlsID : Window
    {
        public Window_ControlsID(Canvas canvas)
        {
            InitializeComponent();

            string prp;

            for(int i=0;i<canvas.Children.Count;i++)
            {
                SP_ControlsID.Children.Add(new Label() { Content = "[" + ControlWorker.GetID(((FrameworkElement)canvas.Children[i]).Name) + "] - " + ControlNameWorker.GetTypeName(canvas.Children[i], out prp) });
            }
        }
    }
}
