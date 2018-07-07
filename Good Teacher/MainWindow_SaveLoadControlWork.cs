using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher
{
    public partial class MainWindow : Window
    {


        public void RemoveUnloadEvent()
        {
            foreach (FrameworkElement elm in DesignCanvas.Children)
            {
                elm.Unloaded -= Control_Unloaded;
            }
        }

        public void CallAllUnloadEvent(Canvas canvas)
        {
            foreach (FrameworkElement elm in canvas.Children)
            {
                Control_Unloaded(elm, null);
            }
        }


        void AddEventsToNewCanvas()
        {
            foreach (FrameworkElement frm in DesignCanvas.Children)
            {
                AddEvents(frm);
            }
        }


    }
}
