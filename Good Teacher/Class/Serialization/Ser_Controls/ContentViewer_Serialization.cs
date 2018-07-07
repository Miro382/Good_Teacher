using Good_Teacher.Class.Serialization.Content_Ser;
using Good_Teacher.Controls;
using System.Windows;

namespace Good_Teacher.Class.Serialization.Ser_Controls
{
    public class ContentViewer_Serialization : ControlSerializer
    {
        public ContentCreator content = new ContentCreator();
        public Brush_Serializer brush = new Brush_Serializer();
        public HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
        public VerticalAlignment verticalAlignment = VerticalAlignment.Center;


        public ContentViewer_Serialization()
        {

        }

        public ContentViewer_Serialization(ContentViewer control, DataStore data)
        {
            Serialize(control, data);
        }

        public void Serialize(ContentViewer control, DataStore data)
        {
            SerializeDefault(control);
            brush.Serialize(control, control.Background, data);
            content = control.contentCreator;
            horizontalAlignment = control.HorizontalContentAlignment;
            verticalAlignment = control.VerticalContentAlignment;
        }


        public void Deserialize(ContentViewer control, DataStore data)
        {
            DeserializeDefault(control);
            control.Background = brush.DeserializeToBrush(control, data);
            control.contentCreator = content;
            control.HorizontalContentAlignment = horizontalAlignment;
            control.VerticalContentAlignment = verticalAlignment;
            control.Content = control.contentCreator.Create(data);
        }


        public ContentViewer CreateControl(DataStore data)
        {
            ContentViewer control = new ContentViewer();

            Deserialize(control, data);

            return control;
        }

    }
}
