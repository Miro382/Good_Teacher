using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_PlayMedia.xaml
    /// </summary>
    public partial class Window_PlayMedia : Window
    {
        public Window_PlayMedia()
        {
            InitializeComponent();
        }


        public Window_PlayMedia(string PathToPlay)
        {
            InitializeComponent();
            MediaPlay.Source = new Uri(PathToPlay);

            MediaPlay.MediaOpened += (sender, e) =>
            {
                if (!MediaPlay.HasVideo && MediaPlay.HasAudio)
                {
                    Image image = new Image();
                    image.Width = 64;
                    image.Height = 64;
                    image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/Media/Music.png"));
                    image.VerticalAlignment = VerticalAlignment.Center;
                    image.HorizontalAlignment = HorizontalAlignment.Center;
                    
                    MGrid.Children.Add(image);
                }
            };
        }

    }
}
