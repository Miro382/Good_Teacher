using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Good_Teacher.Windows;
using System.Text.RegularExpressions;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_CartesianChart.xaml
    /// </summary>
    public partial class Value_CartesianChart : System.Windows.Controls.Page
    {

        CartesianChart cont;
        DataStore data;


        public Value_CartesianChart(DataStore datas ,CartesianChart chart)
        {
            InitializeComponent();

            cont = chart;

            data = datas;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();

            brushselector.SetData(cont,data, true);
            brushselector.LoadData(cont.Background);
            brushselector.ChangedBrush -= Brushselector_ChangedBrush;
            brushselector.ChangedBrush += Brushselector_ChangedBrush;


            Box_LegFont.Text = "" + cont.ChartLegend.FontSize;
            Box_ToolFont.Text = "" + cont.DataTooltip.FontSize;

            CB_interactive.IsChecked = cont.Hoverable;

            ComboBox_LegLoc.SelectedIndex = (int)cont.LegendLocation;

            CB_ToolVis.IsChecked = cont.DataTooltip.Visibility == Visibility.Visible;

            CB_DisableAnimations.IsChecked = cont.DisableAnimations;

            if (cont.AxisX.Count > 0)
                Rect_AxisXColor.Fill = cont.AxisX[0].Foreground;
            if (cont.AxisY.Count > 0)
                Rect_AxisYColor.Fill = cont.AxisY[0].Foreground;

            Debug.WriteLine("Series type: "+cont.Series[0],GetType());
            Debug.WriteLine(""+ (cont.Series[0] is LineSeries));

            Rect_LegendColor.Fill = cont.ChartLegend.Foreground;

            Box_LegBullet.Text = "" + ((DefaultLegend)cont.ChartLegend).BulletSize;
            Box_ToolBullet.Text = "" + ((DefaultTooltip)cont.DataTooltip).BulletSize;
        }

        private void Brushselector_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            cont.Background = Sbrush;
        }

        void DetectSender(object senderob)
        {
            if (senderob == Box_LegFont)
                SetLegFont();
            else if (senderob == Box_ToolFont)
                SetToolFont();
            else if (senderob == Box_AxisXTitle)
                SetAxisTitle(true);
            else if (senderob == Box_AxisYTitle)
                SetAxisTitle(false);
            else if (senderob == Box_LegBullet)
                SetLegendBullet();
            else if (senderob == Box_ToolBullet)
                SetTooltipBullet();
        }


        void SetAxisTitle(bool Xtitle)
        {
            try
            {
                if (Xtitle)
                    cont.AxisX[0].Title = Box_AxisXTitle.Text;
                else
                    cont.AxisY[0].Title = Box_AxisYTitle.Text;

            }
            catch(Exception ex)
            {
                if (Xtitle)
                    Box_AxisXTitle.Text = "";
                else
                    Box_AxisYTitle.Text = "";
                Debug.WriteLine("Error to set axis title: "+ex);
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



        void SetLegendBullet()
        {
            try
            {
                ((DefaultLegend)cont.ChartLegend).BulletSize = double.Parse(Box_LegBullet.Text);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Data legend set bullet size: " + ex);
                Box_LegBullet.Text = "" + ((DefaultLegend)cont.ChartLegend).BulletSize;
            }
        }

        void SetTooltipBullet()
        {
            try
            {
                ((DefaultTooltip)cont.DataTooltip).BulletSize = double.Parse(Box_ToolBullet.Text);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Data tooltip set bullet size: " + ex);
                Box_ToolBullet.Text = "" + ((DefaultTooltip)cont.DataTooltip).BulletSize;
            }
        }



        private void Data_Click(object sender, RoutedEventArgs e)
        {

            Window_ChartData chartData = new Window_ChartData(cont, Class.Enumerators.ChartType.Chart_Type.ColumnChart);

            if (cont.Series.Count > 0)
            {
                if(cont.Series[0] is ColumnSeries)
                    chartData = new Window_ChartData(cont, Class.Enumerators.ChartType.Chart_Type.ColumnChart);
                else if (cont.Series[0] is StackedAreaSeries)
                    chartData = new Window_ChartData(cont, Class.Enumerators.ChartType.Chart_Type.StackedAreaChart);
                else if(cont.Series[0] is LineSeries)
                    chartData = new Window_ChartData(cont, Class.Enumerators.ChartType.Chart_Type.LineChart);
                else if (cont.Series[0] is RowSeries)
                    chartData = new Window_ChartData(cont, Class.Enumerators.ChartType.Chart_Type.RowChart);
                else if (cont.Series[0] is StackedColumnSeries)
                    chartData = new Window_ChartData(cont, Class.Enumerators.ChartType.Chart_Type.StackedColumnChart);
                else if (cont.Series[0] is StackedRowSeries)
                    chartData = new Window_ChartData(cont, Class.Enumerators.ChartType.Chart_Type.StackedRowChart);
                else if (cont.Series[0] is StepLineSeries)
                    chartData = new Window_ChartData(cont, Class.Enumerators.ChartType.Chart_Type.StepLineChart);
            }

            chartData.Owner = Window.GetWindow(this);
            chartData.ShowDialog();

        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (cont != null)
            {
                if (sender == CB_ToolVis)
                {
                    if (CB_ToolVis.IsChecked == true)
                        cont.DataTooltip.Visibility = Visibility.Visible;
                    else
                        cont.DataTooltip.Visibility = Visibility.Collapsed;

                }
                else if (sender == CB_interactive)
                {
                    cont.Hoverable = CB_interactive.IsChecked == true;
                }
                else if (sender == CB_DisableAnimations)
                {
                    cont.DisableAnimations = CB_DisableAnimations.IsChecked == true;
                }

            }
        }


        void SetLegFont()
        {
            try
            {
                cont.ChartLegend.FontSize = double.Parse(Box_LegFont.Text);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Legend set font size: " + ex);
                Box_LegFont.Text = "12";
            }
        }


        void SetToolFont()
        {
            try
            {
                cont.DataTooltip.FontSize = double.Parse(Box_ToolFont.Text);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Data tooltip set font size: " + ex);
                Box_ToolFont.Text = "12";
            }
        }



        private void ComboBoxLegLoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cont != null)
            {
                cont.LegendLocation = (LegendLocation)ComboBox_LegLoc.SelectedIndex;
            }
        }

        private void ButtonAxisXColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_AxisXColor.Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                cont.AxisX[0].Foreground = new SolidColorBrush(colorp.GetColor());
                cont.AxisX[0].Separator.Stroke = new SolidColorBrush(colorp.GetColor());

                Rect_AxisXColor.Fill = new SolidColorBrush(colorp.GetColor());
            }
        }

        private void ButtonAxisYColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_AxisYColor.Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                cont.AxisY[0].Foreground = new SolidColorBrush(colorp.GetColor());
                cont.AxisY[0].Separator.Stroke = new SolidColorBrush(colorp.GetColor());

                Rect_AxisYColor.Fill = new SolidColorBrush(colorp.GetColor());
            }
        }

        private void ButtonLegendColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(cont.ChartLegend.Foreground);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                Rect_LegendColor.Fill = new SolidColorBrush(colorp.GetColor());
                cont.ChartLegend.Foreground = new SolidColorBrush(colorp.GetColor());
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


    }
}
