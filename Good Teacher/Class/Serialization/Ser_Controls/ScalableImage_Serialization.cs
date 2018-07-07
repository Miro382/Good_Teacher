using Good_Teacher.Controls;

namespace Good_Teacher.Class.Serialization.Ser_Controls
{
    public class ScalableImage_Serialization : ControlSerializer
    {
        public string ImageKey = "";
        public double SliderZoom = 100;
        public Brush_Serializer CPbrush = new Brush_Serializer();

        public ScalableImage_Serialization()
        {

        }


        public ScalableImage_Serialization(ScalableImage image, DataStore data)
        {
            Serialize(image,data);
        }

        public void Serialize(ScalableImage control, DataStore data)
        {
            SerializeDefault(control);
            ImageKey = control.ImageKey;
            SliderZoom = control.SliderZoom.Value;

            CPbrush.SerializeWithKey(control.ControlPanelBack, data, control.PathToCPImage);
            CPbrush.SerializeQuality(control);
        }


        public void Deserialize(ScalableImage control, DataStore data)
        {
            DeserializeDefault(control);
            control.ImageKey = ImageKey;
            control.M_Img.Source = data.archive.GetImage(ImageKey);
            data.archive.GetImageSize(ImageKey, out control.DefaultW, out control.DefaultH);
            control.M_Img.Width = control.DefaultW;
            control.M_Img.Height = control.DefaultH;
            control.SliderZoom.Value = SliderZoom;

            string ikey = "";

            control.ControlPanelBack = CPbrush.DeserializeToBrushWithKey(data, out ikey);
            control.PathToCPImage = ikey;
            control.CPStretch = CPbrush.GetStretch();

            CPbrush.DeserializeQuality(control);
        }


        public ScalableImage CreateControl(DataStore data)
        {
            ScalableImage control = new ScalableImage();

            Deserialize(control,data);

            return control;
        }

    }
}
