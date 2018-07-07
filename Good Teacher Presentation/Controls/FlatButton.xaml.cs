using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Good_Teacher_Presentation.Controls
{
    /// <summary>
    /// Interaction logic for FlatButton.xaml
    /// </summary>
    public partial class FlatButton : UserControl
    {

        public delegate void OnClick(object sender, MouseEventArgs e);
        public event OnClick Click;

        private bool Clicked = false;
        private DispatcherTimer timer = new DispatcherTimer();


        public static readonly DependencyProperty DefaultProperty =
            DependencyProperty.Register("Default_Brush", typeof(Brush), typeof(FlatButton), new PropertyMetadata(null));

        public static readonly DependencyProperty HoverProperty =
            DependencyProperty.Register("Hover_Brush", typeof(Brush), typeof(FlatButton));

        public static readonly DependencyProperty ClickedBrushProperty =
            DependencyProperty.Register("Clicked_Brush", typeof(Brush), typeof(FlatButton));

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Image_Source", typeof(ImageSource), typeof(FlatButton));

        public static readonly DependencyProperty StrechProperty =
            DependencyProperty.Register("Image_Stretch", typeof(Stretch), typeof(FlatButton), new PropertyMetadata(Stretch.Uniform));

        public static readonly DependencyProperty RepeaterProperty =
    DependencyProperty.Register("Repeat", typeof(bool), typeof(FlatButton), new PropertyMetadata(false));

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


        public ImageSource Image_Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set
            {
                SetValue(SourceProperty, value);
            }
        }

        public Stretch Image_Stretch
        {
            get { return (Stretch)GetValue(StrechProperty); }
            set
            {
                SetValue(StrechProperty, value);
            }
        }

        public bool Repeat
        {
            get { return (bool)GetValue(RepeaterProperty); }
            set
            {
                SetValue(RepeaterProperty, value);
            }
        }


        public FlatButton()
        {
            InitializeComponent();

            Loaded += FlatButton_Loaded;
        }

        private void FlatButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (Repeat)
            {
                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 0, 0, 400);
                timer.Tick -= Timer_Tick;
                timer.Tick += Timer_Tick;
                timer.IsEnabled = true;
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if(Repeat)
            {
                if (Clicked)
                {
                    timer.Interval = new TimeSpan(0, 0, 0, 0, 45);

                    if (Click != null)
                        Click(this, null);
                }
            }
        }

        private void flatbutton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            flatbutton.Background = ClickBrush;

            if (Repeat)
            {
                timer.Stop();
                timer.Interval = new TimeSpan(0,0,0,0,400);
                timer.Start();
            }

            Clicked = true;

            if (Click != null)
                Click(this,e);
        }

        private void flatbutton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            flatbutton.Background = Hover;
            timer.Stop();
            Clicked = false;
        }

        private void flatbutton_MouseEnter(object sender, MouseEventArgs e)
        {
            flatbutton.Background = Hover;
        }

        private void flatbutton_MouseLeave(object sender, MouseEventArgs e)
        {
            flatbutton.Background = DefaultBrush;
            timer.Stop();
            Clicked = false;
        }


    }
}
