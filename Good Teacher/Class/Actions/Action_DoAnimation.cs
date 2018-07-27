using Good_Teacher.Class.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_Teacher.Class.Actions
{
    public class Action_DoAnimation : IActions
    {
        public int AnimationID = 0;

        public int DoAction()
        {
            return 6;
        }

        public ActionType.Action_Type GetActionType()
        {
            return ActionType.Action_Type.DoAnimation;
        }
    }
}
