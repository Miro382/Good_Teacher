using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Good_Teacher.Class.Enumerators;
namespace Good_Teacher.Windows
{
    /// <summary>
    /// Interaction logic for Window_ChartData.xaml
    /// </summary>
    public partial class Window_ChartData : Window
    {
        Chart chart;
        ChartType.Chart_Type Ctype;

        DataTable dt = new DataTable("Data");

        int col = 2;
        public Window_ChartData(Chart _Chart, ChartType.Chart_Type type)
        {
            InitializeComponent();
            chart = _Chart;
            Ctype = type;

            dt.Columns.Add("C1", typeof(double));
            dt.Rows.Add("0");
            datagrid.DataContext = dt.DefaultView;

            int s = 0;
            foreach(Series ser in chart.Series)
            {
                IChartValues values = ser.Values;
                for(int i=0;i<values.Count;i++)
                {
                    if(s>=dt.Rows.Count)
                        dt.Rows.Add("0");

                    if (i >= dt.Columns.Count)
                    {
                        dt.Columns.Add("C" + col, typeof(double));
                        col++;
                    }

                    dt.Rows[s][i] = ConvertType<Double>(values[i]);
                    Debug.WriteLine(ConvertType<Double>( values[i]) );

                }
                s++;
            }


            if (chart.SeriesColors != null)
            {
                RB_CusColors.IsChecked = true;

                foreach (Color col in chart.SeriesColors)
                {
                    AddColor(col);
                }
            }

        }



        public T ConvertType<T>(object input)
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }


        private void ButtonColumn_Click(object sender, RoutedEventArgs e)
        {
            dt.Columns.Add("C"+col, typeof(double));
            datagrid.Columns.Add(new DataGridTextColumn() { Binding = new Binding("C"+col), Header = "C"+col });
            col++;
        }

        private void ButtonColumnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (col > 2)
            {
                col--;
                dt.Columns.RemoveAt(col - 1);
                datagrid.Columns.RemoveAt(col - 1);
            }
        }


        private void ButtonRowRemove_Click(object sender, RoutedEventArgs e)
        {
            if(dt.Rows.Count>1)
            dt.Rows.RemoveAt(dt.Rows.Count-1);
        }


        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Update();
            Close();
        }


        void Update()
        {

            chart.Series.Clear();
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                Series CSeries;

                if (Ctype == ChartType.Chart_Type.PieChart)
                    CSeries = new PieSeries();
                else if (Ctype == ChartType.Chart_Type.ColumnChart)
                    CSeries = new ColumnSeries();
                else if (Ctype == ChartType.Chart_Type.RowChart)
                    CSeries = new RowSeries();
                else if (Ctype == ChartType.Chart_Type.StackedColumnChart)
                    CSeries = new StackedColumnSeries();
                else if (Ctype == ChartType.Chart_Type.StackedRowChart)
                    CSeries = new StackedRowSeries();
                else if (Ctype == ChartType.Chart_Type.StackedAreaChart)
                    CSeries = new StackedAreaSeries();
                else if (Ctype == ChartType.Chart_Type.StepLineChart)
                    CSeries = new StepLineSeries();
                else
                    CSeries = new LineSeries();

                CSeries.Values = new ChartValues<double> { };
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    // Debug.WriteLine(dt.Rows[r].Field<double?>(c));
                    if (dt.Rows[r].Field<double?>(c) != null)
                        CSeries.Values.Add(dt.Rows[r].Field<double>(c));
                }//columns

                if (r < ListBox_Customization.Items.Count)
                {

                    StackPanel panel = (StackPanel)ListBox_Customization.Items[r];
                    CSeries.Title = ((TextBox)panel.Children[0]).Text;
                    CSeries.DataLabels = ( ( (CheckBox)panel.Children[1]).IsChecked == true);

                    double size = 10;
                    if (double.TryParse(((TextBox)panel.Children[2]).Text, out size) == false)
                        size = 10;

                    CSeries.FontSize = size;
                    CSeries.Stroke = ((Rectangle)((StackPanel)panel.Children[3]).Children[0]).Fill;
                    double sth = 0;
                    if (double.TryParse(((TextBox)panel.Children[4]).Text, out sth))
                        CSeries.StrokeThickness = sth;

                    /*
                    if ((((CheckBox)panel.Children[4]).IsChecked == true))
                        CSeries.Fill = ((Rectangle)((StackPanel)panel.Children[5]).Children[0]).Fill;
                    else
                        CSeries.Fill = null;*/

                }
                else
                {
                    CSeries.Title = "R" + r;
                    CSeries.DataLabels = true;
                }

                chart.Series.Add(CSeries);
            }//rows


            if(RB_CusColors.IsChecked==true)
            {
                ColorsCollection colorsCollection = new ColorsCollection();

                for(int i=0;i<ListBox_Colors.Items.Count;i++)
                {
                    StackPanel stack = (StackPanel)ListBox_Colors.Items[i];
                    colorsCollection.Add( ((SolidColorBrush)((Rectangle)stack.Children[0]).Fill).Color );
                }

                chart.SeriesColors = colorsCollection;
            }
            else
            {
                chart.SeriesColors = null;
            }

        }


        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }


        void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (dg != null)
            {
                DataGridRow dgr = (DataGridRow)(dg.ItemContainerGenerator.ContainerFromIndex(dg.SelectedIndex));
                if (e.Key == Key.Delete && !dgr.IsEditing)
                {
                    e.Handled = false;
                }
            }
        }



        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(tabControl.SelectedItem == TabItem_Cust)
            {
                ListBox_Customization.Items.Clear();
                int s = 0;
                ListBox_Customization.Items.Clear();
                foreach (Series ser in chart.Series)
                {
                    if (s < dt.Rows.Count)
                    {
                        AddCustomizationBlocks(ser.Title, ser.DataLabels,((SolidColorBrush)ser.Stroke).Color,ser.StrokeThickness,ser.FontSize);
                        s++;
                    }
                }

                for (int r = s; r < dt.Rows.Count; r++)
                {
                    AddCustomizationBlocks("R" + r, true, Colors.White,2,10);
                }//rows
            }
        }//TabControl_SelectionChanged


        void AddCustomizationBlocks(string title, bool ischeckedD, Color color, double strokeThickness, double DataLabelFontSize)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;

            TextBox text = new TextBox();
            text.Text = title;
            text.VerticalContentAlignment = VerticalAlignment.Center;
            text.Height = 20;
            text.Width = 100;
            text.Margin = new Thickness(5);
            text.ToolTip = Strings.ResStrings.Title;


            CheckBox check = new CheckBox();
            check.IsChecked = ischeckedD;
            check.Height = 21;
            check.Margin = new Thickness(17,5,5,5);
            check.ToolTip = Strings.ResStrings.DataLabel;


            TextBox textd = new TextBox();
            textd.Text = "" + DataLabelFontSize;
            textd.VerticalContentAlignment = VerticalAlignment.Center;
            textd.Height = 20;
            textd.Width = 50;
            textd.Margin = new Thickness(5, 5, 45, 5);
            textd.ToolTip = Strings.ResStrings.DataLabelSize;


            TextBox text2 = new TextBox();
            text2.Text = ""+strokeThickness;
            text2.VerticalContentAlignment = VerticalAlignment.Center;
            text2.Height = 20;
            text2.Width = 50;
            text2.Margin = new Thickness(35,5,25,5);
            text2.ToolTip = Strings.ResStrings.StrokeThickness;


            /*
            CheckBox checkF = new CheckBox();
            checkF.IsChecked = CustomFill;
            checkF.Height = 21;
            checkF.Margin = new Thickness(17, 5, 50, 5);
            checkF.ToolTip = Strings.ResStrings.CustomFill;
            */

            stackPanel.Children.Add(text);
            stackPanel.Children.Add(check);
            stackPanel.Children.Add(textd);
            stackPanel.Children.Add(AddColorToPanel(color));
            stackPanel.Children.Add(text2);
            //stackPanel.Children.Add(checkF);
            //stackPanel.Children.Add(AddColorToPanel(fillcolor));
            ListBox_Customization.Items.Add(stackPanel);
        }


        private void AddColor_Click(object sender, RoutedEventArgs e)
        {
            AddColor(Colors.White);
        }


        public StackPanel AddColorToPanel(Color color)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;

            Rectangle rectangle = new Rectangle();
            rectangle.Width = 32;
            rectangle.Height = 24;
            rectangle.Stroke = new SolidColorBrush(Colors.Black);
            rectangle.StrokeThickness = 1;
            rectangle.Fill = new SolidColorBrush(color);

            Button button = new Button();

            button.Content = new Image
            {
                Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/EditValues/colorpicker.png")),
                VerticalAlignment = VerticalAlignment.Center
            };

            button.Margin = new Thickness(5, 0, 0, 0);
            button.Width = 24;
            button.Height = 24;
            button.Click += ButtonChangeCustomColor_Click;

            stackPanel.Children.Add(rectangle);
            stackPanel.Children.Add(button);

            return stackPanel;
        }



        public void AddColor(Color color)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;

            Rectangle rectangle = new Rectangle();
            rectangle.Width = 32;
            rectangle.Height = 24;
            rectangle.Stroke = new SolidColorBrush(Colors.Black);
            rectangle.StrokeThickness = 1;
            rectangle.Fill = new SolidColorBrush(color);

            Button button = new Button();

            button.Content = new Image
            {
                Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/EditValues/colorpicker.png")),
                VerticalAlignment = VerticalAlignment.Center
            };

            button.Margin = new Thickness(5, 0, 0, 0);
            button.Width = 24;
            button.Height = 24;
            button.Click += ButtonChangeCustomColor_Click;

            stackPanel.Children.Add(rectangle);
            stackPanel.Children.Add(button);

            ListBox_Colors.Items.Add(stackPanel);
        }


        private void ButtonChangeCustomColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(((Rectangle)((StackPanel)((FrameworkElement)sender).Parent).Children[0]).Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                ((Rectangle) ((StackPanel)((FrameworkElement)sender).Parent).Children[0]).Fill = new SolidColorBrush(colorp.GetColor());
            }

        }


        private void RemoveColor_Click(object sender, RoutedEventArgs e)
        {
            if(ListBox_Colors.SelectedIndex>=0)
            {
                ListBox_Colors.Items.RemoveAt(ListBox_Colors.SelectedIndex);
            }
        }

    }
}
