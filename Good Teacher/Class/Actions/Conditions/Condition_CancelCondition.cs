using Good_Teacher.Class.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_Teacher.Class.Actions.Conditions
{
    class Condition_CancelCondition : IActions
    {
        public int DoAction()
        {
            return 2;
        }

        public ActionType.Action_Type GetActionType()
        {
            return ActionType.Action_Type.CONDITION_CancelCondition;
        }

        public bool IsCondition()
        {
            return true;
        }
    }
}
