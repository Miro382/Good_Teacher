using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Good_Teacher.Windows;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_Line.xaml
    /// </summary>
    public partial class Value_Line : System.Windows.Controls.Page
    {
        public Line cont;

        public Value_Line(Line line)
        {
            InitializeComponent();

            cont = line;

            Box_X1.Text = "" + cont.X1;
            Box_X2.Text = "" + cont.X2;
            Box_Y1.Text = "" + cont.Y1;
            Box_Y2.Text = "" + cont.Y2;


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

                if (cont.Effect is BlurEffect)
                {
                    CheckBoxBlur.IsChecked = true;
                    BlurEffect effect = (BlurEffect)cont.Effect;
                    TextBox_BlurEffectBlurRadius.Text = effect.Radius + "";

                    if (effect.KernelType == KernelType.Box)
                        ComboBox_BlurType.SelectedIndex = 0;
                    else
                        ComboBox_BlurType.SelectedIndex = 1;
                }
            }

            Slider_ImgOpacity.Value = cont.Opacity * 100;


            if (cont.Stroke == null || cont.Stroke is SolidColorBrush)
            {
                if (cont.Stroke == null || cont.Stroke.ToString() == "#00FFFFFF")
                    Rect_BackColor.Fill = null;
                else
                    Rect_BackColor.Fill = (SolidColorBrush)cont.Stroke;
            }
            else if (cont.Stroke is LinearGradientBrush)
            {
                RadioButton_LinearGradient.IsChecked = true;
                RadioButton_RadialGradient.IsChecked = false;
                Rect_StartColor.Fill = new SolidColorBrush(((LinearGradientBrush)cont.Stroke).GradientStops[0].Color);
                Rect_EndColor.Fill = new SolidColorBrush(((LinearGradientBrush)cont.Stroke).GradientStops[1].Color);
                TabControlFill.SelectedIndex = 2;
            }
            else if (cont.Stroke is RadialGradientBrush)
            {
                RadioButton_RadialGradient.IsChecked = true;
                RadioButton_LinearGradient.IsChecked = false;
                Rect_StartColor.Fill = new SolidColorBrush(((RadialGradientBrush)cont.Stroke).GradientStops[0].Color);
                Rect_EndColor.Fill = new SolidColorBrush(((RadialGradientBrush)cont.Stroke).GradientStops[1].Color);
                TabControlFill.SelectedIndex = 2;
            }

            Box_LineSize.Text = ""+cont.StrokeThickness;

        }


        void DetectSender(object senderob)
        {

            if (senderob == Box_X1)
                SetPosX(1);
            else if (senderob == Box_X2)
                SetPosX(2);
            else if (senderob == Box_Y1)
                SetPosY(1);
            else if (senderob == Box_Y2)
                SetPosY(2);
            else if (senderob == Box_LineSize)
                SetLineSize();
        }


        void SetLineSize()
        {
            try
            {
                cont.StrokeThickness = double.Parse(Box_LineSize.Text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Line Set Size: " + ex);
                Box_LineSize.Text = "3";
                cont.StrokeThickness = 3;
            }
        }


        void SetPosX(int xtype)
        {
            try
            {
                double x = 0;

                if (xtype == 1)
                {
                    x = double.Parse(Box_X1.Text);
                    cont.X1 = x;
                }
                else if (xtype == 2)
                {
                    x = double.Parse(Box_X2.Text);
                    cont.X2 = x;
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine("Line Set position: "+ex);

                if (xtype == 1)
                {
                    Box_X1.Text = "0";
                    cont.X1 = 0;
                }
                else if (xtype == 2)
                {
                    Box_X2.Text = "0";
                    cont.X2 = 0;
                }

            }
        }



        void SetPosY(int ytype)
        {
            try
            {
                double y = 0;

                if (ytype == 1)
                {
                    y = double.Parse(Box_Y1.Text);
                    cont.Y1 = y;
                }
                else if (ytype == 2)
                {
                    y = double.Parse(Box_Y2.Text);
                    cont.Y2 = y;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Line Set position: " + ex);

                if (ytype == 1)
                {
                    Box_Y1.Text = "0";
                    cont.Y1 = 0;
                }
                else if (ytype == 2)
                {
                    Box_Y2.Text = "0";
                    cont.Y2 = 0;
                }

            }
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

        private void Control_LostFocus(object sender, RoutedEventArgs e)
        {
            DetectSender(sender);
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



        private void ButtonCreateBlur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckBoxBlur.IsChecked == true)
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
                }
                else
                {
                    cont.Effect = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                TextBox_BlurEffectBlurRadius.Text = "5";
                ComboBox_BlurType.SelectedIndex = 2;
                cont.Effect = null;
            }
        }


        private void ButtonColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_BackColor.Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                cont.Stroke = new SolidColorBrush(colorp.GetColor());
                Rect_BackColor.Fill = new SolidColorBrush(colorp.GetColor());
            }
        }



        private void ButtonGradient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Rect_StartColor.Fill != null && Rect_EndColor.Fill != null)
                {

                    if (RadioButton_LinearGradient.IsChecked == true)
                    {
                        cont.Stroke = new LinearGradientBrush(((SolidColorBrush)Rect_StartColor.Fill).Color,
                             ((SolidColorBrush)Rect_EndColor.Fill).Color, Double.Parse(Box_Angle.Text));
                    }
                    else
                    {
                        cont.Stroke = new RadialGradientBrush(((SolidColorBrush)Rect_StartColor.Fill).Color,
                             ((SolidColorBrush)Rect_EndColor.Fill).Color);
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Box_Angle.Text = "0";
                RadioButton_LinearGradient.IsChecked = true;
                RadioButton_RadialGradient.IsChecked = false;
            }
        }



        private void ButtonStartColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_StartColor.Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                Rect_StartColor.Fill = new SolidColorBrush(colorp.GetColor());
            }
        }

        private void ButtonEndColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_EndColor.Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                Rect_EndColor.Fill = new SolidColorBrush(colorp.GetColor());
            }
        }


        private void SliderShadowOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            OpacityValue.Content = (int)SliderShadowOpacity.Value + " %";
        }


        private void Slider_ImgOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Label_ImageOpacity.Content = (int)Slider_ImgOpacity.Value + " %";
            if (cont != null)
            {
                cont.Opacity = Slider_ImgOpacity.Value / 100;
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


    }
}
