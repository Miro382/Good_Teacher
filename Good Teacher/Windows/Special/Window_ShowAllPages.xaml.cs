using Good_Teacher.Class.Serialization;
using Good_Teacher.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_ShowAllPages.xaml
    /// </summary>
    public partial class Window_ShowAllPages : Window
    {

        private const int MiniatureW = 220, MiniatureH = 124;

        Canvas IMG_can = new Canvas();

        DataStore data;

        DispatcherTimer timer;

        private int ActualLoadPosition = 0;

        public int ClickedID { get; private set; } = 0;
        public bool IsOK { get; private set; } = false;


        public Window_ShowAllPages(DataStore datastore)
        {
            InitializeComponent();

            data = datastore;

            for (int i = 0; i < data.pages.Count;i++)
            {
                PageMiniature pageMiniature = new PageMiniature();
                pageMiniature.Margin = new Thickness(12);
                pageMiniature.Width = MiniatureW;
                pageMiniature.Height = MiniatureH;

                pageMiniature.VerticalAlignment = VerticalAlignment.Top;
                pageMiniature.HorizontalAlignment = HorizontalAlignment.Left;

                pageMiniature.SaveData();
                pageMiniature.SetData(i, null);

                pageMiniature.Tag = i;

                pageMiniature.MouseLeftButtonUp += PageMiniature_MouseLeftButtonUp;

                WP_Pages.Children.Add(pageMiniature);
            }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

        }


        private void PageMiniature_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClickedID = (int)((PageMiniature)sender).Tag;
            IsOK = true;
            Close();
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            if (ActualLoadPosition < data.pages.Count)
            {
                IMG_can = CanvasSaveLoad.LoadSpecificCanvas(data, ActualLoadPosition);
                IMG_can.Width = MainWindow.CanvasW;
                IMG_can.Height = MainWindow.CanvasH;

                BitmapSource bitmapSource = (BitmapSource)new ImageSourceConverter().ConvertFrom(CanvasWriter.SaveCanvasToImgSimulate(IMG_can, MiniatureW, MiniatureH));
                ((PageMiniature)WP_Pages.Children[ActualLoadPosition]).SetData(ActualLoadPosition, bitmapSource);

                ActualLoadPosition++;
            }
            else
            {
                timer.Stop();
            }
        }


    }
}
