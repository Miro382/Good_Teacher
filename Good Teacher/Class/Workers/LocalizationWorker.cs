namespace Good_Teacher.Class.Workers
{
    public class LocalizationWorker
    {

        public static string BoolToYesNo(bool booleanB)
        {
            if (booleanB)
                return Strings.ResStrings.Yes;
            else
                return Strings.ResStrings.No;
        }


        public static string BoolCheckedUnchecked(bool booleanB)
        {
            if (booleanB)
                return Strings.ResStrings.Checked;
            else
                return Strings.ResStrings.Unchecked;
        }
    }
}
