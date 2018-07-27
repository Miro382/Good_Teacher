using Good_Teacher.Class;
using Good_Teacher.Class.Serialization;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_RealView.xaml
    /// </summary>
    public partial class Window_RealView : Window
    {
        int curloaded = 0;
        DataStore data;

        public Window_RealView(DataStore datas, int LoadIndex)
        {
            InitializeComponent();

            TextBox_Zoom.HorizontalContentAlignment = HorizontalAlignment.Right;
            TextBox_Page.HorizontalContentAlignment = HorizontalAlignment.Right;
            data = datas;
            curloaded = LoadIndex;
            LoadCanvas();
        }

        private void LoadCanvas()
        {
            Canvas can = CanvasSaveLoad.LoadSpecificCanvas(data, curloaded);
            CanvasSaveLoad.ToPresentationMode(can);
            CanvasSaveLoad.SimulateCanvas(can);

            can.UpdateLayout();
            Size size = new Size();
            can.Measure(size);
            can.Arrange(new Rect(size));

            ImageSource bs = ImageWorker.ByteDataToImage(CanvasWriter.SaveCanvasToImgSimulateFullPng(can));

            IMBorder.Background = new ImageBrush(bs);
            IMBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            IMBorder.BorderThickness = new Thickness(1);
            IMBorder.Width = data.CanvasW;
            IMBorder.Height = data.CanvasH;

            TextBox_Page.Text = ""+(curloaded + 1);
            L_Pages.Content = "/" + data.pages.Count;
        }

        private void TB_Zoom_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateZoomValueByZoomTextBox();
        }


        private void TB_Zoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Enter)
            {
                UpdateZoomValueByZoomTextBox();
                Keyboard.ClearFocus();
            }
        }

        void UpdateZoomValueByZoomTextBox()
        {
            try
            {
                int zm = int.Parse(TextBox_Zoom.Text);
                SliderZoom.Value = zm;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Zoom edit: " + ex);
                TextBox_Zoom.Text = "" + SliderZoom.Value;
            }
        }


        private void SliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IMBorder != null)
            {
                if (SliderZoom.Value == 100)
                {
                    IMBorder.LayoutTransform = null;
                }
                else
                {

                    ScaleTransform scaleTransform = new ScaleTransform((SliderZoom.Value / 100), (SliderZoom.Value / 100));
                    //IMBorder.RenderTransform
                    IMBorder.LayoutTransform = scaleTransform;
                }
                TextBox_Zoom.Text = "" + ((int)SliderZoom.Value);
            }
        }


        private void FlatButtonZoomCancel_Click(object sender, MouseEventArgs e)
        {
            TextBox_Zoom.Text = "100";
            SliderZoom.Value = 100;
            IMBorder.LayoutTransform = null;
        }


        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (!handle)
                return;

            if (e.Delta > 0)
                SliderZoom.Value += 10;
            else
                SliderZoom.Value -= 10;
        }

        private void FlatButtonPrevious_Click(object sender, MouseEventArgs e)
        {
            Previous();
        }

        private void FlatButtonNext_Click(object sender, MouseEventArgs e)
        {
            Next();
        }


        private void Next()
        {
            if (curloaded < data.pages.Count - 1)
            {
                curloaded++;
                LoadCanvas();
            }
        }

        private void Previous()
        {
            if (curloaded > 0)
            {
                curloaded--;
                LoadCanvas();
            }
        }


        private void TB_Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ParsePageBox();
            }
        }

        private void TB_Page_LostFocus(object sender, RoutedEventArgs e)
        {
            ParsePageBox();
        }

        private void ParsePageBox()
        {
            int topage = 1;

            if (int.TryParse(TextBox_Page.Text, out topage))
            {
                if (topage-1 < data.pages.Count && topage > 0)
                {
                    curloaded = topage - 1;
                    LoadCanvas();
                }
                else
                {
                    TextBox_Page.Text = "" + (curloaded + 1);
                }
            }
            else
            {
                TextBox_Page.Text = "" + (curloaded + 1);
            }
        }

        private void FlatButtonClose_Click(object sender, MouseEventArgs e)
        {
            Close();
        }


        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Right)
            {
                Next();

            }else if(e.Key == Key.Left)
            {
                Previous();

            }else if(e.Key == Key.Escape)
            {
                Close();
            }
        }

    }
}
