using Good_Teacher.Class.Save;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for DWindow_MediaSelector.xaml
    /// </summary>
    public partial class DWindow_MediaSelector : Window
    {
        public bool OK = false;
        public string FileName = "";

        public DWindow_MediaSelector()
        {
            InitializeComponent();
            AddItems();
        }


        public void AddItems()
        {
            ItemList.Items.Clear();


            string path = "";
            if (LocalPath.GetDirectoryPath(out path))
            {
                path = System.IO.Path.Combine(path, "Resources\\Media\\");

                if (Directory.Exists(path))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(path);

                    FileInfo[] info = dirInfo.GetFiles("*.*");
                    foreach (FileInfo f in info)
                    {
                        Button button = new Button();
                        button.FontSize = 14;
                        button.Content = ""+f.Name;
                        button.MinHeight = 35;
                        button.Margin = new Thickness(10);
                        button.Click += ButtonSelect_Click;
                        button.Tag = f.Name;
                        ItemList.Items.Add(button);
                    }
                }
            }

        }


        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            if (((Control)sender).Tag != null)
            {
                OK = true;
                FileName = ((Control)sender).Tag.ToString();
                Close();
            }
        }


        private void ButtonAddNew_Click(object sender, RoutedEventArgs e)
        {

            string path = "";
            if (LocalPath.GetDirectoryPath(out path))
            {

                OpenFileDialog fileDialog = new OpenFileDialog();

                fileDialog.Title = Strings.ResStrings.AddNewMedia;
                fileDialog.Filter = Strings.ResStrings.AllFiles + "|*.*|" + Strings.ResStrings.Video + "(.3g2,.3gp,.asf,.avi,.m2v,.m4v,.mov,.mp4,.mpeg,.wmv,.ts,.vob)|*.3g2;*.3gp;*.asf;*.avi;*.m2v;*.m4v;*.mov;*.mp4;*.mpeg;*.mpg;*.wmv;*.ts;*.vob|"
                    + Strings.ResStrings.Audio + "(.mp3,.wav,.wma,.bwf,.m4B,.m4A,.aiff)|*.mp3;*.wav;*.wma;*.bwf;*.m4B;*.m4A;*.aiff";
                if (fileDialog.ShowDialog() == true)
                {
                    LocalPath.CopyFileToResourcesMedia(fileDialog.FileName);
                    AddItems();
                }
            }
            else
            {
                MessageBox.Show(Strings.ResStrings.NotSaved, Strings.ResStrings.NotSavedTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

    }
}
