using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Controls.Shapes
{
    public class RightAngledTriangleSE : Shape
    {
        public RightAngledTriangleSE() : base()
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
            return Geometry.Parse("M 1,0 v 1 h -1 Z");
        }
    }
}
