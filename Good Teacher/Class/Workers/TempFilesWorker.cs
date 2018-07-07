using System;
using System.Diagnostics;
using System.IO;

namespace Good_Teacher.Class.Workers
{
    public class TempFilesWorker
    {

        public static void RemoveUpdateFolder()
        {
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Update\\"))
            {
                try
                {
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Update\\", true);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        public static bool CheckVersion()
        {
            try
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\CheckVersion.data"))
                {
                    string dayT = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\CheckVersion.data");

                    int day = int.Parse(dayT);

                    /*
                    if ( (day+2) < DateTime.Now.DayOfYear)
                        return true;

                    if ( (day-1) > DateTime.Now.DayOfYear)
                        return true;
                        */

                    if (day != DateTime.Now.DayOfYear)
                        return true;

                    return false;
                }

                return true;

            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return true;
            }
        }


        public static void WriteCurrentDay()
        {
            try
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\CheckVersion.data"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\CheckVersion.data");
                }

                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\CheckVersion.data","" +DateTime.Now.DayOfYear);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}
