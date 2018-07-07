using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Good_Teacher_Repairo.Class
{
    public static class ImageLoader
    {

        public static ImageSource GetImage(byte[] data)
        {
            using (var ms = new System.IO.MemoryStream(data))
            {
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.StreamSource = ms;
                src.EndInit();
                return src;
            }
        }


        public static ImageSource GetImageOptimal(int width, int height, byte[] data)
        {
            using (var ms = new System.IO.MemoryStream(data))
            {
                BitmapImage src = new BitmapImage();
                src.BeginInit();

                if (width > height)
                    src.DecodePixelWidth = width;
                else
                    src.DecodePixelHeight = height;

                src.CacheOption = BitmapCacheOption.OnLoad;
                src.StreamSource = ms;
                src.EndInit();

                return src;
            }
        }


    }
}
