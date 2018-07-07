using System.Windows;
using System.Windows.Media;

namespace Good_Teacher.Class.Serialization
{
    public class ControlDefaultData
    {
        public double Opacity = 0;
        public double Angle = 0;

        public ControlDefaultData()
        {

        }

        public ControlDefaultData(FrameworkElement element)
        {
            Serialize(element);
        }


        public void Serialize(FrameworkElement element)
        {
            Opacity = element.Opacity;

            if (element.RenderTransform != null && element.RenderTransform is RotateTransform)
                Angle = ((RotateTransform)element.RenderTransform).Angle;

        }


        public void Deserialize(FrameworkElement element)
        {
            element.Opacity = Opacity;

            if(Angle!=0)
            {
                RotateTransform rotateTransform = new RotateTransform(Angle);
                element.RenderTransformOrigin = new Point(0.5, 0.5);
                element.RenderTransform = rotateTransform;
            }
        }

    }
}
