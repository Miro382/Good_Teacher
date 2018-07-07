using Good_Teacher.Class.Workers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Good_Teacher.Class.Serialization.Content_Ser
{
    class Content_Text : Content_Default
    {

        public string text = "Text";
        public double fontsize = 20;
        public double MarginLeft = 0;
        public bool Bold = false, Italic = false;
        public Brush_Serializer foreground = new Brush_Serializer(new SolidColorBrush(Colors.Black));
        public FontFamily fontFamily;


        public int ContentType()
        {
            return 1;
        }

        public Content_Text()
        {
        }

        public Content_Text(string Text, double FontSize, double marginLeft)
        {
            text = Text;
            fontsize = FontSize;
            MarginLeft = marginLeft;
        }

        public Label Create(DataStore data)
        {
            Label lab = new Label()
            {
                Content = text,
                Margin = new System.Windows.Thickness(MarginLeft, 0, 0, 0),
                FontSize = fontsize
            };

            lab.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            lab.Foreground = foreground.DeserializeToBrushWithKey(data);

            if (fontFamily != null)
            {
                lab.FontFamily = fontFamily;

                if (!string.IsNullOrEmpty(fontFamily.Source))
                {
                    FontFamily fontFamily;
                    if (FontWorker.GetFontFamily(lab.FontFamily, out fontFamily))
                    {
                        lab.FontFamily = fontFamily;
                    }
                }
            }


            if (Bold)
                lab.FontWeight = FontWeights.Bold;

            if (Italic)
                lab.FontStyle = FontStyles.Italic;

            return lab;
        }


    }
}
