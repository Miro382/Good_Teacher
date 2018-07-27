using System.Windows;

namespace Good_Teacher.Class.Serialization
{
    public abstract class ControlSerializer
    {
        public Effect_Serializer effect;
        public PositionSize position;
        public ControlDefaultData ControlDef = new ControlDefaultData();
        public string ContID = "";
        public Visibility ControlVisibility = Visibility.Visible;


        protected void SerializeDefault(FrameworkElement elm)
        {
            position = new PositionSize(elm);

            ControlDef.Serialize(elm);

            ContID = elm.Name;

            ControlVisibility = elm.Visibility;

            if (elm.Effect != null)
            {
                effect = new Effect_Serializer(elm.Effect);
            }
            else
            {
                effect = null;
            }
        }

        protected void DeserializeDefault(FrameworkElement elm)
        {
            position.SetControlPositionSize(elm);
            ControlDef.Deserialize(elm);

            elm.Name = ContID;

            elm.Visibility = ControlVisibility;

            if(effect!=null)
            elm.Effect = effect.CreateEffect();
        }

    }
}
