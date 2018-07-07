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
    /// Interaction logic for Value_PieChart.xaml
    /// </summary>
    public partial class Value_PieChart : System.Windows.Controls.Page
    {

        PieChart cont;
        DataStore data;

        public Value_PieChart(DataStore dataStore ,PieChart pieChart)
        {
            InitializeComponent();

            cont = pieChart;

            data = dataStore;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();

            brushselector.SetData(cont,data,true);
            brushselector.LoadData(cont.Background);
            brushselector.ChangedBrush -= Brushselector_ChangedBrush;
            brushselector.ChangedBrush += Brushselector_ChangedBrush;

            Box_PushOut.Text = ""+cont.HoverPushOut;
            Box_LegFont.Text = "" + cont.ChartLegend.FontSize;
            Box_ToolFont.Text = "" + cont.DataTooltip.FontSize;

            CB_interactive.IsChecked = cont.Hoverable;

            ComboBox_LegLoc.SelectedIndex = (int)cont.LegendLocation;

            CB_ToolVis.IsChecked = cont.DataTooltip.Visibility == Visibility.Visible;

            Box_InnerRadius.Text = "" + cont.InnerRadius;

            CB_DisableAnimations.IsChecked = cont.DisableAnimations;

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
            else if (senderob == Box_PushOut)
                SetPushOut();
            else if (senderob == Box_InnerRadius)
                SetInnerRadius();
            else if (senderob == Box_LegBullet)
                SetLegendBullet();
            else if (senderob == Box_ToolBullet)
                SetTooltipBullet();
        }


        void SetInnerRadius()
        {
            try
            {
                cont.InnerRadius = double.Parse(Box_InnerRadius.Text);

            }catch(Exception ex)
            {
                Box_InnerRadius.Text = "0";
                Debug.WriteLine("Set Inner Radius: "+ex);
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




        private void Data_Click(object sender, RoutedEventArgs e)
        {
            Window_ChartData chartData = new Window_ChartData(cont, Class.Enumerators.ChartType.Chart_Type.PieChart);
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

                }else if(sender == CB_interactive)
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
                Debug.WriteLine("Legend set font size: "+ex);
                Box_LegFont.Text = "12";
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
                Box_ToolBullet.Text = ""+((DefaultTooltip)cont.DataTooltip).BulletSize;
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


        void SetPushOut()
        {
            try
            {
                cont.HoverPushOut = double.Parse(Box_PushOut.Text);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Hover Push out: " + ex);
                Box_PushOut.Text = "5";
            }
        }



        private void ComboBoxLegLoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cont != null)
            {
                cont.LegendLocation = (LegendLocation)ComboBox_LegLoc.SelectedIndex;
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
