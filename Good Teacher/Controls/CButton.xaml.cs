using Good_Teacher.Class.Actions;
using Good_Teacher.Class.Serialization.Content_Ser;
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

        public delegate void ClickDelegate(CButton sender, MouseButtonEventArgs e);
        public event ClickDelegate Click;

        public double OpacityN
        {
            get { return OpacityNs; }
            set { OpacityNs = value; }
        }

        public double OpacityHover
        {
            get { return OpacityHovers; }
            set { OpacityHovers = value; }
        }

        public double OpacityClick
        {
            get { return OpacityClicks; }
            set { OpacityClicks = value; }
        }

        public ContentCreator contentCreator
        {
            get { return contentCreators; }
            set { contentCreators = value; }
        }

        public IActions action
        {
            get { return actions; }
            set { actions = value; }
        }

        public bool ChangeHover
        {
            get { return ChangeHovers; }
            set { ChangeHovers = value; }
        }

        public bool ChangeClick
        {
            get { return ChangeClicks; }
            set { ChangeClicks = value; }
        }

        public Stretch stretchN
        {
            get { return stretchNs; }
            set { stretchNs = value; }
        }

        public Stretch stretchH
        {
            get { return stretchHs; }
            set { stretchHs = value; }
        }

        public Stretch stretchC
        {
            get { return stretchCs; }
            set { stretchCs = value; }
        }

        public Stretch stretchFor
        {
            get { return stretchFors; }
            set { stretchFors = value; }
        }

        public string keyN
        {
            get { return keyNs; }
            set { keyNs = value; }
        }

        public string keyH
        {
            get { return keyHs; }
            set { keyHs = value; }
        }

        public string keyC
        {
            get { return keyCs; }
            set { keyCs = value; }
        }

        public string keyFor
        {
            get { return keyFors; }
            set { keyFors = value; }
        }

        private double OpacityHovers = 1;
        private double OpacityClicks = 1;
        private double OpacityNs = 1;

        private ContentCreator contentCreators = new ContentCreator();

        private IActions actions = null;

        private bool ChangeHovers = true, ChangeClicks = true;

        private Stretch stretchNs,stretchHs,stretchCs, stretchFors;

        private string keyNs, keyHs, keyCs, keyFors;

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
