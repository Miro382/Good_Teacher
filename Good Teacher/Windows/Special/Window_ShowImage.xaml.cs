using System.Windows;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_ShowImage.xaml
    /// </summary>
    public partial class Window_ShowImage : Window
    {
        public Window_ShowImage(DataStore data ,string key)
        {
            InitializeComponent();

            img.Source = data.archive.GetImage(key);
            img.MaxWidth = ((BitmapSource)img.Source).PixelWidth;
            img.MaxHeight = ((BitmapSource)img.Source).PixelHeight;
        }
    }
}
