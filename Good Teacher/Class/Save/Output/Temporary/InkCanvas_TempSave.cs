using Good_Teacher.Controls;
using System.IO;
using System.Windows.Ink;

namespace Good_Teacher.Class.Save.Output.Temporary
{
    public class InkCanvas_TempSave : Temporary_Save
    {
        public int ID;
        public byte[] strokeCollection;

        public InkCanvas_TempSave(int id, StrokeCollection strokes)
        {
            ID = id;

            using (MemoryStream str = new MemoryStream())
            {
                strokes.Save(str, true);
                strokeCollection = str.ToArray();
            }
        }

        public void LoadStrokes(InkCanvas_Control control)
        {
            using (Stream stream = new MemoryStream(strokeCollection))
            {
                StrokeCollection strokes = new StrokeCollection(stream);
                control.inkCanvas.Strokes.Add(strokes);
            }
        }

        public int GetOwnedControl()
        {
            return ID;
        }
    }
}
