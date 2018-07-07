using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for MediaPlayerController_Control.xaml
    /// </summary>
    public partial class MediaPlayerController_Control : UserControl
    {

        public static readonly DependencyProperty ControlPanelBackground =
                DependencyProperty.Register("ControlPanelBackground", typeof(Brush), typeof(MediaPlayerController_Control), new PropertyMetadata(new LinearGradientBrush(Color.FromRgb(162,162,162),Color.FromRgb(230,230,230),90)));

        public Brush ControlPanelBack {
            get { return (Brush)GetValue(ControlPanelBackground); }
            set { SetValue(ControlPanelBackground, value); }
        }

        public string PathToCPImage = "";
        public Stretch CPStretch = Stretch.Uniform;

        private bool Playing = false;
        public bool Autoplay { get; set; } = false;
        public int Volume { get; set; } = 50;
        DispatcherTimer dispatcherTimer;

        double TotalSec = 0;

        public MediaPlayerController_Control()
        {
            InitializeComponent();
        }


        private void FlatButtonPlayPause_Click(object sender, MouseEventArgs e)
        {
            if(Playing)
            {
                Playing = false;
                FB_PlayPause.Image_Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/Media/Play.png"));
                ME_MediaPlayer.Pause();
            }
            else
            {
                Playing = true;
                FB_PlayPause.Image_Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/Media/Pause.png"));

                if (ME_MediaPlayer.NaturalDuration.HasTimeSpan && ME_MediaPlayer.Position.TotalSeconds == ME_MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds)
                {
                    ME_MediaPlayer.Position = TimeSpan.Zero;
                }

                ME_MediaPlayer.Play();
            }
        }


        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(IsInitialized)
            {
                ME_MediaPlayer.Volume = (SL_Volume.Value/100);
                Debug.WriteLine(ME_MediaPlayer.Volume);
            }
        }

        private void FlatButtonPlayFromStart_Click(object sender, MouseEventArgs e)
        {
            ME_MediaPlayer.Position = TimeSpan.Zero;
            ME_MediaPlayer.Play();
        }

        private void ME_MediaPlayer_Loaded(object sender, RoutedEventArgs e)
        {

            //Very strange situation... Volume 100 is like Volume 50
            if (Volume > 99)
            {
                ME_MediaPlayer.Volume = 0.99;
                SL_Volume.Value = 99;
            }
            else
            {
                ME_MediaPlayer.Volume = (double)Volume / 100;
                SL_Volume.Value = Volume;
            }

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500);
            dispatcherTimer.Start();

            if(Autoplay)
            {
                ME_MediaPlayer.Play();
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdateMediaTimes();
        }

        void UpdateMediaTimes()
        {
            if (ME_MediaPlayer.NaturalDuration.HasTimeSpan)
            {
                if (ME_MediaPlayer.Position.Hours > 0)
                {
                    LB_CTime.Text = ME_MediaPlayer.Position.Hours + ":" + ME_MediaPlayer.Position.Minutes.ToString("00") + ":" +
                        ME_MediaPlayer.Position.Seconds.ToString("00");
                }
                else
                {
                    LB_CTime.Text = ME_MediaPlayer.Position.Minutes + ":" +
                        ME_MediaPlayer.Position.Seconds.ToString("00");
                }

                double value = (ME_MediaPlayer.Position.TotalSeconds / TotalSec) * 100;

                if (!double.IsNaN(value))
                {
                    PB_TimePlay.Value = value;
                }
            }
        }

        private void ME_MediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {

            if (ME_MediaPlayer.NaturalDuration.TimeSpan.Hours>0)
            {
                LB_Time.Text = ME_MediaPlayer.NaturalDuration.TimeSpan.Hours+":" + ME_MediaPlayer.NaturalDuration.TimeSpan.Minutes.ToString("00")+":"+
                    ME_MediaPlayer.NaturalDuration.TimeSpan.Seconds.ToString("00");
            }
            else
            {
                LB_Time.Text = ME_MediaPlayer.NaturalDuration.TimeSpan.Minutes + ":" +
                    ME_MediaPlayer.NaturalDuration.TimeSpan.Seconds.ToString("00");
            }

            TotalSec = ME_MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void Border_Unloaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            dispatcherTimer.IsEnabled = false;
            dispatcherTimer.Tick -= new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer = null;
        }

        private void PB_TimePlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(PB_TimePlay);
            double tsn = (TotalSec / 100) * p.X;
            ME_MediaPlayer.Position = TimeSpan.FromSeconds(tsn);
            UpdateMediaTimes();
        }

        private void ME_MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            Playing = false;
            FB_PlayPause.Image_Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/Media/Play.png"));
            ME_MediaPlayer.Pause();
        }

    }
}
