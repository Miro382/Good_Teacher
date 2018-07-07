using System.IO;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Class
{
    public class ResourceData
    {
        public byte[] Data;
        public int W = 0, H = 0;

        public ResourceData()
        {

        }

        public ResourceData(int references, byte[] data)
        {
            Data = data;
            GetImageSize(data, out W, out H);
        }

        public ResourceData(byte[] data)
        {
            Data = data;
            GetImageSize(data, out W, out H);
        }

        private bool GetImageSize(byte[] array, out int WS, out int HS)
        {
            WS = 0;
            HS = 0;
            using (MemoryStream ms = new MemoryStream(array))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();

                WS = image.PixelWidth;
                HS = image.PixelHeight;
                image = null;
            }
            return true;
        }


    }
}
