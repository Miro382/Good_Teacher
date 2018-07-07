using Good_Teacher.Class.Save;
using Good_Teacher.Class.Workers;
using Good_Teacher.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_FontManager.xaml
    /// </summary>
    public partial class Window_FontManager : Window
    {
        DataStore data;

        public Window_FontManager(DataStore dataStore)
        {
            InitializeComponent();

            data = dataStore;

            AddFonts();
        }


        public void AddFonts()
        {
            SP_Fonts.Children.Clear();
            foreach(FontPackage fontp in data.FontManager)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;

                Image image = new Image();
                image.Width = 48;
                image.Height = 48;
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Good Teacher;component/Resources/Specific/FontFile.png"));
                image.Margin = new Thickness(5);

                Label label = new Label();
                label.FontSize = 16;
                label.Content = fontp.FontFamilyName;
                label.VerticalAlignment = VerticalAlignment.Center;
                label.Margin = new Thickness(5);

                FontFamily fontFamily;
                if (FontWorker.GetFontFamilyByString(fontp.FontFamilyName, out fontFamily))
                {
                    label.FontFamily = fontFamily;
                }


                Label label2 = new Label();
                label2.FontSize = 12;
                label2.Content = "["+fontp.FontFamilyName+"]";
                label2.VerticalAlignment = VerticalAlignment.Center;
                label2.FontStyle = FontStyles.Italic;
                label2.Margin = new Thickness(5);


                FlatButton Delete = new FlatButton();
                Delete.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/DeleteFill.png")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 20,
                    Height = 20
                };
                Delete.Click += DeleteFont_Click;
                Delete.Margin = new Thickness(10, 0, 0, 0);
                Delete.DefaultBrush = null;
                Delete.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                Delete.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                Delete.Height = 24;
                Delete.Width = 24;
                Delete.Tag = stackPanel;
                Delete.VerticalContentAlignment = VerticalAlignment.Center;

                stackPanel.Children.Add(image);
                stackPanel.Children.Add(label);
                stackPanel.Children.Add(label2);
                stackPanel.Children.Add(Delete);

                stackPanel.Tag = fontp;

                SP_Fonts.Children.Add(stackPanel);
            }
        }


        private void DeleteFont_Click(object sender, MouseEventArgs e)
        {
            
            FontPackage fontp = (FontPackage)((StackPanel)((Control)sender).Tag).Tag;

            data.FontManager.Remove(fontp);

            AddFonts();
            FontWorker.LoadFonts(data);
        }


        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.Filter = "Font files (*.ttf)|*.ttf|All files|*.*";

            // Get the selected file name and display in a TextBox 
            if (dlg.ShowDialog() == true)
            {
                // Open document 
                string filename = dlg.FileName;
                string familyname = "";
                foreach(FontFamily family in Fonts.GetFontFamilies(dlg.FileName))
                {
                    Debug.WriteLine(family.FamilyNames.Count+"   "+family);
                    foreach (KeyValuePair<XmlLanguage,string> sfamily in family.FamilyNames)
                    {
                        Debug.WriteLine(sfamily.Value);
                        familyname = sfamily.Value;
                    }

                }
                data.FontManager.Add(new Class.Save.FontPackage(File.ReadAllBytes(dlg.FileName), familyname));
            }

            FontWorker.LoadFonts(data);
            AddFonts();
        }


    }
}
