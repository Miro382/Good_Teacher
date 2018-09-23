using Good_Teacher.Class.Enumerators;

namespace Good_Teacher.Class.Actions
{
    public class Action_SetVisibility : IActions
    {
        public SetVisibilityEnum.SetVisibilityValue VisibilityValue = SetVisibilityEnum.SetVisibilityValue.SetToInvisible;
        public int ID = 0;

        public int DoAction()
        {
            return 5;
        }

        public ActionType.Action_Type GetActionType()
        {
            return ActionType.Action_Type.SetVisibility;
        }

        public bool IsCondition()
        {
            return false;
        }
    }
}
