using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for Image_Control.xaml
    /// </summary>
    public partial class Image_Control : UserControl
    {
        public Image_Control()
        {
            InitializeComponent();
        }


        public Image_Control(ImageSource iSource)
        {
            InitializeComponent();

            CanvasImage.Source = iSource;
            SizeLabel.Content = Strings.ResStrings.Width + ": " + ((BitmapSource)iSource).PixelWidth + "   " + Strings.ResStrings.Height + ": " + ((BitmapSource)iSource).PixelHeight;
        }

        public Image_Control(ImageSource iSource, int w, int h)
        {
            InitializeComponent();

            CanvasImage.Source = iSource;
            SizeLabel.Content = Strings.ResStrings.Width + ": " + w + "   " + Strings.ResStrings.Height + ": " + h;
        }

    }
}
