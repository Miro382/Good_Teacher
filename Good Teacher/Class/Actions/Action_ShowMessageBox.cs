using Good_Teacher.Class.Enumerators;
using System.Windows;

namespace Good_Teacher.Class.Actions
{
    public class Action_ShowMessageBox : IActions
    {
        public string Text="", Title="";

        public int DoAction()
        {
            MessageBox.Show(Text, Title);
            return 0;
        }

        public ActionType.Action_Type GetActionType()
        {
            return ActionType.Action_Type.ShowMessageBox;
        }

        public bool IsCondition()
        {
            return false;
        }
    }
}
