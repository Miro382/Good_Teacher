using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Controls.Special.EditorSpecial
{
    /// <summary>
    /// Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageButton : UserControl
    {

        public static readonly DependencyProperty DefaultProperty =
            DependencyProperty.Register("DefaultBrush", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));
            
        public static readonly DependencyProperty HoverProperty =
            DependencyProperty.Register("Hover", typeof(ImageSource), typeof(ImageButton));

        public static readonly DependencyProperty ClickedBrushProperty =
            DependencyProperty.Register("ClickBrush", typeof(ImageSource), typeof(ImageButton));

        public ImageSource Hover
        {
            get { return (ImageSource)GetValue(HoverProperty); }
            set { SetValue(HoverProperty, value); }
        }

        public ImageSource ClickBrush
        {
            get { return (ImageSource)GetValue(ClickedBrushProperty); }
            set { SetValue(ClickedBrushProperty, value); }
        }

        public ImageSource DefaultBrush
        {
            get { return (ImageSource)GetValue(DefaultProperty); }
            set { SetValue(DefaultProperty, value); }
        }

        public ImageButton()
        {
            InitializeComponent();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            img.Source = Hover;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            img.Source = DefaultBrush;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            img.Source = ClickBrush;
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            img.Source = Hover;
        }

        private void ImageButtonUC_Loaded(object sender, RoutedEventArgs e)
        {
            img.Source = DefaultBrush;
        }

    }
}
