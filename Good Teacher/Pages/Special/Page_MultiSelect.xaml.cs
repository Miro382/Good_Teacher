using Good_Teacher.Class.Workers;
using Good_Teacher.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Good_Teacher.Pages.Special
{
    /// <summary>
    /// Interaction logic for Page_MultiSelect.xaml
    /// </summary>
    public partial class Page_MultiSelect : Page
    {
        List<FrameworkElement> Selectedcontrols = new List<FrameworkElement>();
        Canvas designcanvas;
        public int CUnit = 0;

        double DLowX, DLowY, DMaxX, DMaxY;
        double LowX, LowY, MaxX, MaxY;


        public Page_MultiSelect(Canvas canvas ,List<FrameworkElement> selectedcontrols, double lowX, double lowY, double maxX, double maxY)
        {
            InitializeComponent();
            Selectedcontrols = new List<FrameworkElement>(selectedcontrols);
            designcanvas = canvas;

            foreach (FrameworkElement cont in Selectedcontrols)
            {

                FlatButtonContent flatButton = new FlatButtonContent();
                string par;


                TextBlock label = new TextBlock();
                label.TextWrapping = TextWrapping.Wrap;
                label.Inlines.Add(new Run(ControlWorker.GetTypeName(cont, out par)+Environment.NewLine) {FontWeight = FontWeights.Bold } );
                label.Inlines.Add(new Run("X: "+ Math.Round(Canvas.GetLeft(cont),2) + Environment.NewLine ));
                label.Inlines.Add(new Run("Y: " + Math.Round(Canvas.GetTop(cont),2) + Environment.NewLine ));
                label.Inlines.Add(new Run("W: " + Math.Round(cont.Width,2) + Environment.NewLine ));
                label.Inlines.Add(new Run("H: " + Math.Round(cont.Height,2) ));

                flatButton.contentPresenter.Content = label;
                flatButton.contentPresenter.HorizontalAlignment = HorizontalAlignment.Left;
                flatButton.contentPresenter.Margin = new Thickness(5);
                flatButton.MinHeight = 24;
                flatButton.MinWidth = 24;
                flatButton.Tag = cont;
                flatButton.BorderBrush = new SolidColorBrush(Colors.Black);
                flatButton.BorderThickness = new Thickness(1);
                flatButton.Foreground = new SolidColorBrush(Colors.White);
                flatButton.Background = new SolidColorBrush(Color.FromRgb(52, 73, 94));
                flatButton.DefaultBrush = new SolidColorBrush(Color.FromRgb(52, 73, 94));
                flatButton.Hover = new SolidColorBrush(Color.FromArgb(180, 52, 73, 94));
                flatButton.ClickBrush = new SolidColorBrush(Color.FromArgb(100, 52, 73, 94));

                flatButton.MouseLeftButtonUp += FlatButton_MouseLeftButtonUp;

                SP_AllControls.Children.Add(flatButton);

            }

            LowX = lowX;
            LowY = lowY;
            MaxX = maxX;
            MaxY = maxY;

            DLowX = lowX;
            DLowY = lowY;
            DMaxX = maxX;
            DMaxY = maxY;

            LoadData();
        }

        private void FlatButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).EditControl(((FrameworkElement)sender).Tag);
        }


        public void LoadData()
        {
            Box_W.Text = "" + MaxX;
            Box_H.Text = "" + MaxY;
            Box_X.Text = "" + LowX;
            Box_Y.Text = "" + LowY;

            ComboBox_SizeUnits.SelectedIndex = MainWindow.CUnit;
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
                    Box_H.Text = "" + lim;
                }
                else if (pos > MainWindow.LimitSize)
                {
                    pos = MainWindow.LimitSize;
                    Box_H.Text = "" + MainWindow.LimitSize;
                }

                MaxY = pos;
                foreach (FrameworkElement cont in Selectedcontrols)
                {
                    cont.Height += MaxY - DMaxY; 
                }
                DMaxY = MaxY;

                ((MainWindow)Window.GetWindow(this)).CalcNewAreaSizes();
                ((MainWindow)Window.GetWindow(this)).DrawMultipleControlsArea();
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
                    Box_W.Text = "" + lim;
                }
                else if (pos > MainWindow.LimitSize)
                {
                    pos = MainWindow.LimitSize;
                    Box_W.Text = "" + MainWindow.LimitSize;
                }

                MaxX = pos;
                foreach (FrameworkElement cont in Selectedcontrols)
                {
                    cont.Width += MaxX - DMaxX;
                }
                DMaxX = MaxX;

                ((MainWindow)Window.GetWindow(this)).CalcNewAreaSizes();
                ((MainWindow)Window.GetWindow(this)).DrawMultipleControlsArea();
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

                LowY = pos;
                foreach (FrameworkElement cont in Selectedcontrols)
                {
                    Canvas.SetTop(cont, Canvas.GetTop(cont) + (LowY-DLowY));
                }
                DLowY = LowY;

                ((MainWindow)Window.GetWindow(this)).CalcNewAreaSizes();
                ((MainWindow)Window.GetWindow(this)).DrawMultipleControlsArea();

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

                LowX = pos;
                foreach (FrameworkElement cont in Selectedcontrols)
                {
                    Canvas.SetLeft(cont, Canvas.GetLeft(cont) + (LowX - DLowX));
                }
                DLowX = LowX;

                ((MainWindow)Window.GetWindow(this)).CalcNewAreaSizes();
                ((MainWindow)Window.GetWindow(this)).DrawMultipleControlsArea();
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



        private void ComboBox_SizeUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized)
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



        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (FrameworkElement cont in Selectedcontrols)
                {
                    designcanvas.Children.Remove(cont);
                }

                ((MainWindow)Window.GetWindow(this)).RemoveSelectedItemEffect();
                NavigationService.Content = "";
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}
