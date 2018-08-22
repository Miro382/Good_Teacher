namespace Good_Teacher.Class.Save.Output
{
    public class ToggleButtonInput : InputType
    {
        public bool Check;
        public string ID;
        public string Text = "";

        public ToggleButtonInput(string id, string text, bool ischecked)
        {
            ID = id;
            Text = text;
            Check = ischecked;
        }

        public int Type()
        {
            return 3;
        }

        public string GetID()
        {
            return ID;
        }
    }
}
