using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Controls.Shapes
{
    public class Ribbon : Shape
    {
        public Ribbon() : base()
        {
            this.Stretch = System.Windows.Media.Stretch.Fill;
        }

        protected override Geometry DefiningGeometry
        {
            get { return GetGeometry(); }
        }

        private Geometry GetGeometry()
        {
            return Geometry.Parse("m 143.0073 199.63333 -36.79599 -29.19231 -36.795986 29.19231 v -91.74737 c 0 -4.60815 4.116509 -8.340728 9.19904 -8.340728 h 55.193896 c 5.08235 0 9.19904 3.732578 9.19904 8.340728 z");
        }
    }
}
