using Good_Teacher.Class.Workers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static Good_Teacher.Class.Enumerators.DateTypeShow;

namespace Good_Teacher.Class.Serialization.Content_Ser
{
    public class Content_Date : Content_Default
    {
        public double fontsize = 20;
        public double MarginLeft = 0;
        public bool Bold = false, Italic = false;
        public Brush_Serializer foreground = new Brush_Serializer(new SolidColorBrush(Colors.Black));
        public FontFamily fontFamily;

        public ShowDateType showDateType = ShowDateType.Day;

        public int ContentType()
        {
            return 5;
        }

        public Label Create(DataStore data)
        {
            Label lab = new Label()
            {
                Margin = new System.Windows.Thickness(MarginLeft, 0, 0, 0),
                FontSize = fontsize
            };


            switch(showDateType)
            {
                case ShowDateType.Second:
                    lab.Content = "" + DateTime.Now.Second;
                    break;
                case ShowDateType.Minute:
                    lab.Content = "" + DateTime.Now.Minute;
                    break;
                case ShowDateType.Hour:
                    lab.Content = "" + DateTime.Now.Hour;
                    break;
                case ShowDateType.Day:
                    lab.Content = "" + DateTime.Now.Day;
                    break;
                case ShowDateType.DayOfYear:
                    lab.Content = "" + DateTime.Now.DayOfYear;
                    break;
                case ShowDateType.Month:
                    lab.Content = "" + DateTime.Now.Month;
                    break;
                case ShowDateType.Year:
                    lab.Content = "" + DateTime.Now.Year;
                    break;
                default:
                    lab.Content = "" + DateTime.Now;
                    break;
            }

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
