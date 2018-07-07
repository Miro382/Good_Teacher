using Good_Teacher.Class.Controls;
using Good_Teacher.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Class.Serialization.Ser_Controls
{
    class Gallery_Serialization : ControlSerializer
    {

        public Brush_Serializer brush = new Brush_Serializer();
        public List<GalleryImage> images = new List<GalleryImage>();
        public Font_Serializer font_Serializer = new Font_Serializer();
        public bool defaultImages = false;
        public double TimeToTranslate = 0.5;
        public uint Time = 3;
        public Visibility Circle = Visibility.Visible, Description = Visibility.Visible, ControlV = Visibility.Visible;
        public Stretch istretch = Stretch.UniformToFill;

        public Gallery_Serialization()
        {

        }

        public Gallery_Serialization(Gallery control, DataStore data)
        {
            Serialize(control, data);
        }

        public void Serialize(Gallery control, DataStore data)
        {
            SerializeDefault(control);
            brush.SerializeWithKey(control.Foreground, data, control.ForegroundKey);

            font_Serializer.Serialize(control);

            if(control.Tag != null)
            {
                if (control.Tag.ToString() == "D")
                    defaultImages = true;
            }
            else
            images = control.images.ToList();

            TimeToTranslate = control.TimeToTranslate;
            Time = control.Time;

            Circle = control.CircleItems.Visibility;
            Description = control.Description.Visibility;
            ControlV = control.Left.Visibility;

            istretch = control.GetStretch();
        }


        public void Deserialize(Gallery control, DataStore data)
        {
            DeserializeDefault(control);
            string fkey = "";
            control.Foreground = brush.DeserializeToBrushWithKey(data,out fkey);

            control.ForegroundKey = fkey;

            font_Serializer.Deserialize(control);

            control.Time = Time;
            control.TimeToTranslate = TimeToTranslate;

            control.CircleItems.Visibility = Circle;
            control.Description.Visibility = Description;

            control.Left.Visibility = ControlV;
            control.Right.Visibility = ControlV;

            control.SetStretch(istretch);

            if (!defaultImages)
            {
                control.images = images.ToList();
                control.Tag = null;
                control.LoadImageSources(data);
                control.RefreshAndUpdate();
            }
            else
            {
                control.Tag = "D";
                control.AddGalleryImage(new Class.Controls.GalleryImage(Strings.ResStrings.Text, ""), new BitmapImage(new Uri("pack://application:,,,/Good Teacher;component/Resources/Background/SelectModelBackground.jpg")));
                control.AddGalleryImage(new Class.Controls.GalleryImage(Strings.ResStrings.Text, ""), new BitmapImage(new Uri("pack://application:,,,/Good Teacher;component/Resources/Background/BackgroundMat.jpg")));
                control.AddGalleryImage(new Class.Controls.GalleryImage(Strings.ResStrings.Text, ""), new BitmapImage(new Uri("pack://application:,,,/Good Teacher;component/Resources/Background/ImgBackground.jpg")));
                control.RefreshAndUpdate();
            }
        }


        public Gallery CreateControl(DataStore data)
        {
            Gallery control = new Gallery();

            Deserialize(control, data);

            return control;
        }


    }
}
