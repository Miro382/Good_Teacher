using Good_Teacher.Class.Enumerators;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace Good_Teacher.Class.Save
{
    public class AppSettings
    {
        public TextFormattingMode textFormattingMode = TextFormattingMode.Ideal;
        public LanguageSettings.Language language = LanguageSettings.Language.Automatic;
        public uint GridSize = 10;
        public uint HistoryLimit = 30;
        public float ControlAreaSize = 0f;


        public AppSettings()
        {

        }


        public AppSettings(AppSettings settings)
        {
            LoadData(settings);
        }

        public void LoadData(AppSettings settings)
        {
            textFormattingMode = settings.textFormattingMode;
            language = settings.language;
            GridSize = settings.GridSize;
            HistoryLimit = settings.HistoryLimit;
            ControlAreaSize = settings.ControlAreaSize;
        }

        public void Save()
        {
            try
            {
                Debug.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\");
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Settings.setd"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Settings.setd");
                }

                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Settings.setd",SaveEditor.SerializeObject(this));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
            }
        }

        public bool Load()
        {
            try
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Settings.setd"))
                {
                    string data = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Settings.setd");

                    LoadData((AppSettings)SaveEditor.DeserializeObject(data));

                    return true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
            }

            return false;
        }


        public void ApplyLanguage()
        {
            if (language == LanguageSettings.Language.DefaultEng)
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
            }
            else if (language == LanguageSettings.Language.Slovakia)
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("sk");
            }
        }

        public void ApplyComponentData(FrameworkElement windowChanger)
        {
            windowChanger.SetValue(TextOptions.TextFormattingModeProperty, textFormattingMode);
            MainWindow.GridSize = GridSize;
            MainWindow.HistoryLimit = HistoryLimit;
            MainWindow.ControlAreaSize = ControlAreaSize;
        }

    }
}
