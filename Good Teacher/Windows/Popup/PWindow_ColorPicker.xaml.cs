using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Good_Teacher.Windows.Popup
{
    /// <summary>
    /// Interaction logic for PWindow_ColorPicker.xaml
    /// </summary>
    public partial class PWindow_ColorPicker : Window
    {
        public Color color;
        public bool OK = false;
        public bool DestroyColorB = false;
        public object obj;
        DispatcherTimer dispatcherTimer;

        public PWindow_ColorPicker()
        {
            InitializeComponent();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0,0,0,20);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.IsEnabled = true;
        }

        public PWindow_ColorPicker(bool DestroyColorAvailable)
        {
            InitializeComponent();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 20);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.IsEnabled = true;

            if (DestroyColorAvailable)
            {
                DestroyColor.MouseDown -= Rectangle_MouseDown;
                DestroyColor.Fill = new SolidColorBrush(Colors.White);
                DestroyColor.StrokeThickness = 2;
                DestroyColor.Stroke = new SolidColorBrush(Colors.Red);
                DestroyColor.MouseDown += DestroyColor_MouseDown;
            }

        }

        private void DestroyColor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DestroyColorB = true;
            OK = true;
            Close();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Close();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (!IsLoaded)
            {
                if (!IsMouseOver)
                    Close();
            }

            dispatcherTimer.IsEnabled = false;

        }


        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Clicked");
            color = ((SolidColorBrush)((Rectangle)sender).Fill).Color;
            OK = true;
            DestroyColorB = false;
            Close();
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Rectangle)sender).Opacity = 0.7f;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Rectangle)sender).Opacity = 1f;
        }
    }
}
