using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using Good_Teacher.Class;
using Good_Teacher.Controls;
using Good_Teacher.Windows.Special;

namespace Good_Teacher.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for DWindow_Image.xaml
    /// </summary>
    public partial class DWindow_Image : Window
    {
        public string SelectedKey = "";
        public bool SelectedImage = false;

        private bool onViewB = false,DeleteClick = false;
        private DataStore datastore;

        public DWindow_Image(DataStore data)
        {
            InitializeComponent();

            datastore = data;

            AddImages();
        }


        void AddImages()
        {
            foreach (KeyValuePair<string, ResourceData> res in datastore.archive.Res)
            {
                int ws, hs;
                datastore.archive.GetImageSize(res.Key, out ws, out hs);
                Image_Control imageC = new Image_Control(datastore.archive.GetImage(res.Key,250),ws,hs);
                imageC.Width = 250;
                imageC.Height = 150;
                imageC.Margin = new Thickness(5, 5, 5, 5);
                imageC.MouseEnter += ImageC_MouseEnter;
                imageC.MouseLeave += ImageC_MouseLeave;
                imageC.MouseLeftButtonDown += ImageC_MouseLeftButtonDown;
                imageC.MouseLeftButtonUp += ImageC_MouseLeftButtonUp;
                imageC.Effect = new DropShadowEffect
                {
                    ShadowDepth = 3,
                    Color = Colors.Black,
                    BlurRadius = 5,
                    Direction = 315,
                    Opacity = 0.6
                };
                imageC.Tag = res.Key;
                imageC.ViewButton.MouseEnter += ViewButton_MouseEnter;
                imageC.ViewButton.MouseLeave += ViewButton_MouseLeave;
                imageC.ViewButton.MouseLeftButtonDown += ViewButton_MouseLeftButtonDown;
                imageC.ViewButton.Tag = res.Key;

                imageC.DeleteButton.MouseEnter += ViewButton_MouseEnter;
                imageC.DeleteButton.MouseLeave += ViewButton_MouseLeave;
                imageC.DeleteButton.MouseLeftButtonDown += DeleteButton_MouseLeftButtonDown;
                imageC.DeleteButton.Tag = res.Key;

                ExistingImagesList.Children.Add(imageC);
            }
        }


        private void DeleteButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            onViewB = true;
            if (MessageBox.Show(Strings.ResStrings.RemoveImage, Strings.ResStrings.Delete, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                datastore.archive.RemoveData(((Control)sender).Tag.ToString());
                ExistingImagesList.Children.Clear();
                AddImages();
                DeleteClick = true;
                onViewB = true;
            }
            else
            {
                DeleteClick = true;
                onViewB = true;
            }
            //onViewB = false;
        }

        private void ViewButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window_ShowImage showImage = new Window_ShowImage(datastore, ((Control)sender).Tag.ToString());
            showImage.Owner = this;
            showImage.Show();
        }

        private void ViewButton_MouseLeave(object sender, MouseEventArgs e)
        {
            onViewB = false;
        }

        private void ViewButton_MouseEnter(object sender, MouseEventArgs e)
        {
            onViewB = true;
        }

        private void ImageC_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((Image_Control)sender).Opacity = 1f;
        }

        private void ImageC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!onViewB)
            {
                ((Image_Control)sender).Opacity = 0.6f;
                SelectedKey = ((Control)sender).Tag.ToString();
                SelectedImage = true;
                Close();
            }

            if(DeleteClick)
            {
                DeleteClick = false;
                onViewB = false;
            }
        }

        private void ImageC_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Image_Control)sender).Width = 250;
            ((Image_Control)sender).Height = 150;
            ((Image_Control)sender).Margin = new Thickness(5);
            ((Image_Control)sender).border.BorderBrush = new SolidColorBrush(Colors.Black);
            ((Image_Control)sender).border.BorderThickness = new Thickness(1);
            ((Image_Control)sender).InfoGrid.Background = new SolidColorBrush(Color.FromArgb(178, 255, 255, 255));
            ((Image_Control)sender).SizeLabel.Foreground = new SolidColorBrush(Colors.Black);
            ((Image_Control)sender).Opacity = 1;
        }

        private void ImageC_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Image_Control)sender).Width = 256;
            ((Image_Control)sender).Height = 156;
            ((Image_Control)sender).Margin = new Thickness(2,2,2,2);
            ((Image_Control)sender).border.BorderBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
            ((Image_Control)sender).border.BorderThickness = new Thickness(3);
            ((Image_Control)sender).InfoGrid.Background = new SolidColorBrush(Color.FromRgb(233, 30, 99));
            ((Image_Control)sender).SizeLabel.Foreground = new SolidColorBrush(Colors.White);
        }


        private void ButtonFromDisk_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = Strings.ResStrings.Image + " (png,jpg,jpeg,gif,tif)|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*tif|" + Strings.ResStrings.AllFiles + "|*.*";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string file in openFileDialog.FileNames)
                {
                    datastore.archive.StoreImage(file, "" + datastore.LastAddedImageID++);
                    ExistingImagesList.Children.Clear();
                    AddImages();
                }
            }
        }

        private void ButtonFromClipboard_Click(object sender, RoutedEventArgs e)
        {
            if(Clipboard.ContainsImage())
            {
                DWindow_AddFromClipboard addFromClipboard = new DWindow_AddFromClipboard();
                addFromClipboard.img.Source = Clipboard.GetImage();
                addFromClipboard.Owner = this;
                addFromClipboard.ShowDialog();

                if(addFromClipboard.AddImage)
                {
                    datastore.archive.StoreImage((BitmapSource)addFromClipboard.img.Source, "" + datastore.LastAddedImageID++);
                    ExistingImagesList.Children.Clear();
                    AddImages();
                }
            }
            else
            {
                MessageBox.Show(Strings.ResStrings.ClipboardNoImage,Strings.ResStrings.Error);
            }
        }

        private void ButtonFromWeb_Click(object sender, RoutedEventArgs e)
        {
            DWindow_AddFromWeb addFromWeb = new DWindow_AddFromWeb();
            addFromWeb.Owner = this;
            addFromWeb.ShowDialog();

            if(addFromWeb.OK)
            {
                datastore.archive.StoreImageFromWeb(addFromWeb.Address, "" + datastore.LastAddedImageID++);
                ExistingImagesList.Children.Clear();
                AddImages();
            }
        }


    }
}
