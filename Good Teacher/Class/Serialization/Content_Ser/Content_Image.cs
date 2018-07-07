using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Good_Teacher.Class.Serialization.Content_Ser
{
    public class Content_Image : Content_Default
    {
        public string ImageKey = "";
        public double W = Double.NaN, H = Double.NaN, MarginLeft = 0;
        public Stretch stretch = Stretch.Uniform;
        public BitmapScalingMode scalingMode = BitmapScalingMode.Linear;

        public int ContentType()
        {
            return 0;
        }

        public Image Create(DataStore data)
        {
            Image img = new Image()
            {
                Width = W,
                Height = H,
                Margin = new System.Windows.Thickness(MarginLeft, 0, 0, 0),
                Source = data.archive.GetImage(ImageKey),
                Stretch = stretch
            };

            RenderOptions.SetBitmapScalingMode(img, scalingMode);

            return img;
        }
    }
}
