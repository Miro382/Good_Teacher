using System.Windows.Media;

namespace Good_Teacher.Class
{
    public class ImageRepresentation
    {
        public string Path = "";
        public Stretch stretch = Stretch.Uniform;
        public double Angle = 0;
        public double ScaleX = 1;
        public double ScaleY = 1;
        public bool DoTransform = false;
        public TileMode tileMode = TileMode.None;
        public double TranslateX = 0, TranslateY = 0;

        public ImageRepresentation()
        {

        }

        public ImageRepresentation(string path, Stretch stretchT)
        {
            Path = path;
            stretch = stretchT;
        }

    }
}
