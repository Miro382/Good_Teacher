namespace Good_Teacher.Class.Workers
{
    public class SizeFormatWorker
    {

        private struct PixelUnitFactor
        {
            public const double Px = 1.0;
            public const double Inch = 96.0;
            public const double Cm = 37.7952755905512;
            public const double Pt = 1.33333333333333;
        }


        //CM
        public static double CmToPx(double cm)
        {
            return cm * PixelUnitFactor.Cm;
        }

        public static double PxToCm(double px)
        {
            return px / PixelUnitFactor.Cm;
        }



        //IN
        public static double PxToIn(double px)
        {
            return px / PixelUnitFactor.Inch;
        }

        public static double InToPx(double inches)
        {
            return inches * PixelUnitFactor.Inch;
        }



        //PT
        public static double PxToPt(double px)
        {
            return px / PixelUnitFactor.Pt;
        }

        public static double PtToPx(double Points)
        {
            return Points * PixelUnitFactor.Pt;
        }

    }
}
