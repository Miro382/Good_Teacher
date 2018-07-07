using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Controls
{
    public class Heart : Shape
    {
        public Heart() : base()
        {
            this.Stretch = System.Windows.Media.Stretch.Fill;
        }

        protected override Geometry DefiningGeometry
        {
            get { return GetGeometry(); }
        }

        private Geometry GetGeometry()
        {
            return Geometry.Parse("m 120.28316 89.256709 c 0 -6.081015 -4.92987 -11.011798 -11.01134 -11.011798 -4.2913 0 -8.92937 3.159824 -11.375537 6.042135 -2.409977 -2.854156 -7.085136 -6.041679 -11.375989 -6.041679 -6.081455 0 -11.011782 4.929871 -11.011782 11.010886 0 11.503337 14.580486 20.708427 22.387771 27.288147 7.806827 -6.57928 22.386877 -15.72894 22.386877 -27.287691 z");
        }
    }
}
