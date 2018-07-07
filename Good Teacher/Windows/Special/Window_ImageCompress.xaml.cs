using Good_Teacher.Class;
using Good_Teacher.Class.Workers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_ImageCompress.xaml
    /// </summary>
    public partial class Window_ImageCompress : Window
    {
        DataStore data;
        string IMKey = "";
        byte[] imageAR;
        int newW = 0;
        int newH = 0;
        public bool Compressed = false;

        public Window_ImageCompress(DataStore dataStore, string key)
        {
            InitializeComponent();
            data = dataStore;
            IMKey = key;

            IMG_Before.Source = data.archive.GetImage(key);
            Run_BeforeSize.Text = "" + FileWorker.GetBytesReadable(data.archive.Res[key].Data.LongLength);

            Run_Quality.Text = "" + Slider_Quality.Value;
            Run_Size.Text = data.archive.Res[IMKey].W + " x " + data.archive.Res[IMKey].H;
            Run_OrSize.Text = data.archive.Res[IMKey].W + " x " + data.archive.Res[IMKey].H;
            Compress();
        }

        void Compress()
        {
            newW = (int)(data.archive.Res[IMKey].W * (Slider_Size.Value / 100));
            newH = (int)(data.archive.Res[IMKey].H * (Slider_Size.Value / 100));
            IMG_After.Source = ImageWorker.ReduceQualityJPG(data.archive.GetImage(IMKey) as BitmapSource, (int)Slider_Quality.Value,out imageAR, (int)(data.archive.Res[IMKey].W*(Slider_Size.Value/100)), (int)(data.archive.Res[IMKey].H*(Slider_Size.Value/100)));
            Run_AfterSize.Text = "" + FileWorker.GetBytesReadable(imageAR.LongLength);
        }

        private void Slider_Quality_ThumbDragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (IsInitialized)
            {
                Run_Quality.Text = ""+(int)Slider_Quality.Value;
                Compress();
            }
        }

        private void ButtonOverwrite_Click(object sender, RoutedEventArgs e)
        {
            data.archive.Res[IMKey].Data = imageAR;
            data.archive.Res[IMKey].W = newW;
            data.archive.Res[IMKey].H = newH;
            Compressed = true;
            Close();
        }

        private void Slider_Size_ThumbDragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (IsInitialized)
            {
                Run_Size.Text = +(int)(data.archive.Res[IMKey].W * (Slider_Size.Value / 100))+" x " + (int)(data.archive.Res[IMKey].H * (Slider_Size.Value / 100));
                Compress();
            }
        }

        private void Slider_Quality_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsInitialized)
            {
                Run_Quality.Text = "" + (int)Slider_Quality.Value;
                Compress();
            }
        }

        private void Slider_Quality_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (IsInitialized)
            {
                Run_Quality.Text = "" + (int)Slider_Quality.Value;
                Compress();
            }
        }

        private void Slider_Size_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsInitialized)
            {
                Run_Size.Text = +(int)(data.archive.Res[IMKey].W * (Slider_Size.Value / 100)) + " x " + (int)(data.archive.Res[IMKey].H * (Slider_Size.Value / 100));
                Compress();
            }
        }

        private void Slider_Size_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (IsInitialized)
            {
                Run_Size.Text = +(int)(data.archive.Res[IMKey].W * (Slider_Size.Value / 100)) + " x " + (int)(data.archive.Res[IMKey].H * (Slider_Size.Value / 100));
                Compress();
            }
        }

    }
}
