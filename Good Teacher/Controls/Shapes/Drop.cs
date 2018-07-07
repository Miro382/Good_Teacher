using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Controls.Shapes
{
    public class Drop : Shape
    {
        public Drop()
        {
            this.Stretch = Stretch.Fill;
            this.StrokeLineJoin = PenLineJoin.Round;
        }

        protected override Geometry DefiningGeometry
        {
            get { return GetGeometry(); }
        }

        private Geometry GetGeometry()
        {
            return Geometry.Parse("m316.96 2.3125c-43.436 192.24-149.94 211.47-147.47 333.14 2.0572 101.38 75.676 139 139.81 139.97 70.842 1.0727 137.12-34.422 136.45-140.22-0.61856-97.905-96.708-141.08-128.78-332.89z");
        }
    }
}
