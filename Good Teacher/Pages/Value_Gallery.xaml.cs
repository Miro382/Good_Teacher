using Good_Teacher.Controls;
using Good_Teacher.Windows.Dialogs;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_Gallery.xaml
    /// </summary>
    public partial class Value_Gallery : System.Windows.Controls.Page
    {

        DataStore data;
        Gallery cont;

        public Value_Gallery(DataStore datas, Gallery gallery)
        {
            InitializeComponent();


            cont = gallery;

            data = datas;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();

            TB_TransitionSpeed.Text = ""+cont.TimeToTranslate;
            TB_RestTime.Text = ""+cont.Time;

            if (cont.CircleItems.Visibility == Visibility.Visible)
                CB_CircleVis.IsChecked = true;
            else
                CB_CircleVis.IsChecked = false;

            if (cont.Description.Visibility == Visibility.Visible)
                CB_TextVis.IsChecked = true;
            else
                CB_TextVis.IsChecked = false;


            if (cont.Left.Visibility == Visibility.Visible)
                CB_ControlVis.IsChecked = true;
            else
                CB_ControlVis.IsChecked = false;


            brushselectorFor.SetData(cont, data, false, cont.ForegroundKey);
            brushselectorFor.LoadData(cont.Foreground);
            brushselectorFor.ChangedBrush -= BrushselectorFor_ChangedBrush;
            brushselectorFor.ChangedBrush += BrushselectorFor_ChangedBrush;

            fontEditorPanel.Load(cont,data);
            fontEditorPanel.AlignmentPanel.Visibility = Visibility.Collapsed;

            ComboBox_Stretch.SelectedIndex = (int)cont.GetStretch();
        }


        private void BrushselectorFor_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            cont.Foreground = Sbrush;

            if (Sbrush is ImageBrush)
            {
                cont.ForegroundKey = brushSelector.LastSelectedImageKey;
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


        private void ImagesGallery_Click(object sender, RoutedEventArgs e)
        {
            DWindow_GalleryImages galleryImages = new DWindow_GalleryImages(data, cont);
            galleryImages.Owner = Window.GetWindow(this);
            galleryImages.ShowDialog();
        }


        private void CB_TextVis_Checked(object sender, RoutedEventArgs e)
        {
            if (cont != null)
            {
                if (CB_TextVis.IsChecked == true)
                    cont.Description.Visibility = Visibility.Visible;
                else
                    cont.Description.Visibility = Visibility.Collapsed;
            }
        }

        private void CB_CircleVis_Checked(object sender, RoutedEventArgs e)
        {
            if (cont != null)
            {
                if (CB_CircleVis.IsChecked == true)
                    cont.CircleItems.Visibility = Visibility.Visible;
                else
                    cont.CircleItems.Visibility = Visibility.Collapsed;
            }
        }


        private void TB_RestTime_KeyUp(object sender, KeyEventArgs e)
        {
            if (cont != null)
            {
                uint RestTime = 3;
                if (uint.TryParse(TB_RestTime.Text, out RestTime))
                {
                    cont.Time = RestTime;
                    cont.RefreshTime();
                }
                else
                {
                    TB_RestTime.Text = "" + cont.Time;
                }
            }
        }


        private void UpdateTransitionSpeed()
        {
            if (cont != null)
            {
                double speed = 0.5;
                if (double.TryParse(TB_TransitionSpeed.Text, out speed))
                {
                    cont.TimeToTranslate = speed;
                    cont.RefreshTime();
                }
                else
                {
                    TB_TransitionSpeed.Text = "" + cont.TimeToTranslate;
                }
            }
        }


        private void TB_TransitionSpeed_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateTransitionSpeed();
        }

        private void TB_TransitionSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UpdateTransitionSpeed();
            }
            else if (e.Key == Key.Escape)
            {
                Keyboard.ClearFocus();
                UpdateTransitionSpeed();
            }
        }


        private void CB_ControlVis_Checked(object sender, RoutedEventArgs e)
        {
            if (cont != null)
            {
                if (CB_ControlVis.IsChecked == true)
                {
                    cont.Left.Visibility = Visibility.Visible;
                    cont.Right.Visibility = Visibility.Visible;
                }
                else
                {
                    cont.Left.Visibility = Visibility.Collapsed;
                    cont.Right.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cont != null)
                {

                    if (sender == ComboBox_Stretch)
                    {
                        switch (((ComboBox)sender).SelectedIndex)
                        {
                            case 0:
                                cont.SetStretch(Stretch.None);
                                break;
                            case 1:
                                cont.SetStretch(Stretch.Fill);
                                break;
                            case 2:
                                cont.SetStretch(Stretch.Uniform);
                                break;
                            case 3:
                                cont.SetStretch(Stretch.UniformToFill);
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
