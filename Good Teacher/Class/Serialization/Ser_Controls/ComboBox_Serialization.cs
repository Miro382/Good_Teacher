using Good_Teacher.Class.Serialization.Content_Ser;
using Good_Teacher.Controls;
using System.Collections.Generic;
using System.Windows;

namespace Good_Teacher.Class.Serialization.Ser_Controls
{
    public class ComboBox_Serialization : ControlSerializer
    {
        public List<ContentCreator> Contents = new List<ContentCreator>();
        public HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
        public VerticalAlignment verticalAlignment = VerticalAlignment.Center;

        public ComboBox_Serialization()
        {

        }

        public ComboBox_Serialization(ComboBox_Control control, DataStore data)
        {
            Serialize(control, data);
        }

        public void Serialize(ComboBox_Control control, DataStore data)
        {
            SerializeDefault(control);
            Contents = control.contents;
            horizontalAlignment = control.combobox.HorizontalContentAlignment;
            verticalAlignment = control.combobox.VerticalContentAlignment;
        }


        public void Deserialize(ComboBox_Control control, DataStore data)
        {
            DeserializeDefault(control);
            control.contents = Contents;
            control.Create(data);
            control.combobox.HorizontalContentAlignment = horizontalAlignment;
            control.combobox.VerticalContentAlignment = verticalAlignment;
        }


        public ComboBox_Control CreateControl(DataStore data)
        {
            ComboBox_Control control = new ComboBox_Control();

            Deserialize(control, data);

            return control;
        }


    }
}
