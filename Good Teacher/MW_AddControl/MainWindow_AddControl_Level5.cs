using Good_Teacher.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfMath.Controls;

namespace Good_Teacher
{
    public partial class MainWindow : Window
    {
        public void AddLevel5Control(Point p)
        {
            switch (ControlTag)
            {

                case 51:
                    {

                        Barcode con = new Barcode("Good Teacher", Class.Enumerators.BarcodeType.Barcode_Type.QRCode);

                        con.Width = 128;
                        con.Height = 128;

                        Panel.SetZIndex(con, 3);

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);

                    }
                    break;

                case 52:
                    {

                        Barcode con = new Barcode("001234567890", Class.Enumerators.BarcodeType.Barcode_Type.EAN13);

                        con.Width = 256;
                        con.Height = 128;

                        Panel.SetZIndex(con, 3);

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        con.RefreshBarcode();

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);

                    }
                    break;

                case 53:
                    {

                        Barcode con = new Barcode("55123457", Class.Enumerators.BarcodeType.Barcode_Type.EAN8);

                        con.Width = 192;
                        con.Height = 96;

                        Panel.SetZIndex(con, 3);

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        con.RefreshBarcode();

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);

                    }
                    break;

                case 54:
                    {

                        Barcode con = new Barcode("Good Teacher", Class.Enumerators.BarcodeType.Barcode_Type.Code128);

                        con.Width = 384;
                        con.Height = 192;

                        Panel.SetZIndex(con, 3);

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        con.RefreshBarcode();

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);

                    }
                    break;
                case 55:
                    {

                        Barcode con = new Barcode("0123456789", Class.Enumerators.BarcodeType.Barcode_Type.Codabar);

                        con.Width = 384;
                        con.Height = 192;

                        Panel.SetZIndex(con, 3);

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        con.RefreshBarcode();

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);

                    }
                    break;


                case 56:
                    {

                        FormulaControl con = new FormulaControl();

                        con.Formula = "\\int_0^{\\infty}{x^{2n} e^{-a x^2} dx} = \\frac{2n-1}{2a} \\int_0^{\\infty}{x^{2(n-1)} e^{-a x^2} dx} = \\frac{(2n-1)!!}{2^{n+1}} \\sqrt{\\frac{\\pi}{a^{2n+1}}}";

                        Panel.SetZIndex(con, 2);


                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));


                        con.Width = con.DesiredSize.Width;
                        con.Height = con.DesiredSize.Height;

                        con.Background = new SolidColorBrush(Colors.Transparent);

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);

                    }
                    break;

                case 57:
                    {

                        InkCanvas_Control con = new InkCanvas_Control();

                        Panel.SetZIndex(con, 5);


                        con.Width = 600;
                        con.Height = 400;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        con.ControlPanelBack = new LinearGradientBrush(Color.FromRgb(162, 162, 162), Color.FromRgb(230, 230, 230), 90);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);

                    }
                    break;
                case 58:
                    {

                        ScalableImage con = new ScalableImage();

                        Panel.SetZIndex(con, 5);

                        con.M_Img.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Controls/image.png"));


                        con.Width = 500;
                        con.Height = 400;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        con.ControlPanelBack = new LinearGradientBrush(Colors.White, Color.FromRgb(236, 240, 241), 90);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);

                    }
                    break;

            }
        }
    }
}
