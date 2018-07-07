using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Good_Teacher.Class;
using Good_Teacher.Class.Save;
using Good_Teacher.Windows.Dialogs;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_Image.xaml
    /// </summary>
    /// 
    public partial class Value_Image : System.Windows.Controls.Page
    {
        Image cont;
        DataStore data;

        public Value_Image(DataStore datas ,Image image)
        {
            InitializeComponent();

            cont = image;

            data = datas;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();

            ComboBox_Stretch.SelectedIndex = (int)image.Stretch;

            BitmapScalingMode scalingMode = RenderOptions.GetBitmapScalingMode(cont);
            if ((int)scalingMode == 2)
                ComboBox_Quality.SelectedIndex = 0;
            if ((int)scalingMode == 1)
                ComboBox_Quality.SelectedIndex = 1;
            if ((int)scalingMode == 3)
                ComboBox_Quality.SelectedIndex = 2;


            if (cont.Source != null)
            {
                R_ImageFill.Fill = new ImageBrush(cont.Source);
                ((ImageBrush)R_ImageFill.Fill).Stretch = Stretch.UniformToFill;
                ComboBox_Stretch.SelectedIndex = (int)cont.Stretch;
            }

            GetImageSize();
        }


        void GetImageSize()
        {
            try
            {
                if (cont.Source != null)
                    Label_imageWH.Content = Strings.ResStrings.Size + ": " + ((BitmapSource)cont.Source).PixelWidth + " x " + ((BitmapSource)cont.Source).PixelHeight;
            }catch(Exception ex)
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

                    if (sender == ComboBox_Stretch)
                    {
                        switch (((ComboBox)sender).SelectedIndex)
                        {
                            case 0:
                                cont.Stretch = Stretch.None;
                                break;
                            case 1:
                                cont.Stretch = Stretch.Fill;
                                break;
                            case 2:
                                cont.Stretch = Stretch.Uniform;
                                break;
                            case 3:
                                cont.Stretch = Stretch.UniformToFill;
                                break;
                        }
                    }
                    else if (sender == ComboBox_Quality)
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
                cont.Source = data.archive.GetImage(imgsel.SelectedKey);
                cont.Stretch = (Stretch)ComboBox_Stretch.SelectedIndex;

                R_ImageFill.Fill = new ImageBrush(cont.Source);
                ((ImageBrush)R_ImageFill.Fill).Stretch = Stretch.UniformToFill;

                Debug.WriteLine("" + imgsel.SelectedKey);

                cont.Tag = new DesignSave(new ImageRepresentation(imgsel.SelectedKey, (Stretch)ComboBox_Stretch.SelectedIndex)).Serialize();
            }
        }



    }
}
