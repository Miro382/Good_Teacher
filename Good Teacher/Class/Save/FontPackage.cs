namespace Good_Teacher.Class.Save
{
    public class FontPackage
    {
        public byte[] FontData;
        public string FontFamilyName = "";

        public FontPackage(byte[] data, string familyname)
        {
            FontData = data;
            FontFamilyName = familyname;
        }

    }
}
