using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for SelectButton.xaml
    /// </summary>
    public partial class SelectButton : UserControl
    {
        public delegate void CheckedChanged(object sender, bool IsChecked);
        public event CheckedChanged OnCheckChanged;

        public Brush DefaultBrush;

        public static readonly DependencyProperty HoverProperty =
            DependencyProperty.Register("Hover_Brush", typeof(Brush), typeof(SelectButton));

        public static readonly DependencyProperty CheckedBrushProperty =
            DependencyProperty.Register("Checked_Brush", typeof(Brush), typeof(SelectButton));

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Image_Source", typeof(ImageSource), typeof(SelectButton));

        public static readonly DependencyProperty StrechProperty =
            DependencyProperty.Register("Image_Stretch", typeof(Stretch), typeof(SelectButton), new PropertyMetadata(Stretch.Uniform));

        public Brush Hover
        {
            get { return (Brush)GetValue(HoverProperty); }
            set { SetValue(HoverProperty, value);}
        }

        public Brush OnChecked
        {
            get { return (Brush)GetValue(CheckedBrushProperty); }
            set { SetValue(CheckedBrushProperty, value); }
        }

        private bool IsCheckedButton = false;


        public ImageSource Image_Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value);
            }
        }

        public Stretch Image_Stretch
        {
            get { return (Stretch)GetValue(StrechProperty); }
            set { SetValue(StrechProperty, value);
            }
        }

        private void selectbutton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsCheckedButton)
                selectbutton.Background = Hover;
        }

        private void selectbutton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsCheckedButton)
                selectbutton.Background = DefaultBrush;
        }

        private void selectbutton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetChecked(!IsCheckedButton);
        }

        public SelectButton()
        {
            InitializeComponent();
            DefaultBrush = selectbutton.Background;
        }


        public void SetChecked(bool Checked)
        {
            if (Checked == false)
                selectbutton.Background = DefaultBrush;
            else
                selectbutton.Background = OnChecked;

            IsCheckedButton = Checked;

            if (OnCheckChanged != null)
                OnCheckChanged(this,Checked);
        }

        public void SetCheckedNoCall(bool Checked)
        {
            if (Checked == false)
                selectbutton.Background = DefaultBrush;
            else
                selectbutton.Background = OnChecked;

            IsCheckedButton = Checked;
        }


        public bool IsChecked()
        {
            return IsCheckedButton;
        }




    }

}
