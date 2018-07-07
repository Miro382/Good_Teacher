using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Good_Teacher.Class.Serialization.Ser_Controls
{
    public class Content_Serialization
    {

        public FontFamily fontfamily;
        public double fontsize;
        public HorizontalAlignment horizontalAlignment;
        public VerticalAlignment verticalAlignment;
        public FontWeight fontWeight;
        public FontStyle fontStyle;
        public string content;

        public void Serialize(Control cont,string Content)
        {
            fontfamily = cont.FontFamily;
            fontsize = cont.FontSize;
            verticalAlignment = cont.VerticalContentAlignment;
            horizontalAlignment = cont.HorizontalContentAlignment;
            fontStyle = cont.FontStyle;
            fontWeight = cont.FontWeight;
            content = Content;
        }

        public string Deserialize(Control cont)
        {
            cont.FontFamily = fontfamily;
            cont.FontSize = fontsize;
            cont.VerticalContentAlignment = verticalAlignment;
            cont.HorizontalContentAlignment = horizontalAlignment;
            cont.FontStyle = fontStyle;
            cont.FontWeight = fontWeight;
            return content;
        }

    }
}
