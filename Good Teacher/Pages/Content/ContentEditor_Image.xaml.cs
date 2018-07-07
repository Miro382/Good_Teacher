using Good_Teacher.Class.Serialization.Content_Ser;
using Good_Teacher.Controls;
using Good_Teacher.Windows.Dialogs;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Pages.Content
{
    /// <summary>
    /// Interaction logic for ContentEditor_Image.xaml
    /// </summary>
    public partial class ContentEditor_Image : System.Windows.Controls.Page
    {

        DataStore data;
        SelectButton cont;

        public ContentEditor_Image(DataStore dataStore, SelectButton control)
        {
            InitializeComponent();

            data = dataStore;
            cont = control;

            R_ImageFill.Fill = new ImageBrush(((Image)cont.Content).Source);
            ((ImageBrush)R_ImageFill.Fill).Stretch = Stretch.UniformToFill;

            TB_Width.Text = ""+cont.Width;
            TB_Height.Text = "" + cont.Height;
            TB_MarginLeft.Text = "" + cont.Margin.Left;

            ComboBox_Stretch.SelectedIndex = (int)((Image)cont.Content).Stretch;

            BitmapScalingMode bitmapScalingMode = ((Content_Image)cont.Tag).scalingMode;
            if (bitmapScalingMode == BitmapScalingMode.Fant)
            {
                ComboBox_Quality.SelectedIndex = 0;
            }
            else if (((Content_Image)cont.Tag).scalingMode == BitmapScalingMode.NearestNeighbor)
            {
                ComboBox_Quality.SelectedIndex = 2;
            }
            else
            {
                ComboBox_Quality.SelectedIndex = 1;
            }
        }




        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            DWindow_Image imgsel = new DWindow_Image(data);
            imgsel.Owner = Window.GetWindow(this);
            imgsel.ShowDialog();

            if (imgsel.SelectedImage)
            {
                cont.Content = new Image() { Source = data.archive.GetImage(imgsel.SelectedKey) };
                //cont.Stretch = (Stretch)ComboBox_Stretch.SelectedIndex;

                R_ImageFill.Fill = new ImageBrush(((Image)cont.Content).Source);
                ((ImageBrush)R_ImageFill.Fill).Stretch = Stretch.UniformToFill;

                ((Content_Image)cont.Tag).ImageKey = imgsel.SelectedKey;

                Debug.WriteLine("" + imgsel.SelectedKey);
            }
        }

        private void TB_Width_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                double val = 0;
                if(double.TryParse(TB_Width.Text,out val))
                {
                    cont.Width = val;
                    ((Content_Image)cont.Tag).W = val;
                }
                else
                {
                    cont.Width = Double.NaN;
                    ((Content_Image)cont.Tag).W = Double.NaN;
                }

            }catch(Exception ex)
            {
                Debug.WriteLine("" + ex);
                cont.Width = Double.NaN;
            }
        }


        private void TB_Height_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                double val = 0;
                if (double.TryParse(TB_Height.Text, out val))
                {
                    cont.Height = val;
                    ((Content_Image)cont.Tag).H = val;
                }
                else
                {
                    cont.Height = Double.NaN;
                    ((Content_Image)cont.Tag).H = Double.NaN;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                cont.Height = Double.NaN;
            }
        }

        private void TB_MarginLeft_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                double val = 0;
                if (double.TryParse(TB_MarginLeft.Text, out val))
                {
                    cont.Margin = new Thickness(val, 0, 0, 0);
                    ((Content_Image)cont.Tag).MarginLeft = val;
                }
                else
                {
                    cont.Margin = new Thickness(0, 0, 0, 0);
                    ((Content_Image)cont.Tag).MarginLeft = 0;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                cont.Margin = new Thickness(0, 0, 0, 0);
                ((Content_Image)cont.Tag).MarginLeft = 0;
            }
        }

        private void ComboBoxStretch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cont != null)
                {

                    Debug.WriteLine("ComboBox_Stretch selected index: " + ComboBox_Stretch.SelectedIndex);

                    switch (ComboBox_Stretch.SelectedIndex)
                    {
                        case 0:
                            ((Image)cont.Content).Stretch = Stretch.None;
                            ((Content_Image)cont.Tag).stretch = Stretch.None;
                            break;
                        case 1:
                            ((Image)cont.Content).Stretch = Stretch.Fill;
                            ((Content_Image)cont.Tag).stretch = Stretch.Fill;
                            break;
                        case 2:
                            ((Image)cont.Content).Stretch = Stretch.Uniform;
                            ((Content_Image)cont.Tag).stretch = Stretch.Uniform;
                            break;
                        case 3:
                            ((Image)cont.Content).Stretch = Stretch.UniformToFill;
                            ((Content_Image)cont.Tag).stretch = Stretch.UniformToFill;
                            break;
                    }
                }
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
                    if (sender == ComboBox_Quality)
                    {
                        switch (((ComboBox)sender).SelectedIndex)
                        {
                            case 0:
                                RenderOptions.SetBitmapScalingMode(cont, BitmapScalingMode.Fant);
                                ((Content_Image)cont.Tag).scalingMode = BitmapScalingMode.Fant;
                                break;
                            case 1:
                                RenderOptions.SetBitmapScalingMode(cont, BitmapScalingMode.Linear);
                                ((Content_Image)cont.Tag).scalingMode = BitmapScalingMode.Linear;
                                break;
                            case 2:
                                RenderOptions.SetBitmapScalingMode(cont, BitmapScalingMode.NearestNeighbor);
                                ((Content_Image)cont.Tag).scalingMode = BitmapScalingMode.NearestNeighbor;
                                break;
                        }
                    }
                }
            }
            catch
            {
            }
        }


    }
}
