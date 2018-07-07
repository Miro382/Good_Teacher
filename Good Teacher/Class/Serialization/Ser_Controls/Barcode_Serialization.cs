using Good_Teacher.Class.Enumerators;
using Good_Teacher.Controls;

namespace Good_Teacher.Class.Serialization.Ser_Controls
{
    public class Barcode_Serialization : ControlSerializer
    {
        public string EnText = "";
        public BarcodeType.Barcode_Type type;

        public Barcode_Serialization()
        {

        }

        public Barcode_Serialization(Barcode control)
        {
            Serialize(control);
        }

        public void Serialize(Barcode control)
        {
            position = new PositionSize(control);
            EnText = control.GetEncodedText();
            type = control.GetBarcodeType();
        }


        public void Deserialize(Barcode control)
        {
            position.SetControlPositionSize(control);
            control.SetNewBarcode(EnText, type);
        }


        public Barcode CreateControl()
        {
            Barcode control = new Barcode();

            Deserialize(control);

            return control;
        }


    }
}
