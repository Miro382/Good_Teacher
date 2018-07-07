using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Class.Serialization.Content_Ser
{
    class Content_Splitter : Content_Default
    {

        public Brush_Serializer fill = new Brush_Serializer(new SolidColorBrush(Colors.Black));
        public double W = 2, MarginLeft = 0;


        public int ContentType()
        {
            return 2;
        }


        public Rectangle Create(DataStore data)
        {
            Rectangle rec = new Rectangle()
            {
                Width = W,
                Height = Double.NaN,
                Margin = new System.Windows.Thickness(MarginLeft, 0, 0, 0),
                Fill = fill.DeserializeToBrushWithKey(data)
            };

            return rec;
        }

    }
}
