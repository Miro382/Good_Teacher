using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Controls
{
    public class Triangle : Shape
    {
        public Triangle() : base()
        {
            this.Stretch = System.Windows.Media.Stretch.Fill;
            this.StrokeLineJoin = PenLineJoin.Round;
        }

        protected override Geometry DefiningGeometry
        {
            get { return GetGeometry(); }
        }

        private Geometry GetGeometry()
        {
            return Geometry.Parse("M 0,1 l 1,1 h -2 Z");
        }
    }
}
