using Good_Teacher.Class.Actions;
using Good_Teacher.Class.Save;
using Good_Teacher.Class.Serialization.Content_Ser;
using Good_Teacher.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Class.Serialization.Ser_Controls
{
    class Button_Serialization : ControlSerializer
    {

        public Brush_Serializer brush = new Brush_Serializer();
        public Brush_Serializer brushHover = new Brush_Serializer();
        public Brush_Serializer brushClick = new Brush_Serializer();

        public Brush_Serializer brushFor = new Brush_Serializer();

        public HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
        public VerticalAlignment verticalAlignment = VerticalAlignment.Center;

        public Stretch stretchN, stretchH, stretchC, stretchFor;

        public string keyN, keyH, keyC, keyFor;

        public double opacity = 1, opacityhover = 1, opacityclick = 1;

        public bool ChangeHover = true, ChangeClick = true;

        public List<IActions> action = new List<IActions>();

        public string Tooltip = "";

        public bool ChangeCursor = true;

        public ContentCreator content = new ContentCreator();

        public Button_Serialization()
        {

        }

        public Button_Serialization(CButton control, DataStore data)
        {
            Serialize(control, data);
        }


        public void Serialize(CButton control, DataStore data)
        {
            SerializeDefault(control);

            action = new List<IActions>(control.actions);

            keyN = control.keyN;
            keyH = control.keyH;
            keyC = control.keyC;
            keyFor = control.keyFor;

            stretchN = control.stretchN;
            stretchH = control.stretchH;
            stretchC = control.stretchC;
            stretchFor = control.stretchFor;

            ChangeClick = control.ChangeClick;
            ChangeHover = control.ChangeHover;

            content = control.contentCreator;

            if (control.DefaultBrush is ImageBrush)
            control.Tag = SaveEditor.SerializeObject( new DesignSave( new ImageRepresentation( control.keyN, ((ImageBrush)control.DefaultBrush).Stretch)));
            brush.Serialize(control, control.DefaultBrush, data);
            
            if (control.Hover is ImageBrush)
            control.Tag = SaveEditor.SerializeObject(new DesignSave(new ImageRepresentation(control.keyH, ((ImageBrush)control.Hover).Stretch)));
            brushHover.Serialize(control, control.Hover, data);

            if (control.ClickBrush is ImageBrush)
            control.Tag = SaveEditor.SerializeObject(new DesignSave(new ImageRepresentation(control.keyC, ((ImageBrush)control.ClickBrush).Stretch)));
            brushClick.Serialize(control, control.ClickBrush, data);

            if (control.Foreground is ImageBrush)
                control.Tag = SaveEditor.SerializeObject(new DesignSave(new ImageRepresentation(control.keyFor, ((ImageBrush)control.Foreground).Stretch)));
            brushFor.Serialize(control, control.Foreground, data);

            control.Tag = null;
            
            opacity = control.OpacityN;
            opacityhover = control.OpacityHover;
            opacityclick = control.OpacityClick;


            if(control.ToolTip!=null)
            {
                Tooltip = control.ToolTip.ToString();
            }

            horizontalAlignment = control.HorizontalContentAlignment;
            verticalAlignment = control.VerticalContentAlignment;

            if (control.Cursor != null)
            {
                ChangeCursor = true;
            }
            else
            {
                ChangeCursor = false;
            }
        }


        public void Deserialize(CButton control, DataStore data)
        {
            DeserializeDefault(control);

            if(!String.IsNullOrWhiteSpace(Tooltip))
            {
                control.ToolTip = Tooltip;
            }

            control.HorizontalContentAlignment = horizontalAlignment;
            control.VerticalContentAlignment = verticalAlignment;

            control.actions = new List<IActions>(action);

            control.ChangeHover = ChangeHover;
            control.ChangeClick = ChangeClick;
            
            control.DefaultBrush = brush.DeserializeToBrush(control, data);
            control.Background = control.DefaultBrush;
            control.Hover = brushHover.DeserializeToBrush(control, data);
            control.ClickBrush = brushClick.DeserializeToBrush(control, data);

            control.Foreground = brushFor.DeserializeToBrush(control, data);

            control.contentCreator = content;
            control.Content = control.contentCreator.Create(data);

            control.keyN = keyN;
            control.keyH = keyH;
            control.keyC = keyC;
            control.keyFor = keyFor;

            control.stretchN = stretchN;
            control.stretchH = stretchH;
            control.stretchC = stretchC;
            control.stretchFor = stretchFor;

            control.OpacityN = opacity;
            control.Opacity = opacity;
            control.OpacityHover = opacityhover;
            control.OpacityClick = opacityclick;

            if(ChangeCursor)
            {
                control.Cursor = Cursors.Hand;
            }
            else
            {
                control.Cursor = null;
            }

        }


        public CButton CreateControl(DataStore data)
        {
            CButton control = new CButton();

            Deserialize(control, data);

            return control;
        }

    }
}
