using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_AppSettings.xaml
    /// </summary>
    public partial class Window_AppSettings : Window
    {
        FrameworkElement windowChanger;
        bool trigger = false;

        public Window_AppSettings(FrameworkElement elm)
        {
            InitializeComponent();

            windowChanger = elm;

            LoadData();

            Closing += Window_AppSettings_Closing;
        }


        private void LoadData()
        {
            trigger = true;

            if ((TextFormattingMode)windowChanger.GetValue(TextOptions.TextFormattingModeProperty) == TextFormattingMode.Ideal)
            {
                CB_TextMode.SelectedIndex = 0;
            }
            else
            {
                CB_TextMode.SelectedIndex = 1;
            }

            TB_AlignmentGridSize.Text = "" + MainWindow.GridSize;

            TB_HistoryLimit.Text = "" + MainWindow.HistoryLimit;

            CB_Language.SelectedIndex = (int)MainWindow.appSettings.language;

            if (MainWindow.ControlAreaSize < -0.4f)
                CB_AreaSize.SelectedIndex = 0;
            else if (MainWindow.ControlAreaSize < 0)
                CB_AreaSize.SelectedIndex = 1;
            else if (MainWindow.ControlAreaSize > 0.6f)
                CB_AreaSize.SelectedIndex = 4;
            else if (MainWindow.ControlAreaSize > 0.2f)
                CB_AreaSize.SelectedIndex = 3;
            else
                CB_AreaSize.SelectedIndex = 2;

            trigger = false;

        }


        private void Window_AppSettings_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.appSettings.textFormattingMode = (TextFormattingMode)windowChanger.GetValue(TextOptions.TextFormattingModeProperty);

            uint gridsize = 0;
            if(uint.TryParse(TB_AlignmentGridSize.Text,out gridsize))
            {
                MainWindow.appSettings.GridSize = gridsize;
                MainWindow.GridSize = gridsize;
            }


            uint Hlimit = 1;
            if (uint.TryParse(TB_HistoryLimit.Text, out Hlimit))
            {
                if (Hlimit > 0)
                {
                    MainWindow.appSettings.HistoryLimit = Hlimit;
                    MainWindow.HistoryLimit = Hlimit;
                }
            }

            MainWindow.appSettings.Save();
        }

        private void CB_TextMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (windowChanger != null)
            {
                if (CB_TextMode.SelectedIndex == 0)
                {
                    windowChanger.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Ideal);
                }
                else
                {
                    windowChanger.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
                }
            }
        }


        private void ToDefaultSettings_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show(Strings.ResStrings.DefaultSettingsQ, Strings.ResStrings.DefaultSettings,MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MainWindow.appSettings = new Class.Save.AppSettings();
                MainWindow.appSettings.ApplyComponentData(windowChanger);
                LoadData();
            }
        }


        private void CB_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized && !trigger)
            {
                MainWindow.appSettings.language = (Class.Enumerators.LanguageSettings.Language)CB_Language.SelectedIndex;
                MessageBox.Show(Strings.ResStrings.LanguageChangedText, Strings.ResStrings.LanguageChanged);
            }
        }


        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }


        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9,-]+");
            return !regex.IsMatch(text);
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {

            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }


        private void CB_AreaSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized && !trigger)
            {
                switch (CB_AreaSize.SelectedIndex)
                {
                    case 0:
                        MainWindow.ControlAreaSize = -0.5f;
                        MainWindow.appSettings.ControlAreaSize = -0.5f;
                        break;
                    case 1:
                        MainWindow.ControlAreaSize = -0.3f;
                        MainWindow.appSettings.ControlAreaSize = -0.3f;
                        break;
                    case 2:
                        MainWindow.ControlAreaSize = 0;
                        MainWindow.appSettings.ControlAreaSize = 0f;
                        break;
                    case 3:
                        MainWindow.ControlAreaSize = 0.3f;
                        MainWindow.appSettings.ControlAreaSize = 0.3f;
                        break;
                    case 4:
                        MainWindow.ControlAreaSize = 0.70f;
                        MainWindow.appSettings.ControlAreaSize = 0.70f;
                        break;
                    default:
                        MainWindow.ControlAreaSize = 0;
                        MainWindow.appSettings.ControlAreaSize = 0f;
                        break;
                }
            }
        }

    }
}
