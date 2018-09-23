using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Good_Teacher.Class.Enumerators;

namespace Good_Teacher.Class.Actions.Conditions
{
    public class Condition_IsChecked : IActions
    {
        public int ID = 0;
        public bool CheckIfChecked = true;

        public int DoAction()
        {
            return 0;
        }

        public ActionType.Action_Type GetActionType()
        {
            return ActionType.Action_Type.CONDITION_IsChecked;
        }

        public bool IsCondition()
        {
            return true;
        }
    }
}
