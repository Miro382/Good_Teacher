using Good_Teacher.Class.Actions;
using Good_Teacher.Class.Serialization.Content_Ser;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for CButton.xaml
    /// </summary>
    public partial class CButton : UserControl
    {

        public delegate void ClickDelegate(FrameworkElement sender, MouseButtonEventArgs e);
        public event ClickDelegate Click;

        public double OpacityN { get; set; } = 1;

        public double OpacityHover { get; set; } = 1;

        public double OpacityClick { get; set; } = 1;

        public ContentCreator contentCreator { get; set; } = new ContentCreator();

        public bool ChangeHover { get; set; } = true;

        public bool ChangeClick { get; set; } = true;

        public Stretch stretchN { get; set; }

        public Stretch stretchH { get; set; }

        public Stretch stretchC { get; set; }

        public Stretch stretchFor { get; set; }

        public string keyN { get; set; }

        public string keyH { get; set; }

        public string keyC { get; set; }

        public string keyFor { get; set; }

        public List<IActions> actions = new List<IActions>();

        public static readonly DependencyProperty DefaultProperty =
            DependencyProperty.Register("Button_Default_Brush", typeof(Brush), typeof(FlatButton), new PropertyMetadata(null));

        public static readonly DependencyProperty HoverProperty =
            DependencyProperty.Register("Button_Hover_Brush", typeof(Brush), typeof(FlatButton));

        public static readonly DependencyProperty ClickedBrushProperty =
            DependencyProperty.Register("Button_Clicked_Brush", typeof(Brush), typeof(FlatButton));

        public Brush Hover
        {
            get { return (Brush)GetValue(HoverProperty); }
            set { SetValue(HoverProperty, value); }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Click != null)
                Click(this, e);
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


        public CButton()
        {
            InitializeComponent();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {

            Opacity = OpacityHover;

            if (ChangeHover)
            {
                Background = Hover;

                if (Background is ImageBrush)
                    ((ImageBrush)Background).Stretch = stretchH;
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Opacity = OpacityN;

            if (ChangeHover)
            {
                Background = DefaultBrush;

                if (Background is ImageBrush)
                    ((ImageBrush)Background).Stretch = stretchN;
            }
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Opacity = OpacityClick;

            if (ChangeClick)
            {
                Background = ClickBrush;

                if (Background is ImageBrush)
                    ((ImageBrush)Background).Stretch = stretchC;
            }
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Opacity = OpacityHover;

            if (ChangeClick)
            {
                if (ChangeHover)
                    Background = Hover;
                else
                    Background = DefaultBrush;

                if (Background is ImageBrush)
                    ((ImageBrush)Background).Stretch = stretchH;
            }
        }

    }
}
