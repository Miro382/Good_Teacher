using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher.Class.History
{
    public class His_AddControl : HistoryCommand
    {
        public UIElement frameworkElement;
        public int ID = 0;

        public His_AddControl(UIElement elm, int id)
        {
            frameworkElement = elm;
            ID = id;
        }

        public void DoRedoAction(Canvas canvas, DataStore data)
        {
            canvas.Children.Insert(ID,frameworkElement);
        }

        public void DoUndoAction(Canvas canvas, DataStore data)
        {
            canvas.Children.RemoveAt(ID);
        }

    }
}
