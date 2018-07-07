using Good_Teacher.Class.Serialization.Content_Ser;
using Good_Teacher.Controls;
using System.Windows;

namespace Good_Teacher.Class.Serialization.Ser_Controls
{
    class AnswerButton_Serialization : ControlSerializer
    {

        public ContentCreator content = new ContentCreator();
        public Brush_Serializer brush = new Brush_Serializer();
        public Brush_Serializer brushSelect = new Brush_Serializer();
        public HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
        public VerticalAlignment verticalAlignment = VerticalAlignment.Center;
        public double NormalOp = 1, HoverOp = 0.8f, ClickOp = 0.6f;
        public string ID = "";
        public bool Good = true;
        public bool ShowGoodAnswer = true;


        public AnswerButton_Serialization(AnswerButton control, DataStore data)
        {
            Serialize(control, data);
        }


        public void Serialize(AnswerButton control, DataStore data)
        {
            if (control != null)
            {
                SerializeDefault(control);
                brush.Serialize(control, control.Background, data);
                brushSelect.SerializeWithKey(control.SelectedBrush, data, control.SelectedBrushKey);
                ID = control.ID;

                content = control.contentCreator;

                NormalOp = control.NormalOp;
                HoverOp = control.HoverOp;
                ClickOp = control.ClickOp;

                horizontalAlignment = control.AnswerPanel.HorizontalAlignment;
                verticalAlignment = control.AnswerPanel.VerticalAlignment;

                Good = control.Good;
                ShowGoodAnswer = control.ShowGood;
            }
        }


        public void Deserialize(AnswerButton control, DataStore data)
        {
            DeserializeDefault(control);
            brush.Deserialize(control, data);

            string key;
            control.SelectedBrush = brushSelect.DeserializeToBrushWithKey(data, out key);
            control.SelectedBrushKey = key;
            control.ID = ID;

            control.contentCreator = content;
            control.AnswerPanel.Children.Add(control.contentCreator.Create(data));

            control.NormalOp = NormalOp;
            control.HoverOp = HoverOp;
            control.ClickOp = ClickOp;

            control.AnswerPanel.HorizontalAlignment = horizontalAlignment;
            control.AnswerPanel.VerticalAlignment = verticalAlignment;

            control.Good = Good;
            control.ShowGood = ShowGoodAnswer;
        }


        public AnswerButton CreateControl(DataStore data)
        {
            AnswerButton control = new AnswerButton();

            Deserialize(control, data);

            return control;
        }



    }
}
