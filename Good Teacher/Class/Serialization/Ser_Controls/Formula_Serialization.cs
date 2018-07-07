using WpfMath.Controls;

namespace Good_Teacher.Class.Serialization.Ser_Controls
{
    class Formula_Serialization : ControlSerializer
    {
        public string Formula = "";
        public double Scale = 20;
        public Brush_Serializer brush = new Brush_Serializer();

        public Formula_Serialization()
        {

        }

        public Formula_Serialization(FormulaControl control, DataStore data)
        {
            Serialize(control,data);
        }

        public void Serialize(FormulaControl control, DataStore data)
        {
            SerializeDefault(control);
            brush.Serialize(control, control.Background, data);
            Formula = control.Formula;
            Scale = control.Scale;
        }


        public void Deserialize(FormulaControl control,DataStore data)
        {
            DeserializeDefault(control);
            brush.Deserialize(control, data);
            control.Formula = Formula;
            control.Scale = Scale;
        }


        public FormulaControl CreateControl(DataStore data)
        {
            FormulaControl control = new FormulaControl();

            Deserialize(control,data);

            return control;
        }


    }
}
