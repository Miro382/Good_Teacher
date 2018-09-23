using System;
using Good_Teacher.Class.Enumerators;
using System.Diagnostics;

namespace Good_Teacher.Class.Actions
{
    public class Action_OpenWeb : IActions
    {
        public string Url = "";

        public int DoAction()
        {
            try
            {
                System.Diagnostics.Process.Start(Url);
                return 0;

            }catch(Exception ex)
            {
                Debug.WriteLine(""+ex);
                return 0;
            }
        }

        public ActionType.Action_Type GetActionType()
        {
            return ActionType.Action_Type.OpenWeb;
        }

        public bool IsCondition()
        {
            return false;
        }
    }
}
