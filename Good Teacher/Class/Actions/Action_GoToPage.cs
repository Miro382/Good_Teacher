using Good_Teacher.Class.Enumerators;

namespace Good_Teacher.Class.Actions
{
    public class Action_GoToPage : IActions
    {
        public int ToPage = 0;

        public bool ToSpecific = true,Next = false, Previous = false;

        public int DoAction()
        {
            return 1;
        }

        public ActionType.Action_Type GetActionType()
        {
            return ActionType.Action_Type.GoToPage;
        }

        public void SetValues(int toPage,bool toSpecific,bool next, bool previous)
        {
            ToPage = toPage;
            ToSpecific = toSpecific;
            Next = next;
            Previous = previous;
        }

    }
}
