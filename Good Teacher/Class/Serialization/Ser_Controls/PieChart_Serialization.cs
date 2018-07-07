using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;
using System.Windows.Media;

namespace Good_Teacher.Class.Serialization
{
    public class PieChart_Serialization : ControlSerializer
    {
        public LegendLocation legend = new LegendLocation();
        public ChartData chdata = new ChartData();
        public Color LegendColor = Colors.Black;
        public Brush_Serializer brush = new Brush_Serializer();
        public bool Hoverable = false, disableAnimations = false;
        public Visibility DataTooltipV = Visibility.Visible;
        public double LegendFontSize = 12, TooltipFontSize = 12, PushOut = 5;
        public ColorsCollection SeriesColors = null;
        public double InnerRadius = 0;
        public double LegendBullet = 15, TooltipBullet = 15;

        public PieChart_Serialization()
        {

        }

        public PieChart_Serialization(PieChart control, DataStore data)
        {
            Serialize(control,data);
        }


        private void Serialize(PieChart control, DataStore data)
        {
            SerializeDefault(control);
            legend = control.LegendLocation;
            InnerRadius = control.InnerRadius;
            chdata = new ChartData(control.Series);
            ControlDef.Serialize(control);
            brush.Serialize(control,control.Background, data);
            Hoverable = control.Hoverable;
            DataTooltipV = control.DataTooltip.Visibility;
            PushOut = control.HoverPushOut;
            LegendFontSize = control.ChartLegend.FontSize;
            TooltipFontSize = control.DataTooltip.FontSize;
            disableAnimations = control.DisableAnimations;
            LegendColor = ((SolidColorBrush)control.ChartLegend.Foreground).Color;

            LegendBullet = ((DefaultLegend)control.ChartLegend).BulletSize;
            TooltipBullet = ((DefaultTooltip)control.DataTooltip).BulletSize;

            if (control.SeriesColors != null)
                SeriesColors = control.SeriesColors;
            else
                SeriesColors = null;
        }



        private void Deserialize(PieChart control, DataStore data)
        {
            DeserializeDefault(control);
            control.LegendLocation = legend;
            control.InnerRadius = InnerRadius;
            control.Series = chdata.ToPieSeriesCollection();
            brush.Deserialize(control,data);

            if (effect != null)
                control.Effect = effect.CreateEffect();

            control.Hoverable = Hoverable;
            control.DataTooltip.Visibility = DataTooltipV;
            control.HoverPushOut = PushOut;
            control.ChartLegend.FontSize = LegendFontSize;
            control.DataTooltip.FontSize = TooltipFontSize;
            control.DisableAnimations = disableAnimations;
            control.ChartLegend.Foreground = new SolidColorBrush(LegendColor);

            ((DefaultLegend)control.ChartLegend).BulletSize = LegendBullet;
            ((DefaultTooltip)control.DataTooltip).BulletSize = TooltipBullet;

            if (SeriesColors != null)
                control.SeriesColors = SeriesColors;
            else
                control.SeriesColors = null;
        }


        public PieChart CreateControl(DataStore data)
        {
            PieChart control = new PieChart();
            Deserialize(control, data);
            return control;
        }

    }
}
