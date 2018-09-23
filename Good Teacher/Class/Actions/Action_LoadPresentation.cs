using Good_Teacher.Class.Enumerators;

namespace Good_Teacher.Class.Actions
{
    public class Action_LoadPresentation : IActions
    {
        public string PresentationPath = "";

        public int DoAction()
        {
            return 4;
        }

        public ActionType.Action_Type GetActionType()
        {
            return ActionType.Action_Type.LoadPresentation;
        }

        public bool IsCondition()
        {
            return false;
        }
    }
}
