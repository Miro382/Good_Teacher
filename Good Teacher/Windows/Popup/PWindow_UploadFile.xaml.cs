using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Good_Teacher.Windows.Popup
{
    /// <summary>
    /// Interaction logic for PWindow_UploadFile.xaml
    /// </summary>
    public partial class PWindow_UploadFile : Window
    {
        string FileN = "", UploadTo = "";
        short valr = 0;

        public PWindow_UploadFile(string FileFrom, string UploadAddress)
        {
            InitializeComponent();

            FileN = FileFrom;
            UploadTo = UploadAddress;

            ContentRendered += PWindow_UploadFile_ContentRendered;
        }

        private void PWindow_UploadFile_ContentRendered(object sender, EventArgs e)
        {
            try
            {
                WebClient myWebClient = new WebClient();
                myWebClient.UploadFileCompleted += MyWebClient_UploadFileCompleted;
                myWebClient.UploadProgressChanged += MyWebClient_UploadProgressChanged;
                myWebClient.UploadFileAsync(new Uri(UploadTo), "POST", FileN);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Upload error: " + ex);
            }
        }

        private void MyWebClient_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            if (valr > 5)
            {
                double percent = (e.BytesSent / (e.TotalBytesToSend / 100));
                //Debug.WriteLine(percent);
                PB_Upload.Value = percent;
                valr = 0;
            }

            valr++;
        }

        private void MyWebClient_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            Close();
        }
    }
}
