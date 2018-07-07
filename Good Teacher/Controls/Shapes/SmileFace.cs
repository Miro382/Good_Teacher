using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Controls.Shapes
{
    public class SmileFace : Shape
    {
        public SmileFace() : base()
        {
            this.Stretch = System.Windows.Media.Stretch.Fill;
        }

        protected override Geometry DefiningGeometry
        {
            get { return GetGeometry(); }
        }

        private Geometry GetGeometry()
        {
            return Geometry.Parse("M 16.5 29 C 23.40356 29 29 23.40356 29 16.5 29 9.5964403 23.40356 4 16.5 4 9.5964403 4 4 9.5964403 4 16.5 4 23.40356 9.5964403 29 16.5 29 Z M 12 14 c 0.552285 0 1 -0.447715 1 -1 0 -0.552285 -0.447715 -1 -1 -1 -0.552285 0 -1 0.447715 -1 1 0 0.552285 0.447715 1 1 1 z m 9 0 c 0.552285 0 1 -0.447715 1 -1 0 -0.552285 -0.447715 -1 -1 -1 -0.552285 0 -1 0.447715 -1 1 0 0.552285 0.447715 1 1 1 z m -4.518677 8 C 13 22 11 20 11 20 v 1 c 0 0 2 2 5.481323 2 C 19.962647 23 22 21 22 21 v -1 c 0 0 -2.037353 2 -5.518677 2 z");
        }
    }
}
