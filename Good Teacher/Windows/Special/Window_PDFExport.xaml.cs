using Good_Teacher.Class;
using Good_Teacher.Class.Save.Output;
using Good_Teacher.Class.Serialization;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_PDFExport.xaml
    /// </summary>
    public partial class Window_PDFExport : Window
    {

        DataStore data;
        PresentationOutput output;

        public bool IsExported = false;
        private bool OutputExport = false;


        public Window_PDFExport(DataStore dataStore)
        {
            InitializeComponent();

            data = dataStore;
            OutputExport = false;
        }


        public Window_PDFExport(PresentationOutput PRoutput)
        {
            InitializeComponent();

            output = PRoutput;
            OutputExport = true;
        }



        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }


        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9,-]+");
            return !regex.IsMatch(text);
        }


        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }


        private void Export_Click(object sender, RoutedEventArgs e)
        {
            IsExported = false;

            if(OutputExport)
            {
                ExportOutputToPDF();
            }
            else
            {
                ExportCanvasToPDF();
            }
        }


        public static byte[] SaveInkCanvasToPng(InkCanvas canvas)
        {
            canvas.UpdateLayout();
            Size sizet = new Size();
            canvas.Arrange(new Rect(sizet));

            Size size = new Size(canvas.ActualWidth, canvas.ActualHeight);
            canvas.Margin = new Thickness(0, 0, 0, 0);

            canvas.Measure(size);
            canvas.Arrange(new Rect(size));
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            RenderTargetBitmap bitmapTarget = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Default);
            bitmapTarget.Render(canvas);
            encoder.Frames.Add(BitmapFrame.Create(bitmapTarget));

            MemoryStream ms = new MemoryStream();
            encoder.Save(ms);
            return ms.ToArray();
        }


        public static byte[] SaveInkCanvasToJpg(InkCanvas canvas,int quality)
        {

            canvas.UpdateLayout();
            Size sizet = new Size();
            canvas.Arrange(new Rect(sizet));

            Size size = new Size(canvas.ActualWidth, canvas.ActualHeight);
            canvas.Margin = new Thickness(0, 0, 0, 0);

            canvas.Measure(size);
            canvas.Arrange(new Rect(size));
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.QualityLevel = quality;
            RenderTargetBitmap bitmapTarget = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Default);
            bitmapTarget.Render(canvas);
            encoder.Frames.Add(BitmapFrame.Create(bitmapTarget));

            MemoryStream ms = new MemoryStream();
            encoder.Save(ms);
            return ms.ToArray();
        }


        private void ExportOutputToPDF()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.FileName = Strings.ResStrings.File+".pdf";
            saveFileDialog.Filter = Good_Teacher.Strings.FormatStrings.PDF + "| *.pdf|" + Good_Teacher.Strings.ResStrings.AllFiles + "| *.*";

            if (saveFileDialog.ShowDialog() == true)
            {

                PdfDocument pdf = new PdfDocument();

                for (int i = 0; i < output.Pages.Count; i++)
                {


                    InkCanvas canvas = new InkCanvas();
                    canvas.Width = output.W;
                    canvas.Height = output.H;
                    canvas.Margin = new Thickness(0, 0, 0, 0);

                    if (output.Pages[i].strokeCollection != null)
                    {
                        using (Stream stream = new MemoryStream(output.Pages[i].strokeCollection))
                        {
                            StrokeCollection strokes = new StrokeCollection(stream);
                            canvas.Strokes.Add(strokes);
                        }
                    }

                    canvas.Background = new ImageBrush(GetImage(output.Pages[i].Image));

                    BitmapSource bs;

                    if (RB_BQuality.IsChecked == true)
                    {
                        bs = (BitmapSource)ImageWorker.ByteDataToImage(SaveInkCanvasToPng(canvas));
                    }
                    else
                    {
                        int quality = 90;

                        int.TryParse(TB_Quality.Text, out quality);

                        bs = (BitmapSource)ImageWorker.ByteDataToImage(SaveInkCanvasToJpg(canvas, quality));
                    }

                    var ximg = XImage.FromBitmapSource(bs);
                    ximg.Interpolate = false;

                    PdfPage pdfPage = pdf.AddPage();
                    pdfPage.Width = XUnit.FromPresentation(output.W);
                    pdfPage.Height = XUnit.FromPresentation(output.H);
                    XGraphics xgr = XGraphics.FromPdfPage(pdfPage);


                    xgr.DrawImage(
                    ximg,
                    0, 0, pdfPage.Width, pdfPage.Height);

                    /*
                    BitmapSource bs = (BitmapSource)GetImage(output.Pages[i].Image);

                    var ximg = XImage.FromBitmapSource(bs);
                    ximg.Interpolate = false;

                    PdfPage pdfPage = pdf.AddPage();
                    pdfPage.Width = XUnit.FromPresentation(output.W);
                    pdfPage.Height = XUnit.FromPresentation(output.H);
                    XGraphics xgr = XGraphics.FromPdfPage(pdfPage);


                    xgr.DrawImage(
                    ximg,
                    0, 0, pdfPage.Width, pdfPage.Height);
                    */
                }

                pdf.Info.Creator = Strings.ResStrings.AppName;
                pdf.Info.CreationDate = DateTime.Now;
                pdf.Info.Subject = TB_Subject.Text;
                pdf.Info.Author = TB_Author.Text;
                pdf.Info.Title = TB_Title.Text;
                pdf.Info.Keywords = TB_Keywords.Text;

                pdf.Save(saveFileDialog.FileName);

                IsExported = true;
                Close();
            }
        }



        private void ExportCanvasToPDF()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.FileName = Strings.ResStrings.File+".pdf";
            saveFileDialog.Filter = Strings.FormatStrings.PDF + "| *.pdf|" + Strings.ResStrings.AllFiles + "| *.*";

            if (saveFileDialog.ShowDialog() == true)
            {

                PdfDocument pdf = new PdfDocument();

                for (int i = 0; i < data.pages.Count; i++)
                {

                    Canvas can = CanvasSaveLoad.LoadSpecificCanvas(data, i);
                    CanvasSaveLoad.ToPresentationMode(can);
                    CanvasSaveLoad.SimulateCanvas(can);


                    can.UpdateLayout();
                    Size size = new Size();
                    can.Measure(size);
                    can.Arrange(new Rect(size));

                    BitmapSource bs;

                    if (RB_BQuality.IsChecked == true)
                    {
                        bs = (BitmapSource)ImageWorker.ByteDataToImage(CanvasWriter.SaveCanvasToImgSimulateFullPng(can));
                    }
                    else
                    {
                        int quality = 90;

                        int.TryParse(TB_Quality.Text, out quality);

                        bs = (BitmapSource)ImageWorker.ByteDataToImage(CanvasWriter.SaveCanvasToImgSimulateFullJpg(can, quality));
                    }

                    var ximg = XImage.FromBitmapSource(bs);
                    ximg.Interpolate = false;

                    PdfPage pdfPage = pdf.AddPage();
                    pdfPage.Width = XUnit.FromPresentation(data.CanvasW);
                    pdfPage.Height = XUnit.FromPresentation(data.CanvasH);
                    XGraphics xgr = XGraphics.FromPdfPage(pdfPage);

                    xgr.DrawImage(
                    ximg,
                    0, 0, pdfPage.Width, pdfPage.Height);
                }

                pdf.Info.Creator = Strings.ResStrings.AppName;
                pdf.Info.CreationDate = DateTime.Now;
                pdf.Info.Subject = TB_Subject.Text;
                pdf.Info.Author = TB_Author.Text;
                pdf.Info.Title = TB_Title.Text;
                pdf.Info.Keywords = TB_Keywords.Text;

                pdf.Save(saveFileDialog.FileName);

                IsExported = true;
                Close();
            }
        }



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

    }
}
