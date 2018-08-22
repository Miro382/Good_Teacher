using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher.Class.Enumerators
{
    public class MathSignEnum
    {
        public enum MathSignType
        {
            Equals = 0,
            Plus = 1,
            Minus = 2,
            Multiply = 3,
            Divide = 4
        }

        public static string GetSign(MathSignType sign)
        {
            switch(sign)
            {
                case MathSignType.Equals:
                    return "=";
                case MathSignType.Plus:
                    return "+";
                case MathSignType.Minus:
                    return "-";
                case MathSignType.Multiply:
                    return "*";
                case MathSignType.Divide:
                    return "/";
            }
            return "";
        }


        public static void SetPositionX(double value, MathSignType sign, FrameworkElement elm)
        {
            double setv = Canvas.GetLeft(elm);

            if(sign == MathSignType.Equals)
            {
                setv = value;

            }else if(sign == MathSignType.Plus)
            {
                setv += value;
            }
            else if (sign == MathSignType.Minus)
            {
                setv -= value;
            }
            else if (sign == MathSignType.Multiply)
            {
                setv *= value;
            }
            else
            {
                if (value != 0)
                {
                    setv /= value;
                }
            }

            Canvas.SetLeft(elm, setv);
        }


        public static void SetPositionY(double value, MathSignType sign, FrameworkElement elm)
        {
            double setv = Canvas.GetTop(elm);

            if (sign == MathSignType.Equals)
            {
                setv = value;

            }
            else if (sign == MathSignType.Plus)
            {
                setv += value;
            }
            else if (sign == MathSignType.Minus)
            {
                setv -= value;
            }
            else if (sign == MathSignType.Multiply)
            {
                setv *= value;
            }
            else
            {
                if (value != 0)
                {
                    setv /= value;
                }
            }

            Canvas.SetTop(elm, setv);
        }


    }
}
