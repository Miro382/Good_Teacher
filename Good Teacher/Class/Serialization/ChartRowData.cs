using System.Collections.Generic;
using System.Windows.Media;

namespace Good_Teacher.Class.Serialization
{
    public class ChartRowData
    {
        public List<double> ColumnsValues = new List<double>();
        public bool DataLabel = false;
        public double FontSize = 10;
        public string Title = "";
        public SolidColorBrush StrokeColor = new SolidColorBrush();
    }
}
