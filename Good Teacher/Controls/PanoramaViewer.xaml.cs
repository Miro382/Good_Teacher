using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for PanoramaViewer.xaml
    /// </summary>
    public partial class PanoramaViewer : UserControl
    {
        public PanoramaViewer()
        {
            InitializeComponent();
            Loaded += PanoramaViewer_Loaded;
        }

        private void PanoramaViewer_Loaded(object sender, RoutedEventArgs e)
        {
            Vector offset = VisualTreeHelper.GetOffset(panorama);
            var top = offset.Y;
            var left = offset.X;
            TranslateTransform trans = new TranslateTransform();
            trans.X = 500;
            panorama.RenderTransform = trans;

        }
    }
}
