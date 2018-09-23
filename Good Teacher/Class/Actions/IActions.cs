using Good_Teacher.Class.Enumerators;

namespace Good_Teacher.Class.Actions
{
    public interface IActions
    {
        bool IsCondition();
        int DoAction();
        ActionType.Action_Type GetActionType();
    }
}
