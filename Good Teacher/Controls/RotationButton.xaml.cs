using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for RotationButton.xaml
    /// </summary>
    public partial class RotationButton : UserControl
    {
        public delegate void ClickDelegate(object sender, double Angle);
        public event ClickDelegate Click;

        private bool clickE = false;


        public RotationButton()
        {
            InitializeComponent();
        }

        private void Ellipse_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            clickE = true;
            Rotate(e);
        }

        private void ellipse_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            clickE = false;
        }


        private void ellipse_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (clickE)
            {
                Rotate(e);
            }
        }


        public void SetAngleToPointer(double angle)
        {
            RotateTransform rotateTransform = new RotateTransform(angle);
            RotatePointer.RenderTransform = rotateTransform;
        }


        void Rotate(MouseEventArgs e)
        {
            Point curPoint = e.GetPosition(ellipse);

            Point center = new Point(ellipse.ActualWidth / 2, ellipse.ActualHeight / 2);

            Point relPoint = new Point(curPoint.X - center.X, curPoint.Y - center.Y);

            double radians = Math.Atan2(relPoint.Y, relPoint.X);
            double angle = radians * (180 / Math.PI);
            angle += 90;

            angle = Math.Round(angle, 2);

            RotateTransform rotateTransform = new RotateTransform(angle);
            RotatePointer.RenderTransform = rotateTransform;

            if (Click != null)
                Click(this, angle);
        }


        /*
        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Rectangle)sender).Fill = new SolidColorBrush(Colors.Red);
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Rectangle)sender).Fill = new SolidColorBrush(Colors.Black);
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double angle = double.Parse(((Rectangle)sender).Tag.ToString());
            Angle = angle;

                if (Click != null)
                    Click(this,angle);
        }
        */



    }
}
