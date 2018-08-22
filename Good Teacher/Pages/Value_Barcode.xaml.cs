using Good_Teacher.Class.Enumerators;
using Good_Teacher.Controls;
using Good_Teacher.Strings;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_Barcode.xaml
    /// </summary>
    public partial class Value_Barcode : System.Windows.Controls.Page
    {

        Barcode cont;

        public Value_Barcode(Barcode barcode)
        {
            InitializeComponent();

            cont = barcode;

            positionselector.SetData(cont);
            positionselector.LoadData();

            Box_Text.Text = cont.GetEncodedText();

            CB_IsVisible.IsChecked = (cont.Visibility == Visibility.Visible);


            switch (cont.GetBarcodeType())
            {
                case BarcodeType.Barcode_Type.Codabar:
                    TBL_BarcodeType.Text = FormatStrings.Codabar;
                    break;
                case BarcodeType.Barcode_Type.Code128:
                    TBL_BarcodeType.Text = FormatStrings.Code128;
                    break;
                case BarcodeType.Barcode_Type.EAN13:
                    TBL_BarcodeType.Text = FormatStrings.EAN13;
                    break;
                case BarcodeType.Barcode_Type.EAN8:
                    TBL_BarcodeType.Text = FormatStrings.EAN8;
                    break;
                case BarcodeType.Barcode_Type.QRCode:
                    TBL_BarcodeType.Text = Strings.ResStrings.QRCode;
                    break;
                default:
                    TBL_BarcodeType.Text = "-";
                    break;
            }
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((Canvas)cont.Parent).Children.Remove(cont);
                NavigationService.Content = "";
                ((MainWindow)Window.GetWindow(this)).RemoveSelectedItemEffect();
            }
            catch
            {

            }
        }


        private void ButtonEncode_Click(object sender, RoutedEventArgs e)
        {
            string oldt = cont.GetEncodedText();
            if(cont.SetNewBarcode(Box_Text.Text, cont.GetBarcodeType())==false)
            {
                MessageBox.Show(Strings.ResStrings.WrongFormat, Strings.ResStrings.Error);
                Box_Text.Text = oldt;
                cont.SetNewBarcode(oldt, cont.GetBarcodeType());
            }
        }

        private void CB_IsVisible_Checked(object sender, RoutedEventArgs e)
        {
            if (cont != null)
            {
                if (CB_IsVisible.IsChecked == true)
                {
                    cont.Visibility = Visibility.Visible;
                }
                else
                {
                    cont.Visibility = Visibility.Collapsed;
                }
            }
        }

    }
}
