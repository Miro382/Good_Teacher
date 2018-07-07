using Good_Teacher.Controls;
using Good_Teacher.Controls.Special;
using Good_Teacher.Windows.Dialogs;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_ScalableImage.xaml
    /// </summary>
    public partial class Value_ScalableImage : Page
    {
        ScalableImage cont;
        DataStore data;

        public Value_ScalableImage(DataStore datas, ScalableImage image)
        {
            InitializeComponent();

            cont = image;

            data = datas;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();

            BitmapScalingMode scalingMode = RenderOptions.GetBitmapScalingMode(cont);
            if ((int)scalingMode == 2)
                ComboBox_Quality.SelectedIndex = 0;
            if ((int)scalingMode == 1)
                ComboBox_Quality.SelectedIndex = 1;
            if ((int)scalingMode == 3)
                ComboBox_Quality.SelectedIndex = 2;


            if (cont.M_Img.Source != null)
            {
                R_ImageFill.Fill = new ImageBrush(cont.M_Img.Source);
                ((ImageBrush)R_ImageFill.Fill).Stretch = Stretch.UniformToFill;
            }


            SliderZoom.Value = cont.SliderZoom.Value;
            NTB_Zoom.Text = "" + (int)SliderZoom.Value;

            GetImageSize();


            BS_ControlPanel.SetData(cont, data, false, cont.PathToCPImage);
            BS_ControlPanel.LoadData(cont.ControlPanelBack);
            BS_ControlPanel.ChangedBrush -= ControlBackground_ChangedBrush;
            BS_ControlPanel.ChangedBrush += ControlBackground_ChangedBrush;
        }

        private void ControlBackground_ChangedBrush(BrushSelector brushSelector, Brush Sbrush)
        {
            cont.ControlPanelBack = Sbrush;

            if (Sbrush is ImageBrush)
            {
                cont.PathToCPImage = brushSelector.LastSelectedImageKey;
                cont.CPStretch = ((ImageBrush)Sbrush).Stretch;
            }
        }

        void GetImageSize()
        {
            try
            {
                if (cont.M_Img.Source != null)
                    Label_imageWH.Content = Strings.ResStrings.Size + ": " + ((BitmapSource)cont.M_Img.Source).PixelWidth + " x " + ((BitmapSource)cont.M_Img.Source).PixelHeight;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
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



        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cont != null)
                {

                    Debug.WriteLine(((ComboBox)sender).SelectedIndex);

                    if (sender == ComboBox_Quality)
                    {
                        switch (((ComboBox)sender).SelectedIndex)
                        {
                            case 0:
                                RenderOptions.SetBitmapScalingMode(cont, BitmapScalingMode.Fant);
                                break;
                            case 1:
                                RenderOptions.SetBitmapScalingMode(cont, BitmapScalingMode.Linear);
                                break;
                            case 2:
                                RenderOptions.SetBitmapScalingMode(cont, BitmapScalingMode.NearestNeighbor);
                                break;
                        }
                    }
                }
            }
            catch
            {
            }
        }




        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            DWindow_Image imgsel = new DWindow_Image(data);
            imgsel.Owner = Window.GetWindow(this);
            imgsel.ShowDialog();

            if (imgsel.SelectedImage)
            {
                cont.M_Img.Source = data.archive.GetImage(imgsel.SelectedKey);

                cont.M_Img.Stretch = Stretch.Uniform;

                R_ImageFill.Fill = new ImageBrush(cont.M_Img.Source);
                ((ImageBrush)R_ImageFill.Fill).Stretch = Stretch.UniformToFill;

                Debug.WriteLine("" + imgsel.SelectedKey);

                cont.ImageKey = imgsel.SelectedKey;
                data.archive.GetImageSize(imgsel.SelectedKey, out cont.DefaultW, out cont.DefaultH);
                cont.M_Img.Width = cont.DefaultW;
                cont.M_Img.Height = cont.DefaultH;
            }
        }

        private void SliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (cont != null)
            {
                NTB_Zoom.Text = "" + (int)SliderZoom.Value;
                cont.SliderZoom.Value = SliderZoom.Value;
            }
        }

        private void NTB_Zoom_LostFocus(object sender, RoutedEventArgs e)
        {
            SetValueFromZoomTextBox();
        }

        private void NTB_Zoom_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                SetValueFromZoomTextBox();
            }
        }

        void SetValueFromZoomTextBox()
        {
            if (cont != null)
            {
                try
                {
                    double readV = 100;
                    if (double.TryParse(NTB_Zoom.Text, out readV))
                    {
                        SliderZoom.Value = readV;
                    }
                    else
                    {
                        NTB_Zoom.Text = "" + (int)SliderZoom.Value;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error set value from Zoom box: " + ex);
                }
            }
        }


    }
}
