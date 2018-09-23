using Good_Teacher.Class.Serialization;
using Microsoft.Win32;
using SharpAvi;
using SharpAvi.Codecs;
using SharpAvi.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_ExportToVideo.xaml
    /// </summary>
    public partial class Window_ExportToVideo : Window
    {
        DataStore data;

        public Window_ExportToVideo(DataStore datastore)
        {
            InitializeComponent();
            data = datastore;
        }


        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = Strings.ResStrings.Video+" (.avi)|.avi";
            saveFileDialog.FileName = Strings.ResStrings.Video;

            if (saveFileDialog.ShowDialog(this)==true)
            {

                Canvas canvas = new Canvas();
                canvas.Width = data.CanvasW;
                canvas.Height = data.CanvasH;

                Debug.WriteLine("Canvas W: " + (int)canvas.Width + "  H: " + (int)canvas.Height);

                var writer = new AviWriter(saveFileDialog.FileName)
                {
                    FramesPerSecond = 10,
                    // Emitting AVI v1 index in addition to OpenDML index (AVI v2)
                    // improves compatibility with some software, including 
                    // standard Windows programs like Media Player and File Explorer
                    EmitIndex1 = true
                };

                var stream = writer.AddMotionJpegVideoStream((int)data.CanvasW, (int)data.CanvasH, NB_Quality.GetInt(80));

                for (int can = 0; can < data.pages.Count; can++)
                {
                    canvas = CanvasSaveLoad.LoadSpecificCanvas(data, can);
                    CanvasSaveLoad.ToPresentationMode(canvas);
                    CanvasSaveLoad.SimulateCanvas(canvas);

                    //I convert canvas to Imagesource bytes
                    byte[] canvasdata = BitmapSourceToArray((BitmapSource)new ImageSourceConverter().ConvertFrom(CanvasWriter.SaveCanvasToImgSimulate(canvas, (int)canvas.Width, (int)canvas.Height)));

                    int howmany = (int)(NB_DefaultTime.GetFloat(3) * 10);

                    if (RB_UseTransitionTime.IsChecked == true)
                    {
                        if (data.pages[can].transitionType == Class.Enumerators.TransitionTypeEnum.TransitionType.Automatic)
                        {
                            howmany = (int)(((float)data.pages[can].TransitionMove / 1000) * 10);

                        }
                        else if (data.pages[can].transitionType == Class.Enumerators.TransitionTypeEnum.TransitionType.AutomaticClose)
                        {
                            howmany = (int)(((float)data.pages[can].TransitionMove / 1000) * 10);
                            for (int i = 0; i < howmany; i++)
                            {
                                stream.WriteFrame(true, canvasdata, 0, canvasdata.Length);
                            }
                            //Debug.WriteLine("ADD and BREAK");
                            break;
                        }
                    }

                    //Debug.WriteLine("ADD");
                    for (int i = 0; i < howmany; i++)
                    {
                        stream.WriteFrame(true, canvasdata, 0, canvasdata.Length);
                    }
                }

                writer.Close();

                MessageBox.Show(Strings.ResStrings.ExportToVideoSuccess+Environment.NewLine+saveFileDialog.FileName, Strings.ResStrings.Success);
            }
        }

        private byte[] BitmapSourceToArray(BitmapSource bitmapSource)
        {
            // Stride = (width) x (bytes per pixel)
            int stride = (int)bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel / 8);
            byte[] pixels = new byte[(int)bitmapSource.PixelHeight * stride];

            bitmapSource.CopyPixels(pixels, stride, 0);

            return pixels;
        }

    }
}
