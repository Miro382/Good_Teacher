namespace Good_Teacher.Class.Save.Output
{
    public class TextboxInput : InputType
    {
        public string InputText = "";
        public string ID = "";

        public TextboxInput(string id, string InputT)
        {
            ID = id;
            InputText = InputT;
        }

        public string GetID()
        {
            return ID;
        }

        public int Type()
        {
            return 0;
        }
    }
}
