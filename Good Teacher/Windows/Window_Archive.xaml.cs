using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Good_Teacher.Class;
using Good_Teacher.Class.Save;
using Good_Teacher.Controls;
using Good_Teacher.Windows.Special;
using Good_Teacher.Class.Workers;

namespace Good_Teacher.Windows
{
    /// <summary>
    /// Interaction logic for Window_Archive.xaml
    /// </summary>
    public partial class Window_Archive : Window
    {
        DataStore datastore;

        public Window_Archive(DataStore data)
        {
            InitializeComponent();

            datastore = data;

            LoadImages();

            //Load models
            string path = "";
            if (LocalPath.GetDirectoryPath(out path))
            {
                if (Directory.Exists(path + "\\Resources\\"))
                {
                    string mpath = System.IO.Path.Combine(path, "Resources\\Media\\");

                    if (Directory.Exists(mpath))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(mpath);

                        FileInfo[] info = dirInfo.GetFiles("*.*");
                        foreach (FileInfo f in info)
                        {
                            StackPanel stackPanel = new StackPanel();
                            stackPanel.Orientation = Orientation.Horizontal;
                            stackPanel.Margin = new Thickness(0, 0, 0, 5);

                            TextBlock textBlock = new TextBlock();
                            textBlock.Text = f.Name;
                            textBlock.FontSize = 14;
                            textBlock.VerticalAlignment = VerticalAlignment.Center;



                            FlatButton Play = new FlatButton();
                            Play.Content = new Image
                            {
                                Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/Media/Play.png")),
                                VerticalAlignment = VerticalAlignment.Center,
                                Width = 20,
                                Height = 20
                            };
                            Play.Click += Play_Click;
                            Play.Margin = new Thickness(15, 0, 0, 0);
                            Play.DefaultBrush = null;
                            Play.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                            Play.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                            Play.Height = 24;
                            Play.Width = 24;
                            Play.Tag = f.FullName;
                            Play.VerticalContentAlignment = VerticalAlignment.Center;


                            FlatButton Delete = new FlatButton();
                            Delete.Content = new Image
                            {
                                Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/DeleteFill.png")),
                                VerticalAlignment = VerticalAlignment.Center,
                                Width = 20,
                                Height = 20
                            };
                            Delete.Click += DeleteMedia_Click;
                            Delete.Margin = new Thickness(10, 0, 0, 0);
                            Delete.DefaultBrush = null;
                            Delete.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                            Delete.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                            Delete.Height = 24;
                            Delete.Width = 24;
                            Delete.Tag = f.FullName;
                            Delete.VerticalContentAlignment = VerticalAlignment.Center;

                            stackPanel.Children.Add(textBlock);
                            stackPanel.Children.Add(Play);
                            stackPanel.Children.Add(Delete);
                            MenuMedia_list.Items.Add(stackPanel);
                        }
                    }



                    path = System.IO.Path.Combine(path, "Resources");

                    Debug.WriteLine("Path: " + path);


                    IEnumerable<string> mfiles = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                        .Where(s => s.EndsWith(".obj", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".3ds", StringComparison.OrdinalIgnoreCase)
                        || s.EndsWith(".lwo", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".stl", StringComparison.OrdinalIgnoreCase)
                        || s.EndsWith(".off", StringComparison.OrdinalIgnoreCase));


                    foreach (string file in mfiles)
                    {
                        StackPanel panel = new StackPanel();
                        panel.Orientation = Orientation.Horizontal;

                        Label label = new Label();
                        label.Content = System.IO.Path.GetFileNameWithoutExtension(file);
                        label.FontSize = 15;

                        FlatButton ViewB = new FlatButton();
                        ViewB.Content = new Image
                        {
                            Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/View.png")),
                            VerticalAlignment = VerticalAlignment.Center,
                            Width = 20,
                            Height = 20
                        };
                        ViewB.Click += ViewBModel_Click;
                        ViewB.Margin = new Thickness(10, 0, 0, 0);
                        ViewB.DefaultBrush = null;
                        ViewB.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                        ViewB.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                        ViewB.Height = 24;
                        ViewB.Width = 24;
                        ViewB.Tag = file;
                        ViewB.VerticalContentAlignment = VerticalAlignment.Center;



                        FlatButton Delete = new FlatButton();
                        Delete.Content = new Image
                        {
                            Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/DeleteFill.png")),
                            VerticalAlignment = VerticalAlignment.Center,
                            Width = 20,
                            Height = 20
                        };
                        Delete.Click += DeleteModel_Click;
                        Delete.Margin = new Thickness(10, 0, 0, 0);
                        Delete.DefaultBrush = null;
                        Delete.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                        Delete.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                        Delete.Height = 24;
                        Delete.Width = 24;
                        Delete.Tag = file;
                        Delete.VerticalContentAlignment = VerticalAlignment.Center;


                        panel.Children.Add(label);
                        panel.Children.Add(ViewB);
                        panel.Children.Add(Delete);

                        MenuModels_list.Items.Add(panel);
                    }
                }
            }

        }


        private void LoadImages()
        {

            int k = 0;

            //Load images
            foreach (KeyValuePair<string, ResourceData> res in datastore.archive.Res)
            {
                StackPanel panel = new StackPanel();
                Rectangle img = new Rectangle();

                int ws, hs;
                datastore.archive.GetImageSize(res.Key, out ws, out hs);

                if (ws < hs)
                    img.Fill = new ImageBrush(datastore.archive.GetImage(res.Key, 70));
                else
                    img.Fill = new ImageBrush(datastore.archive.GetImageByHeight(res.Key, 70));

                ((ImageBrush)img.Fill).Stretch = Stretch.UniformToFill;
                img.Width = 70;
                img.Height = 70;
                img.Stroke = new SolidColorBrush(Colors.Black);
                img.StrokeThickness = 1;

                Label lab = new Label();
                lab.Margin = new Thickness(5, 0, 0, 0);
                lab.VerticalContentAlignment = VerticalAlignment.Center;
                lab.Content = Strings.ResStrings.Width + ": " + ws + "   " + Strings.ResStrings.Height + ": " + hs + "   " + Strings.ResStrings.Size + ": " + FileWorker.GetBytesReadableTwoDecimals(res.Value.Data.LongLength);


                FlatButton ViewB = new FlatButton();
                ViewB.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/View.png")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 20,
                    Height = 20
                };
                ViewB.Click += ViewB_Click;
                ViewB.Margin = new Thickness(10, 0, 0, 0);
                ViewB.DefaultBrush = null;
                ViewB.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                ViewB.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                ViewB.Height = 24;
                ViewB.Width = 24;
                RenderOptions.SetBitmapScalingMode(ViewB, BitmapScalingMode.Fant);
                ViewB.Tag = res.Key;
                ViewB.VerticalContentAlignment = VerticalAlignment.Center;



                FlatButton compr = new FlatButton();
                compr.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/Compress.png")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 20,
                    Height = 20
                };
                compr.Click += Compress_Click;
                compr.Margin = new Thickness(10, 0, 0, 0);
                compr.DefaultBrush = null;
                compr.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                compr.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                compr.Height = 24;
                compr.Width = 24;
                compr.Tag = res.Key;
                compr.VerticalContentAlignment = VerticalAlignment.Center;


                FlatButton save = new FlatButton();
                save.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/SaveBig.png")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 20,
                    Height = 20
                };
                save.Click += Save_Click;
                save.Margin = new Thickness(10, 0, 0, 0);
                save.DefaultBrush = null;
                save.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                save.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                save.Height = 24;
                save.Width = 24;
                save.Tag = res.Key;
                save.VerticalContentAlignment = VerticalAlignment.Center;

                /*
                Button save = new Button();
                save.Content = Strings.ResStrings.Save;
                save.Click += Save_Click;
                save.Margin = new Thickness(10,0,0,0); 
                save.Height = 26;
                save.MinWidth = 50;
                save.Tag = res.Key;
                save.VerticalContentAlignment = VerticalAlignment.Center;
                */


                FlatButton Delete = new FlatButton();
                Delete.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/DeleteFill.png")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 20,
                    Height = 20
                };
                Delete.Click += Delete_Click;
                Delete.Margin = new Thickness(10, 0, 0, 0);
                Delete.DefaultBrush = null;
                Delete.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                Delete.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                Delete.Height = 24;
                Delete.Width = 24;
                Delete.Tag = res.Key;
                Delete.VerticalContentAlignment = VerticalAlignment.Center;


                /*
                Button Delete = new Button();
                Delete.Content = Strings.ResStrings.Delete;
                Delete.Click += Delete_Click;
                Delete.Margin = new Thickness(10, 0, 0, 0);
                Delete.Height = 26;
                Delete.MinWidth = 50;
                Delete.Tag = res.Key;
                Delete.VerticalContentAlignment = VerticalAlignment.Center;
                */


                panel.Children.Add(img);
                panel.Children.Add(lab);
                panel.Children.Add(compr);
                panel.Children.Add(ViewB);
                panel.Children.Add(save);
                panel.Children.Add(Delete);
                panel.Orientation = Orientation.Horizontal;
                panel.Margin = new Thickness(2, 2, 0, 2);
                /*
                item.Click += Item_Click;
                item.Tag = elem;
                */
                if (k % 2 == 0)
                    panel.Background = new SolidColorBrush(Colors.WhiteSmoke);

                k++;

                Menu_list.Children.Add(panel);
            }

        }


        private void Compress_Click(object sender, MouseEventArgs e)
        {
            Window_ImageCompress imageCompress = new Window_ImageCompress(datastore , ((Control)sender).Tag.ToString());
            imageCompress.Owner = this;
            imageCompress.ShowDialog();

            if(imageCompress.Compressed)
            {
                Menu_list.Children.Clear();
                LoadImages();
            }
        }

        private void Play_Click(object sender, MouseEventArgs e)
        {
            Window_PlayMedia playMedia = new Window_PlayMedia(((Control)sender).Tag.ToString());
            playMedia.Owner = this;
            playMedia.Show();
        }


        private void DeleteMedia_Click(object sender, MouseEventArgs e)
        {
            if (((Control)sender).Tag != null)
            {
                if (MessageBox.Show(Strings.ResStrings.DeleteDialogMedia, Strings.ResStrings.Delete, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if(File.Exists(((Control)sender).Tag.ToString()))
                    File.Delete(((Control)sender).Tag.ToString());

                    MenuMedia_list.Items.Remove(((StackPanel)((Control)sender).Parent));
                }
            }
        }

        private void DeleteModel_Click(object sender, RoutedEventArgs e)
        {
            if (((Control)sender).Tag != null)
            {
                if (MessageBox.Show(Strings.ResStrings.DeleteDialogModel, Strings.ResStrings.Delete, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Directory.Delete(System.IO.Path.GetDirectoryName(((Control)sender).Tag.ToString()), true);
                        MenuModels_list.Items.Remove(((StackPanel)((Control)sender).Parent));
                    }catch(Exception ex)
                    {
                        Debug.WriteLine(ex);
                        MessageBox.Show(Strings.ResStrings.ErrorDelete,Strings.ResStrings.Error);
                    }
                }
            }
        }

        private void ViewB_Click(object sender, MouseEventArgs e)
        {
            Window_ShowImage showImage = new Window_ShowImage(datastore, ((Control)sender).Tag.ToString());
            showImage.Owner = this;
            showImage.Show();
        }


        private void ViewBModel_Click(object sender, MouseEventArgs e)
        {
            Window_ShowModel showModel = new Window_ShowModel(((Control)sender).Tag.ToString());
            showModel.Owner = this;
            if(!showModel.CloseOnStart)
            showModel.Show();
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (((Control)sender).Tag != null)
            {
                if (MessageBox.Show(Strings.ResStrings.DeleteDialog, Strings.ResStrings.Delete, MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    datastore.archive.Res.Remove(((Control)sender).Tag.ToString());
                    Menu_list.Children.Remove(((StackPanel)((Control)sender).Parent));
                }
            }
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "image.png";
            saveFile.Filter = "PNG "+Strings.ResStrings.Image+" (.png)|*.png|"+ "JPG " + Strings.ResStrings.Image + " (.jpg)|*.jpg|" + Strings.ResStrings.AllFiles+"|*.*";

            if(saveFile.ShowDialog() == true)
            {
                if (((Control)sender).Tag != null)
                {
                    if (System.IO.Path.GetExtension(saveFile.FileName) == ".jpg")
                    {
                        Debug.WriteLine("Saving as JPG");
                        ImageWorker.SaveImageToFileJPG((BitmapSource)datastore.archive.GetImage(((Control)sender).Tag.ToString()), saveFile.FileName);
                    }
                    else
                    {
                        Debug.WriteLine("Saving as PNG");
                        ImageWorker.SaveImageToFilePNG((BitmapSource)datastore.archive.GetImage(((Control)sender).Tag.ToString()), saveFile.FileName);
                    }

                }

                MessageBox.Show(Strings.ResStrings.SavedTo + ": " + saveFile.FileName, Strings.ResStrings.Success);
            }
        }


    }
}
