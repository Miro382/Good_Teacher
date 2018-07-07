using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Class
{
    class ImageWorker
    {

        public static BitmapSource ReduceQualityJPG(BitmapSource image, int Quality)
        {
            
            JpegBitmapEncoder enc = new JpegBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(image));
            enc.QualityLevel = Quality;

            byte[] img;
            using (MemoryStream mem = new MemoryStream())
            {
                enc.Save(mem);
                img = mem.ToArray();
            }
            return (BitmapSource)new ImageSourceConverter().ConvertFrom(img);
        }


        public static BitmapSource ReduceQualityJPG(BitmapSource image, int Quality, out byte[] IMarray)
        {

            JpegBitmapEncoder enc = new JpegBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(image));
            enc.QualityLevel = Quality;

            byte[] img;
            using (MemoryStream mem = new MemoryStream())
            {
                enc.Save(mem);
                img = mem.ToArray();
            }
            IMarray = img;
            return (BitmapSource)new ImageSourceConverter().ConvertFrom(img);
        }


        public static BitmapSource ReduceQualityJPG(BitmapSource image, int Quality, out byte[] IMarray, int width, int height)
        {

            JpegBitmapEncoder enc = new JpegBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(ChangeSize(image,width,height) as BitmapSource));
            enc.QualityLevel = Quality;

            byte[] img;
            using (MemoryStream mem = new MemoryStream())
            {
                enc.Save(mem);
                img = mem.ToArray();
            }
            IMarray = img;
            return (BitmapSource)new ImageSourceConverter().ConvertFrom(img);
        }


        public static ImageSource ChangeSize(BitmapSource image ,int width, int height)
        {
            TransformedBitmap bitmap = new TransformedBitmap(image,
            new ScaleTransform(
                (double)width / image.PixelWidth,
                (double)height / image.PixelHeight));

            return bitmap;
        }


        public static void SaveImageToFilePNG(BitmapSource image, string path)
        {

            PngBitmapEncoder enc = new PngBitmapEncoder();

            enc.Frames.Add(BitmapFrame.Create(image));

            using (MemoryStream mem = new MemoryStream())
            {
                enc.Save(mem);
                byte[] img = mem.ToArray();
                File.WriteAllBytes(path, img);
            }
        }


        public static void SaveImageToFileJPG(BitmapSource image, string path)
        {

            JpegBitmapEncoder enc = new JpegBitmapEncoder();

            enc.Frames.Add(BitmapFrame.Create(image));

            using (MemoryStream mem = new MemoryStream())
            {
                enc.Save(mem);
                byte[] img = mem.ToArray();
                File.WriteAllBytes(path, img);
            }
        }


        public static byte[] ToByteData(string Path)
        {
            byte[] imgraw = File.ReadAllBytes(Path);
            return imgraw;
        }


        public static byte[] BitmapSourceToByteData(BitmapSource img)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();

            byte[] txt = null;
            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Frames.Add(BitmapFrame.Create(img));
                encoder.Save(stream);
                txt = stream.ToArray();
                stream.Close();
            }

            return txt;
        }


        public static bool ImageFromWebToBase64Data(string url, out byte[] ibytes)
        {
                WebClient webClient = new WebClient();
                ibytes = webClient.DownloadData(url);
            
            if (IsValidImage(ibytes))
            {
                return true;
            }

            return false;
        }


        public static bool IsValidImage(byte[] data)
        {
            
            try
            {
                ByteDataToImage(data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
            return true;
            
        }


        public static ImageSource ByteDataToImage(byte[] Data)
        {
            using (MemoryStream ms = new MemoryStream(Data))
            {
                return BitmapFrame.Create(
                    BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad));
            }
        }


    }
}
