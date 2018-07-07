using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for FlatButtonContent.xaml
    /// </summary>
    public partial class FlatButtonContent : UserControl
    {

        public static readonly DependencyProperty DefaultProperty =
            DependencyProperty.Register("Button_Default_Brush", typeof(Brush), typeof(FlatButtonContent), new PropertyMetadata(null));

        public static readonly DependencyProperty HoverProperty =
            DependencyProperty.Register("Button_Hover_Brush", typeof(Brush), typeof(FlatButtonContent));

        public static readonly DependencyProperty ClickedBrushProperty =
            DependencyProperty.Register("Button_Clicked_Brush", typeof(Brush), typeof(FlatButtonContent));

        public Brush Hover
        {
            get { return (Brush)GetValue(HoverProperty); }
            set { SetValue(HoverProperty, value); }
        }

        public Brush ClickBrush
        {
            get { return (Brush)GetValue(ClickedBrushProperty); }
            set { SetValue(ClickedBrushProperty, value); }
        }

        public Brush DefaultBrush
        {
            get { return (Brush)GetValue(DefaultProperty); }
            set { SetValue(DefaultProperty, value); }
        }

        public FlatButtonContent()
        {
            InitializeComponent();
        }

        private void flatbutton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Background = ClickBrush;
        }

        private void flatbutton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Background = Hover;
        }

        private void flatbutton_MouseEnter(object sender, MouseEventArgs e)
        {
            Background = Hover;
        }

        private void flatbutton_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = DefaultBrush;
        }
    }
}
