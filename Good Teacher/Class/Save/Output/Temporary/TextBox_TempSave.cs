namespace Good_Teacher.Class.Save.Output.Temporary
{
    public class TextBox_TempSave : Temporary_Save
    {
        public int ID;
        public string Text;

        public TextBox_TempSave(int id, string text)
        {
            ID = id;
            Text = text;
        }

        public int GetOwnedControl()
        {
            return ID;
        }
    }
}
