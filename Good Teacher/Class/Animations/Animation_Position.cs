using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Good_Teacher.Class.Animations
{
    public class Animation_Position : IAnimation
    {
        public bool MX { get; set; } = false;
        public bool MY { get; set; } = false;
        public double ToX { get; set; }
        public double ToY { get; set; }
        public double TimeX { get; set; }
        public double TimeY { get; set; }
        public int id { get; set; }
        public double accX { get; set; } = 0;
        public double decX { get; set; } = 0;
        public double accY { get; set; } = 0;
        public double decY { get; set; } = 0;
        public double BTimeX { get; set; } = 0;
        public double BTimeY { get; set; } = 0;
        public bool Repeat { get; set; } = false;
        public bool Reverse { get; set; } = false;

        public Animation_Position()
        {

        }

        public Animation_Position(int ControlID,bool mx, bool my, double tox, double toy,
            double durationx, double durationy, double acx, double acy, double dcx, double dcy, double btx, double bty,bool repeat, bool reverse)
        {
            id = ControlID;
            MX = mx;
            MY = my;
            ToX = tox;
            ToY = toy;
            TimeX = durationx;
            TimeY = durationy;
            accX = acx;
            decX = dcx;
            accY = acy;
            decY = dcy;
            BTimeX = btx;
            BTimeY = bty;
            Repeat = repeat;
            Reverse = reverse;
        }

        public int GetID()
        {
            return id;
        }

        public void MakeAnimation(FrameworkElement elm)
        {
            if (MX)
            {
                DoubleAnimation moveAnim = new DoubleAnimation(Canvas.GetLeft(elm), ToX, new Duration(TimeSpan.FromSeconds(TimeX)));
                if ((accX + decX) >= 1)
                {
                    moveAnim.AccelerationRatio = 0.5;
                    moveAnim.DecelerationRatio = 0.5;
                }
                else
                {
                    moveAnim.AccelerationRatio = accX;
                    moveAnim.DecelerationRatio = decX;
                }
                moveAnim.BeginTime = TimeSpan.FromSeconds(BTimeX);

                if(Repeat)
                moveAnim.RepeatBehavior = RepeatBehavior.Forever;

                moveAnim.AutoReverse = Reverse;

                elm.BeginAnimation(Canvas.LeftProperty, moveAnim);
            }
            if (MY)
            {
                DoubleAnimation moveAnim = new DoubleAnimation(Canvas.GetTop(elm), ToY, new Duration(TimeSpan.FromSeconds(TimeY)));
                if ((accY + decY) >= 1)
                {
                    moveAnim.AccelerationRatio = 0.5;
                    moveAnim.DecelerationRatio = 0.5;
                }
                else
                {
                    moveAnim.AccelerationRatio = accY;
                    moveAnim.DecelerationRatio = decY;
                }
                moveAnim.BeginTime = TimeSpan.FromSeconds(BTimeY);
                if (Repeat)
                    moveAnim.RepeatBehavior = RepeatBehavior.Forever;

                moveAnim.AutoReverse = Reverse;

                elm.BeginAnimation(Canvas.TopProperty, moveAnim);
            }
        }

    }
}
