using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_Teacher.Class.Actions
{
    public class ActionOrder
    {
        public int order = 0;
        public bool inElse = false;
        public bool ConditionTrue = true;

        public ActionOrder(int ord, bool inelse = false)
        {
            order = ord;
            inElse = inelse;
        }

    }
}
