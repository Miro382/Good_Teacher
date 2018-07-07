using Good_Teacher.Class.Workers;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_CheckForUpdates.xaml
    /// </summary>
    public partial class Window_CheckForUpdates : Window
    {
        bool closeW = false;

        public Window_CheckForUpdates()
        {
            InitializeComponent();

            string is64XVersion = "";

            #if RELEASEX64
                is64XVersion = " (x64)";
            #endif

            CurVersion.Content = Assembly.GetExecutingAssembly().GetName().Version.ToString() + " " + MainWindow.VersionSpecialIdentificationName + is64XVersion;

            try
            {
                float code = 0;
                float important = 0;
                string versiont = "";
                DateTime date;

                WebWorker.GetVersionInfo(out code,out important, out versiont,out date);

                if(code > MainWindow.VersionCode)
                {
                    SP_UpToDate.Visibility = Visibility.Collapsed;
                    SP_NewVersion.Visibility = Visibility.Visible;
                    LB_Date.Visibility = Visibility.Visible;

                    NewVersion.Content = versiont;

                    LB_Date.Content = date.ToShortDateString();

                    if (MainWindow.VersionCode >= important)
                        ImportantV.Visibility = Visibility.Collapsed;
                    else
                        ImportantV.Visibility = Visibility.Visible;
                }
                else
                {
                    SP_UpToDate.Visibility = Visibility.Visible;
                    SP_NewVersion.Visibility = Visibility.Collapsed;
                    LB_Date.Visibility = Visibility.Collapsed;
                }

            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(Strings.ResStrings.CantCheckVersion, Strings.ResStrings.Error);
                closeW = true;
            }

            Loaded += Window_CheckForUpdates_Loaded;
        }



        public Window_CheckForUpdates(float code, float important, string versiont, DateTime datetime)
        {
            InitializeComponent();

            CurVersion.Content = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            if (code > MainWindow.VersionCode)
            {
                SP_UpToDate.Visibility = Visibility.Collapsed;
                SP_NewVersion.Visibility = Visibility.Visible;

                LB_Date.Visibility = Visibility.Visible;

                NewVersion.Content = versiont;

                LB_Date.Content = datetime.ToShortDateString();

                if (MainWindow.VersionCode >= important)
                    ImportantV.Visibility = Visibility.Collapsed;
                else
                    ImportantV.Visibility = Visibility.Visible;
            }
            else
            {
                SP_UpToDate.Visibility = Visibility.Visible;
                SP_NewVersion.Visibility = Visibility.Collapsed;
                LB_Date.Visibility = Visibility.Collapsed;
            }
        }


        private void Window_CheckForUpdates_Loaded(object sender, RoutedEventArgs e)
        {
            if (closeW)
                Close();
        }


        private void LabelClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender == L_About)
            {
                Window_AboutApp wabout = new Window_AboutApp();
                wabout.Owner = this;
                wabout.ShowDialog();

            }else if(sender == L_Web)
            {
                Process.Start(MainWindow.HomeWebURL);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Updater.exe","Update");
        }

    }
}
