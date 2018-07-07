using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Controls.Shapes
{
    public class Speech : Shape
    {
        public Speech() : base()
        {
            this.Stretch = System.Windows.Media.Stretch.Fill;
        }

        protected override Geometry DefiningGeometry
        {
            get { return GetGeometry(); }
        }

        private Geometry GetGeometry()
        {
            return Geometry.Parse("m 728 205 c -0.90731 0 -1.77431 -0.13563 -2.56994 -0.38249 -0.8123 0.63348 -2.18128 1.51359 -3.51746 1.51359 0.70009 -0.66264 0.89814 -1.94691 0.94162 -2.90236 C 721.70317 202.24859 721 200.93881 721 199.5 c 0 -3.03757 3.13401 -5.5 7 -5.5 3.86599 0 7 2.46243 7 5.5 0 3.03757 -3.13401 5.5 -7 5.5 z m 0 0");
        }
    }
}
