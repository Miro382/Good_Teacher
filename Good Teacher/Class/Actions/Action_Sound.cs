using Good_Teacher.Class.Enumerators;

namespace Good_Teacher.Class.Actions
{
    public class Action_Sound : IActions
    {
        public bool Stop = false, PlayAgain = false, Repeat = true;
        public string PathToPlay = "";

        public Action_Sound(bool stop, bool playagain, bool repeat, string pathplay)
        {
            Stop = stop;
            Repeat = repeat;
            PathToPlay = pathplay;
            PlayAgain = playagain;
        }

        public int DoAction()
        {
            return 3;
        }

        public ActionType.Action_Type GetActionType()
        {
            return ActionType.Action_Type.Sound;
        }
    }
}
