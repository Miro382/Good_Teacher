using Good_Teacher.Class.Actions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Good_Teacher.Class.Special
{
    public class TimerAction
    {
        /// <summary>
        /// Default time in sec
        /// </summary>
        public float DefaultTime = 0;

        //Actual time is only for decreasing default time (DefaultTime * 10)
        [JsonIgnore]
        public float ActualTime = 0;

        //Count down?
        [JsonIgnore]
        public bool Stop = true;

        public List<IActions> Actions = new List<IActions>();

        public TimerAction()
        {

        }

        public TimerAction(float defaultTime, List<IActions> actions)
        {
            DefaultTime = defaultTime;
            Actions = new List<IActions>(actions);
        }

        public void SetActualTime()
        {
            ActualTime = DefaultTime;
        }
    }
}
