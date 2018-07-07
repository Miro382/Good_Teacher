using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Good_Teacher.Class.Animations
{
    public class Animation_Opacity : IAnimation
    {
        public int id { get; set; }
        public double opacity { get; set; } = 0;
        public double Time { get; set; } = 0;
        public double FromSec { get; set; } = 0;
        public bool Repeat { get; set; } = false;
        public bool Reverse { get; set; } = false;

        public Animation_Opacity(int ControlID,double Opacity, double time, double fromSec, bool repeat, bool reverse)
        {
            id = ControlID;
            opacity = Opacity;
            Time = time;
            FromSec = fromSec;
            Repeat = repeat;
            Reverse = reverse;
        }

        public int GetID()
        {
            return id;
        }

        public void MakeAnimation(FrameworkElement elm)
        {
            DoubleAnimation Anim = new DoubleAnimation(elm.Opacity, opacity, new Duration(TimeSpan.FromSeconds(Time)));

            if (Repeat)
                Anim.RepeatBehavior = RepeatBehavior.Forever;

            Anim.AutoReverse = Reverse;

            Anim.BeginTime = TimeSpan.FromSeconds(FromSec);
            elm.BeginAnimation(UIElement.OpacityProperty, Anim);
        }
    }
}
