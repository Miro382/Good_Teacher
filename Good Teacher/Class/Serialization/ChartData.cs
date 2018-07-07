using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Good_Teacher.Class.Serialization
{
    public class ChartData
    {
        public List<ChartRowData> SeriesD = new List<ChartRowData>();

        public ChartData()
        {

        }

        public ChartData(SeriesCollection series)
        {
            ToChartData(series);
        }


        public void ToChartData(SeriesCollection series)
        {
            SeriesD.Clear();
            int s = 0;
            foreach (Series ser in series)
            {
                ChartRowData rowdata = new ChartRowData();
                IChartValues values = ser.Values;
                for (int i = 0; i < values.Count; i++)
                {
                    rowdata.ColumnsValues.Add(ConvertType<Double>(values[i]));
                }
                rowdata.DataLabel = ser.DataLabels;
                rowdata.FontSize = ser.FontSize;
                rowdata.Title = ser.Title;
                rowdata.StrokeColor = (SolidColorBrush)ser.Stroke;
                SeriesD.Add(rowdata);
                s++;
            }
        }


        public SeriesCollection ToPieSeriesCollection()
        {
            SeriesCollection seriesCollection = new SeriesCollection();

            for(int r=0;r<SeriesD.Count;r++)
            {
                PieSeries series = new PieSeries();
                ConfigureSeries(series, r);

                seriesCollection.Add(series);
            }

            return seriesCollection;
        }



        public SeriesCollection ToColumnSeriesCollection()
        {
            SeriesCollection seriesCollection = new SeriesCollection();

            for (int r = 0; r < SeriesD.Count; r++)
            {
                ColumnSeries series = new ColumnSeries();
                ConfigureSeries(series, r);

                seriesCollection.Add(series);
            }

            return seriesCollection;
        }



        public SeriesCollection ToLineSeriesCollection()
        {
            SeriesCollection seriesCollection = new SeriesCollection();

            for (int r = 0; r < SeriesD.Count; r++)
            {
                LineSeries series = new LineSeries();
                ConfigureSeries(series, r);

                seriesCollection.Add(series);
            }

            return seriesCollection;
        }


        public SeriesCollection ToRowSeriesCollection()
        {
            SeriesCollection seriesCollection = new SeriesCollection();

            for (int r = 0; r < SeriesD.Count; r++)
            {
                RowSeries series = new RowSeries();
                ConfigureSeries(series, r);

                seriesCollection.Add(series);
            }

            return seriesCollection;
        }


        public SeriesCollection ToStackedColumnSeriesCollection()
        {
            SeriesCollection seriesCollection = new SeriesCollection();

            for (int r = 0; r < SeriesD.Count; r++)
            {
                StackedColumnSeries series = new StackedColumnSeries();
                ConfigureSeries(series, r);

                seriesCollection.Add(series);
            }

            return seriesCollection;
        }


        public SeriesCollection ToStackedRowSeriesCollection()
        {
            SeriesCollection seriesCollection = new SeriesCollection();

            for (int r = 0; r < SeriesD.Count; r++)
            {
                StackedRowSeries series = new StackedRowSeries();
                ConfigureSeries(series, r);

                seriesCollection.Add(series);
            }

            return seriesCollection;
        }


        public SeriesCollection ToStepLineSeriesCollection()
        {
            SeriesCollection seriesCollection = new SeriesCollection();

            for (int r = 0; r < SeriesD.Count; r++)
            {
                StepLineSeries series = new StepLineSeries();
                ConfigureSeries(series, r);

                seriesCollection.Add(series);
            }

            return seriesCollection;
        }


        public SeriesCollection ToStackedAreaSeriesCollection()
        {
            SeriesCollection seriesCollection = new SeriesCollection();

            for (int r = 0; r < SeriesD.Count; r++)
            {
                StackedAreaSeries series = new StackedAreaSeries();
                ConfigureSeries(series, r);

                seriesCollection.Add(series);
            }

            return seriesCollection;
        }


        private void ConfigureSeries(Series series, int r)
        {
            series.Values = new ChartValues<double>();
            series.Title = SeriesD[r].Title;
            series.FontSize = SeriesD[r].FontSize;
            series.Stroke = SeriesD[r].StrokeColor;
            series.DataLabels = SeriesD[r].DataLabel;
            
            for (int c = 0; c < SeriesD[r].ColumnsValues.Count; c++)
            {
                series.Values.Add(SeriesD[r].ColumnsValues[c]);
            }
        }


        public T ConvertType<T>(object input)
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }



    }
}
