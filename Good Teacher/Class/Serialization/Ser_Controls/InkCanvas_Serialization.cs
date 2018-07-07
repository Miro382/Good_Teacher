using Good_Teacher.Controls;
using System.IO;
using System.Windows.Ink;

namespace Good_Teacher.Class.Serialization.Ser_Controls
{
    public class InkCanvas_Serialization : ControlSerializer
    {
        public Brush_Serializer brush = new Brush_Serializer();
        public byte[] strokeCollection;
        public Brush_Serializer CPbrush = new Brush_Serializer();

        public InkCanvas_Serialization()
        {

        }

        public InkCanvas_Serialization(InkCanvas_Control control, DataStore data)
        {
            Serialize(control, data);
        }

        public void Serialize(InkCanvas_Control control, DataStore data)
        {
            SerializeDefault(control);
            brush.Serialize(control, control.inkCanvas.Background, data);

            using (MemoryStream str = new MemoryStream())
            {
                control.inkCanvas.Strokes.Save(str,true);
                strokeCollection = str.ToArray();
            }

            CPbrush.SerializeWithKey(control.ControlPanelBack, data, control.PathToCPImage);
            CPbrush.SerializeQuality(control);
        }


        public void Deserialize(InkCanvas_Control control, DataStore data)
        {
            DeserializeDefault(control);
            control.inkCanvas.Background = brush.DeserializeToBrush(control, data);
            using (Stream stream = new MemoryStream(strokeCollection))
            {
                StrokeCollection strokes = new StrokeCollection(stream);
                control.inkCanvas.Strokes.Add(strokes);
            }

            string ikey = "";

            control.ControlPanelBack = CPbrush.DeserializeToBrushWithKey(data, out ikey);
            control.PathToCPImage = ikey;
            control.CPStretch = CPbrush.GetStretch();

            CPbrush.DeserializeQuality(control);
        }


        public InkCanvas_Control CreateControl(DataStore data)
        {
            InkCanvas_Control control = new InkCanvas_Control();

            Deserialize(control, data);

            return control;
        }

    }
}
