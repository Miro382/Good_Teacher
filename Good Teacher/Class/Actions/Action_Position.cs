using Good_Teacher.Class.Enumerators;

namespace Good_Teacher.Class.Actions
{
    public class Action_Position : IActions
    {
        public int ID = 0;
        public bool ChangeX = false, ChangeY = false;
        public double CX = 0, CY = 0;
        public MathSignEnum.MathSignType SignX = MathSignEnum.MathSignType.Equals;
        public MathSignEnum.MathSignType SignY = MathSignEnum.MathSignType.Equals;

        public int DoAction()
        {
            return 7;
        }

        public ActionType.Action_Type GetActionType()
        {
            return ActionType.Action_Type.Position;
        }

        public bool IsCondition()
        {
            return false;
        }
    }
}
