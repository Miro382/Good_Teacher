using Good_Teacher.Class.Workers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Good_Teacher.Class.Serialization
{
    public class Font_Serializer
    {
        public double fontsize = 20;
        public FontFamily fontFamily;
        public FontWeight fontWeight;
        public FontStyle fontStyle;

        public Font_Serializer()
        {

        }


        public Font_Serializer(Control cont)
        {
            Serialize(cont);
        }


        public void Serialize(Control cont)
        {
            fontsize = cont.FontSize;
            fontStyle = cont.FontStyle;
            fontWeight = cont.FontWeight;
            fontFamily = cont.FontFamily;
        }

        public void Deserialize(Control cont)
        {
            cont.FontSize = fontsize;
            cont.FontStyle = fontStyle;
            cont.FontWeight = fontWeight;
            cont.FontFamily = fontFamily;

            if (!string.IsNullOrEmpty(cont.FontFamily.Source))
            {
                FontFamily fontFamily;
                if (FontWorker.GetFontFamily(cont.FontFamily, out fontFamily))
                {
                    cont.FontFamily = fontFamily;
                }
            }
        }

    }
}
