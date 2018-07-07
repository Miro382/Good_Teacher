using LiveCharts.Wpf;
using System.Windows.Media;

namespace Good_Teacher.Class.Serialization
{
    public class AxisSerializer
    {

        public Brush AxisX, AxisY;
        public string TitleX = "", TitleY = "";

        public AxisSerializer()
        {

        }


        public AxisSerializer(CartesianChart chart)
        {
            Serialize(chart);
        }


        public void Serialize(CartesianChart S_chart)
        {
            if (S_chart.AxisX.Count > 0)
            {
                AxisX = S_chart.AxisX[0].Foreground;
                TitleX = S_chart.AxisX[0].Title;
            }
            else

                AxisX = new SolidColorBrush(Colors.Black);

            if (S_chart.AxisY.Count > 0)
            {
                AxisY = S_chart.AxisY[0].Foreground;
                TitleY = S_chart.AxisY[0].Title;
            }
            else
                AxisY = new SolidColorBrush(Colors.Black);
        }



        public void Deserialize(CartesianChart D_chart)
        {
            if (D_chart.AxisX.Count < 1)
                D_chart.AxisX.Add(new Axis());

            if (D_chart.AxisY.Count < 1)
                D_chart.AxisY.Add(new Axis());


            D_chart.AxisX[0].Foreground = AxisX;
            D_chart.AxisX[0].Separator.Stroke = AxisX;
            D_chart.AxisX[0].Title = TitleX;

            D_chart.AxisY[0].Foreground = AxisY;
            D_chart.AxisY[0].Separator.Stroke = AxisY;
            D_chart.AxisY[0].Title = TitleY;
        }


    }
}
