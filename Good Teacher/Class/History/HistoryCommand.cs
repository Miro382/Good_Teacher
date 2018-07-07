using System.Windows.Controls;

namespace Good_Teacher.Class.History
{
    public interface HistoryCommand
    {
        void DoUndoAction(Canvas canvas, DataStore data);
        void DoRedoAction(Canvas canvas, DataStore data);
    }
}
