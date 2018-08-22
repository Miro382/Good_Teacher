using System;
using System.Collections.Generic;
using Good_Teacher.Class.Actions;
using Good_Teacher.Class.Save;
using Good_Teacher.Class.Serialization.Content_Ser;
using Good_Teacher.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Class.Serialization.Ser_Controls
{
    class ToggleButton_Serialization : ControlSerializer
    {

        public Brush_Serializer brush = new Brush_Serializer();
        public Brush_Serializer brushUN = new Brush_Serializer();

        public HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
        public VerticalAlignment verticalAlignment = VerticalAlignment.Center;

        public Stretch stretchN, stretchC;

        public string keyN, keyC;

        public double opacity = 1, opacityhover = 1, opacityclick = 1;

        public string Tooltip = "";

        public bool ChangeCursor = true;

        public double CornerRadius = 30;

        public bool DoAnimation = true;

        public bool DefaultIsChecked = false;

        public ContentCreator contentCH = new ContentCreator();
        public ContentCreator contentUN = new ContentCreator();

        public List<IActions> actionU = new List<IActions>();
        public List<IActions> actionC = new List<IActions>();

        public ToggleButton_Serialization()
        {

        }

        public ToggleButton_Serialization(ToggleButton_Control control, DataStore data)
        {
            Serialize(control, data);
        }


        public void Serialize(ToggleButton_Control control, DataStore data)
        {
            SerializeDefault(control);

            DefaultIsChecked = control.DefaultIsChecked;

            actionU = new List<IActions>(control.UncheckedActions);
            actionC = new List<IActions>(control.CheckedActions);

            keyN = control.keyN;
            keyC = control.keyC;

            stretchN = control.stretchN;
            stretchC = control.stretchC;

            contentCH = control.contentCreatorChecked;
            contentUN = control.contentCreatorUnchecked;

            CornerRadius = control.CBorder.CornerRadius.TopLeft;

            if (control.CheckedBrush is ImageBrush)
                control.Tag = SaveEditor.SerializeObject(new DesignSave(new ImageRepresentation(control.keyC, ((ImageBrush)control.CheckedBrush).Stretch)));
            brush.Serialize(control, control.CheckedBrush, data);

            if (control.UncheckedBrush is ImageBrush)
                control.Tag = SaveEditor.SerializeObject(new DesignSave(new ImageRepresentation(control.keyN, ((ImageBrush)control.UncheckedBrush).Stretch)));
            brushUN.Serialize(control, control.UncheckedBrush, data);

            control.Tag = null;

            opacity = control.OpacityN;
            opacityhover = control.OpacityHover;
            opacityclick = control.OpacityClick;


            if (control.ToolTip != null)
            {
                Tooltip = control.ToolTip.ToString();
            }

            horizontalAlignment = control.Ccontent.HorizontalAlignment;
            verticalAlignment = control.Ccontent.VerticalAlignment;

            DoAnimation = control.DoAnimation;

            if (control.Cursor != null)
            {
                ChangeCursor = true;
            }
            else
            {
                ChangeCursor = false;
            }
        }


        public void Deserialize(ToggleButton_Control control, DataStore data)
        {
            DeserializeDefault(control);

            if (!String.IsNullOrWhiteSpace(Tooltip))
            {
                control.ToolTip = Tooltip;
            }

            control.DefaultIsChecked = DefaultIsChecked;

            control.CheckedActions = new List<IActions>(actionC);
            control.UncheckedActions = new List<IActions>(actionU);

            control.CBorder.CornerRadius = new CornerRadius(CornerRadius);

            control.Ccontent.HorizontalAlignment = horizontalAlignment;
            control.Ccontent.VerticalAlignment = verticalAlignment;

            control.UncheckedBrush = brushUN.DeserializeToBrush(control, data);
            control.CheckedBrush = brush.DeserializeToBrush(control, data);

            control.contentCreatorChecked = contentCH;
            control.contentCreatorUnchecked = contentUN;

            control.keyN = keyN;
            control.keyC = keyC;

            control.stretchN = stretchN;
            control.stretchC = stretchC;

            control.OpacityN = opacity;
            control.Opacity = opacity;
            control.OpacityHover = opacityhover;
            control.OpacityClick = opacityclick;

            control.DoAnimation = DoAnimation;

            if (ChangeCursor)
            {
                control.Cursor = Cursors.Hand;
            }
            else
            {
                control.Cursor = null;
            }

        }


        public ToggleButton_Control CreateControl(DataStore data)
        {
            ToggleButton_Control control = new ToggleButton_Control();
            
            Deserialize(control, data);
            control.SetData(data);

            control.SetChecked(control.DefaultIsChecked, true);

            return control;
        }

    }
}
