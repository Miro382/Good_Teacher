using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Controls.Shapes
{
    public class Cross : Shape
    {
        public Cross() : base()
        {
            this.Stretch = System.Windows.Media.Stretch.Fill;
        }

        protected override Geometry DefiningGeometry
        {
            get { return GetGeometry(); }
        }

        private Geometry GetGeometry()
        {
            return Geometry.Parse("m 3.8805557 287.1222 -1.0583334 1.05834 2.4694445 2.46944 -2.4694445 2.46944 1.0583334 1.05834 2.4694445 -2.46945 2.4694445 2.46945 1.0583334 -1.05834 -2.4694445 -2.46944 2.4694445 -2.46944 -1.0583334 -1.05834 -2.4694445 2.46945 z");
        }
    }
}
