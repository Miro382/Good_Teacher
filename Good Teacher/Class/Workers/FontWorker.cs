using Good_Teacher.Class.Save;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;

namespace Good_Teacher.Class.Workers
{
    public static class FontWorker
    {

        public static void LoadFonts(DataStore data)
        {
            try
            {
                /*
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\"))
                {
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\", true);
                }
                */

                if (data.FontManager.Count > 0)
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\");
                    foreach (FontPackage fontp in data.FontManager)
                    {

                        if(!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\" + fontp.FontFamilyName + ".ttf"))
                        File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\" + fontp.FontFamilyName + ".ttf", fontp.FontData);
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine("Failed load fonts: " + ex);
            }
        }


        public static void RemoveTemporaryFolder()
        {
            try
            {

                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\"))
                {
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\", true);
                }

            }catch(Exception ex)
            {
                Debug.WriteLine("Failed load fonts: " + ex);
            }
        }

        public static bool GetFontFamily(FontFamily family, out FontFamily Rfamily)
        {
            Rfamily = null;
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\"))
            {
                string pathF = family.Source.ToString();
                //Debug.WriteLine(pathF);

                int indexof = pathF.IndexOf(".ttf#");

                if (indexof > 0)
                {
                    pathF = pathF.Substring(indexof+2);
                }

                //Debug.WriteLine(pathF);

                if (pathF[2] == '#')
                {
                    pathF = pathF.Substring(3);
                }
                else
                {
                    //Debug.WriteLine("---RETURN False");
                    return false;
                }

                //Debug.WriteLine("FINAL: "+pathF);


                foreach (FontFamily fam in Fonts.GetFontFamilies(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\" + pathF + ".ttf"))
                {
                    Rfamily = fam;
                    //Debug.WriteLine("FOUND: "+Rfamily);
                    return true;
                }
            }


            Rfamily = new FontFamily("Segoe UI");
            return true;
        }




        public static bool GetFontFamilyByString(string family, out FontFamily Rfamily)
        {
            Rfamily = null;
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\"))
            {

                foreach (FontFamily fam in Fonts.GetFontFamilies(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\" + family + ".ttf"))
                {
                    Rfamily = fam;
                    //Debug.WriteLine("FOUND: "+Rfamily);
                    return true;
                }
            }

            return false;
        }



    }
}
