namespace Good_Teacher.Class.Save.Output.Temporary
{
    public class ComboBox_TempSave : Temporary_Save
    {
        public int Selected, ID;

        public ComboBox_TempSave(int id, int selected)
        {
            Selected = selected;
            ID = id;
        }

        public int GetOwnedControl()
        {
            return ID;
        }
    }
}
