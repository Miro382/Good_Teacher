using Good_Teacher.Class;
using Good_Teacher.Class.Save;
using Good_Teacher.Windows;
using Good_Teacher.Windows.Dialogs;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Controls.Special
{
    /// <summary>
    /// Interaction logic for BrushSelector.xaml
    /// </summary>
    public partial class BrushSelector : UserControl
    {


        //public Control cont;
        public DataStore data;
        
        public string LastSelectedImageKey = "";
        public Brush brush;
        public FrameworkElement cont;
        public bool foreground = false;


        public delegate void ChangedBrushDelegate(BrushSelector brushSelector, Brush Sbrush);

        public event ChangedBrushDelegate ChangedBrush;

        public bool SetTag = true;


        public BrushSelector()
        {
            InitializeComponent();
        }


        public BrushSelector(DataStore datastore)
        {
            InitializeComponent();

            data = datastore;
        }

        public void SetData(FrameworkElement control, DataStore datas ,bool setTag, string LastSelectedIkey = "")
        {
            data = datas;
            cont = control;
            SetTag = setTag;
            LastSelectedImageKey = LastSelectedIkey;
        }

        public void LoadData(Brush brushs)
        {

            brush = brushs;

            BitmapScalingMode scalingMode = RenderOptions.GetBitmapScalingMode(cont);
            if ((int)scalingMode == 2)
                ComboBox_Quality.SelectedIndex = 0;
            if ((int)scalingMode == 1)
                ComboBox_Quality.SelectedIndex = 1;
            if ((int)scalingMode == 3)
                ComboBox_Quality.SelectedIndex = 2;


            if (brushs is SolidColorBrush)
            {
                if (brushs == null || brushs.ToString() == "#00FFFFFF")
                    Rect_BackColor.Fill = null;
                else
                    Rect_BackColor.Fill = (SolidColorBrush)brushs;
            }
            else if (brushs is ImageBrush)
            {
                if (((ImageBrush)brushs).ImageSource != null)
                {
                    R_ImageFill.Fill = new ImageBrush(((ImageBrush)brushs).ImageSource);
                    ((ImageBrush)R_ImageFill.Fill).Stretch = Stretch.UniformToFill;
                    ComboBox_Stretch.SelectedIndex = (int)((ImageBrush)brushs).Stretch;
                    TabControlFill.SelectedIndex = 1;

                    if (((ImageBrush)brush).RelativeTransform != null && ((ImageBrush)brush).RelativeTransform is TransformGroup)
                    {

                        TransformGroup transformGroup = ((TransformGroup)((ImageBrush)brush).RelativeTransform);

                        CB_TileMode.SelectedIndex = (int)((ImageBrush)brushs).TileMode;
                        TextBox_RotationAngle.Text = "" + ((RotateTransform)transformGroup.Children[0]).Angle;
                        rotationbutton.SetAngleToPointer(((RotateTransform)transformGroup.Children[0]).Angle);
                        TB_SCW.Text = "" + ((ScaleTransform)transformGroup.Children[1]).ScaleX;
                        TB_SCH.Text = ""+ ((ScaleTransform)transformGroup.Children[1]).ScaleY;
                        TB_MVX.Text = "" + ((TranslateTransform)transformGroup.Children[2]).X;
                        TB_MVY.Text = "" + ((TranslateTransform)transformGroup.Children[2]).Y;
                    }

                }
            }
            else if (brushs is LinearGradientBrush)
            {
                RadioButton_LinearGradient.IsChecked = true;
                RadioButton_RadialGradient.IsChecked = false;
                Rect_StartColor.Fill = new SolidColorBrush(((LinearGradientBrush)brushs).GradientStops[0].Color);
                Rect_EndColor.Fill = new SolidColorBrush(((LinearGradientBrush)brushs).GradientStops[1].Color);
                TabControlFill.SelectedIndex = 2;
            }
            else if (brushs is RadialGradientBrush)
            {
                RadioButton_RadialGradient.IsChecked = true;
                RadioButton_LinearGradient.IsChecked = false;
                Rect_StartColor.Fill = new SolidColorBrush(((RadialGradientBrush)brushs).GradientStops[0].Color);
                Rect_EndColor.Fill = new SolidColorBrush(((RadialGradientBrush)brushs).GradientStops[1].Color);
                TabControlFill.SelectedIndex = 2;
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
                brush = new ImageBrush(data.archive.GetImage(imgsel.SelectedKey));
                ((ImageBrush)brush).Stretch = (Stretch)ComboBox_Stretch.SelectedIndex;

                R_ImageFill.Fill = ((ImageBrush)brush);
                ((ImageBrush)R_ImageFill.Fill).Stretch = Stretch.UniformToFill;

                LastSelectedImageKey = imgsel.SelectedKey;

                if (SetTag)
                {
                    if(!foreground)
                    cont.Tag = new DesignSave(new ImageRepresentation(imgsel.SelectedKey, (Stretch)ComboBox_Stretch.SelectedIndex)).Serialize();
                    else
                        cont.Tag = new DesignSave(null,new ImageRepresentation(imgsel.SelectedKey, (Stretch)ComboBox_Stretch.SelectedIndex)).Serialize();
                }

                if (ChangedBrush != null)
                    ChangedBrush(this,brush);
            }
        }



        private void ButtonColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_BackColor.Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                //brush = new SolidColorBrush(colorp.GetColor());
                brush = new SolidColorBrush(colorp.GetColor());
                Rect_BackColor.Fill = new SolidColorBrush(colorp.GetColor());

                if (ChangedBrush != null)
                    ChangedBrush(this,brush);
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



        private void ButtonGradient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Rect_StartColor.Fill != null && Rect_EndColor.Fill != null)
                {

                    if (RadioButton_LinearGradient.IsChecked == true)
                    {
                        brush = new LinearGradientBrush(((SolidColorBrush)Rect_StartColor.Fill).Color,
                             ((SolidColorBrush)Rect_EndColor.Fill).Color, Double.Parse(Box_Angle.Text));
                    }
                    else
                    {
                        brush = new RadialGradientBrush(((SolidColorBrush)Rect_StartColor.Fill).Color,
                             ((SolidColorBrush)Rect_EndColor.Fill).Color);
                    }

                    if (ChangedBrush != null)
                        ChangedBrush(this,brush);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Box_Angle.Text = "90";
                RadioButton_LinearGradient.IsChecked = true;
                RadioButton_RadialGradient.IsChecked = false;
            }
        }



        private void ComboBoxStretch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (cont != null)
                {

                    Debug.WriteLine("ComboBox_Stretch selected index: "+ ComboBox_Stretch.SelectedIndex);

                        switch (ComboBox_Stretch.SelectedIndex)
                        {
                            case 0:
                                ((ImageBrush)brush).Stretch = Stretch.None;
                                break;
                            case 1:
                                ((ImageBrush)brush).Stretch = Stretch.Fill;
                                break;
                            case 2:
                                ((ImageBrush)brush).Stretch = Stretch.Uniform;
                                break;
                            case 3:
                                ((ImageBrush)brush).Stretch = Stretch.UniformToFill;
                                break;
                        }

                        if (ChangedBrush != null)
                            ChangedBrush(this, brush);
                }
            }
            catch
            {
            }

        }


        void AddNewTransform()
        {
            TransformGroup transformGroup = new TransformGroup();
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.CenterX = 0.5;
            rotateTransform.CenterY = 0.5;
            rotateTransform.Angle = 0;
            //((ImageBrush)brush).TileMode = TileMode.

            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.CenterX = 0.5;
            scaleTransform.CenterY = 0.5;
            scaleTransform.ScaleX = 1;
            scaleTransform.ScaleY = 1;

            TranslateTransform translateTransform = new TranslateTransform();
            translateTransform.X = 0f;
            translateTransform.Y = 0f;

            transformGroup.Children.Add(rotateTransform);
            transformGroup.Children.Add(scaleTransform);
            transformGroup.Children.Add(translateTransform);

            ((ImageBrush)brush).RelativeTransform = transformGroup;
        }



        void CheckIfTransformRequired()
        {
            TransformGroup transformGroup = ((TransformGroup)((ImageBrush)brush).RelativeTransform);
            if (((RotateTransform)transformGroup.Children[0]).Angle == 0)
            {
                if(((ScaleTransform)transformGroup.Children[1]).ScaleX == 1)
                {
                    if(((ScaleTransform)transformGroup.Children[1]).ScaleY == 1)
                    {
                        if (((TranslateTransform)transformGroup.Children[2]).X == 0)
                        {
                            if (((TranslateTransform)transformGroup.Children[2]).Y == 0)
                            {
                                Debug.WriteLine("Removed Transform");
                                ((ImageBrush)brush).RelativeTransform = null;
                            }
                        }
                    }
                }
            }
        }


        private void RotationButton_Click(object sender, double Angle)
        {
            if (cont != null)
            {
                try
                {
                    if (brush != null)
                    {

                        if (((ImageBrush)brush).RelativeTransform != null && ((ImageBrush)brush).RelativeTransform is TransformGroup)
                        {

                        }
                        else
                        {
                            AddNewTransform();
                        }

                        ((RotateTransform)((TransformGroup)((ImageBrush)brush).RelativeTransform).Children[0]).Angle = Angle;

                        TextBox_RotationAngle.Text = "" + Angle;

                        if (Angle == 0)
                            CheckIfTransformRequired();
                        
                    }

                }catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }

            }
        }

        private void Control_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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

        private void Control_LostFocus(object sender, RoutedEventArgs e)
        {
            Rotate();
        }


        void Rotate()
        {
            try
            {
                double angle = Double.Parse(TextBox_RotationAngle.Text);

                rotationbutton.SetAngleToPointer(angle);

                RotationButton_Click(rotationbutton, angle);
            }
            catch
            {
                TextBox_RotationAngle.Text = "0";
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


        private void GetScaleWidth()
        {
            try
            {
                double scw = 0;

                if (double.TryParse(TB_SCW.Text, out scw))
                {

                    if (cont != null)
                    {

                        if (brush != null)
                        {
                            if (((ImageBrush)brush).RelativeTransform != null && ((ImageBrush)brush).RelativeTransform is TransformGroup)
                            {

                            }
                            else
                            {
                                AddNewTransform();
                            }

                            ((ScaleTransform)((TransformGroup)((ImageBrush)brush).RelativeTransform).Children[1]).ScaleX = scw;

                            if(((ScaleTransform)((TransformGroup)((ImageBrush)brush).RelativeTransform).Children[1]).ScaleX == 1)
                                CheckIfTransformRequired();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }



        private void GetScaleHeight()
        {
            try
            {
                double sch = 0;

                if (double.TryParse(TB_SCH.Text, out sch))
                {

                    if (cont != null)
                    {

                        if (brush != null)
                        {
                            if (((ImageBrush)brush).RelativeTransform != null && ((ImageBrush)brush).RelativeTransform is TransformGroup)
                            {

                            }
                            else
                            {
                                AddNewTransform();
                            }

                            ((ScaleTransform)((TransformGroup)((ImageBrush)brush).RelativeTransform).Children[1]).ScaleY = sch;

                            if (((ScaleTransform)((TransformGroup)((ImageBrush)brush).RelativeTransform).Children[1]).ScaleY == 1)
                                CheckIfTransformRequired();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }



        private void GetMoveX()
        {
            try
            {
                double mvx = 0;

                if (double.TryParse(TB_MVX.Text, out mvx))
                {

                    if (cont != null)
                    {

                        if (brush != null)
                        {
                            if (((ImageBrush)brush).RelativeTransform != null && ((ImageBrush)brush).RelativeTransform is TransformGroup)
                            {

                            }
                            else
                            {
                                AddNewTransform();
                            }

                            ((TranslateTransform)((TransformGroup)((ImageBrush)brush).RelativeTransform).Children[2]).X = mvx;

                            if (((TranslateTransform)((TransformGroup)((ImageBrush)brush).RelativeTransform).Children[2]).X == 0)
                                CheckIfTransformRequired();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


        private void GetMoveY()
        {
            try
            {
                double mvy = 0;

                if (double.TryParse(TB_MVY.Text, out mvy))
                {

                    if (cont != null)
                    {

                        if (brush != null)
                        {
                            if (((ImageBrush)brush).RelativeTransform != null && ((ImageBrush)brush).RelativeTransform is TransformGroup)
                            {

                            }
                            else
                            {
                                AddNewTransform();
                            }

                            ((TranslateTransform)((TransformGroup)((ImageBrush)brush).RelativeTransform).Children[2]).Y = mvy;

                            if (((TranslateTransform)((TransformGroup)((ImageBrush)brush).RelativeTransform).Children[2]).Y == 0)
                                CheckIfTransformRequired();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


        private void TB_SCW_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                GetScaleWidth();
            }
        }

        private void TB_SCW_LostFocus(object sender, RoutedEventArgs e)
        {
            GetScaleWidth();
        }

        private void TB_MVX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetMoveX();
            }
        }

        private void TB_MVX_LostFocus(object sender, RoutedEventArgs e)
        {
            GetMoveX();
        }

        private void TB_SCH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetScaleHeight();
            }
        }

        private void TB_SCH_LostFocus(object sender, RoutedEventArgs e)
        {
            GetScaleHeight();
        }

        private void TB_MVY_KeyDown(object sender, KeyEventArgs e)
        {
            GetMoveY();
        }

        private void TB_MVY_LostFocus(object sender, RoutedEventArgs e)
        {
            GetMoveY();
        }

        private void CB_TileMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized)
            {
                if (brush != null && brush is ImageBrush)
                {
                    ((ImageBrush)brush).TileMode = (TileMode)CB_TileMode.SelectedIndex;
                }
            }
        }


    }
}
