using Good_Teacher.Class.Controls;
using Good_Teacher.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Good_Teacher.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for DWindow_GalleryImages.xaml
    /// </summary>
    public partial class DWindow_GalleryImages : Window
    {

        DataStore data;
        Gallery gallery;

        public DWindow_GalleryImages(DataStore dataStore, Gallery galleryC)
        {
            InitializeComponent();

            data = dataStore;
            gallery = galleryC;

            if (gallery.Tag == null || gallery.Tag.ToString() != "D")
            {
                foreach (GalleryImage img in gallery.images)
                {
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    stackPanel.Margin = new Thickness(0, 0, 0, 5);

                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = 128;
                    rectangle.Height = 72;
                    rectangle.Stroke = new SolidColorBrush(Colors.Black);
                    rectangle.StrokeThickness = 1;
                    rectangle.Fill = new ImageBrush(data.archive.GetImage(img.ImageKey, 128));
                    ((ImageBrush)rectangle.Fill).Stretch = Stretch.UniformToFill;


                    FlatButton Delete = new FlatButton();
                    Delete.Content = new Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/DeleteFill.png")),
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 24,
                        Height = 24
                    };
                    Delete.Click += Delete_Click;
                    Delete.Margin = new Thickness(10, 0, 0, 0);
                    Delete.DefaultBrush = null;
                    Delete.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                    Delete.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                    Delete.Height = 28;
                    Delete.Width = 28;
                    Delete.Tag = stackPanel;
                    Delete.VerticalContentAlignment = VerticalAlignment.Center;


                    TextBox textBox = new TextBox();
                    textBox.Width = 150;
                    textBox.VerticalContentAlignment = VerticalAlignment.Center;
                    textBox.Margin = new Thickness(10, 0, 0, 0);
                    textBox.Height = 20;
                    textBox.Text = img.Text;

                    stackPanel.Tag = img.ImageKey;

                    stackPanel.Children.Add(rectangle);
                    stackPanel.Children.Add(textBox);
                    stackPanel.Children.Add(Delete);
                    ItemsColl.Children.Add(stackPanel);
                }
            }


        }


        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            DWindow_Image imgsel = new DWindow_Image(data);
            imgsel.Owner = Window.GetWindow(this);
            imgsel.ShowDialog();

            if (imgsel.SelectedImage)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Margin = new Thickness(0, 0, 0, 5);

                Rectangle rectangle = new Rectangle();
                rectangle.Width = 128;
                rectangle.Height = 72;
                rectangle.Stroke = new SolidColorBrush(Colors.Black);
                rectangle.StrokeThickness = 1;
                rectangle.Fill = new ImageBrush(data.archive.GetImage(imgsel.SelectedKey,128));
                ((ImageBrush)rectangle.Fill).Stretch = Stretch.UniformToFill;


                FlatButton Delete = new FlatButton();
                Delete.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/DeleteFill.png")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 24,
                    Height = 24
                };
                Delete.Click += Delete_Click;
                Delete.Margin = new Thickness(10, 0, 0, 0);
                Delete.DefaultBrush = null;
                Delete.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                Delete.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                Delete.Height = 28;
                Delete.Width = 28;
                Delete.Tag = stackPanel;
                Delete.VerticalContentAlignment = VerticalAlignment.Center;


                TextBox textBox = new TextBox();
                textBox.Width = 150;
                textBox.VerticalContentAlignment = VerticalAlignment.Center;
                textBox.Margin = new Thickness(10, 0, 0, 0);
                textBox.Height = 20;
                textBox.Text = "";

                stackPanel.Tag = imgsel.SelectedKey;

                stackPanel.Children.Add(rectangle);
                stackPanel.Children.Add(textBox);
                stackPanel.Children.Add(Delete);
                ItemsColl.Children.Add(stackPanel);

            }
        }

        private void Delete_Click(object sender, MouseEventArgs e)
        {
            ItemsColl.Children.Remove( (UIElement)((Control)sender).Tag);
        }
        
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsColl.Children.Count > 0)
            {
                gallery.ClearGallery();
                foreach (StackPanel stp in ItemsColl.Children)
                {
                    gallery.AddGalleryImage(new GalleryImage(((TextBox)stp.Children[1]).Text, stp.Tag.ToString()));
                }

                gallery.Tag = null;

                gallery.LoadImageSources(data);
                gallery.RefreshAndUpdate();
            }
            else
            {
                gallery.Tag = "D";
                gallery.ClearGallery();
                gallery.AddGalleryImage(new Class.Controls.GalleryImage(Strings.ResStrings.Text, ""), new BitmapImage(new Uri("pack://application:,,,/Resources/Background/SelectModelBackground.jpg")));
                gallery.AddGalleryImage(new Class.Controls.GalleryImage(Strings.ResStrings.Text, ""), new BitmapImage(new Uri("pack://application:,,,/Resources/Background/BackgroundMat.jpg")));
                gallery.AddGalleryImage(new Class.Controls.GalleryImage(Strings.ResStrings.Text, ""), new BitmapImage(new Uri("pack://application:,,,/Resources/Background/ImgBackground.jpg")));
                gallery.RefreshAndUpdate();
            }
            Close();
        }

    }
}
