using Good_Teacher.Class.Enumerators;

namespace Good_Teacher.Class.Actions
{
    class Action_ClosePresentation : IActions
    {
        public int DoAction()
        {
            return 2;
        }

        public ActionType.Action_Type GetActionType()
        {
            return ActionType.Action_Type.ClosePresentation;
        }

        public bool IsCondition()
        {
            return false;
        }
    }
}
