using Good_Teacher.Class.Workers;
using System;
using System.IO;
using System.Windows;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_FileInformations.xaml
    /// </summary>
    public partial class Window_FileInformations : Window
    {
        public Window_FileInformations(DataStore data, string path)
        {
            InitializeComponent();

            R_PageCount.Text = ""+data.pages.Count;

            if (!String.IsNullOrWhiteSpace(path))
            {
                R_FilePath.Text = path;
                R_FileName.Text = System.IO.Path.GetFileNameWithoutExtension(path);
                R_Modified.Text = File.GetLastWriteTime(path).ToLongDateString() + " " + File.GetLastWriteTime(path).ToLongTimeString();
                R_Created.Text = File.GetCreationTime(path).ToLongDateString() + " " + File.GetCreationTime(path).ToLongTimeString();
                FileInfo fileInfo = new FileInfo(path);
                R_Size.Text = FileWorker.GetBytesReadable(fileInfo.Length);
                R_EnabledScripts.Text = LocalizationWorker.BoolToYesNo(data.AreScriptsAllowed);
                R_SaveOutput.Text = LocalizationWorker.BoolToYesNo(data.SaveOutput);
            }
        }
    }
}
