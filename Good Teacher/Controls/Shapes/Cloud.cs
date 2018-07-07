using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Controls
{
    public class Cloud : Shape
    {
        public Cloud() : base()
        {
            this.Stretch = System.Windows.Media.Stretch.Fill;
        }

        protected override Geometry DefiningGeometry
        {
            get { return GetGeometry(); }
        }

        private Geometry GetGeometry()
        {
            return Geometry.Parse("M 26.882863 15.36551 C 28.713291 16.108508 30 17.903581 30 20 c 0 2.755805 -2.238325 5 -4.99944 5 H 7.9994399 C 5.2324942 25 3 22.761424 3 20 3 17.949131 4.2396588 16.181608 6.0118966 15.411539 v 0 C 6.0040021 15.275367 6 15.138151 6 15 6 11.134007 9.1340066 8 13 8 15.612757 8 17.891182 9.4314488 19.093808 11.552882 19.820616 11.198716 20.637102 11 21.5 11 c 2.648655 0 4.860022 1.872249 5.382863 4.36551 z");
        }
    }
}
