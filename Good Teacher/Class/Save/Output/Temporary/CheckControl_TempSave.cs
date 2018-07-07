namespace Good_Teacher.Class.Save.Output.Temporary
{
    public class CheckControl_TempSave : Temporary_Save
    {
        public int ID;
        public bool IsChecked;

        public CheckControl_TempSave(int id, bool ischecked)
        {
            ID = id;
            IsChecked = ischecked;
        }

        public int GetOwnedControl()
        {
            return ID;
        }
    }
}
