using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Controls
{
    public class Hexagon : Shape
    {
        public Hexagon() : base()
        {
            this.Stretch = System.Windows.Media.Stretch.Fill;
        }

        protected override Geometry DefiningGeometry
        {
            get { return GetGeometry(); }
        }

        private Geometry GetGeometry()
        {
            double sideLength = 100;
            double xd = Math.Sqrt(sideLength * sideLength / 2);
            string x = (""+xd).Replace(",",".");
            return Geometry.Parse(String.Format("M {0},0 h {1} l {0},{0} l -{0},{0} h -{1} l -{0},-{0} Z", x, sideLength));
        }
    }
}
