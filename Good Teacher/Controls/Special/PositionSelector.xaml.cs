using Good_Teacher.Class.Workers;
using Good_Teacher.Windows.Special;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Good_Teacher.Controls.Special
{
    /// <summary>
    /// Interaction logic for PositionSelector.xaml
    /// </summary>
    public partial class PositionSelector : UserControl
    {
        public FrameworkElement cont;
        public int CUnit = 0;

        public PositionSelector()
        {
            InitializeComponent();
        }

        public void SetData(FrameworkElement frameworkElement)
        {
            cont = frameworkElement;
        }


        public void LoadData()
        {
            Box_W.Text = "" + cont.Width;
            Box_H.Text = "" + cont.Height;
            Box_X.Text = "" + +(double)cont.GetValue(Canvas.LeftProperty);
            Box_Y.Text = "" + (double)cont.GetValue(Canvas.TopProperty);
            Box_Z.Text = "" + Panel.GetZIndex(cont);

            ComboBox_SizeUnits.SelectedIndex = MainWindow.CUnit;
        }



        void SetZ()
        {
            try
            {
                Panel.SetZIndex(cont, int.Parse(Box_Z.Text));
            }
            catch
            {
                Box_Z.Text = "0";
            }
        }



        void SetH()
        {
            try
            {

                double pos = Double.Parse(Box_H.Text);
                double lim = 6;
                BackToPixel(ref pos);

                if (pos < lim)
                {
                    pos = lim;
                    Box_H.Text = ""+lim;
                }
                else if (pos > MainWindow.LimitSize)
                {
                    pos = MainWindow.LimitSize;
                    Box_H.Text = "" + MainWindow.LimitSize;
                }

                cont.Height = pos;

                ((MainWindow)Window.GetWindow(this)).UpdateSelectedEffect();
            }
            catch
            {
                Box_H.Text = "6";
            }

        }


        void SetW()
        {
            try
            {
                double pos = Double.Parse(Box_W.Text);
                double lim = 10;
                BackToPixel(ref pos);

                if (pos < lim)
                {
                    pos = lim;
                    Box_W.Text = ""+lim;
                }
                else if (pos > MainWindow.LimitSize)
                {
                    pos = MainWindow.LimitSize;
                    Box_W.Text = "" + MainWindow.LimitSize;
                }

                cont.Width = pos;

                ((MainWindow)Window.GetWindow(this)).UpdateSelectedEffect();
            }
            catch
            {
                Box_W.Text = "10";
            }
        }




        void SetPosY()
        {
            try
            {
                double pos = Double.Parse(Box_Y.Text);
                BackToPixel(ref pos);

                if (pos < MainWindow.LimitScreenMinY)
                {
                    pos = MainWindow.LimitScreenMinY;
                    Box_Y.Text = "" + MainWindow.LimitScreenMinY;
                }
                else if (pos > MainWindow.LimitScreenMaxY)
                {
                    pos = MainWindow.LimitScreenMaxY;
                    Box_Y.Text = "" + MainWindow.LimitScreenMaxY;
                }

                Canvas.SetTop(cont, pos);

                ((MainWindow)Window.GetWindow(this)).UpdateSelectedEffect();
            }
            catch
            {
                Box_Y.Text = "0";
            }
        }

        void SetPosX()
        {
            try
            {
                double pos = Double.Parse(Box_X.Text);
                BackToPixel(ref pos);

                if (pos < MainWindow.LimitScreenMinX)
                {
                    pos = MainWindow.LimitScreenMinX;
                    Box_X.Text = "" + MainWindow.LimitScreenMinX;
                }
                else if (pos > MainWindow.LimitScreenMaxX)
                {
                    pos = MainWindow.LimitScreenMaxX;
                    Box_X.Text = "" + MainWindow.LimitScreenMaxX;
                }

                Canvas.SetLeft(cont, pos);

                ((MainWindow)Window.GetWindow(this)).UpdateSelectedEffect();
            }
            catch
            {
                Box_X.Text = "0";
            }
        }




        void DetectSender(object senderob)
        {
            if (senderob == Box_Y)
                SetPosY();
            else if (senderob == Box_X)
                SetPosX();
            else if (senderob == Box_Z)
                SetZ();
            else if (senderob == Box_W)
                SetW();
            else if (senderob == Box_H)
                SetH();
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

        private void Button_LayerUp_Click(object sender, RoutedEventArgs e)
        {
            Panel.SetZIndex(cont, Panel.GetZIndex(cont) + 1);
            Box_Z.Text = "" + Panel.GetZIndex(cont);
        }

        private void Button_LayerDown_Click(object sender, RoutedEventArgs e)
        {
            Panel.SetZIndex(cont, Panel.GetZIndex(cont) - 1);
            Box_Z.Text = "" + Panel.GetZIndex(cont);
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


        private void B_EditWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                B_EditWindow.Visibility = Visibility.Collapsed;
                Window_Editor window_Editor = new Window_Editor();
                window_Editor.Owner = Window.GetWindow(this);

                MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
                window_Editor.frame.Content = parentWindow.ValueEditor.Content;

                window_Editor.ShowDialog();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
            }
        }


        private void ComboBox_SizeUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(IsInitialized)
            {
                double x = 0, w = 0;
                double y = 0, h = 0;
                bool set = true;

                if (double.TryParse(Box_X.Text, out x) == false)
                    set = false;

                if (double.TryParse(Box_Y.Text, out y) == false)
                    set = false;

                if (double.TryParse(Box_W.Text, out w) == false)
                    set = false;

                if (double.TryParse(Box_H.Text, out h) == false)
                    set = false;

                if (set == false)
                {
                    Debug.WriteLine("Wrong inputs");
                    Box_X.Text = "0";
                    Box_Y.Text = "0";
                    Box_W.Text = "100";
                    Box_H.Text = "100";
                }


                switch (ComboBox_SizeUnits.SelectedIndex)
                {
                    case 0:
                        BackToPixel(ref x, ref y, ref w, ref h);
                        Box_X.Text = "" + x;
                        Box_Y.Text = "" + y;
                        Box_W.Text = "" + w;
                        Box_H.Text = "" + h;
                        MainWindow.CUnit = 0;
                        CUnit = 0;
                        R_Un1.Text = " (" + Strings.FormatStrings.pxO + ")";
                        R_Un2.Text = " (" + Strings.FormatStrings.pxO + ")";
                        R_Un3.Text = " (" + Strings.FormatStrings.pxO + ")";
                        R_Un4.Text = " (" + Strings.FormatStrings.pxO + ")";
                        break;
                    case 1:
                        BackToPixel(ref x, ref y, ref w, ref h);
                        Box_X.Text = "" + SizeFormatWorker.PxToCm(x);
                        Box_Y.Text = "" + SizeFormatWorker.PxToCm(y);
                        Box_W.Text = "" + SizeFormatWorker.PxToCm(w);
                        Box_H.Text = "" + SizeFormatWorker.PxToCm(h);
                        MainWindow.CUnit = 1;
                        CUnit = 1;
                        R_Un1.Text = " (" + Strings.FormatStrings.cmO + ")";
                        R_Un2.Text = " (" + Strings.FormatStrings.cmO + ")";
                        R_Un3.Text = " (" + Strings.FormatStrings.cmO + ")";
                        R_Un4.Text = " (" + Strings.FormatStrings.cmO + ")";
                        break;
                    case 2:
                        BackToPixel(ref x, ref y, ref w, ref h);
                        Box_X.Text = "" + SizeFormatWorker.PxToIn(x);
                        Box_Y.Text = "" + SizeFormatWorker.PxToIn(y);
                        Box_W.Text = "" + SizeFormatWorker.PxToIn(w);
                        Box_H.Text = "" + SizeFormatWorker.PxToIn(h);
                        MainWindow.CUnit = 2;
                        CUnit = 2;
                        R_Un1.Text = " (" + Strings.FormatStrings.inO + ")";
                        R_Un2.Text = " (" + Strings.FormatStrings.inO + ")";
                        R_Un3.Text = " (" + Strings.FormatStrings.inO + ")";
                        R_Un4.Text = " (" + Strings.FormatStrings.inO + ")";
                        break;
                    case 3:
                        BackToPixel(ref x, ref y, ref w, ref h);
                        Box_X.Text = "" + SizeFormatWorker.PxToPt(x);
                        Box_Y.Text = "" + SizeFormatWorker.PxToPt(y);
                        Box_W.Text = "" + SizeFormatWorker.PxToPt(w);
                        Box_H.Text = "" + SizeFormatWorker.PxToPt(h);
                        MainWindow.CUnit = 3;
                        CUnit = 3;
                        R_Un1.Text = " (" + Strings.FormatStrings.ptO + ")";
                        R_Un2.Text = " (" + Strings.FormatStrings.ptO + ")";
                        R_Un3.Text = " (" + Strings.FormatStrings.ptO + ")";
                        R_Un4.Text = " (" + Strings.FormatStrings.ptO + ")";
                        break;
                }
            }

        }



        bool BackToPixel(ref double x)
        {
            if (MainWindow.CUnit == 0)
                return true;
            else if (MainWindow.CUnit == 1)
            {
                x = SizeFormatWorker.CmToPx(x);
                return true;
            }
            else if (MainWindow.CUnit == 2)
            {
                x = SizeFormatWorker.InToPx(x);
                return true;
            }
            else if (MainWindow.CUnit == 3)
            {
                x = SizeFormatWorker.PtToPx(x);
                return true;
            }

            return false;
        }


        bool BackToPixel(ref double x, ref double y, ref double w, ref double h)
        {
            if (CUnit == 0)
                return true;
            else if (CUnit == 1)
            {
                x = SizeFormatWorker.CmToPx(x);
                y = SizeFormatWorker.CmToPx(y);
                w = SizeFormatWorker.CmToPx(w);
                h = SizeFormatWorker.CmToPx(h);
                return true;
            }
            else if (CUnit == 2)
            {
                x = SizeFormatWorker.InToPx(x);
                y = SizeFormatWorker.InToPx(y);
                w = SizeFormatWorker.InToPx(w);
                h = SizeFormatWorker.InToPx(h);
                return true;
            }
            else if (CUnit == 3)
            {
                x = SizeFormatWorker.PtToPx(x);
                y = SizeFormatWorker.PtToPx(y);
                w = SizeFormatWorker.PtToPx(w);
                h = SizeFormatWorker.PtToPx(h);
                return true;
            }

            return false;
        }

    }
}
