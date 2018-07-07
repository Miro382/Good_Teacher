namespace Good_Teacher.Class.Save.Output
{
    public class CheckBoxInput : InputType
    {
        public bool Check;
        public string ID;

        public CheckBoxInput(string id, bool ischecked)
        {
            ID = id;
            Check = ischecked;
        }

        public int Type()
        {
            return 1;
        }

        public string GetID()
        {
            return ID;
        }
    }
}
