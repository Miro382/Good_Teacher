using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher.Class.History
{
    public class His_RemovedControl : HistoryCommand
    {
        public int ID = 0;
        public FrameworkElement frameworkElement;

        public His_RemovedControl(FrameworkElement elm, int idC)
        {
            frameworkElement = elm;
            ID = idC;
        }

        public void DoRedoAction(Canvas canvas, DataStore data)
        {
            canvas.Children.RemoveAt(ID);
        }

        public void DoUndoAction(Canvas canvas, DataStore data)
        {
            canvas.Children.Add(frameworkElement);
        }
    }
}
