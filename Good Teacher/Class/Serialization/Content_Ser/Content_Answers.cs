using Good_Teacher.Class.Workers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Good_Teacher.Class.Serialization.Content_Ser
{
    public class Content_Answers : Content_Default
    {

        public double fontsize = 20;
        public double MarginLeft = 0;
        public bool Bold = false, Italic = false;
        public Brush_Serializer foreground = new Brush_Serializer(new SolidColorBrush(Colors.Black));
        public FontFamily fontFamily;
        /// <summary>
        /// True - Show good answers
        /// False - Show wrong answers
        /// </summary>
        public bool ShowGood = true;

        public int ContentType()
        {
            return 4;
        }

        public Label Create(DataStore data)
        {
            Label lab = new Label()
            {
                Margin = new System.Windows.Thickness(MarginLeft, 0, 0, 0),
                FontSize = fontsize
            };

            if (ShowGood)
                lab.Content = "" + MainWindow.GoodAnswersCount;
            else
                lab.Content = "" + MainWindow.WrongAnswersCount;

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
