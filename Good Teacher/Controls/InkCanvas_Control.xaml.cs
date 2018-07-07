using Good_Teacher.Windows;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for InkCanvas_Control.xaml
    /// </summary>
    public partial class InkCanvas_Control : UserControl
    {

        public static readonly DependencyProperty ControlPanelBackground =
                DependencyProperty.Register("ControlPanelBackground", typeof(Brush), typeof(InkCanvas_Control), new PropertyMetadata(new LinearGradientBrush(Color.FromRgb(162, 162, 162), Color.FromRgb(230, 230, 230), 90)));

        public Brush ControlPanelBack
        {
            get { return (Brush)GetValue(ControlPanelBackground); }
            set { SetValue(ControlPanelBackground, value); }
        }

        public string PathToCPImage = "";
        public Stretch CPStretch = Stretch.Uniform;


        public InkCanvas_Control()
        {
            InitializeComponent();
            Button_Draw.SetCheckedNoCall(true);
        }


        private void ButtonColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_BackColor.Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                Rect_BackColor.Fill = new SolidColorBrush(colorp.GetColor());

                inkCanvas.DefaultDrawingAttributes.Color = colorp.GetColor();
                Debug.WriteLine("W: "+ inkCanvas.DefaultDrawingAttributes.Width+"  H: "+ inkCanvas.DefaultDrawingAttributes.Height);
            }
        }

        private void Button_EraseAll_Click(object sender, MouseEventArgs e)
        {
            inkCanvas.Strokes.Clear();
        }


        public StrokeCollection GetStrokeCollection()
        {
            return inkCanvas.Strokes;
        }


        private void CB_PaintSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (inkCanvas != null)
            {
                InkCanvasEditingMode emode = InkCanvasEditingMode.Ink;

                if (inkCanvas.EditingMode == InkCanvasEditingMode.EraseByPoint)
                    emode = InkCanvasEditingMode.EraseByPoint;


                inkCanvas.EditingMode = InkCanvasEditingMode.None;

                switch (CB_PaintSize.SelectedIndex)
                {
                    case 0:
                        inkCanvas.DefaultDrawingAttributes.Width = 2;
                        inkCanvas.DefaultDrawingAttributes.Height = 2;
                        inkCanvas.EraserShape = new RectangleStylusShape(2,2);
                        break;
                    case 1:
                        inkCanvas.DefaultDrawingAttributes.Width = 4;
                        inkCanvas.DefaultDrawingAttributes.Height = 4;
                        inkCanvas.EraserShape = new RectangleStylusShape(4,4);
                        break;
                    case 2:
                        inkCanvas.DefaultDrawingAttributes.Width = 8;
                        inkCanvas.DefaultDrawingAttributes.Height = 8;
                        inkCanvas.EraserShape = new RectangleStylusShape(8,8);
                        break;
                    case 3:
                        inkCanvas.DefaultDrawingAttributes.Width = 12;
                        inkCanvas.DefaultDrawingAttributes.Height = 12;
                        inkCanvas.EraserShape = new RectangleStylusShape(12,12);
                        break;
                    case 4:
                        inkCanvas.DefaultDrawingAttributes.Width = 15;
                        inkCanvas.DefaultDrawingAttributes.Height = 15;
                        inkCanvas.EraserShape = new RectangleStylusShape(15, 15);
                        break;
                    case 5:
                        inkCanvas.DefaultDrawingAttributes.Width = 18;
                        inkCanvas.DefaultDrawingAttributes.Height = 18;
                        inkCanvas.EraserShape = new RectangleStylusShape(18, 18);
                        break;
                    case 6:
                        inkCanvas.DefaultDrawingAttributes.Width = 24;
                        inkCanvas.DefaultDrawingAttributes.Height = 24;
                        inkCanvas.EraserShape = new RectangleStylusShape(24, 24);
                        break;
                    case 7:
                        inkCanvas.DefaultDrawingAttributes.Width = 30;
                        inkCanvas.DefaultDrawingAttributes.Height = 30;
                        inkCanvas.EraserShape = new RectangleStylusShape(30, 30);
                        break;
                    default:
                        inkCanvas.DefaultDrawingAttributes.Width = 2;
                        inkCanvas.DefaultDrawingAttributes.Height = 2;
                        inkCanvas.EraserShape = new RectangleStylusShape(2, 2);
                        break;
                }


                inkCanvas.EditingMode = emode;
            }
        }

        private void Button_Draw_OnCheckChanged(object sender, bool IsChecked)
        {
            Button_Draw.SetCheckedNoCall(true);
            Button_Eraser.SetCheckedNoCall(false);
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Button_Eraser_OnCheckChanged(object sender, bool IsChecked)
        {
            Button_Eraser.SetCheckedNoCall(true);
            Button_Draw.SetCheckedNoCall(false);
            inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void Button_ExportOut_Click(object sender, MouseEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "image.png";
            saveFileDialog.Filter = Strings.ResStrings.Image + " (*.png) | *.png|" + Strings.ResStrings.Image + " (*.jpg) | *.jpg|" + Strings.ResStrings.AllFiles + "| *.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight, 96d, 96d, PixelFormats.Default);
                rtb.Render(inkCanvas);

                BitmapEncoder encoder;

                var extension = System.IO.Path.GetExtension(saveFileDialog.FileName);

                if(extension.ToLower()==".jpg")
                {
                    encoder = new JpegBitmapEncoder();
                    ((JpegBitmapEncoder)encoder).QualityLevel = 90;
                }
                else
                    encoder = new PngBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create(rtb));
                FileStream fs = File.Open(saveFileDialog.FileName, FileMode.Create);
                encoder.Save(fs);
                fs.Close();
            }
        }


    }
}
