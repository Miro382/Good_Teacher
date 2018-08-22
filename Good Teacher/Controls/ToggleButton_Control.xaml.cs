using Good_Teacher.Class.Actions;
using Good_Teacher.Class.Serialization.Content_Ser;
using Good_Teacher.Class.Special;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for ToggleButton.xaml
    /// </summary>
    public partial class ToggleButton_Control : UserControl
    {
        public delegate void ClickDelegate(FrameworkElement sender, MouseButtonEventArgs e);
        public event ClickDelegate Click;

        public bool DefaultIsChecked = false;

        public bool IsChecked { get; private set; } = false;

        public double OpacityN { get; set; } = 1;

        public double OpacityHover { get; set; } = 0.9f;

        public double OpacityClick { get; set; } = 0.8f;

        public string keyN { get; set; }

        public string keyC { get; set; }

        public ContentCreator contentCreatorUnchecked { get; set; } = new ContentCreator();

        public ContentCreator contentCreatorChecked { get; set; } = new ContentCreator();

        public Stretch stretchC { get; set; }

        public Stretch stretchN { get; set; }

        public static readonly DependencyProperty CheckedBrushProperty =
            DependencyProperty.Register("Button_Checked_Brush", typeof(Brush), typeof(ToggleButton_Control), new PropertyMetadata(new SolidColorBrush(Colors.Green)));

        public static readonly DependencyProperty UncheckedBrushProperty =
            DependencyProperty.Register("Button_Unchecked_Brush", typeof(Brush), typeof(ToggleButton_Control), new PropertyMetadata(new SolidColorBrush(Colors.Red)));

        private DataStore data;

        public bool DoAnimation = true;

        public List<IActions> CheckedActions = new List<IActions>();
        public List<IActions> UncheckedActions = new List<IActions>();

        public Brush CheckedBrush
        {
            get { return (Brush)GetValue(CheckedBrushProperty); }
            set { SetValue(CheckedBrushProperty, value); }
        }

        public Brush UncheckedBrush
        {
            get { return (Brush)GetValue(UncheckedBrushProperty); }
            set { SetValue(UncheckedBrushProperty, value); }
        }

        public ToggleButton_Control()
        {
            InitializeComponent();
            Opacity = OpacityN;
            //Background = UncheckedBrush;

        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Opacity = OpacityHover;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Opacity = OpacityN;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Opacity = OpacityClick;
            CBorder.Width = Double.NaN;
            CBorder.Height = Double.NaN;
            CBorder.VerticalAlignment = VerticalAlignment.Stretch;
            CBorder.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Opacity = OpacityHover;

            Click?.Invoke(this, e);
        }

        public void SetData(DataStore datas)
        {
            data = datas;
        }

        public void SetChecked(bool Vischecked, bool IgnoreAnimation = false)
        {
            if(Vischecked)
            {
                if (DoAnimation && !IgnoreAnimation)
                {
                    BrushAnimation animation = new BrushAnimation
                    {
                        From = UncheckedBrush,
                        To = CheckedBrush,
                        Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                    };

                    Storyboard.SetTarget(animation, CBorder);
                    Storyboard.SetTargetProperty(animation, new PropertyPath("Background"));

                    var sb = new Storyboard();
                    sb.Children.Add(animation);
                    sb.Begin();
                }

                CBorder.Background = CheckedBrush;
                Ccontent.Content = contentCreatorChecked.Create(data);
                IsChecked = true;
            }
            else
            {
                if (DoAnimation && !IgnoreAnimation)
                {
                    if (UncheckedBrush is ImageBrush)
                    {
                        ((ImageBrush)UncheckedBrush).Stretch = stretchN;
                    }

                    BrushAnimation animation = new BrushAnimation
                    {
                        From = CheckedBrush,
                        To = UncheckedBrush,
                        Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                    };

                    Storyboard.SetTarget(animation, CBorder);
                    Storyboard.SetTargetProperty(animation, new PropertyPath("Background"));

                    var sb = new Storyboard();
                    sb.Children.Add(animation);
                    sb.Begin();
                }

                CBorder.Background = UncheckedBrush;
                Ccontent.Content = contentCreatorUnchecked.Create(data);
                IsChecked = false;
            }
        }


        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetChecked(!IsChecked);
        }
    }
}
