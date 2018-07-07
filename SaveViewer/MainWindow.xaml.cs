using Good_Teacher.Class;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Windows;

namespace SaveViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string normaljson = "";

        public const string TestMakerFileExtension = "gtch";
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveEditor save = new SaveEditor();

            OpenFileDialog openfile = new OpenFileDialog();
            openfile.FileName = "File."+ TestMakerFileExtension;
            openfile.Filter = Good_Teacher.Strings.ResStrings.GoodTeacherTestFormat + "|*." + TestMakerFileExtension + "|" + Good_Teacher.Strings.ResStrings.AllFiles + "|*.*";

            if (openfile.ShowDialog() == true)
            {

                try
                {
                    string jdata = save.LoadWithCompressionJson(openfile.FileName);
                    richtextbox.SelectAll();
                    richtextbox.Selection.Text = jdata;
                    normaljson = jdata;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    MessageBox.Show(Good_Teacher.Strings.ResStrings.ErrorLoad, Good_Teacher.Strings.ResStrings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            richtextbox.SelectAll();
            richtextbox.Selection.Text = JValue.Parse(normaljson).ToString(Formatting.Indented);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            richtextbox.SelectAll();
            richtextbox.Selection.Text = normaljson;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (richtextbox.FontSize < 13)
                richtextbox.FontSize = 16;
            else
                richtextbox.FontSize = 12;
        }
    }
}
