using Good_Teacher.Windows.Special;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace Good_Teacher.Windows
{
    /// <summary>
    /// Interaction logic for Window_AboutApp.xaml
    /// </summary>
    public partial class Window_AboutApp : Window
    {
        public Window_AboutApp()
        {
            InitializeComponent();

            string is64XVersion = "";

            #if RELEASEX64
            is64XVersion = " (x64)";
            #endif

            Label_Version.Content =  Assembly.GetExecutingAssembly().GetName().Version.ToString()+" "+MainWindow.VersionSpecialIdentificationName + is64XVersion;
        }

        private void LabelClick_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender == L_Web)
            {
                Process.Start(MainWindow.HomeWebURL);

            }else if(sender == L_CheckForUpdate)
            {
                Window_CheckForUpdates checkForUpdates = new Window_CheckForUpdates();
                checkForUpdates.Owner = this;
                checkForUpdates.ShowDialog();
            }
        }
    }
}
