using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Good_Teacher.Class;
using Good_Teacher.Class.Workers;
using Good_Teacher.Windows.Dialogs;

namespace Good_Teacher.Windows
{
    /// <summary>
    /// Interaction logic for Window_Settings.xaml
    /// </summary>
    public partial class Window_Settings : Window
    {
        Canvas cont;
        DataStore data;
        Border WindowSize;
        int selpos = -1;

        int cunit = 0;

        public Window_Settings(Canvas canvas, DataStore dataStore,int selectedposition, Border windowSize)
        {
            InitializeComponent();

            cont = canvas;
            data = dataStore;
            selpos = selectedposition;
            WindowSize = windowSize;

            BitmapScalingMode scalingMode = RenderOptions.GetBitmapScalingMode(cont);
            if ((int)scalingMode == 2)
                ComboBox_Quality.SelectedIndex = 0;
            if ((int)scalingMode == 1)
                ComboBox_Quality.SelectedIndex = 1;
            if ((int)scalingMode == 3)
                ComboBox_Quality.SelectedIndex = 2;


            if (cont.Background is SolidColorBrush)
            {
                if (cont.Background == null || cont.Background.ToString() == "#00FFFFFF")
                    Rect_BackColor.Fill = null;
                else
                    Rect_BackColor.Fill = cont.Background;
            }
            else if (cont.Background is ImageBrush)
            {
                if (((ImageBrush)cont.Background).ImageSource != null)
                {
                    R_ImageFill.Fill = new ImageBrush(((ImageBrush)cont.Background).ImageSource);
                    ((ImageBrush)R_ImageFill.Fill).Stretch = Stretch.UniformToFill;
                    ComboBox_Stretch.SelectedIndex = (int)((ImageBrush)cont.Background).Stretch;
                    TabControlFill.SelectedIndex = 1;
                }
            }
            else if (cont.Background is LinearGradientBrush)
            {
                RadioButton_LinearGradient.IsChecked = true;
                RadioButton_RadialGradient.IsChecked = false;
                Rect_StartColor.Fill = new SolidColorBrush(((LinearGradientBrush)cont.Background).GradientStops[0].Color);
                Rect_EndColor.Fill = new SolidColorBrush(((LinearGradientBrush)cont.Background).GradientStops[1].Color);
                TabControlFill.SelectedIndex = 2;
            }
            else if (cont.Background is RadialGradientBrush)
            {
                RadioButton_RadialGradient.IsChecked = true;
                RadioButton_LinearGradient.IsChecked = false;
                Rect_StartColor.Fill = new SolidColorBrush(((RadialGradientBrush)cont.Background).GradientStops[0].Color);
                Rect_EndColor.Fill = new SolidColorBrush(((RadialGradientBrush)cont.Background).GradientStops[1].Color);
                TabControlFill.SelectedIndex = 2;
            }

            UpdateDefaultCanvasPreview();

            CanvasSizeX.Text = "" + (WindowSize.Width - 2);
            CanvasSizeY.Text = "" + (WindowSize.Height - 2);

            CB_BlockPresentationInput.IsChecked = data.BlockPresentationInput;

            CB_OptimizedMode.IsChecked = data.OptimizedMode;

            CB_SaveOutput.IsChecked = data.SaveOutput;

            CB_SaveTemp.IsChecked = data.SaveTemporaryData;

            CB_ClickToNext.IsChecked = data.ClickToNext;

            CB_BitmapCache.IsChecked = data.CacheCanvas;

            Rect_OutsideColor.Fill = data.OutsideBrush;

            CB_HideInput.IsChecked = data.HideInput;

            CB_AreScriptsAllowed.IsChecked = data.AreScriptsAllowed;

            CB_DebugScript.IsChecked = data.ScriptDebug;

            TB_WarningScriptMessage.Text = data.ScriptWarningMessage;


            if (selpos >= 0)
            {
                CB_SoundRepeat.IsChecked = data.pages[selpos].SoundLoop;

                if (data.pages[selpos].soundActionType == Class.Enumerators.SoundAction.SoundActionType.NoAction)
                {
                    RB_NoAction.IsChecked = true;
                }
                else if (data.pages[selpos].soundActionType == Class.Enumerators.SoundAction.SoundActionType.Stop)
                {
                    RB_Stop.IsChecked = true;
                }
                else if (data.pages[selpos].soundActionType == Class.Enumerators.SoundAction.SoundActionType.Play)
                {
                    RB_Play.IsChecked = true;
                }

                L_SoundPlay.Content = data.pages[selpos].PathToPlaySound;
            }
        }


        private void UpdateDefaultCanvasPreview()
        {
            double higher = MainWindow.CanvasW;

            if (MainWindow.CanvasH > higher)
                higher = MainWindow.CanvasH;

            int ratio = MathWorker.CalcToOrLowerThan(higher, 150);

            R_DefaultCanvas.Width = MainWindow.CanvasW/ratio;
            R_DefaultCanvas.Height = MainWindow.CanvasH/ratio;

            if (data.IsImageBrush)
            {
                R_DefaultCanvas.Fill = new ImageBrush(data.archive.GetImage(data.ImageBrush.Path));
                ((ImageBrush)R_DefaultCanvas.Fill).Stretch = data.ImageBrush.stretch;
            }
            else if (!String.IsNullOrEmpty(data.AllBackground))
            {
                R_DefaultCanvas.Fill = (Brush)SaveEditor.XMLDeserialize(data.AllBackground);
            }
            else
                R_DefaultCanvas.Fill = new SolidColorBrush(Colors.White);
        }


        private static String ColorToHexConverter(Color c)
        {
            return "#" + c.A.ToString("X2") + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }



        /*

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

        void DetectSender(object senderob)
        {
            if (senderob == Box_ISource)
                SetImg(Box_ISource.Text);
        }
        */


        private void ButtonStartColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_StartColor.Fill);
            colorp.Owner = this;
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                Rect_StartColor.Fill = new SolidColorBrush(colorp.GetColor());
            }
        }

        private void ButtonEndColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_EndColor.Fill);
            colorp.Owner = this;
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
                        cont.Background = new LinearGradientBrush(((SolidColorBrush)Rect_StartColor.Fill).Color,
                             ((SolidColorBrush)Rect_EndColor.Fill).Color, Double.Parse(Box_Angle.Text));
                    }
                    else
                    {
                        cont.Background = new RadialGradientBrush(((SolidColorBrush)Rect_StartColor.Fill).Color,
                             ((SolidColorBrush)Rect_EndColor.Fill).Color);
                    }
                    SetCustomBrush();
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



        private void ButtonColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_BackColor.Fill);
            colorp.Owner = this;
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                cont.Background = new SolidColorBrush(colorp.GetColor());
                Rect_BackColor.Fill = new SolidColorBrush(colorp.GetColor());
                SetCustomBrush();
            }
        }


        private void SetCustomBrush()
        {
            if (data.pages.Count > 0)
            {
                data.pages[selpos].ImageBrush = null;
                data.pages[selpos].IsImageBrush = false;
                data.pages[selpos].CustomBrush = true;
                data.pages[selpos].canvasbrush = SaveEditor.XMLSerialize(cont.Background);
            }
        }


        private void ButtonSetAllBackground_Click(object sender, RoutedEventArgs e)
        {
            if (data.pages.Count > 0)
            {
                data.pages[selpos].CustomBrush = false;
                if (cont.Background is ImageBrush)
                {
                    data.IsImageBrush = true;
                    data.ImageBrush = new ImageRepresentation(data.pages[selpos].ImageBrush.Path, data.pages[selpos].ImageBrush.stretch);
                    data.AllBackground = "";
                }
                else
                {
                    data.AllBackground = SaveEditor.XMLSerialize(cont.Background);
                    data.IsImageBrush = false;
                    data.ImageBrush = null;
                }

                UpdateDefaultCanvasPreview();
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
                                ((ImageBrush)cont.Background).Stretch = Stretch.None;
                                break;
                            case 1:
                                ((ImageBrush)cont.Background).Stretch = Stretch.Fill;
                                break;
                            case 2:
                                ((ImageBrush)cont.Background).Stretch = Stretch.Uniform;
                                break;
                            case 3:
                                ((ImageBrush)cont.Background).Stretch = Stretch.UniformToFill;
                                break;
                        }

                        if (data.pages[selpos].ImageBrush != null)
                        {
                            data.pages[selpos].ImageBrush.stretch = ((ImageBrush)cont.Background).Stretch;
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

                        data.BitmapScalingMode = RenderOptions.GetBitmapScalingMode(cont);
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
                if (data.pages.Count > 0)
                {
                    cont.Background = new ImageBrush(data.archive.GetImage(imgsel.SelectedKey));
                    ((ImageBrush)cont.Background).Stretch = (Stretch)ComboBox_Stretch.SelectedIndex;
                    data.pages[selpos].CustomBrush = true;
                    data.pages[selpos].ImageBrush = new ImageRepresentation(imgsel.SelectedKey, (Stretch)ComboBox_Stretch.SelectedIndex);
                    data.pages[selpos].IsImageBrush = true;

                    R_ImageFill.Fill = ((ImageBrush)cont.Background);
                    ((ImageBrush)R_ImageFill.Fill).Stretch = Stretch.UniformToFill;
                }
            }
        }


        private void SetCanvasSize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double sizex = 1280;
                double sizey = 720;
                bool set = true;

                if (double.TryParse(CanvasSizeX.Text, out sizex) == false)
                    set = false;

                if (double.TryParse(CanvasSizeY.Text, out sizey) == false)
                    set = false;

                BackToPixel(ref sizex, ref sizey);

                if (set && sizex >= 200 && sizey >= 200)
                {
                    WindowSize.Width = (sizex+2);
                    WindowSize.Height = (sizey+2);
                    MainWindow.CanvasW = sizex;
                    MainWindow.CanvasH = sizey;
                    data.CanvasW = sizex;
                    data.CanvasH = sizey;
                    UpdateDefaultCanvasPreview();
                }
                else
                {
                    Debug.WriteLine("Cant set new size of canvas. Wrong input!");
                    CanvasSizeX.Text = ""+(WindowSize.Width - 2);
                    CanvasSizeY.Text = "" + (WindowSize.Height - 2);
                    cunit = 0;
                    ComboBox_SizeUnits.SelectedIndex = 0;
                }

            }catch(Exception ex)
            {
                Debug.WriteLine("Error set canvas size: "+ex);
            }
        }




        private void ComboBox_SizeUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CanvasSizeX != null)
            {
                double sizex = 1280;
                double sizey = 720;
                bool set = true;

                if (double.TryParse(CanvasSizeX.Text, out sizex) == false)
                    set = false;

                if (double.TryParse(CanvasSizeY.Text, out sizey) == false)
                    set = false;

                if (set == false)
                {
                    Debug.WriteLine("Wrong size of canvas input!");
                    CanvasSizeX.Text = "" + (WindowSize.Width - 2);
                    CanvasSizeY.Text = "" + (WindowSize.Height - 2);
                }


                switch (ComboBox_SizeUnits.SelectedIndex)
                {
                    case 0:
                        BackToPixel(ref sizex, ref sizey);
                        CanvasSizeX.Text = "" + sizex;
                        CanvasSizeY.Text = "" + sizey;
                        cunit = 0;
                        Label_Unit1.Content = Strings.FormatStrings.pxO;
                        Label_Unit2.Content = Strings.FormatStrings.pxO;
                        break;
                    case 1:
                        BackToPixel(ref sizex, ref sizey);
                        CanvasSizeX.Text = "" + SizeFormatWorker.PxToCm(sizex);
                        CanvasSizeY.Text = "" + SizeFormatWorker.PxToCm(sizey);
                        cunit = 1;
                        Label_Unit1.Content = Strings.FormatStrings.cmO;
                        Label_Unit2.Content = Strings.FormatStrings.cmO;
                        break;
                    case 2:
                        BackToPixel(ref sizex, ref sizey);
                        CanvasSizeX.Text = "" + SizeFormatWorker.PxToIn(sizex);
                        CanvasSizeY.Text = "" + SizeFormatWorker.PxToIn(sizey);
                        cunit = 2;
                        Label_Unit1.Content = Strings.FormatStrings.inO;
                        Label_Unit2.Content = Strings.FormatStrings.inO;
                        break;
                    case 3:
                        BackToPixel(ref sizex, ref sizey);
                        CanvasSizeX.Text = "" + SizeFormatWorker.PxToPt(sizex);
                        CanvasSizeY.Text = "" + SizeFormatWorker.PxToPt(sizey);
                        cunit = 3;
                        Label_Unit1.Content = Strings.FormatStrings.ptO;
                        Label_Unit2.Content = Strings.FormatStrings.ptO;
                        break;
                }
            }
        }


        bool BackToPixel(ref double x, ref double y)
        {
            if (cunit == 0)
                return true;
            else if (cunit == 1)
            {
                x = SizeFormatWorker.CmToPx(x);
                y = SizeFormatWorker.CmToPx(y);
                return true;
            }
            else if (cunit == 2)
            {
                x = SizeFormatWorker.InToPx(x);
                y = SizeFormatWorker.InToPx(y);
                return true;
            }
            else if (cunit == 3)
            {
                x = SizeFormatWorker.PtToPx(x);
                y = SizeFormatWorker.PtToPx(y);
                return true;
            }

            return false;
        }


        private void ComboBox_PredefinedSizes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(ComboBox_PredefinedSizes.SelectedIndex)
            {
                case 1:
                    CanvasSizeX.Text = "1280";
                    CanvasSizeY.Text = "720";
                    cunit = 0;
                    ComboBox_SizeUnits.SelectedIndex = 0;
                    Label_Unit1.Content = Strings.FormatStrings.pxO;
                    Label_Unit2.Content = Strings.FormatStrings.pxO;
                    break;
                case 2:
                    CanvasSizeX.Text = "559,370078740158";
                    CanvasSizeY.Text = "793,700787401575";
                    cunit = 0;
                    ComboBox_SizeUnits.SelectedIndex = 0;
                    Label_Unit1.Content = Strings.FormatStrings.pxO;
                    Label_Unit2.Content = Strings.FormatStrings.pxO;
                    break;
                case 3:
                    CanvasSizeX.Text = "793,700787401575";
                    CanvasSizeY.Text = "1122,51968503937";
                    cunit = 0;
                    ComboBox_SizeUnits.SelectedIndex = 0;
                    Label_Unit1.Content = Strings.FormatStrings.pxO;
                    Label_Unit2.Content = Strings.FormatStrings.pxO;
                    break;
                case 4:
                    CanvasSizeX.Text = "1122,51968503937";
                    CanvasSizeY.Text = "1587,40157480315";
                    cunit = 0;
                    ComboBox_SizeUnits.SelectedIndex = 0;
                    Label_Unit1.Content = Strings.FormatStrings.pxO;
                    Label_Unit2.Content = Strings.FormatStrings.pxO;
                    break;
                case 5:
                    CanvasSizeX.Text = "1920";
                    CanvasSizeY.Text = "1080";
                    cunit = 0;
                    ComboBox_SizeUnits.SelectedIndex = 0;
                    Label_Unit1.Content = Strings.FormatStrings.pxO;
                    Label_Unit2.Content = Strings.FormatStrings.pxO;
                    break;
                case 6:
                    CanvasSizeX.Text = "1024";
                    CanvasSizeY.Text = "768";
                    cunit = 0;
                    ComboBox_SizeUnits.SelectedIndex = 0;
                    Label_Unit1.Content = Strings.FormatStrings.pxO;
                    Label_Unit2.Content = Strings.FormatStrings.pxO;
                    break;
                case 7:
                    CanvasSizeX.Text = "1280";
                    CanvasSizeY.Text = "800";
                    cunit = 0;
                    ComboBox_SizeUnits.SelectedIndex = 0;
                    Label_Unit1.Content = Strings.FormatStrings.pxO;
                    Label_Unit2.Content = Strings.FormatStrings.pxO;
                    break;
                case 8:
                    CanvasSizeX.Text = "800";
                    CanvasSizeY.Text = "600";
                    cunit = 0;
                    ComboBox_SizeUnits.SelectedIndex = 0;
                    Label_Unit1.Content = Strings.FormatStrings.pxO;
                    Label_Unit2.Content = Strings.FormatStrings.pxO;
                    break;
                case 9:
                    CanvasSizeX.Text = "3840";
                    CanvasSizeY.Text = "2160";
                    cunit = 0;
                    ComboBox_SizeUnits.SelectedIndex = 0;
                    Label_Unit1.Content = Strings.FormatStrings.pxO;
                    Label_Unit2.Content = Strings.FormatStrings.pxO;
                    break;
            }
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (cont != null)
            {
                if (sender == CB_BlockPresentationInput)
                {
                    data.BlockPresentationInput = CB_BlockPresentationInput.IsChecked == true;
                    CB_HideInput.IsEnabled = CB_BlockPresentationInput.IsChecked == false;
                }
                else if (sender == CB_OptimizedMode)
                {
                    data.OptimizedMode = CB_OptimizedMode.IsChecked == true;
                }
                else if (sender == CB_SaveTemp)
                {
                    data.SaveTemporaryData = CB_SaveTemp.IsChecked == true;
                }
                else if (sender == CB_SaveOutput)
                {
                    data.SaveOutput = CB_SaveOutput.IsChecked == true;
                }
                else if (sender == CB_SoundRepeat)
                {
                    if (selpos >= 0)
                    {
                        data.pages[selpos].SoundLoop = CB_SoundRepeat.IsChecked == true;
                    }
                }
                else if (sender == CB_ClickToNext)
                {
                    data.ClickToNext = CB_ClickToNext.IsChecked == true;
                }
                else if (sender == CB_BitmapCache)
                {
                    data.CacheCanvas = CB_BitmapCache.IsChecked == true;
                }
                else if (sender == CB_HideInput)
                {
                    data.HideInput = CB_HideInput.IsChecked == true;
                }
                else if (sender == CB_AreScriptsAllowed)
                {
                    data.AreScriptsAllowed = CB_AreScriptsAllowed.IsChecked == true;
                }
                else if (sender == CB_DebugScript)
                {
                    data.ScriptDebug = CB_DebugScript.IsChecked == true;
                }
            }
        }


        private void ButtonOutsideColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_OutsideColor.Fill);
            colorp.Owner = this;
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                Rect_OutsideColor.Fill = new SolidColorBrush(colorp.GetColor());
                data.OutsideBrush = new SolidColorBrush(colorp.GetColor());
            }
        }


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (data != null)
            {
                if (selpos >= 0)
                {
                    if (RB_NoAction.IsChecked == true)
                    {
                        data.pages[selpos].soundActionType = Class.Enumerators.SoundAction.SoundActionType.NoAction;
                    }
                    else if (RB_Stop.IsChecked == true)
                    {
                        data.pages[selpos].soundActionType = Class.Enumerators.SoundAction.SoundActionType.Stop;
                    }
                    else if (RB_Play.IsChecked == true)
                    {
                        data.pages[selpos].soundActionType = Class.Enumerators.SoundAction.SoundActionType.Play;
                    }
                }
            }
        }


        private void ButtonPlaySound_Click(object sender, RoutedEventArgs e)
        {
            DWindow_MediaSelector mediaSelector = new DWindow_MediaSelector();
            mediaSelector.Owner = Window.GetWindow(this);
            mediaSelector.ShowDialog();

            if (mediaSelector.OK)
            {
                L_SoundPlay.Content = mediaSelector.FileName;
                data.pages[selpos].soundActionType = Class.Enumerators.SoundAction.SoundActionType.Play;
                RB_Play.IsChecked = true;
                data.pages[selpos].PathToPlaySound = mediaSelector.FileName;
            }
        }


        private void FlatButtonReverseXY_Click(object sender, System.Windows.Input.MouseEventArgs e)
        {
            string tmp = CanvasSizeX.Text;
            CanvasSizeX.Text = CanvasSizeY.Text;
            CanvasSizeY.Text = tmp;
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


        private void TB_WarningScriptMessage_KeyUp(object sender, KeyEventArgs e)
        {
            data.ScriptWarningMessage = TB_WarningScriptMessage.Text;
        }

    }
}
