using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Good_Teacher
{
    public class CanvasWriter
    {

        public static string SerializeToXAML(UIElement element)
        {
            string strXAML = System.Windows.Markup.XamlWriter.Save(element);
            return strXAML;
        }

        public static UIElement DeserializeXAML(string XAML)
        {
            return System.Windows.Markup.XamlReader.Parse(XAML) as UIElement;
        }


        public static bool LoadCanvas (Canvas canvas, string XAMLString)
        {
            try
            {
                Canvas can = DeserializeXAML(XAMLString) as Canvas;
                canvas.Children.Clear();

                while (can.Children.Count > 0)
                {
                    UIElement obj = can.Children[0];
                    can.Children.Remove(obj);
                    canvas.Children.Add(obj);
                }
                return true;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }


        public static byte[] SaveCanvasToImg(Canvas canvas,int width, int height)
        {

            Rect rect = new Rect(canvas.RenderSize);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)rect.Right,
            (int)rect.Bottom, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(canvas);

            double ratioX = (double)width / rtb.PixelWidth;
            double ratioY = (double)height / rtb.PixelHeight;
            double ratio = Math.Min(ratioX, ratioY);

            double newWidth = (int)(rtb.PixelWidth * ratio);
            double newHeight = (int)(rtb.PixelHeight * ratio);

            if(newWidth<width || newHeight<height)
            {
                newWidth *= 2;
                newHeight *= 2;
            }

            TransformedBitmap bitmap = new TransformedBitmap(rtb,
                new ScaleTransform(
                    newWidth / rtb.PixelWidth,
                    newHeight / rtb.PixelHeight));

            PngBitmapEncoder imgEncoder = new PngBitmapEncoder();

            imgEncoder.Frames.Add(BitmapFrame.Create(bitmap));

            MemoryStream ms = new MemoryStream();

            imgEncoder.Save(ms);
            ms.Close();
            return ms.ToArray();
        }



        public static byte[] SaveCanvasToImg(Canvas canvas)
        {

            Rect rect = new Rect(canvas.RenderSize);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)rect.Right,
            (int)rect.Bottom, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(canvas);

            PngBitmapEncoder imgEncoder = new PngBitmapEncoder();

            imgEncoder.Frames.Add(BitmapFrame.Create(rtb));

            MemoryStream ms = new MemoryStream();

            imgEncoder.Save(ms);
            ms.Close();
            return ms.ToArray();
        }



        public static byte[] SaveCanvasToImgSimulate(Canvas canvas, int width, int height)
        {
            Class.Serialization.CanvasSaveLoad.SimulateCanvas(canvas);
            canvas.UpdateLayout();
            Size size = new Size();
            //canvas.Measure(size);
            canvas.Arrange(new Rect(size));


            Rect rect = new Rect(canvas.RenderSize);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)rect.Right,
            (int)rect.Bottom, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(canvas);

            double ratioX = (double)width / rtb.PixelWidth;
            double ratioY = (double)height / rtb.PixelHeight;
            double ratio = Math.Min(ratioX, ratioY);

            double newWidth = (int)(rtb.PixelWidth * ratio);
            double newHeight = (int)(rtb.PixelHeight * ratio);

            
            if (newWidth < width || newHeight < height)
            {
                newWidth *= 2;
                newHeight *= 2;
            }
            

            TransformedBitmap bitmap = new TransformedBitmap(rtb,
                new ScaleTransform(
                    newWidth / rtb.PixelWidth,
                    newHeight / rtb.PixelHeight));

            PngBitmapEncoder imgEncoder = new PngBitmapEncoder();

            imgEncoder.Frames.Add(BitmapFrame.Create(bitmap));

            MemoryStream ms = new MemoryStream();

            imgEncoder.Save(ms);

            ms.Close();
            return ms.ToArray();
        }



        public static byte[] SaveCanvasToImgSimulateFullPng(UIElement canvas)
        {
            canvas.UpdateLayout();
            Size size = new Size();
            //canvas.Measure(size);
            canvas.Arrange(new Rect(size));
            Rect rect = new Rect(canvas.RenderSize);
            canvas.UpdateLayout();
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)rect.Right,
            (int)rect.Bottom, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(canvas);

            PngBitmapEncoder imgEncoder = new PngBitmapEncoder();

            imgEncoder.Frames.Add(BitmapFrame.Create(rtb));

            MemoryStream ms = new MemoryStream();

            imgEncoder.Save(ms);

            ms.Close();
            return ms.ToArray();
        }


        public static byte[] SaveCanvasToImgSimulateFullJpg(UIElement canvas, int quality)
        {
            canvas.UpdateLayout();
            Size size = new Size();
            //canvas.Measure(size);
            canvas.Arrange(new Rect(size));
            Rect rect = new Rect(canvas.RenderSize);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)rect.Right,
            (int)rect.Bottom, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(canvas);

            JpegBitmapEncoder imgEncoder = new JpegBitmapEncoder();
            imgEncoder.QualityLevel = quality;

            imgEncoder.Frames.Add(BitmapFrame.Create(rtb));

            MemoryStream ms = new MemoryStream();

            imgEncoder.Save(ms);

            ms.Close();
            return ms.ToArray();
        }



        public static byte[] SaveCanvasToImgSimulateFullJpg(UIElement canvas)
        {
            canvas.UpdateLayout();
            Size size = new Size();
            //canvas.Measure(size);
            canvas.Arrange(new Rect(size));
            Rect rect = new Rect(canvas.RenderSize);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)rect.Right,
            (int)rect.Bottom, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(canvas);

            JpegBitmapEncoder imgEncoder = new JpegBitmapEncoder();
            imgEncoder.QualityLevel = 90;

            imgEncoder.Frames.Add(BitmapFrame.Create(rtb));

            MemoryStream ms = new MemoryStream();

            imgEncoder.Save(ms);

            ms.Close();
            return ms.ToArray();
        }



    }
}
