using Good_Teacher.Class.Workers;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;

namespace Updater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        short prog = 0;

        private string PathToInstaller = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Update\\GoodTeacherUpdateSetup.exe";

        public MainWindow()
        {
            InitializeComponent();
            ContentRendered += MainWindow_ContentRendered;
        }

        private void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();

            Debug.WriteLine(args.Length);


            if(args.Length>0)
            {
                if(args.Contains("Update"))
                {
                    StartDownloadUpdate();
                }
                else
                {
                    System.Windows.Application.Current.Shutdown();
                }
            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }

        }

        private static string GetChecksum(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
        }

        private void StartDownloadUpdate()
        {

            string PathToDownload = "";

#if RELEASEX64
            PathToDownload = "http://goodteacher.diodegames.eu/internal/Good%20Teacher%20Setup%20x64.exe";
#else
            PathToDownload = "http://goodteacher.diodegames.eu/internal/Good%20Teacher%20Setup.exe";
#endif

            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Update\\");
            Thread thread = new Thread(() => {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri(PathToDownload), PathToInstaller);
            });
            thread.Start();
        }


        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            Application.Current.Dispatcher.Invoke(
            () =>
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                R_Percentage.Text = (int)percentage+"%";
                PB_Download.Value = percentage;
                prog--;

                if (prog <= 0)
                {
                    LB_Size.Content = FileWorker.GetBytesReadableTwoDecimals((long)bytesIn) + " / " + FileWorker.GetBytesReadableTwoDecimals((long)totalBytes);
                    prog = 20;
                }
            });
        }


        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(
                () =>
                {
                    Debug.WriteLine("Download Completed");
                    if (e.Error != null)
                    {
                        Debug.WriteLine("Download Failed");
                        LB_Size.Content = Good_Teacher.Strings.ResStrings.Failure;
                    }
                    else
                    {
                        Debug.WriteLine("Download Success");
                        R_Percentage.Text = "100%";
                        LB_Size.Content = Good_Teacher.Strings.ResStrings.Success;

#if RELEASEX64
                        string txt =  RemoveSpecialCharacters( WebWorker.ReadText("http://goodteacher.diodegames.eu/internal/UpdateVerificationx64.txt")).ToUpper();
#else
                        string txt =  RemoveSpecialCharacters( WebWorker.ReadText("http://goodteacher.diodegames.eu/internal/UpdateVerification.txt")).ToUpper();
#endif

                        string checksum = RemoveSpecialCharacters( GetChecksum(PathToInstaller)).ToUpper();

                        Debug.WriteLine("Downloaded Checksum: "+txt);
                        Debug.WriteLine("File Checksum: " + checksum);

                        if (checksum==txt)
                        {
                            Debug.WriteLine("CheckSum is good!");
                            System.Diagnostics.Process.Start(PathToInstaller, "/SILENT /CLOSEAPPLICATIONS /NOCANCEL");
                            System.Windows.Application.Current.Shutdown();
                        }
                        else
                        {
                            Debug.WriteLine("CheckSum is wrong!");
                            if (MessageBox.Show(Good_Teacher.Strings.ResStrings.DownloadVerFailed, Good_Teacher.Strings.ResStrings.Error, MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.No)
                            {
                                System.Windows.Application.Current.Shutdown();
                            }
                            else
                            {
                                Process.Start(Good_Teacher.MainWindow.HomeWebURL);
                                System.Windows.Application.Current.Shutdown();
                            }
                        }

                        /*
                        System.Diagnostics.Process.Start(PathToInstaller, "/SILENT /CLOSEAPPLICATIONS /NOCANCEL");
                        System.Windows.Application.Current.Shutdown();
                        */
                    }
                });
        }

    }
}
