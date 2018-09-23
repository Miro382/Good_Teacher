using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for ScalableImage.xaml
    /// </summary>
    public partial class ScalableImage : UserControl
    {
        public string ImageKey = "";
        public int DefaultW = 0;
        public int DefaultH = 0;


        public static readonly DependencyProperty ControlPanelBackground =
        DependencyProperty.Register("ControlPanelBackground", typeof(Brush), typeof(ScalableImage), new PropertyMetadata(new LinearGradientBrush(Colors.White, Color.FromRgb(236, 240, 241), 90)));

        public Brush ControlPanelBack
        {
            get { return (Brush)GetValue(ControlPanelBackground); }
            set { SetValue(ControlPanelBackground, value); }
        }

        public string PathToCPImage = "";
        public Stretch CPStretch = Stretch.Uniform;


        double currentPositionX, currentPositionY;

        public ScalableImage()
        {
            InitializeComponent();
        }

        private void FlatButtonZoomCancel_Click(object sender, MouseEventArgs e)
        {
            SliderZoom.Value = 100;
            M_Img.LayoutTransform = null;
            M_Img.UpdateLayout();
        }

        private void SliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SliderZoom.Value == 100)
            {
                M_Img.LayoutTransform = null;
                M_Img.UpdateLayout();
            }
            else
            {
                ScaleTransform scale = new ScaleTransform((SliderZoom.Value / 100), (SliderZoom.Value / 100));
                M_Img.LayoutTransform = scale;
                M_Img.UpdateLayout();
            }
        }

        private void UserControl_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool handle = (Keyboard.Modifiers & ModifierKeys.Shift) > 0;
            if (!handle)
                return;

            if (e.Delta > 0)
                SliderZoom.Value += 10;
            else
                SliderZoom.Value -= 10;
        }

        private void M_Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void M_Img_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Cursor = null;
        }

        private void M_Img_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = null;
        }

        private void M_Img_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double deltaDirectionX = currentPositionX - e.GetPosition(this).X;
                currentPositionX = e.GetPosition(this).X;

                double deltaDirectionY = currentPositionY - e.GetPosition(this).Y;
                currentPositionY = e.GetPosition(this).Y;

                ScrollViewer_IMG.ScrollToHorizontalOffset(ScrollViewer_IMG.HorizontalOffset + deltaDirectionX);
                ScrollViewer_IMG.ScrollToVerticalOffset(ScrollViewer_IMG.VerticalOffset + deltaDirectionY);
            }
            else
            {
                currentPositionX = e.GetPosition(this).X;
                currentPositionY = e.GetPosition(this).Y;
            }
        }

    }
}
