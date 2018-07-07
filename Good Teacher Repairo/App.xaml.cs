using System.IO;
using System.Windows;

namespace Good_Teacher_Repairo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                FileInfo file = new FileInfo(e.Args[0]);

                if (file.Exists)
                {
                    Good_Teacher.MainWindow.OpenFileARGPath = file.FullName;
                    Good_Teacher.MainWindow.OpenFileARG = true;
                }

            }

        }
    }
}
