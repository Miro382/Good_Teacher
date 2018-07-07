using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Class.Save
{
    public class DesignSave
    {
        public ImageRepresentation Background = new ImageRepresentation();
        public ImageRepresentation Foreground = new ImageRepresentation();

        public DesignSave()
        {

        }

        public DesignSave(ImageRepresentation background)
        {
            Background = background;
        }


        public DesignSave(ImageRepresentation background, ImageRepresentation foreground)
        {
            Background = background;
            Foreground = foreground;
        }

        public ImageBrush ToImageBrush(DataStore data)
        {
            ImageBrush imgb = new ImageBrush(data.archive.GetImage(Background.Path));
            imgb.Stretch = Background.stretch;
            DeserializeTransform(imgb, Background);
            return imgb;
        }


        public void SerializeTransform(Brush brush, ImageRepresentation representation)
        {
            if (brush != null)
            {
                if (brush is ImageBrush)
                {
                    if (((ImageBrush)brush).RelativeTransform != null && ((ImageBrush)brush).RelativeTransform is TransformGroup)
                    {
                        representation.DoTransform = true;
                        representation.Angle = ((RotateTransform)((TransformGroup)((ImageBrush)brush).RelativeTransform).Children[0]).Angle;
                        representation.ScaleX = ((ScaleTransform)((TransformGroup)((ImageBrush)brush).RelativeTransform).Children[1]).ScaleX;
                        representation.ScaleY = ((ScaleTransform)((TransformGroup)((ImageBrush)brush).RelativeTransform).Children[1]).ScaleY;
                        representation.TranslateX = ((TranslateTransform)((TransformGroup)((ImageBrush)brush).RelativeTransform).Children[2]).X;
                        representation.TranslateY = ((TranslateTransform)((TransformGroup)((ImageBrush)brush).RelativeTransform).Children[2]).Y;
                    }
                    else
                    {
                        representation.DoTransform = false;
                    }

                    representation.tileMode = ((ImageBrush)brush).TileMode;
                }
                else
                {
                    representation.DoTransform = false;
                }

            }
            else
            {
                representation.DoTransform = false;
            }
        }


        public void DeserializeTransform(Brush brush, ImageRepresentation representation)
        {
            if (representation.DoTransform)
            {
                TransformGroup transformGroup = new TransformGroup();
                RotateTransform rotateTransform = new RotateTransform();
                rotateTransform.CenterX = 0.5;
                rotateTransform.CenterY = 0.5;
                rotateTransform.Angle = representation.Angle;
                //((ImageBrush)brush).TileMode = TileMode.

                ScaleTransform scaleTransform = new ScaleTransform();
                scaleTransform.CenterX = 0.5;
                scaleTransform.CenterY = 0.5;
                scaleTransform.ScaleX = representation.ScaleX;
                scaleTransform.ScaleY = representation.ScaleY;

                TranslateTransform translateTransform = new TranslateTransform();
                translateTransform.X = representation.TranslateX;
                translateTransform.Y = representation.TranslateY;

                transformGroup.Children.Add(rotateTransform);
                transformGroup.Children.Add(scaleTransform);
                transformGroup.Children.Add(translateTransform);

                ((ImageBrush)brush).RelativeTransform = transformGroup;
            }

            if(brush!=null && brush is ImageBrush)
            {
                ((ImageBrush)brush).TileMode = representation.tileMode;
            }

        }


        public void ToControl(DataStore data ,Control cont)
        {
            bool ignoreOP = false;

            if(cont.Width < 1 || Double.IsNaN(cont.Width) || cont.Height < 1)
            {
                ignoreOP = true;
            }

            //Debug.WriteLine("CW: "+cont.Width+"  CH: "+cont.Height+"  ignore: "+ignoreOP);

            if(!ignoreOP && MainWindow.OPTIMIZEDMODE)
                cont.Background = new ImageBrush (data.archive.GetImageOptimal( Background.Path,(int)cont.Width, (int)cont.Height ));
            else
                cont.Background = new ImageBrush(data.archive.GetImage(Background.Path));

            DeserializeTransform(cont.Background, Background);

            ((ImageBrush)cont.Background).Stretch = Background.stretch;
        }


        public void ToControlWithForeground(DataStore data, Control cont)
        {
            bool ignoreOP = false;

            if (cont.Width < 1 || Double.IsNaN(cont.Width) ||cont.Height < 1)
            {
                ignoreOP = true;
            }
            //Debug.WriteLine("CW: " + cont.Width + "  CH: " + cont.Height + "  ignore: " + ignoreOP);

            if (Background != null && !String.IsNullOrWhiteSpace(Background.Path))
            {
                if (!ignoreOP && MainWindow.OPTIMIZEDMODE)
                    cont.Background = new ImageBrush(data.archive.GetImageOptimal(Background.Path, (int)cont.Width, (int)cont.Height));
                else
                    cont.Background = new ImageBrush(data.archive.GetImage(Background.Path));
                ((ImageBrush)cont.Background).Stretch = Background.stretch;

                DeserializeTransform(cont.Background, Background);
            }

            if (Foreground != null && !String.IsNullOrWhiteSpace( Foreground.Path))
            {
                if (!ignoreOP && MainWindow.OPTIMIZEDMODE)
                    cont.Foreground = new ImageBrush(data.archive.GetImageOptimal(Foreground.Path, (int)cont.Width, (int)cont.Height));
                else
                    cont.Foreground = new ImageBrush(data.archive.GetImage(Foreground.Path));

                ((ImageBrush)cont.Foreground).Stretch = Foreground.stretch;

                DeserializeTransform(cont.Foreground, Foreground);
            }
        }

        public void ToImage(DataStore data, Image cont)
        {
            if (MainWindow.OPTIMIZEDMODE)
                cont.Source = data.archive.GetImageOptimal(Background.Path, (int)cont.Width, (int)cont.Height);
            else
                cont.Source = data.archive.GetImage(Background.Path);

            cont.Stretch = Background.stretch;
        }

        public void ToShape(DataStore data, Shape cont)
        {
            if (MainWindow.OPTIMIZEDMODE)
                cont.Fill = new ImageBrush(data.archive.GetImageOptimal(Background.Path, (int)cont.Width, (int)cont.Height));
            else
                cont.Fill = new ImageBrush(data.archive.GetImage(Background.Path));
            ((ImageBrush)cont.Fill).Stretch = Background.stretch;

            DeserializeTransform(cont.Fill, Background);
        }


        public string Serialize()
        {
            return SaveEditor.SerializeObject(this);
        }

        public static DesignSave Deserialize(string DesignSaveSerialized)
        {
            return (DesignSave)SaveEditor.DeserializeObject(DesignSaveSerialized);
        }

    }
}
