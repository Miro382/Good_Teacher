using System;
using System.Collections.Generic;

namespace Good_Teacher.Class.History
{
    public class EventList<T> : List<T>
    {
        public event EventHandler OnAdd;

        public void AddRaise(T item)
        {
            if (null != OnAdd)
            {
                OnAdd(this, null);
            }
            base.Add(item);
        }
    }
}
