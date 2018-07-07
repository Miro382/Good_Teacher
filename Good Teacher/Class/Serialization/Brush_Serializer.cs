using System.Windows.Controls;
using System.Windows.Media;
using Good_Teacher.Class.Save;

namespace Good_Teacher.Class.Serialization
{
    public class Brush_Serializer
    {
        public bool isImage = false;
        public string BrushS = "";
        public DesignSave designSave = new DesignSave();
        public BitmapScalingMode ScaleQuality = BitmapScalingMode.Linear;

        public Brush_Serializer()
        {

        }


        public Brush_Serializer(SolidColorBrush brush)
        {
            BrushS = SaveEditor.XMLSerialize(brush);
            isImage = false;
        }

        public Brush_Serializer(Control element,Brush brush, DataStore data)
        {
            Serialize(element,brush,data);
        }


        public void Serialize(Control element,Brush brush, DataStore data )
        {
            if (brush != null)
            {
                if(brush is ImageBrush)
                {
                    if (element.Tag != null)
                    {
                        DesignSave ds = DesignSave.Deserialize(element.Tag.ToString());
                        ds.Background.stretch = ((ImageBrush)brush).Stretch;
                        designSave = ds;
                        isImage = true;
                        ScaleQuality = RenderOptions.GetBitmapScalingMode(element);
                        ds.SerializeTransform(brush, ds.Background);
                    }
                }
                else
                {
                    BrushS = SaveEditor.XMLSerialize(brush);
                    isImage = false;
                }
            }
        }


        public void SerializeWithKey(Brush brush, DataStore data, string key)
        {
            if (brush != null)
            {
                if (brush is ImageBrush)
                {
                    designSave = new DesignSave( new ImageRepresentation(key, ((ImageBrush)brush).Stretch));

                    isImage = true;
                    designSave.SerializeTransform(brush, designSave.Background);
                }
                else
                {
                    BrushS = SaveEditor.XMLSerialize(brush);
                    isImage = false;
                }
            }
        }

        public void SerializeQuality(Control element)
        {
            ScaleQuality = RenderOptions.GetBitmapScalingMode(element);
        }

        public void DeserializeQuality(Control element)
        {
            if (ScaleQuality != BitmapScalingMode.Linear && ScaleQuality != BitmapScalingMode.Unspecified)
                RenderOptions.SetBitmapScalingMode(element, ScaleQuality);
        }

        public void Deserialize(Control element, DataStore data)
        {
            if (isImage)
            {
                designSave.ToControl(data, element);
                element.Tag = designSave.Serialize();

                if(ScaleQuality != BitmapScalingMode.Linear && ScaleQuality != BitmapScalingMode.Unspecified)
                RenderOptions.SetBitmapScalingMode(element, ScaleQuality);
            }
            else
            {
                if(!string.IsNullOrWhiteSpace(BrushS))
                element.Background = (Brush)SaveEditor.XMLDeserialize(BrushS);
            }
            
        }


        public Stretch GetStretch()
        {
            return designSave.Background.stretch;
        }


        public Brush DeserializeToBrushWithKey(DataStore data,out string key)
        {
            key = "";

            if (isImage)
            {
                key = designSave.Background.Path;

                return designSave.ToImageBrush(data);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(BrushS))
                    return (Brush)SaveEditor.XMLDeserialize(BrushS);
            }

            return new SolidColorBrush(Colors.Gray);
        }

        public Brush DeserializeToBrushWithKey(DataStore data)
        {
            if (isImage)
            {
                return designSave.ToImageBrush(data);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(BrushS))
                    return (Brush)SaveEditor.XMLDeserialize(BrushS);
            }

            return new SolidColorBrush(Colors.Gray);
        }


        public Brush DeserializeToBrush(Control element, DataStore data)
        {
            if (isImage)
            {
                if (ScaleQuality != BitmapScalingMode.Linear && ScaleQuality != BitmapScalingMode.Unspecified)
                    RenderOptions.SetBitmapScalingMode(element, ScaleQuality);

                element.Tag = designSave.Serialize();

                return designSave.ToImageBrush(data);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(BrushS))
                    return (Brush)SaveEditor.XMLDeserialize(BrushS);
            }

            return new SolidColorBrush(Colors.Gray);
        }



    }
}
