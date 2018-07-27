using Good_Teacher.Windows;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Good_Teacher.Controls.Special
{
    /// <summary>
    /// Interaction logic for EffectSelector.xaml
    /// </summary>
    public partial class EffectSelector : UserControl
    {

        public FrameworkElement cont;

        public EffectSelector()
        {
            InitializeComponent();
        }

        public void SetData(FrameworkElement control)
        {
            cont = control;
        }


        public void LoadData()
        {
            if (cont.Effect != null)
            {
                if (cont.Effect is DropShadowEffect)
                {
                    B_RemoveShadow.Visibility = Visibility.Visible;
                    DropShadowEffect effect = (DropShadowEffect)cont.Effect;
                    Rect_ShadowColor.Fill = new SolidColorBrush(effect.Color);
                    TextBox_ShadowDepth.Text = effect.ShadowDepth + "";
                    TextBox_ShadowDirection.Text = effect.Direction + "";
                    SliderShadowOpacity.Value = effect.Opacity * 100;
                    TextBox_BlurRadius.Text = effect.BlurRadius + "";
                }

                if (cont.Effect is BlurEffect)
                {
                    B_RemoveBlur.Visibility = Visibility.Visible;
                    BlurEffect effect = (BlurEffect)cont.Effect;
                    TextBox_BlurEffectBlurRadius.Text = effect.Radius + "";

                    if (effect.KernelType == KernelType.Box)
                        ComboBox_BlurType.SelectedIndex = 0;
                    else
                        ComboBox_BlurType.SelectedIndex = 1;
                }
            }



            Slider_ImgOpacity.Value = cont.Opacity * 100;

            if (cont.RenderTransform != null && cont.RenderTransform is RotateTransform)
            {
                TextBox_RotationAngle.Text = "" + ((RotateTransform)cont.RenderTransform).Angle;
                rotationbutton.SetAngleToPointer(((RotateTransform)cont.RenderTransform).Angle);
            }

            if(cont.Visibility == Visibility.Visible)
            {
                CB_IsVisible.IsChecked = true;
            }
            else
            {
                CB_IsVisible.IsChecked = false;
            }

        }


        private void Control_LostFocus(object sender, RoutedEventArgs e)
        {
            Rotate();
        }



        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Rotate();
            }
            else if (e.Key == Key.Escape)
            {
                Keyboard.ClearFocus();
                Rotate();
            }
        }



        void Rotate()
        {
            try
            {
                double angle = Double.Parse(TextBox_RotationAngle.Text);

                rotationbutton.SetAngleToPointer(angle);

                RotateTransform rotateTransform = new RotateTransform(angle);
                cont.RenderTransformOrigin = new Point(0.5, 0.5);
                cont.RenderTransform = rotateTransform;
            }
            catch
            {
                TextBox_RotationAngle.Text = "0";
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
                cont.Effect =
                    new DropShadowEffect
                    {
                        Color = ((SolidColorBrush)Rect_ShadowColor.Fill).Color,
                        Direction = int.Parse(TextBox_ShadowDirection.Text),
                        ShadowDepth = int.Parse(TextBox_ShadowDepth.Text),
                        Opacity = (SliderShadowOpacity.Value / 100),
                        BlurRadius = int.Parse(TextBox_BlurRadius.Text)
                    };

                B_RemoveShadow.Visibility = Visibility.Visible;
                B_RemoveBlur.Visibility = Visibility.Collapsed;
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



        private void ButtonCreateBlur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                KernelType type;

                if (ComboBox_BlurType.SelectedIndex == 0)
                    type = KernelType.Box;
                else
                    type = KernelType.Gaussian;

                cont.Effect = new BlurEffect
                {
                    KernelType = type,
                    Radius = int.Parse(TextBox_BlurEffectBlurRadius.Text),
                    RenderingBias = RenderingBias.Performance
                };
                B_RemoveBlur.Visibility = Visibility.Visible;
                B_RemoveShadow.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                TextBox_BlurEffectBlurRadius.Text = "5";
                ComboBox_BlurType.SelectedIndex = 2;
                cont.Effect = null;
            }
        }


        private void SliderShadowOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            OpacityValue.Content = (int)SliderShadowOpacity.Value + " %";
        }




        private void Slider_ImgOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Label_ImageOpacity.Text = (int)Slider_ImgOpacity.Value + " %";
            if (cont != null)
            {
                cont.Opacity = Slider_ImgOpacity.Value / 100;
            }
        }

        private void RotationButton_Click(object sender, double Angle)
        {
            TextBox_RotationAngle.Text = "" + Angle;
            Rotate();
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

        private void ButtonRemoveShadow_Click(object sender, RoutedEventArgs e)
        {
            cont.Effect = null;
            B_RemoveShadow.Visibility = Visibility.Collapsed;
        }

        private void ButtonRemoveBlur_Click(object sender, RoutedEventArgs e)
        {
            cont.Effect = null;
            B_RemoveBlur.Visibility = Visibility.Collapsed;
        }


        private void CB_IsVisible_Checked(object sender, RoutedEventArgs e)
        {
            if (cont != null)
            {
                if (CB_IsVisible.IsChecked == true)
                {
                    cont.Visibility = Visibility.Visible;
                }
                else
                {
                    cont.Visibility = Visibility.Collapsed;
                }
            }
        }

    }
}
