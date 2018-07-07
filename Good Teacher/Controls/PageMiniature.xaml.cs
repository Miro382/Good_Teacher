using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for PageMiniature.xaml
    /// </summary>
    public partial class PageMiniature : UserControl
    {
        double W, H,BM,TM,LM,RM;

        public PageMiniature()
        {
            InitializeComponent();
        }

        public void SaveData()
        {
            W = Width;
            H = Height;
            BM = Margin.Bottom;
            TM = Margin.Top;
            LM = Margin.Left;
            RM = Margin.Right;
        }

        public void SetData(int page, BitmapSource bitmapSource)
        {
            Label_PageNumber.Content = "" + (page+1);
            B_PageMiniature.Background = new ImageBrush(bitmapSource);
            ((ImageBrush)B_PageMiniature.Background).Stretch = Stretch.UniformToFill;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation Wa = new DoubleAnimation(Width, W * 1.1, TimeSpan.FromMilliseconds(100));
            DoubleAnimation Ha = new DoubleAnimation(Height, H * 1.1, TimeSpan.FromMilliseconds(100));

            this.BeginAnimation(FrameworkElement.WidthProperty, Wa);
            this.BeginAnimation(FrameworkElement.HeightProperty, Ha);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation Wa = new DoubleAnimation(Width, W, TimeSpan.FromMilliseconds(100));
            DoubleAnimation Ha = new DoubleAnimation(Height, H, TimeSpan.FromMilliseconds(100));

            this.BeginAnimation(FrameworkElement.WidthProperty, Wa);
            this.BeginAnimation(FrameworkElement.HeightProperty, Ha);


            //Width = W;
            //Height = H;
            Opacity = 1f;
        }

        private void UserControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Opacity = 0.8f;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Margin = new Thickness(LM - (Width - W) / 2, TM - (Height - H) / 2, RM - (Width - W) / 2, BM - (Height - H)/2);
        }

        private void UserControl_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Opacity = 1f;
        }
    }
}
