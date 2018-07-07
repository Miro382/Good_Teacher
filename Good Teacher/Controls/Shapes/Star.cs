﻿using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Controls
{
    public class Star : Shape
    {
        public Star() : base()
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
            return Geometry.Parse("F1 M 145.637,174.227L 127.619,110.39L 180.809,70.7577L 114.528,68.1664L 93.2725,5.33333L" +
                " 70.3262,67.569L 4,68.3681L 56.0988,109.423L 36.3629,172.75L 91.508,135.888L 145.637,174.227 Z");
        }

    }
}
