using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Good_Teacher.Class.Enumerators;

namespace Good_Teacher.Class.Actions.Conditions
{
    class Condition_Else : IActions
    {
        public int DoAction()
        {
            return 1;
        }

        public ActionType.Action_Type GetActionType()
        {
            return ActionType.Action_Type.CONDITION_Else;
        }

        public bool IsCondition()
        {
            return true;
        }
    }
}
