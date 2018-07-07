using Good_Teacher.Controls;
using Good_Teacher.Controls.Special;
using Good_Teacher.Windows.Dialogs;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_MediaPlayer.xaml
    /// </summary>
    public partial class Value_MediaPlayer : Page
    {

        MediaPlayerController_Control cont;
        DataStore data;


        public Value_MediaPlayer(DataStore datas, MediaPlayerController_Control control)
        {
            InitializeComponent();

            cont = control;
            data = datas;

            if (cont.Width < cont.MinWidth)
                cont.Width = cont.MinWidth;


            if (cont.Height < cont.MinHeight)
                cont.Height = cont.MinHeight;


            positionselector.SetData(cont);
            positionselector.LoadData();

            if (cont.Tag != null)
                TBL_Media.Text = cont.Tag.ToString();

            CB_Autoplay.IsChecked = cont.Autoplay;
            SL_Volume.Value = cont.Volume;

            BS_ControlPanel.SetData(cont, data, false, cont.PathToCPImage);
            BS_ControlPanel.LoadData(cont.G_ControlPanel.Background);
            BS_ControlPanel.ChangedBrush -= ControlBackground_ChangedBrush;
            BS_ControlPanel.ChangedBrush += ControlBackground_ChangedBrush;
        }


        private void ControlBackground_ChangedBrush(BrushSelector brushSelector, Brush Sbrush)
        {
            cont.G_ControlPanel.Background = Sbrush;

            if (Sbrush is ImageBrush)
            {
                cont.PathToCPImage = brushSelector.LastSelectedImageKey;
                cont.CPStretch = ((ImageBrush)Sbrush).Stretch;
            }
        }


        private void SetMedia_Click(object sender, RoutedEventArgs e)
        {
            DWindow_MediaSelector mediaSelector = new DWindow_MediaSelector();
            mediaSelector.Owner = Window.GetWindow(this);
            mediaSelector.ShowDialog();

            if (mediaSelector.OK)
            {
                cont.Tag = mediaSelector.FileName;
                TBL_Media.Text = mediaSelector.FileName;
            }
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((Canvas)cont.Parent).Children.Remove(cont);
                NavigationService.Content = "";
                ((MainWindow)Window.GetWindow(this)).RemoveSelectedItemEffect();
            }
            catch
            {

            }
        }

        private void CB_Autoplay_Checked(object sender, RoutedEventArgs e)
        {
            if(IsInitialized)
            {
                cont.Autoplay = CB_Autoplay.IsChecked == true;
            }
        }

        private void SliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(cont != null)
            {
                TB_Volume.Text = "" + SL_Volume.Value;
                cont.Volume = (int)SL_Volume.Value;
            }
        }


        private void TB_Volume_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter || e.Key == Key.Escape)
            {
                SetVolume();
            }
        }

        private void TB_Volume_LostFocus(object sender, RoutedEventArgs e)
        {
            SetVolume();
        }

        private void SetVolume()
        {
            int volume = 50;

            if(int.TryParse(TB_Volume.Text,out volume))
            {
                SL_Volume.Value = volume;
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


    }
}
