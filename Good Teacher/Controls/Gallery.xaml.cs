using Good_Teacher.Class.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for Gallery.xaml
    /// </summary>
    public partial class Gallery : UserControl, IDisposable
    {

        public List<GalleryImage> images
        {
            get { return imagesp; }
            set { imagesp = value; }
        }

        public List<ImageSource> imgS
        {
            get { return imgSs; }
            set { imgSs = value; }
        }

        public bool ShowEnabled
        {
            get { return ShowEnableds; }
            set { ShowEnableds = value; }
        }

        public string ForegroundKey
        {
            get { return ForegroundKeys; }
            set { ForegroundKeys = value; }
        }

        public uint Time
        {
            get { return Times; }
            set { Times = value; }
        }

        public double TimeToTranslate
        {
            get { return TimeToTranslates; }
            set { TimeToTranslates = value; }
        }

        private List<GalleryImage> imagesp = new List<GalleryImage>();
        private List<ImageSource> imgSs = new List<ImageSource>();

        private uint Times = 3;
        private double TimeToTranslates = 0.7;

        private string ForegroundKeys = "";

        private uint actual = 3;

        private int ActualPage = 0;

        private bool reverse = false;
        private bool CanGoToNext = true;
        private double CanGoTimer = 1;

        private bool ShowEnableds = true;

        private int lastCircle = 0;

        System.Timers.Timer GoNextTimer, timer;

        private SolidColorBrush SelectedCircle = new SolidColorBrush(Colors.White);
        private SolidColorBrush UnselectedCircle = new SolidColorBrush(Colors.Transparent);

        public Gallery()
        {
            InitializeComponent();

            actual = Time;

            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(RTimerElapsed);
            timer.Enabled = true;


            GoNextTimer = new System.Timers.Timer();
            GoNextTimer.Interval = 200;
            GoNextTimer.Elapsed += new System.Timers.ElapsedEventHandler(GoNextTimerTimerElapsed);
            GoNextTimer.Enabled = true;

            GCanvas.Width = Width;
            GCanvas.Height = Height;
            CurrentImage.Width = Width;
            CurrentImage.Height = Height;
            NewImage.Width = Width;
            NewImage.Height = Height;

            if(imgS.Count>0)
            CurrentImage.Source = imgS[0];
        }

        private void GoNextTimerTimerElapsed(object sender, ElapsedEventArgs e)
        {
            CanGoTimer -= 0.2;

            if (CanGoTimer <= 0)
            {
                CanGoToNext = true;
            }
        }

        private void RTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (ShowEnabled)
            {
                actual--;

                if (actual < 1 && images!=null)
                {
                    try
                    {
                        if (App.Current != null)
                        {
                            App.Current.Dispatcher.Invoke((Action)delegate
                            {
                                MakeAnimation();
                            });
                        }
                    }catch(Exception ex)
                    {
                        Debug.WriteLine(""+ex);
                    }
                    actual = Time;
                }
            }
        }


        public void MakeAnimation()
        {
            if (images != null)
            {
                int to = ActualPage + 1;
                ActualPage++;

                if (to >= images.Count)
                {
                    to = 0;
                    ActualPage = 0;
                }

                if (!reverse)
                {
                    Canvas.SetLeft(CurrentImage, 0);
                    Canvas.SetLeft(NewImage, CurrentImage.Width);
                    NewImage.Source = imgS[to];

                    DoubleAnimation moveAnimX = new DoubleAnimation(Canvas.GetLeft(CurrentImage), -CurrentImage.Width, new Duration(TimeSpan.FromSeconds(TimeToTranslate)));
                    DoubleAnimation NmoveAnimX = new DoubleAnimation(CurrentImage.Width, 0, new Duration(TimeSpan.FromSeconds(TimeToTranslate)));

                    moveAnimX.DecelerationRatio = 0.5f;
                    NmoveAnimX.DecelerationRatio = 0.5f;

                    CurrentImage.BeginAnimation(Canvas.LeftProperty, moveAnimX);
                    NewImage.BeginAnimation(Canvas.LeftProperty, NmoveAnimX);
                    reverse = true;
                }
                else
                {
                    Canvas.SetLeft(NewImage, 0);
                    Canvas.SetLeft(CurrentImage, CurrentImage.Width);
                    CurrentImage.Source = imgS[to];

                    DoubleAnimation moveAnimX = new DoubleAnimation(Canvas.GetLeft(NewImage), 0 - NewImage.Width, new Duration(TimeSpan.FromSeconds(TimeToTranslate)));
                    DoubleAnimation NmoveAnimX = new DoubleAnimation(CurrentImage.Width, 0, new Duration(TimeSpan.FromSeconds(TimeToTranslate)));

                    moveAnimX.DecelerationRatio = 0.5f;
                    NmoveAnimX.DecelerationRatio = 0.5f;

                    NewImage.BeginAnimation(Canvas.LeftProperty, moveAnimX);
                    CurrentImage.BeginAnimation(Canvas.LeftProperty, NmoveAnimX);
                    reverse = false;
                }
                CanGoToNext = false;
                CanGoTimer = TimeToTranslate;

                UpdateCirclesAndText();
            }

        }



        public void MakeAnimationReverse()
        {
            if (images != null)
            {
                int to = ActualPage - 1;
                ActualPage--;

                if (to < 0)
                {
                    to = images.Count - 1;
                    ActualPage = images.Count - 1;
                }

                if (!reverse)
                {
                    Canvas.SetLeft(CurrentImage, 0);
                    Canvas.SetLeft(NewImage, CurrentImage.Width);
                    NewImage.Source = imgS[to];

                    DoubleAnimation moveAnimX = new DoubleAnimation(Canvas.GetLeft(CurrentImage), CurrentImage.Width, new Duration(TimeSpan.FromSeconds(TimeToTranslate)));
                    DoubleAnimation NmoveAnimX = new DoubleAnimation(-CurrentImage.Width, 0, new Duration(TimeSpan.FromSeconds(TimeToTranslate)));

                    moveAnimX.DecelerationRatio = 0.5f;
                    NmoveAnimX.DecelerationRatio = 0.5f;

                    CurrentImage.BeginAnimation(Canvas.LeftProperty, moveAnimX);
                    NewImage.BeginAnimation(Canvas.LeftProperty, NmoveAnimX);
                    reverse = true;
                }
                else
                {
                    Canvas.SetLeft(NewImage, 0);
                    Canvas.SetLeft(CurrentImage, CurrentImage.Width);
                    CurrentImage.Source = imgS[to];

                    DoubleAnimation moveAnimX = new DoubleAnimation(Canvas.GetLeft(NewImage), NewImage.Width, new Duration(TimeSpan.FromSeconds(TimeToTranslate)));
                    DoubleAnimation NmoveAnimX = new DoubleAnimation(-CurrentImage.Width, 0, new Duration(TimeSpan.FromSeconds(TimeToTranslate)));

                    moveAnimX.DecelerationRatio = 0.5f;
                    NmoveAnimX.DecelerationRatio = 0.5f;

                    NewImage.BeginAnimation(Canvas.LeftProperty, moveAnimX);
                    CurrentImage.BeginAnimation(Canvas.LeftProperty, NmoveAnimX);
                    reverse = false;
                }
                CanGoToNext = false;
                CanGoTimer = TimeToTranslate;

                UpdateCirclesAndText();
            }

        }


        private void DrawCirclesAndText()
        {
            CircleItems.Children.Clear();

            for (int i = 0; i < imgS.Count; i++)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 8;
                ellipse.Height = 8;

                if (i == ActualPage)
                {
                    ellipse.Fill = SelectedCircle;
                    ellipse.Stroke = SelectedCircle;
                    ellipse.StrokeThickness = 1;
                }
                else
                {
                    ellipse.Fill = UnselectedCircle;
                    ellipse.Stroke = SelectedCircle;
                    ellipse.StrokeThickness = 1;
                }

                lastCircle = ActualPage;

                if (i > 0)
                    ellipse.Margin = new Thickness(5, 0, 0, 0);
                ellipse.Tag = i;
                ellipse.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
                CircleItems.Children.Add(ellipse);
            }

            Description.Text = images[ActualPage].Text;
        }


        private void UpdateCirclesAndText()
        {
            ((Ellipse)CircleItems.Children[ActualPage]).Fill = SelectedCircle;
            ((Ellipse)CircleItems.Children[lastCircle]).Fill = UnselectedCircle;
            lastCircle = ActualPage;

            Description.Text = images[ActualPage].Text;
        }


        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool right = true;

            if (CanGoToNext)
            {
                int page = (int)((Ellipse)sender).Tag;

                if (page < ActualPage)
                    right = false;

                if (page != ActualPage)
                {
                    if (right)
                    {
                        page -= 1;
                        ActualPage = page;
                        actual = Time;
                        MakeAnimation();
                    }
                    else
                    {
                        page += 1;
                        ActualPage = page;
                        actual = Time;
                        MakeAnimationReverse();
                    }
                }

            }
        }


        public void RefreshAndUpdate()
        {
            GCanvas.Width = Width;
            GCanvas.Height = Height;
            CurrentImage.Width = Width;
            CurrentImage.Height = Height;
            NewImage.Width = Width;
            NewImage.Height = Height;

            actual = Time;
            ActualPage = 0;

            DrawCirclesAndText();

            if (imgS.Count > 0)
            {
                CurrentImage.Source = imgS[0];
                NewImage.Source = imgS[0];
            }
        }

        public void RefreshTime()
        {
            actual = Time;
        }


        public void LoadImageSources(DataStore data)
        {
            foreach(GalleryImage gal in images)
            {
                if(MainWindow.OPTIMIZEDMODE)
                    imgS.Add(data.archive.GetImageOptimal(gal.ImageKey,(int)Width,(int)Height));
                else
                    imgS.Add(data.archive.GetImage(gal.ImageKey));
            }
        }

        public void AddGalleryImage(GalleryImage galimg)
        {
            images.Add(galimg);
        }


        public void ClearGallery()
        {
            actual = Time;
            ActualPage = 0;
            images.Clear();
            imgS.Clear();
        }


        public void AddGalleryImage(GalleryImage galimg, ImageSource imageSource)
        {
            images.Add(galimg);
            imgS.Add(imageSource);
        }


        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GCanvas.Width = Width;
            GCanvas.Height = Height;
            CurrentImage.Width = Width;
            CurrentImage.Height = Height;
            NewImage.Width = Width;
            NewImage.Height = Height;
        }

        private void Left_MouseEnter(object sender, MouseEventArgs e)
        {
            Left.Opacity = 0.7f;
        }

        private void Left_MouseLeave(object sender, MouseEventArgs e)
        {
            Left.Opacity = 1;
        }

        private void Left_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CanGoToNext)
            {
                actual = Time;
                MakeAnimationReverse();
            }
        }

        private void Right_MouseEnter(object sender, MouseEventArgs e)
        {
            Right.Opacity = 0.7;
        }

        private void Right_MouseLeave(object sender, MouseEventArgs e)
        {
            Right.Opacity = 1;
        }

        private void Right_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CanGoToNext)
            {
                actual = Time;
                MakeAnimation();
            }
        }


        public void SetStretch(Stretch stretch)
        {
            CurrentImage.Stretch = stretch;
            NewImage.Stretch = stretch;
        }

        public Stretch GetStretch()
        {
            return CurrentImage.Stretch;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    images = null;
                    imgS = null;
                    GoNextTimer.Dispose();
                    timer.Dispose();
                    GoNextTimer = null;
                    timer = null;
                }
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Gallery() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
