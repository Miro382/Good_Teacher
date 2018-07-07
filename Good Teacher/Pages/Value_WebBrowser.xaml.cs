using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Navigation;
using Good_Teacher.Controls;
using Good_Teacher.Controls.Special;
using Good_Teacher.Windows;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_WebBrowser.xaml
    /// </summary>
    /// 
    public partial class Value_WebBrowser : System.Windows.Controls.Page
    {

        WebPage_Control cont;

        public Value_WebBrowser(DataStore data,WebPage_Control webbrowser)
        {
            InitializeComponent();
            cont = webbrowser;

            positionselector.SetData(cont);
            positionselector.LoadData();

            if (cont.Effect != null)
            {
                if (cont.Effect is DropShadowEffect)
                {
                    CheckBoxShadow.IsChecked = true;
                    DropShadowEffect effect = (DropShadowEffect)cont.Effect;
                    Rect_ShadowColor.Fill = new SolidColorBrush(effect.Color);
                    TextBox_ShadowDepth.Text = effect.ShadowDepth + "";
                    TextBox_ShadowDirection.Text = effect.Direction + "";
                    SliderShadowOpacity.Value = effect.Opacity * 100;
                    TextBox_BlurRadius.Text = effect.BlurRadius + "";
                }
            }

            if (cont.BackForwardVisibility == Visibility.Visible)
            {
                NavCheckBox.IsChecked = true;
            }
            else
            {
                NavCheckBox.IsChecked = false;
            }

                Box_Web.Text = cont.WebUrl;


            ToolbarCheckBox.IsChecked = (cont.ToolbarPanel.Visibility == Visibility.Visible);

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

        void DetectSender(object senderob)
        {
           if (senderob == Box_Web)
                NavigateToAddress();
        }


        void NavigateToAddress()
        {
            try
            {
                cont.webBrowser.Navigate(Box_Web.Text);
                cont.WebUrl = Box_Web.Text;
            }catch(Exception ex)
            {
                Debug.WriteLine("WebBrowser Navigate to: "+ex);
            }
        }


        private void Control_LostFocus(object sender, RoutedEventArgs e)
        {
            DetectSender(sender);
        }



        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DetectSender(sender);
            }
            else if (e.Key == Key.Escape)
            {
                Keyboard.ClearFocus();
                DetectSender(sender);
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


        private void ButtonShadowColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_ShadowColor.Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                Rect_ShadowColor.Fill = new SolidColorBrush(colorp.GetColor());
            }
        }

        private void ButtonCreateShadow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckBoxShadow.IsChecked == true)
                {
                    cont.Effect =
                      new DropShadowEffect
                      {
                          Color = ((SolidColorBrush)Rect_ShadowColor.Fill).Color,
                          Direction = int.Parse(TextBox_ShadowDirection.Text),
                          ShadowDepth = int.Parse(TextBox_ShadowDepth.Text),
                          Opacity = (SliderShadowOpacity.Value / 100),
                          BlurRadius = int.Parse(TextBox_BlurRadius.Text)
                      };
                }
                else
                {
                    cont.Effect = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                cont.Effect = null;
                TextBox_ShadowDepth.Text = "5";
                TextBox_ShadowDirection.Text = "315";
                SliderShadowOpacity.Value = 100;
                TextBox_BlurRadius.Text = "5";
            }
        }


        private void SliderShadowOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            OpacityValue.Content = (int)SliderShadowOpacity.Value + " %";
        }



        private void NavCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            if (cont != null)
            {
                Debug.WriteLine("NAVCheckBox!!!");
                if (NavCheckBox.IsChecked == true)
                {
                    cont.BackForwardVisibility = Visibility.Visible;

                }
                else
                {
                    cont.BackForwardVisibility = Visibility.Collapsed;
                }
            }
            
        }

        private void ToolbarCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (cont != null)
            {
                if (ToolbarCheckBox.IsChecked == true)
                {
                    cont.ToolbarPanel.Visibility = Visibility.Visible;

                }
                else
                {
                    cont.ToolbarPanel.Visibility = Visibility.Collapsed;
                }

                Debug.WriteLine("Visibility: "+ cont.ToolbarPanel.Visibility);
            }
        }


    }
}

