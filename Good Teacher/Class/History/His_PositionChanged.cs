using System.Windows.Controls;

namespace Good_Teacher.Class.History
{
    class His_PositionChanged : HistoryCommand
    {
        public int ControlID = 0;
        public double PosX, PosY;
        public double CPosX, CPosY;

        public His_PositionChanged(int id,double x, double y, double Cx, double Cy)
        {
            ControlID = id;
            PosX = x;
            PosY = y;
            CPosX = Cx;
            CPosY = Cy;
        }

        public void DoUndoAction(Canvas canvas, DataStore data)
        {
            Canvas.SetLeft(canvas.Children[ControlID], CPosX);
            Canvas.SetTop(canvas.Children[ControlID], CPosY);
        }

        public void DoRedoAction(Canvas canvas, DataStore data)
        {
            Canvas.SetLeft(canvas.Children[ControlID], PosX);
            Canvas.SetTop(canvas.Children[ControlID], PosY);
        }

    }
}
