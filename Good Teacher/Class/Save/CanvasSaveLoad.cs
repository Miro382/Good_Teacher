using HelixToolkit.Wpf;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Good_Teacher.Class.Save;
using Good_Teacher.Class.Serialization.Ser_Controls;
using Good_Teacher.Controls;
using LiveCharts.Wpf.Charts.Base;
using WpfMath.Controls;
using Good_Teacher.Class.Workers;
using System.Linq;

namespace Good_Teacher.Class.Serialization
{
    public class CanvasSaveLoad
    {


        public static Canvas LoadSpecificCanvas(DataStore data ,int pos)
        {
            Canvas canvas = new Canvas();
            canvas.Width = MainWindow.CanvasW;
            canvas.Height = MainWindow.CanvasH;

            if (!String.IsNullOrWhiteSpace(data.pages[pos].canvas))
            {
                CanvasWriter.LoadCanvas(canvas, data.pages[pos].canvas);
            }

            ExtractHiddenData(data,canvas, pos);

            return canvas;
        }


        public static void ToPresentationMode(Canvas canvas)
        {
            List<FrameworkElement> RemoveList = new List<FrameworkElement>();
            List<FrameworkElement> AddList = new List<FrameworkElement>();

            canvas.IsEnabled = true;
            foreach(FrameworkElement elm in canvas.Children)
            {
                if(elm is RichTextBox)
                {
                    RichTextBox rich = (RichTextBox)elm;
                    rich.IsReadOnly = true;
                    rich.SpellCheck.IsEnabled = false;
                    rich.IsUndoEnabled = false;
                }else if(elm is TextBox)
                {
                    TextBox txt = (TextBox)elm;
                    txt.IsReadOnly = false;
                    txt.Focusable = true;
                    txt.Tag = txt.Text;
                    txt.Text = "";
                }
                else if (elm is MediaPlayer_Control)
                {
                    MediaElement mediaElement = new MediaElement();
                    Canvas.SetLeft(mediaElement, Canvas.GetLeft(elm));
                    Canvas.SetTop(mediaElement, Canvas.GetTop(elm));
                    Panel.SetZIndex(mediaElement, Panel.GetZIndex(elm));

                    mediaElement.Width = elm.Width;
                    mediaElement.Height = elm.Height;

                    if(elm.Tag!=null)
                    mediaElement.Source = new Uri( LocalPath.GetResourcesPath()+"\\Media\\"+ elm.Tag.ToString());

                    mediaElement.LoadedBehavior = MediaState.Manual;

                    mediaElement.MediaEnded += (s, e) => {
                        mediaElement.Position = TimeSpan.Zero;
                        mediaElement.Play();
                    };

                    mediaElement.Play();

                    RemoveList.Add(elm);
                    AddList.Add(mediaElement);
                }
                else if (elm is MediaPlayerController_Control)
                {

                    if (elm.Tag != null)
                    {
                        if (!String.IsNullOrWhiteSpace(elm.Tag.ToString()))
                        {
                            ((MediaPlayerController_Control)elm).ME_MediaPlayer.Source = new Uri(LocalPath.GetResourcesPath() + "\\Media\\" + elm.Tag.ToString());
                        }
                    }

                }
            }

            foreach(FrameworkElement elm in RemoveList)
            {
                canvas.Children.Remove(elm);
            }

            foreach (FrameworkElement elm in AddList)
            {
                canvas.Children.Add(elm);
            }

        }


        public static void SimulateCanvas(Canvas canvas)
        {
            foreach (FrameworkElement elm in canvas.Children)
            {
                if (elm is Chart)
                {
                    Size size = new Size();
                    canvas.Measure(size);
                    canvas.Arrange(new Rect(size));
                    ((Chart)elm).DisableAnimations = true;
                    ((Chart)elm).Update(true, true);
                    canvas.UpdateLayout();
                }/*
                else if(elm is Image)
                {
                    ((Image)elm).Effect = null;
                }*/
            }
        }




        public static void ToSerializableCanvas(DataStore data, Canvas canvas, int pos)
        {
            SerializeCustomControls(data,canvas, pos);

            if (data.pages[pos].IsImageBrush)
            {
                data.archive.StoreImage(data.pages[pos].ImageBrush.Path,""+data.LastAddedImageID++);
            }
            canvas.Background = null;

            foreach (FrameworkElement frw in canvas.Children)
            {
                if (frw is Image) //Image
                {
                    Image img = (Image)frw;
                    if (img.Source != null)
                    {
                        if (img.Tag != null)
                        {
                            DesignSave ds = DesignSave.Deserialize(img.Tag.ToString());
                            ds.Background.stretch = img.Stretch;
                            img.Tag = ds.Serialize();
                        }

                        img.Source = null;
                    }
                }
                else if (frw is Shape) //Shape
                {
                    Shape shape = (Shape)frw;
                    if (shape.Fill != null)
                    {
                        if (shape.Fill is ImageBrush)
                        {

                            if(shape.Tag != null)
                            {
                                DesignSave ds = DesignSave.Deserialize(shape.Tag.ToString());
                                ds.Background.stretch = ((ImageBrush)shape.Fill).Stretch;
                                ds.SerializeTransform(shape.Fill, ds.Background);
                                shape.Tag = ds.Serialize();
                            }

                            shape.Fill = null;
                        }
                        else
                        {
                            shape.Tag = null;
                        }
                    }
                }
                else if (frw is Label) //Label
                {
                    Control control = (Control)frw;

                    if (control.Tag != null)
                    {

                        DesignSave ds = DesignSave.Deserialize(control.Tag.ToString());

                        if (control.Background != null)
                        {
                            if (control.Background is ImageBrush)
                            {
                                ds.Background.stretch = ((ImageBrush)control.Background).Stretch;
                                ds.SerializeTransform(control.Background, ds.Background);
                                control.Background = null;
                            }
                            else
                            {
                                ds.Background.Path = "";
                            }
                        }


                        if (control.Foreground != null)
                        {
                            if (control.Foreground is ImageBrush)
                            {
                                ds.Foreground.stretch = ((ImageBrush)control.Foreground).Stretch;
                                ds.SerializeTransform(control.Foreground, ds.Foreground);
                                control.Foreground = null;
                            }
                            else
                            {
                                ds.Foreground.Path = "";
                            }
                        }

                        control.Tag = ds.Serialize();
                    }
                }
                else if (frw is CheckBox || frw is RadioButton) //Control
                {
                    Control cont = (Control)frw;
                    if (cont.Foreground != null)
                    {
                        if (cont.Foreground is ImageBrush)
                        {

                            if (cont.Tag != null)
                            {
                                DesignSave ds = DesignSave.Deserialize(cont.Tag.ToString());
                                ds.Foreground.stretch = ((ImageBrush)cont.Foreground).Stretch;
                                ds.SerializeTransform(cont.Foreground, ds.Foreground);
                                cont.Tag = ds.Serialize();
                            }

                            cont.Foreground = null;
                        }
                        else
                        {
                            cont.Tag = null;
                        }
                    }
                }
                else if (frw is Control) //Control
                {
                    Control cont = (Control)frw;
                    if (cont.Background != null)
                    {
                        if (cont.Background is ImageBrush)
                        {

                            if (cont.Tag != null)
                            {
                                DesignSave ds = DesignSave.Deserialize(cont.Tag.ToString());
                                ds.Background.stretch = ((ImageBrush)cont.Background).Stretch;
                                ds.SerializeTransform(cont.Background, ds.Background);
                                cont.Tag = ds.Serialize();
                            }

                            cont.Background = null;
                        }
                        else
                        {
                            cont.Tag = null;
                        }
                    }
                }


            }


        }//ToSerializableCanvas





        public static void ExtractHiddenData(DataStore data, Canvas canvas, int pos)
        {
            if((int)data.BitmapScalingMode != 1)
            {
                RenderOptions.SetBitmapScalingMode(canvas, data.BitmapScalingMode);
            }

            if (data.pages[pos].IsImageBrush)
            {
                canvas.Background = new ImageBrush(data.archive.GetImage(data.pages[pos].ImageBrush.Path));
                ((ImageBrush)canvas.Background).Stretch = data.pages[pos].ImageBrush.stretch;

            }
            else if (data.pages[pos].CustomBrush && !String.IsNullOrWhiteSpace(data.pages[pos].canvasbrush))
            {
                canvas.Background = (Brush)SaveEditor.XMLDeserialize(data.pages[pos].canvasbrush);

            }
            else
            {

                if (data.IsImageBrush)
                {
                    canvas.Background = new ImageBrush(data.archive.GetImage(data.ImageBrush.Path));
                    ((ImageBrush)canvas.Background).Stretch = data.ImageBrush.stretch;
                }
                else if (!String.IsNullOrEmpty(data.AllBackground))
                {
                    canvas.Background = (Brush)SaveEditor.XMLDeserialize(data.AllBackground);
                }
                else
                    canvas.Background = new SolidColorBrush(Colors.White);
            }

            foreach (FrameworkElement frw in canvas.Children)
            {
                if (frw is Image)
                {
                    Image img = (Image)frw;


                    if (frw.Tag == null || String.IsNullOrWhiteSpace(frw.Tag.ToString()))
                    {
                        img.Source = new BitmapImage(new Uri("pack://application:,,,/Good Teacher;Component/Resources/Controls/image.png"));
                    }
                    else
                    {
                        DesignSave designSave = DesignSave.Deserialize(img.Tag.ToString());
                        designSave.ToImage(data, img);
                    }
                }
                else if (frw is Shape)
                {
                    Shape shape = (Shape)frw;

                    if (frw.Tag == null || String.IsNullOrWhiteSpace(frw.Tag.ToString()))
                    {
                    }
                    else
                    {
                        DesignSave designSave = DesignSave.Deserialize(shape.Tag.ToString());
                        designSave.ToShape(data, shape);
                    }
                }
                else if (frw is Label || frw is CheckBox || frw is RadioButton)
                {
                    Control control = (Control)frw;

                    if (frw.Tag == null || String.IsNullOrWhiteSpace(frw.Tag.ToString()))
                    {
                    }
                    else
                    {
                        DesignSave designSave = DesignSave.Deserialize(control.Tag.ToString());
                        designSave.ToControlWithForeground(data, control);
                    }

                    
                    if (control.FontFamily.BaseUri != null && control.FontFamily.Source != null)
                    {
                        Debug.WriteLine("Ffontfamilysource: " + control.FontFamily.Source);

                        string fontfamilyloc = "";
                        string fontfamilyfile = "";

                        if(control.FontFamily.Source.Length >= 5 )
                        {
                            fontfamilyloc = control.FontFamily.Source.Substring(0, 3);
                            fontfamilyfile = control.FontFamily.Source.Substring(0, 5);
                        }

                        if (!string.IsNullOrWhiteSpace(control.FontFamily.Source) && (fontfamilyloc == "./#" || fontfamilyfile == "file:"))
                        {
                            //Debug.WriteLine("-------------------\nBU: " + control.FontFamily.BaseUri + "\nFF: " + control.FontFamily + "\nAP: " + control.FontFamily.BaseUri.AbsolutePath + "\nAU: " + control.FontFamily.BaseUri.AbsoluteUri + "\nH: " + control.FontFamily.BaseUri.Host + "\nS: " + control.FontFamily.Source + "\n********\n");

                            FontFamily fontFamily;
                            if (FontWorker.GetFontFamily(control.FontFamily, out fontFamily))
                            {
                                control.FontFamily = fontFamily;
                            }
                        }
                    }
                    

                }
                else if (frw is MediaPlayer_Control)
                {
                    //Nothing
                }
                else if (frw is Control)
                {
                    Control control = (Control)frw;

                    if (frw.Tag == null || String.IsNullOrWhiteSpace(frw.Tag.ToString()))
                    {
                    }
                    else
                    {
                        DesignSave designSave = DesignSave.Deserialize(control.Tag.ToString());
                        designSave.ToControl(data, control);
                    }

                    if (control.FontFamily.BaseUri != null)
                    {

                        //Debug.WriteLine(""+ control.FontFamily.BaseUri+ "  "+ control.FontFamily+"    AP: "+control.FontFamily.BaseUri.AbsolutePath+"   AU: "+control.FontFamily.BaseUri.AbsoluteUri+"   H: "+control.FontFamily.BaseUri.Host+ "   S: "+control.FontFamily.Source);

                        FontFamily fontFamily;
                        if (FontWorker.GetFontFamily(control.FontFamily, out fontFamily))
                        {
                            control.FontFamily = fontFamily;
                        }
                    }
                }

            }

            DeserializeCustomControls(data,canvas, pos);
        }


        public static void SerializeCustomControls(DataStore data, Canvas canvas, int pos)
        {
            List<FrameworkElement> dsl = new List<FrameworkElement>();

            data.pages[pos].CustomControls.Clear();
            for (int i = 0; i < canvas.Children.Count; i++)
            {
                if (canvas.Children[i] is WebPage_Control)
                {
                    WebPage_Control wbc = (WebPage_Control)canvas.Children[i];
                    Debug.WriteLine("Webpage control serializing");

                    data.pages[pos].CustomControls.Add(new WebPage_Serialization(wbc, data));
                    dsl.Add(wbc);
                }
                else if (canvas.Children[i] is PieChart)
                {

                    PieChart cont = (PieChart)canvas.Children[i];
                    Debug.WriteLine("PieChart control serializing");

                    data.pages[pos].CustomControls.Add(new PieChart_Serialization(cont, data));
                    dsl.Add(cont);
                }
                else if (canvas.Children[i] is CartesianChart)
                {

                    CartesianChart cont = (CartesianChart)canvas.Children[i];
                    Debug.WriteLine("PieChart control serializing");

                    data.pages[pos].CustomControls.Add(new CartesianChart_Serialization(cont, data));
                    dsl.Add(cont);
                }
                else if (canvas.Children[i] is HelixViewport3D)
                {

                    HelixViewport3D cont = (HelixViewport3D)canvas.Children[i];
                    Debug.WriteLine("HelixViewport3D control serializing");

                    data.pages[pos].CustomControls.Add(new Model_Serialization(cont, data));
                    dsl.Add(cont);
                }
                else if (canvas.Children[i] is Barcode)
                {

                    Barcode cont = (Barcode)canvas.Children[i];
                    Debug.WriteLine("Barcode control serializing");

                    data.pages[pos].CustomControls.Add(new Barcode_Serialization(cont));
                    dsl.Add(cont);
                }
                else if (canvas.Children[i] is CButton)
                {

                    CButton cont = (CButton)canvas.Children[i];
                    Debug.WriteLine("Button control serializing");

                    data.pages[pos].CustomControls.Add(new Button_Serialization(cont, data));
                    dsl.Add(cont);
                }
                else if (canvas.Children[i] is FormulaControl)
                {

                    FormulaControl cont = (FormulaControl)canvas.Children[i];
                    Debug.WriteLine("Formula control serializing");

                    data.pages[pos].CustomControls.Add(new Formula_Serialization(cont, data));
                    dsl.Add(cont);
                }
                else if (canvas.Children[i] is InkCanvas_Control)
                {

                    InkCanvas_Control cont = (InkCanvas_Control)canvas.Children[i];
                    Debug.WriteLine("InkCanvas Control serializing");

                    data.pages[pos].CustomControls.Add(new InkCanvas_Serialization(cont, data));
                    dsl.Add(cont);
                }
                else if (canvas.Children[i] is AnswerButton)
                {
                    AnswerButton cont = (AnswerButton)canvas.Children[i];
                    Debug.WriteLine("Answer button Control serializing");

                    data.pages[pos].CustomControls.Add(new AnswerButton_Serialization(cont, data));
                    dsl.Add(cont);
                }
                else if (canvas.Children[i] is ContentViewer)
                {
                    ContentViewer cont = (ContentViewer)canvas.Children[i];
                    Debug.WriteLine("Content viewer Control serializing");

                    data.pages[pos].CustomControls.Add(new ContentViewer_Serialization(cont, data));
                    dsl.Add(cont);
                }
                else if (canvas.Children[i] is Gallery)
                {
                    Gallery cont = (Gallery)canvas.Children[i];
                    Debug.WriteLine("Gallery Control serializing");

                    data.pages[pos].CustomControls.Add(new Gallery_Serialization(cont, data));
                    dsl.Add(cont);
                }
                else if (canvas.Children[i] is ComboBox_Control)
                {
                    ComboBox_Control cont = (ComboBox_Control)canvas.Children[i];
                    Debug.WriteLine("ComboBox Control serializing");

                    data.pages[pos].CustomControls.Add(new ComboBox_Serialization(cont, data));
                    dsl.Add(cont);
                }
                else if (canvas.Children[i] is ScalableImage)
                {
                    ScalableImage cont = (ScalableImage)canvas.Children[i];
                    Debug.WriteLine("Scalable Image serializing");

                    data.pages[pos].CustomControls.Add(new ScalableImage_Serialization(cont, data));
                    dsl.Add(cont);
                }
                else if (canvas.Children[i] is MediaPlayerController_Control)
                {
                    MediaPlayerController_Control cont = (MediaPlayerController_Control)canvas.Children[i];
                    Debug.WriteLine("MediaPlayerController_Control serializing");

                    data.pages[pos].CustomControls.Add(new MediaPlayerController_Serialization(cont, data));
                    dsl.Add(cont);
                }
                else if (canvas.Children[i] is ToggleButton_Control)
                {
                    ToggleButton_Control cont = (ToggleButton_Control)canvas.Children[i];
                    Debug.WriteLine("MediaPlayerController_Control serializing");

                    data.pages[pos].CustomControls.Add(new ToggleButton_Serialization(cont, data));
                    dsl.Add(cont);
                }
            }

            for (int i = 0; i < dsl.Count; i++)
            {
                canvas.Children.Remove(dsl[i]);
            }

            dsl.Clear();
        }



        public static void DeserializeCustomControls(DataStore data, Canvas canvas, int pos)
        {
            for (int i = 0; i < data.pages[pos].CustomControls.Count; i++)
            {
                if (data.pages[pos].CustomControls[i] is WebPage_Serialization)
                {
                    WebPage_Serialization ser = (WebPage_Serialization)data.pages[pos].CustomControls[i];

                    WebPage_Control web = ser.CreateControl(data);

                    canvas.Children.Add(web);
                }
                else if (data.pages[pos].CustomControls[i] is PieChart_Serialization)
                {
                    PieChart_Serialization ser = (PieChart_Serialization)data.pages[pos].CustomControls[i];

                    PieChart cont = ser.CreateControl(data);

                    canvas.Children.Add(cont);
                }
                else if (data.pages[pos].CustomControls[i] is CartesianChart_Serialization)
                {
                    CartesianChart_Serialization ser = (CartesianChart_Serialization)data.pages[pos].CustomControls[i];

                    CartesianChart cont = ser.CreateControl(data);

                    canvas.Children.Add(cont);
                }
                else if (data.pages[pos].CustomControls[i] is Model_Serialization)
                {
                    Model_Serialization ser = (Model_Serialization)data.pages[pos].CustomControls[i];

                    HelixViewport3D cont = ser.CreateControl(data);

                    canvas.Children.Add(cont);
                }
                else if (data.pages[pos].CustomControls[i] is Barcode_Serialization)
                {
                    Barcode_Serialization ser = (Barcode_Serialization)data.pages[pos].CustomControls[i];

                    Barcode cont = ser.CreateControl();

                    canvas.Children.Add(cont);
                }
                else if (data.pages[pos].CustomControls[i] is Button_Serialization)
                {
                    Button_Serialization ser = (Button_Serialization)data.pages[pos].CustomControls[i];

                    CButton cont = ser.CreateControl(data);

                    canvas.Children.Add(cont);
                }
                else if (data.pages[pos].CustomControls[i] is Formula_Serialization)
                {
                    Formula_Serialization ser = (Formula_Serialization)data.pages[pos].CustomControls[i];

                    FormulaControl cont = ser.CreateControl(data);

                    canvas.Children.Add(cont);
                }
                else if (data.pages[pos].CustomControls[i] is InkCanvas_Serialization)
                {
                    InkCanvas_Serialization ser = (InkCanvas_Serialization)data.pages[pos].CustomControls[i];

                    InkCanvas_Control cont = ser.CreateControl(data);

                    canvas.Children.Add(cont);
                }
                else if (data.pages[pos].CustomControls[i] is AnswerButton_Serialization)
                {
                    AnswerButton_Serialization ser = (AnswerButton_Serialization)data.pages[pos].CustomControls[i];

                    AnswerButton cont = ser.CreateControl(data);

                    canvas.Children.Add(cont);
                }
                else if (data.pages[pos].CustomControls[i] is ContentViewer_Serialization)
                {
                    ContentViewer_Serialization ser = (ContentViewer_Serialization)data.pages[pos].CustomControls[i];

                    ContentViewer cont = ser.CreateControl(data);

                    canvas.Children.Add(cont);
                }
                else if (data.pages[pos].CustomControls[i] is Gallery_Serialization)
                {
                    Gallery_Serialization ser = (Gallery_Serialization)data.pages[pos].CustomControls[i];

                    Gallery cont = ser.CreateControl(data);

                    canvas.Children.Add(cont);
                }
                else if (data.pages[pos].CustomControls[i] is ComboBox_Serialization)
                {
                    ComboBox_Serialization ser = (ComboBox_Serialization)data.pages[pos].CustomControls[i];

                   ComboBox_Control cont = ser.CreateControl(data);

                    canvas.Children.Add(cont);
                }
                else if (data.pages[pos].CustomControls[i] is ScalableImage_Serialization)
                {
                    ScalableImage_Serialization ser = (ScalableImage_Serialization)data.pages[pos].CustomControls[i];

                    ScalableImage cont = ser.CreateControl(data);

                    canvas.Children.Add(cont);
                }
                else if (data.pages[pos].CustomControls[i] is MediaPlayerController_Serialization)
                {
                    MediaPlayerController_Serialization ser = (MediaPlayerController_Serialization)data.pages[pos].CustomControls[i];

                    MediaPlayerController_Control cont = ser.CreateControl(data);

                    canvas.Children.Add(cont);
                }
                else if (data.pages[pos].CustomControls[i] is ToggleButton_Serialization)
                {
                    ToggleButton_Serialization ser = (ToggleButton_Serialization)data.pages[pos].CustomControls[i];

                    ToggleButton_Control cont = ser.CreateControl(data);

                    canvas.Children.Add(cont);
                }

            }
        }


    }
}
