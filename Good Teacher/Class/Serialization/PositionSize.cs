using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher.Class.Serialization
{
    public class PositionSize
    {
        public double X = 0, Y = 0, W = 0, H = 0;
        public int Z = 0;

        public PositionSize()
        {

        }


        public PositionSize(double x, double y, double w, double h, int z)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
            Z = z;
        }


        public PositionSize(FrameworkElement control)
        {
            X = Canvas.GetLeft(control);
            Y = Canvas.GetTop(control);
            W = control.Width;
            H = control.Height;
            Z = Panel.GetZIndex(control);
        }


        public void SetControlPositionSize(FrameworkElement control)
        {
            control.Width = W;
            control.Height = H;
            Canvas.SetLeft(control, X);
            Canvas.SetTop(control, Y);
            Panel.SetZIndex(control, Z);
        }


    }
}
