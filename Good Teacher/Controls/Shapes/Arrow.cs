using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Controls.Shapes
{
    public class Arrow : Shape
    {
        public Arrow() : base()
        {
            this.Stretch = System.Windows.Media.Stretch.Fill;
        }

        protected override Geometry DefiningGeometry
        {
            get { return GetGeometry(); }
        }

        private Geometry GetGeometry()
        {
            return Geometry.Parse("m 109.74845 108.42439 c 0 0 29.26247 25.71545 32.51383 28.08009 3.25137 2.66022 0.88702 6.79834 0.88702 6.79834 0 0 -28.67127 26.01104 -33.69611 28.96684 -4.72936 2.9558 -5.61604 -2.66022 -5.61604 -2.66022 v -14.18783 l -35.469604 0 c -1.77348 0 -2.95587 -1.18233 -2.95587 -2.95581 v -23.6464 c 0 -1.77348 1.18239 -2.9558 2.95587 -2.9558 l 35.469604 0 v -13.89225 c 0 -7.09392 5.91152 -3.54696 5.91152 -3.54696 z");
        }
    }
}
