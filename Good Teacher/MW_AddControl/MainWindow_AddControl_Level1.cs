using Good_Teacher.Class.Serialization.Content_Ser;
using Good_Teacher.Controls;
using HelixToolkit.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Good_Teacher
{
    public partial class MainWindow : Window
    {
        public void AddLevel1Control(Point p)
        {
            switch (ControlTag)
            {
                case 12:
                    {

                        Label label = new Label();
                        label.Content = Strings.ResStrings.Label;

                        if ((CanvasW + CanvasH) > 6000)
                            label.FontSize = 42;
                        else if ((CanvasW + CanvasH) > 4000)
                            label.FontSize = 32;
                        else if ((CanvasW + CanvasH) > 2500)
                            label.FontSize = 24;
                        else
                            label.FontSize = 14;

                        label.Focusable = true;

                        label.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(label, 2);
                        Canvas.SetLeft(label, p.X - label.DesiredSize.Width / 2);
                        Canvas.SetTop(label, p.Y - label.DesiredSize.Height / 2);


                        label.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                        label.MouseLeftButtonUp += Control_MouseLeftButtonUp;
                        label.MouseMove += Control_MouseMove;
                        label.MouseLeave += Control_MouseLeave;

                        AddEvents(label);
                        DesignCanvas.Children.Add(label);
                    }
                    break;

                case 11:
                    {
                        RichTextBox txt = new RichTextBox();
                        txt.Document.Blocks.Clear();
                        txt.Document.Blocks.Add(new Paragraph(new Run("Text")));
                        txt.FontSize = 12;

                        txt.Width = 400;
                        txt.Height = 300;

                        txt.IsUndoEnabled = true;
                        txt.UndoLimit = 150;
                        txt.IsDocumentEnabled = true;

                        txt.SpellCheck.IsEnabled = true;
                        txt.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(txt, 1);
                        Canvas.SetLeft(txt, p.X - txt.DesiredSize.Width / 2);
                        Canvas.SetTop(txt, p.Y - txt.DesiredSize.Height / 2);

                        AddEvents(txt);
                        DesignCanvas.Children.Add(txt);
                    }
                    break;

                case 13:

                    {
                        Image con = new Image();
                        con.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Controls/image.png"));

                        con.Width = 128;
                        con.Height = 128;

                        con.Focusable = true;

                        Panel.SetZIndex(con, 2);

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;
                case 14:
                    {

                        HelixViewport3D Mviewport = new HelixViewport3D();
                        Teapot teaPot = new Teapot();
                        Mviewport.Children.Add(teaPot);

                        DefaultLights lights = new DefaultLights();
                        Mviewport.Children.Add(lights);

                        Mviewport.Width = 600;
                        Mviewport.Height = 400;

                        Mviewport.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(Mviewport, 3);
                        Canvas.SetLeft(Mviewport, p.X - Mviewport.DesiredSize.Width / 2);
                        Canvas.SetTop(Mviewport, p.Y - Mviewport.DesiredSize.Height / 2);

                        AddEvents(Mviewport);
                        DesignCanvas.Children.Add(Mviewport);
                    }
                    break;
                case 15:
                    {
                        WebPage_Control web = new WebPage_Control();

                        web.Width = 600;
                        web.Height = 400;
                        //web.BackForwardVisibility = Visibility.Collapsed;

                        web.webBrowser.Navigate("");

                        web.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        web.WebUrl = "";

                        Panel.SetZIndex(web, 100);
                        Canvas.SetLeft(web, p.X - web.DesiredSize.Width / 2);
                        Canvas.SetTop(web, p.Y - web.DesiredSize.Height / 2);

                        web.ControlPanelBack = new LinearGradientBrush(Color.FromRgb(222, 222, 222), Colors.White, 90);

                        web.Name = "ID_" + data.pages[SelectedPosition].LastID++;

                        AddEvents(web);
                        DesignCanvas.Children.Add(web);
                    }
                    break;

                case 16:
                    {

                        MediaPlayer_Control con = new MediaPlayer_Control();

                        Panel.SetZIndex(con, 5);


                        con.Width = 600;
                        con.Height = 400;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);

                    }
                    break;

                case 17:
                    {

                        ContentViewer con = new ContentViewer();

                        Panel.SetZIndex(con, 3);

                        con.Width = 220;
                        con.Height = 50;

                        con.contentCreator.contents.Add(new Content_Text(Strings.ResStrings.ContentViewer, 20, 0));
                        con.Content = con.contentCreator.Create(data);

                        con.HorizontalContentAlignment = HorizontalAlignment.Center;
                        con.VerticalContentAlignment = VerticalAlignment.Center;

                        con.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);

                    }
                    break;


                case 18:
                    {
                        Gallery con = new Gallery();

                        Panel.SetZIndex(con, 3);

                        con.Width = 600;
                        con.Height = 400;

                        con.Foreground = new SolidColorBrush(Colors.White);

                        con.HorizontalContentAlignment = HorizontalAlignment.Center;
                        con.VerticalContentAlignment = VerticalAlignment.Center;

                        con.AddGalleryImage(new Class.Controls.GalleryImage(Strings.ResStrings.Text, ""), new BitmapImage(new Uri("pack://application:,,,/Resources/Background/SelectModelBackground.jpg")));
                        con.AddGalleryImage(new Class.Controls.GalleryImage(Strings.ResStrings.Text, ""), new BitmapImage(new Uri("pack://application:,,,/Resources/Background/BackgroundMat.jpg")));
                        con.AddGalleryImage(new Class.Controls.GalleryImage(Strings.ResStrings.Text, ""), new BitmapImage(new Uri("pack://application:,,,/Resources/Background/ImgBackground.jpg")));

                        con.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        con.RefreshAndUpdate();

                        con.Tag = "D";

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;


                case 19:
                    {

                        MediaPlayerController_Control con = new MediaPlayerController_Control();

                        Panel.SetZIndex(con, 6);

                        con.Width = 600;
                        con.Height = 400;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        con.G_ControlPanel.Background = new LinearGradientBrush(Color.FromRgb(162, 162, 162), Color.FromRgb(230, 230, 230), 90);

                        con.Name = "ID_" + data.pages[SelectedPosition].LastID++;

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);

                    }
                    break;


            }
        }
    }
}
