using Good_Teacher.Controls;

namespace Good_Teacher.Class.Serialization.Ser_Controls
{
    public class MediaPlayerController_Serialization : ControlSerializer
    {

        public string MediaURL = "";
        public bool autoplay = false;
        public int volume = 50;
        public Brush_Serializer CPbrush = new Brush_Serializer();

        public MediaPlayerController_Serialization()
        {

        }

        public MediaPlayerController_Serialization(MediaPlayerController_Control cont, DataStore data)
        {
            Serialize(cont, data);
        }


        public void Serialize(MediaPlayerController_Control control, DataStore data)
        {
            if (control.Tag != null)
            {
                MediaURL = control.Tag.ToString();
            }

            autoplay = control.Autoplay;
            volume = control.Volume;

            SerializeDefault(control);

            CPbrush.SerializeWithKey(control.G_ControlPanel.Background, data, control.PathToCPImage);
            CPbrush.SerializeQuality(control);
        }


        public void Deserialize(MediaPlayerController_Control control, DataStore data)
        {
            DeserializeDefault(control);
            control.Tag = MediaURL;
            control.Autoplay = autoplay;
            control.Volume = volume;

            string ikey = "";

            control.G_ControlPanel.Background = CPbrush.DeserializeToBrushWithKey(data,out ikey);
            control.PathToCPImage = ikey;
            control.CPStretch = CPbrush.GetStretch();

            CPbrush.DeserializeQuality(control);
        }


        public MediaPlayerController_Control CreateControl(DataStore data)
        {
            MediaPlayerController_Control control = new MediaPlayerController_Control();

            Deserialize(control, data);

            return control;
        }

    }
}
