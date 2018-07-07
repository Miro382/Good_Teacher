using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Controls
{
    public class Diamond : Shape
    {
        public Diamond() : base()
        {
            this.Stretch = System.Windows.Media.Stretch.Fill;
        }

        protected override Geometry DefiningGeometry
        {
            get { return GetGeometry(); }
        }

        private Geometry GetGeometry()
        {
            return Geometry.Parse("m 106.92946 21.436457 h 49.96846 l 14.02292 14.022916 -39.0922 39.092189 -38.893745 -38.893749 z");
        }
    }
}
