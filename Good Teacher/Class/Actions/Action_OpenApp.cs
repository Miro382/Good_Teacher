using Good_Teacher.Class.Enumerators;
using Good_Teacher.Class.Save;
using System;
using System.Diagnostics;

namespace Good_Teacher.Class.Actions
{
    public class Action_OpenApp : IActions
    {
        public string AppPath = "";

        public int DoAction()
        {
            try
            {
                string path = "";
                if (LocalPath.GetDirectoryPath(out path))
                {
                    System.Diagnostics.Process.Start( path+"/"+ AppPath);
                }
                return 0;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                return 0;
            }
        }

        public ActionType.Action_Type GetActionType()
        {
            return ActionType.Action_Type.OpenApplication;
        }
    }
}
