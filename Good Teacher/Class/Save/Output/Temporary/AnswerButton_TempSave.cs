namespace Good_Teacher.Class.Save.Output.Temporary
{
    public class AnswerButton_TempSave : Temporary_Save
    {
        public int ID;
        public bool Answered;

        public AnswerButton_TempSave(int id, bool answered)
        {
            ID = id;
            Answered = answered;
        }

        public int GetOwnedControl()
        {
            return ID;
        }
    }
}
