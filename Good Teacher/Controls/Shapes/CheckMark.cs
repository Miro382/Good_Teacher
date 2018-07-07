using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Controls.Shapes
{
    public class CheckMark : Shape
    {
        public CheckMark() : base()
        {
            this.Stretch = System.Windows.Media.Stretch.Fill;
        }

        protected override Geometry DefiningGeometry
        {
            get { return GetGeometry(); }
        }

        private Geometry GetGeometry()
        {
            return Geometry.Parse("m 9.5250005 287.47496 -3.8805557 3.88056 -2.4694446 -2.46944 -1.0583334 1.05835 3.5291668 3.52776 4.9375004 -4.93888 z");
        }
    }
}
