using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher
{
    public partial class MainWindow : Window
    {
        public void AddLevel3Control(Point p)
        {
            switch (ControlTag)
            {
                case 31:
                    {
                        PieChart pieChart = new PieChart();
                        pieChart.Width = 400;
                        pieChart.Height = 300;

                        pieChart.LegendLocation = LegendLocation.Bottom;

                        PieSeries pieSeries = new PieSeries();
                        pieSeries.Values = new ChartValues<int> { 5 };
                        pieSeries.Title = "A";
                        pieSeries.DataLabels = true;

                        PieSeries pieSeries2 = new PieSeries();
                        pieSeries2.Values = new ChartValues<int> { 3 };
                        pieSeries2.Title = "B";
                        pieSeries2.DataLabels = true;

                        PieSeries pieSeries3 = new PieSeries();
                        pieSeries3.Values = new ChartValues<int> { 6 };
                        pieSeries3.Title = "C";
                        pieSeries3.DataLabels = true;

                        pieChart.Series.Add(pieSeries);
                        pieChart.Series.Add(pieSeries2);
                        pieChart.Series.Add(pieSeries3);
                        pieChart.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(pieChart, 0);
                        Canvas.SetLeft(pieChart, p.X - pieChart.DesiredSize.Width / 2);
                        Canvas.SetTop(pieChart, p.Y - pieChart.DesiredSize.Height / 2);

                        AddEvents(pieChart);
                        DesignCanvas.Children.Add(pieChart);
                    }

                    break;
                case 32:
                    {
                        CartesianChart cartesianChart = new CartesianChart();
                        cartesianChart.Width = 400;
                        cartesianChart.Height = 300;

                        cartesianChart.LegendLocation = LegendLocation.Bottom;

                        ColumnSeries columnSeries = new ColumnSeries();
                        columnSeries.Values = new ChartValues<int> { 5 };
                        columnSeries.Title = "A";
                        columnSeries.DataLabels = true;

                        ColumnSeries columnSeries2 = new ColumnSeries();
                        columnSeries2.Values = new ChartValues<int> { 3 };
                        columnSeries2.Title = "B";
                        columnSeries2.DataLabels = true;

                        ColumnSeries columnSeries3 = new ColumnSeries();
                        columnSeries3.Values = new ChartValues<int> { 6 };
                        columnSeries3.Title = "C";
                        columnSeries3.DataLabels = true;

                        cartesianChart.Series.Add(columnSeries);
                        cartesianChart.Series.Add(columnSeries2);
                        cartesianChart.Series.Add(columnSeries3);

                        cartesianChart.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(cartesianChart, 0);
                        Canvas.SetLeft(cartesianChart, p.X - cartesianChart.DesiredSize.Width / 2);
                        Canvas.SetTop(cartesianChart, p.Y - cartesianChart.DesiredSize.Height / 2);

                        AddEvents(cartesianChart);
                        DesignCanvas.Children.Add(cartesianChart);

                    }
                    break;
                case 33:
                    {
                        CartesianChart cartesianChart = new CartesianChart();
                        cartesianChart.Width = 400;
                        cartesianChart.Height = 300;

                        cartesianChart.LegendLocation = LegendLocation.Bottom;

                        StackedColumnSeries columnSeries = new StackedColumnSeries();
                        columnSeries.Values = new ChartValues<int> { 5, 6, 5 };
                        columnSeries.Title = "A";
                        columnSeries.DataLabels = true;

                        StackedColumnSeries columnSeries2 = new StackedColumnSeries();
                        columnSeries2.Values = new ChartValues<int> { 3, 4, 2 };
                        columnSeries2.Title = "B";
                        columnSeries2.DataLabels = true;

                        StackedColumnSeries columnSeries3 = new StackedColumnSeries();
                        columnSeries3.Values = new ChartValues<int> { 6, 3, 3 };
                        columnSeries3.Title = "C";
                        columnSeries3.DataLabels = true;

                        cartesianChart.Series.Add(columnSeries);
                        cartesianChart.Series.Add(columnSeries2);
                        cartesianChart.Series.Add(columnSeries3);

                        cartesianChart.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(cartesianChart, 0);
                        Canvas.SetLeft(cartesianChart, p.X - cartesianChart.DesiredSize.Width / 2);
                        Canvas.SetTop(cartesianChart, p.Y - cartesianChart.DesiredSize.Height / 2);

                        AddEvents(cartesianChart);
                        DesignCanvas.Children.Add(cartesianChart);

                    }
                    break;

                case 34:
                    {
                        CartesianChart cartesianChart = new CartesianChart();
                        cartesianChart.Width = 400;
                        cartesianChart.Height = 300;

                        cartesianChart.LegendLocation = LegendLocation.Bottom;

                        RowSeries CarSeries = new RowSeries();
                        CarSeries.Values = new ChartValues<int> { 5 };
                        CarSeries.Title = "A";
                        CarSeries.DataLabels = true;

                        RowSeries CarSeries2 = new RowSeries();
                        CarSeries2.Values = new ChartValues<int> { 3 };
                        CarSeries2.Title = "B";
                        CarSeries2.DataLabels = true;

                        RowSeries CarSeries3 = new RowSeries();
                        CarSeries3.Values = new ChartValues<int> { 6 };
                        CarSeries3.Title = "C";
                        CarSeries3.DataLabels = true;

                        cartesianChart.Series.Add(CarSeries);
                        cartesianChart.Series.Add(CarSeries2);
                        cartesianChart.Series.Add(CarSeries3);

                        cartesianChart.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(cartesianChart, 0);
                        Canvas.SetLeft(cartesianChart, p.X - cartesianChart.DesiredSize.Width / 2);
                        Canvas.SetTop(cartesianChart, p.Y - cartesianChart.DesiredSize.Height / 2);

                        AddEvents(cartesianChart);
                        DesignCanvas.Children.Add(cartesianChart);

                    }
                    break;

                case 35:
                    {
                        CartesianChart cartesianChart = new CartesianChart();
                        cartesianChart.Width = 400;
                        cartesianChart.Height = 300;

                        cartesianChart.LegendLocation = LegendLocation.Bottom;

                        StackedRowSeries columnSeries = new StackedRowSeries();
                        columnSeries.Values = new ChartValues<int> { 5, 6, 5 };
                        columnSeries.Title = "A";
                        columnSeries.DataLabels = true;

                        StackedRowSeries columnSeries2 = new StackedRowSeries();
                        columnSeries2.Values = new ChartValues<int> { 3, 4, 2 };
                        columnSeries2.Title = "B";
                        columnSeries2.DataLabels = true;

                        StackedRowSeries columnSeries3 = new StackedRowSeries();
                        columnSeries3.Values = new ChartValues<int> { 6, 3, 3 };
                        columnSeries3.Title = "C";
                        columnSeries3.DataLabels = true;

                        cartesianChart.Series.Add(columnSeries);
                        cartesianChart.Series.Add(columnSeries2);
                        cartesianChart.Series.Add(columnSeries3);

                        cartesianChart.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(cartesianChart, 0);
                        Canvas.SetLeft(cartesianChart, p.X - cartesianChart.DesiredSize.Width / 2);
                        Canvas.SetTop(cartesianChart, p.Y - cartesianChart.DesiredSize.Height / 2);

                        AddEvents(cartesianChart);
                        DesignCanvas.Children.Add(cartesianChart);

                    }
                    break;
                case 36:
                    {
                        CartesianChart lineChart = new CartesianChart();
                        lineChart.Width = 400;
                        lineChart.Height = 300;

                        lineChart.LegendLocation = LegendLocation.Bottom;

                        LineSeries lineSeries = new LineSeries();
                        lineSeries.Values = new ChartValues<int> { 5, 6, 8 };
                        lineSeries.Title = "A";
                        lineSeries.DataLabels = true;

                        LineSeries lineSeries2 = new LineSeries();
                        lineSeries2.Values = new ChartValues<int> { 3, 2, 5 };
                        lineSeries2.Title = "B";
                        lineSeries2.DataLabels = true;

                        LineSeries lineSeries3 = new LineSeries();
                        lineSeries3.Values = new ChartValues<int> { 6, 5, 3 };
                        lineSeries3.Title = "C";
                        lineSeries3.DataLabels = true;

                        lineChart.Series.Add(lineSeries);
                        lineChart.Series.Add(lineSeries2);
                        lineChart.Series.Add(lineSeries3);

                        lineChart.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(lineChart, 0);
                        Canvas.SetLeft(lineChart, p.X - lineChart.DesiredSize.Width / 2);
                        Canvas.SetTop(lineChart, p.Y - lineChart.DesiredSize.Height / 2);

                        AddEvents(lineChart);
                        DesignCanvas.Children.Add(lineChart);

                    }
                    break;

                case 37:
                    {
                        CartesianChart cartesianChart = new CartesianChart();
                        cartesianChart.Width = 400;
                        cartesianChart.Height = 300;

                        cartesianChart.LegendLocation = LegendLocation.Bottom;

                        StackedAreaSeries columnSeries = new StackedAreaSeries();
                        columnSeries.Values = new ChartValues<int> { 2, 4, 6 };
                        columnSeries.Title = "A";
                        columnSeries.DataLabels = true;

                        StackedAreaSeries columnSeries2 = new StackedAreaSeries();
                        columnSeries2.Values = new ChartValues<int> { 3, 5, 7 };
                        columnSeries2.Title = "B";
                        columnSeries2.DataLabels = true;

                        StackedAreaSeries columnSeries3 = new StackedAreaSeries();
                        columnSeries3.Values = new ChartValues<int> { 4, 6, 8 };
                        columnSeries3.Title = "C";
                        columnSeries3.DataLabels = true;

                        cartesianChart.Series.Add(columnSeries);
                        cartesianChart.Series.Add(columnSeries2);
                        cartesianChart.Series.Add(columnSeries3);

                        cartesianChart.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(cartesianChart, 0);
                        Canvas.SetLeft(cartesianChart, p.X - cartesianChart.DesiredSize.Width / 2);
                        Canvas.SetTop(cartesianChart, p.Y - cartesianChart.DesiredSize.Height / 2);

                        AddEvents(cartesianChart);
                        DesignCanvas.Children.Add(cartesianChart);

                    }
                    break;
                case 38:
                    {
                        CartesianChart cartesianChart = new CartesianChart();
                        cartesianChart.Width = 400;
                        cartesianChart.Height = 300;

                        cartesianChart.LegendLocation = LegendLocation.Bottom;

                        StepLineSeries columnSeries = new StepLineSeries();
                        columnSeries.Values = new ChartValues<int> { 1, 2, 1 };
                        columnSeries.Title = "A";
                        columnSeries.DataLabels = true;

                        StepLineSeries columnSeries2 = new StepLineSeries();
                        columnSeries2.Values = new ChartValues<int> { 3, 5, 6 };
                        columnSeries2.Title = "B";
                        columnSeries2.DataLabels = true;

                        StepLineSeries columnSeries3 = new StepLineSeries();
                        columnSeries3.Values = new ChartValues<int> { 6, 7, 7 };
                        columnSeries3.Title = "C";
                        columnSeries3.DataLabels = true;

                        cartesianChart.Series.Add(columnSeries);
                        cartesianChart.Series.Add(columnSeries2);
                        cartesianChart.Series.Add(columnSeries3);

                        cartesianChart.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(cartesianChart, 0);
                        Canvas.SetLeft(cartesianChart, p.X - cartesianChart.DesiredSize.Width / 2);
                        Canvas.SetTop(cartesianChart, p.Y - cartesianChart.DesiredSize.Height / 2);

                        AddEvents(cartesianChart);
                        DesignCanvas.Children.Add(cartesianChart);

                    }
                    break;


            }
        }
    }
}
