namespace Good_Teacher.Class.Save.Output
{
    public class RadioButtonInput : InputType
    {
        public bool Check;
        public string ID;
        public string RadioGroup;

        public RadioButtonInput(string id, bool ischecked, string group)
        {
            ID = id;
            Check = ischecked;
            RadioGroup = group;
        }

        public int Type()
        {
            return 2;
        }

        public string GetID()
        {
            return ID;
        }
    }
}
