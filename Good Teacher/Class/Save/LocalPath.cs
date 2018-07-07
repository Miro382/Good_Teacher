using System;
using System.Diagnostics;
using System.IO;

namespace Good_Teacher.Class.Save
{
    public static class LocalPath
    {
        //Miroslav Murin

        /// <summary>
        /// Returns path to directory of current open file
        /// </summary>
        /// <param name="path">Out paramater - path</param>
        /// <returns>Bool - True if success</returns>
        public static bool GetDirectoryPath(out string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(MainWindow.pathtofile))
                {
                    path = "";
                    return false;
                }
                else
                {
                    path = Path.GetDirectoryName(MainWindow.pathtofile);
                    return true;
                }
            }catch(Exception ex)
            {
                Debug.WriteLine("Cant create local path! : "+ex);
                path = "";
                return false;
            }
        }


        public static string GetResourcesPath()
        {
            try
            {
               return Path.Combine(Path.GetDirectoryName(MainWindow.pathtofile),"Resources");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cant create local path! : " + ex);
                return "";
            }
        }

        /// <summary>
        /// Returns combined path with path to directory of current open file
        /// </summary>
        /// <param name="path">File or directory to combine with directory of current open file</param>
        /// <returns>String - Combined Path</returns>
        public static string CombinePath(string path)
        {
            string cpath = "";
            if (GetDirectoryPath(out cpath))
            {
                return Path.Combine(cpath, path);
            }
            else
                return "";
        }


        public static void CopyDirectory(string olddir, string newdir)
        {
            Directory.CreateDirectory(newdir);
            Debug.WriteLine("OLDDIR: "+olddir+"   NEWDIR: "+newdir);

            //Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(olddir, "*",
                SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(olddir, newdir));
            }

            //Copy all the files
            foreach (string newPath in Directory.GetFiles(olddir, "*.*",
                SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(olddir, newdir), true);
            }
        }


        public static void CopyDirectoryToResources(string SourceDir)
        {
            Directory.CreateDirectory(CombinePath("Resources\\"));
            CopyDirectory(SourceDir,CombinePath("Resources\\" + Path.GetFileName(SourceDir)));
        }


        public static void CopyFileToResourcesMedia(string SourceFile)
        {
            Directory.CreateDirectory(CombinePath("Resources\\Media\\"));
            File.Copy(SourceFile, CombinePath("Resources\\Media\\" + Path.GetFileName(SourceFile)));
        }


    }
}
