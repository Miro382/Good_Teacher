namespace Good_Teacher.Class.Workers
{
    public class MathWorker
    {
        public static int CalcToOrLowerThan(double input, double ScaleToOrLowerThan)
        {
            int ratio = 1;

            double calci = input;

            //Debug.WriteLine("+++++++  Input: "+input+"  SCL: "+ScaleToOrLowerThan+"  ++++++++++++");
            while (calci>ScaleToOrLowerThan)
            {
                ratio++;
                calci = input;
                calci = calci / ratio;

                //Debug.WriteLine(calci + "   Ratio: " + (ratio));

                if (input < 1)
                    break;
            }

            if (ratio == 0)
                return 1;

            return ratio;
        }


    }
}
