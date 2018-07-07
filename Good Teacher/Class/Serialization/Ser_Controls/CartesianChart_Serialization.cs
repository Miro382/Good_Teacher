using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;
using Good_Teacher.Class.Enumerators;
using System.Windows.Media;

namespace Good_Teacher.Class.Serialization.Ser_Controls
{
    public class CartesianChart_Serialization : ControlSerializer
    {
        public LegendLocation legend = new LegendLocation();
        public ChartData chdata = new ChartData();
        public Color LegendColor = Colors.Black;
        public Brush_Serializer brush = new Brush_Serializer();
        public bool Hoverable = false,disableAnimations = false;
        public Visibility DataTooltipV = Visibility.Visible;
        public double LegendFontSize = 12, TooltipFontSize = 12;
        public ColorsCollection SeriesColors = null;
        public AxisSerializer axisSerializer = new AxisSerializer();
        public ChartType.CartesianChart_Type CarType = new ChartType.CartesianChart_Type();
        public double LegendBullet = 15, TooltipBullet = 15;

        public CartesianChart_Serialization()
        {

        }

        public CartesianChart_Serialization(CartesianChart control, DataStore data)
        {
            Serialize(control, data);
        }


        private void Serialize(CartesianChart control, DataStore data)
        {
            if(control.Series.Count>0)
            {
                if(control.Series[0] is ColumnSeries)
                {
                    CarType = ChartType.CartesianChart_Type.ColumnChart;
                }
                else if (control.Series[0] is StackedAreaSeries)
                {
                    CarType = ChartType.CartesianChart_Type.StackedAreaChart;
                }
                else if (control.Series[0] is LineSeries)
                {
                    CarType = ChartType.CartesianChart_Type.LineChart;
                }
                else if (control.Series[0] is RowSeries)
                {
                    CarType = ChartType.CartesianChart_Type.RowChart;
                }
                else if (control.Series[0] is StackedColumnSeries)
                {
                    CarType = ChartType.CartesianChart_Type.StackedColumnChart;
                }
                else if (control.Series[0] is StackedRowSeries)
                {
                    CarType = ChartType.CartesianChart_Type.StackedRowChart;
                }
                else if (control.Series[0] is StepLineSeries)
                {
                    CarType = ChartType.CartesianChart_Type.StepLineChart;
                }
            }

            SerializeDefault(control);
            legend = control.LegendLocation;
            chdata = new ChartData(control.Series);
            brush.Serialize(control, control.Background, data);
            axisSerializer.Serialize(control);

            Hoverable = control.Hoverable;
            DataTooltipV = control.DataTooltip.Visibility;
            LegendFontSize = control.ChartLegend.FontSize;
            TooltipFontSize = control.DataTooltip.FontSize;
            disableAnimations = control.DisableAnimations;

            LegendBullet = ((DefaultLegend)control.ChartLegend).BulletSize;
            TooltipBullet = ((DefaultTooltip)control.DataTooltip).BulletSize;

            LegendColor = ((SolidColorBrush)control.ChartLegend.Foreground).Color;

            if (control.SeriesColors != null)
                SeriesColors = control.SeriesColors;
            else
                SeriesColors = null;
        }



        private void Deserialize(CartesianChart control, DataStore data)
        {
            DeserializeDefault(control);
            control.LegendLocation = legend;

            if (CarType == ChartType.CartesianChart_Type.ColumnChart)
                control.Series = chdata.ToColumnSeriesCollection();
            else if (CarType == ChartType.CartesianChart_Type.StackedAreaChart)
                control.Series = chdata.ToStackedAreaSeriesCollection();
            else if (CarType == ChartType.CartesianChart_Type.LineChart)
                control.Series = chdata.ToLineSeriesCollection();
            else if (CarType == ChartType.CartesianChart_Type.RowChart)
                control.Series = chdata.ToRowSeriesCollection();
            else if (CarType == ChartType.CartesianChart_Type.StackedColumnChart)
                control.Series = chdata.ToStackedColumnSeriesCollection();
            else if (CarType == ChartType.CartesianChart_Type.StackedRowChart)
                control.Series = chdata.ToStackedRowSeriesCollection();
            else if (CarType == ChartType.CartesianChart_Type.StepLineChart)
                control.Series = chdata.ToStepLineSeriesCollection();

            brush.Deserialize(control, data);
            axisSerializer.Deserialize(control);

            if (effect != null)
                control.Effect = effect.CreateEffect();

            control.Hoverable = Hoverable;
            control.DataTooltip.Visibility = DataTooltipV;
            control.ChartLegend.FontSize = LegendFontSize;
            control.DataTooltip.FontSize = TooltipFontSize;
            control.DisableAnimations = disableAnimations;

            ((DefaultLegend)control.ChartLegend).BulletSize = LegendBullet;
            ((DefaultTooltip)control.DataTooltip).BulletSize = TooltipBullet;

            control.ChartLegend.Foreground = new SolidColorBrush(LegendColor);

            if (SeriesColors != null)
                control.SeriesColors = SeriesColors;
            else
                control.SeriesColors = null;
                
        }


        public CartesianChart CreateControl(DataStore data)
        {
            CartesianChart control = new CartesianChart();
            Deserialize(control, data);
            return control;
        }


    }
}
