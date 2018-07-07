using Good_Teacher.Class.Enumerators;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using ZXing;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for Barcode.xaml
    /// </summary>
    public partial class Barcode : UserControl
    {


        public string text
        {
            get { return texts; }
            set { texts = value; }
        }


        public BarcodeType.Barcode_Type type
        {
            get { return types; }
            set { types = value; }
        }

        string texts = "";
        BarcodeType.Barcode_Type types;

        public Barcode()
        {
            InitializeComponent();
        }

        public Barcode(string txt, BarcodeType.Barcode_Type Btype)
        {
            InitializeComponent();
            Width = 128;
            Height = 128;
            text = txt;
            type = Btype;
            RefreshBarcode();
        }

        public bool SetNewBarcode(string txt, BarcodeType.Barcode_Type Btype)
        {
            text = txt;
            type = Btype;
            return RefreshBarcode();
        }

        public bool RefreshBarcode()
        {
            //Debug.WriteLine("ActualW: "+Width+"  ActualH: "+Height);
            try
            {
                int hSize = 0;
                if (Height > Width)
                    hSize = (int)Height;
                else
                    hSize = (int)Width;


                ZXing.Presentation.BarcodeWriter writer = new ZXing.Presentation.BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Height = (int)Height,
                        Width = (int)Width,
                        Margin = 0
                    }
                };

                if (type == BarcodeType.Barcode_Type.QRCode)
                {
                    writer.Format = BarcodeFormat.QR_CODE;
                    writer.Options.Width = hSize;
                    writer.Options.Height = hSize;
                }
                else if (type == BarcodeType.Barcode_Type.EAN13)
                {
                    writer.Format = BarcodeFormat.EAN_13;
                }
                else if (type == BarcodeType.Barcode_Type.EAN8)
                {
                    writer.Format = BarcodeFormat.EAN_8;
                }
                else if (type == BarcodeType.Barcode_Type.Code128)
                {
                    writer.Format = BarcodeFormat.CODE_128;
                }
                else if (type == BarcodeType.Barcode_Type.Codabar)
                {
                    writer.Format = BarcodeFormat.CODABAR;
                }

                BarcodeImage.Source = writer.Write(text);
                return true;
            }catch(Exception ex)
            {
                Debug.WriteLine("Barcode Error: "+ex);
                return false;
            }
        }



        public string GetEncodedText()
        {
            return text;
        }


        public BarcodeType.Barcode_Type GetBarcodeType()
        {
            return type;
        }

    }
}
