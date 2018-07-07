using Good_Teacher.Controls;
using Good_Teacher.Controls.Shapes;
using HelixToolkit.Wpf;
using LiveCharts.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using WpfMath.Controls;

namespace Good_Teacher.Class.Workers
{
    public class ControlWorker
    {

        public static string GetTypeName(UIElement uie, out string property)
        {
            property = Strings.ResStrings.Width + ": " + ((FrameworkElement)uie).Width + "   " + Strings.ResStrings.Height + ": " + ((FrameworkElement)uie).Height;


            if (uie is Shape)
            {
                return GetShapes(uie, ref property);
            }
            else
            {

                if (uie is Label)
                {
                    property = Strings.ResStrings.Text + ": " + ((Label)uie).Content;
                    return Strings.ResStrings.Label;
                }
                else if (uie is TextBox)
                {
                    property += "   " + Strings.ResStrings.ID + ": " + ((TextBox)uie).Tag;
                    return Strings.ResStrings.EditBox;
                }
                else if (uie is Image)
                {
                    return Strings.ResStrings.Image;
                }
                else if (uie is RichTextBox)
                {
                    return Strings.ResStrings.Text;
                }
                else if (uie is PieChart)
                {
                    return Strings.ResStrings.PieChart;
                }
                else if (uie is CartesianChart)
                {
                    return Strings.ResStrings.CartesianChart;
                }
                else if (uie is MediaPlayer_Control)
                {
                    return Strings.ResStrings.Media;
                }
                else if (uie is MediaPlayerController_Control)
                {
                    return Strings.ResStrings.MediaPlayer;
                }
                else if (uie is WebPage_Control)
                {
                    property += "   " + Strings.ResStrings.WebAddress + ": " + ((WebPage_Control)uie).WebUrl;
                    return Strings.ResStrings.WebPage;
                }
                else if (uie is CButton)
                {
                    return Strings.ResStrings.Button;
                }
                else if (uie is Barcode)
                {
                    property += "   " + Strings.ResStrings.Text + ": " + ((Barcode)uie).GetEncodedText();
                    return Strings.ResStrings.Barcode;
                }
                else if (uie is HelixViewport3D)
                {
                    return Strings.ResStrings.ModelObject;
                }
                else if (uie is FormulaControl)
                {
                    return Strings.ResStrings.MathFormula;
                }
                else if (uie is Gallery)
                {
                    return Strings.ResStrings.Gallery;
                }
                else if (uie is ContentViewer)
                {
                    return Strings.ResStrings.ContentViewer;
                }
                else if (uie is CheckBox)
                {
                    return Strings.ResStrings.Checkbox;
                }
                else if (uie is RadioButton)
                {
                    return Strings.ResStrings.RadioButton;
                }
                else if (uie is ComboBox_Control)
                {
                    return Strings.ResStrings.ComboBox;
                }
                else if (uie is InkCanvas_Control)
                {
                    return Strings.ResStrings.DrawingCanvas;
                }
                else if (uie is AnswerButton)
                {
                    return Strings.ResStrings.AnswerButton;
                }
                else if (uie is ScalableImage)
                {
                    return Strings.ResStrings.ScalableImage;
                }
            }


            return uie.GetType().Name;
        }


        private static string GetShapes(UIElement uie, ref string property)
        {
            if (uie is Rectangle)
            {
                return Strings.ResStrings.Box;
            }
            else if (uie is Ellipse)
            {
                return Strings.ResStrings.Ellipse;
            }
            else if (uie is Line)
            {
                property = "X1: " + ((Line)uie).X1 + "   X2: " + ((Line)uie).X2 + "   Y1: " + ((Line)uie).Y1 + "   Y2: " + ((Line)uie).Y2;
                return Strings.ResStrings.Line;
            }
            else if (uie is Arrow)
            {
                return Strings.ResStrings.Arrow;
            }
            else if (uie is Ellipse)
            {
                return Strings.ResStrings.Ellipse;
            }
            else if (uie is Hexagon)
            {
                return Strings.ResStrings.Hexagon;
            }
            else if (uie is Good_Teacher.Controls.Triangle)
            {
                return Strings.ResStrings.Triangle;
            }
            else if (uie is Star)
            {
                return Strings.ResStrings.Star;
            }
            else if (uie is Diamond)
            {
                return Strings.ResStrings.Diamond;
            }
            else if (uie is Heart)
            {
                return Strings.ResStrings.Heart;
            }
            else if (uie is Cloud)
            {
                return Strings.ResStrings.Cloud;
            }
            else if (uie is Arrow)
            {
                return Strings.ResStrings.Arrow;
            }
            else if (uie is SmileFace)
            {
                return Strings.ResStrings.Smiling;
            }
            else if (uie is Speech)
            {
                return Strings.ResStrings.Speech;
            }
            else if (uie is Ribbon)
            {
                return Strings.ResStrings.Ribbon;
            }
            else if (uie is CheckMark)
            {
                return Strings.ResStrings.CheckMark;
            }
            else if (uie is Cross)
            {
                return Strings.ResStrings.Cross;
            }
            else if (uie is Drop)
            {
                return Strings.ResStrings.Drop;
            }
            else if (uie is Chevron)
            {
                return Strings.ResStrings.Chevron;
            }
            else if (uie is RightAngledTriangle || uie is RightAngledTriangleSE)
            {
                return Strings.ResStrings.RightAngledTriangle;
            }

            return uie.GetType().Name;
        }

    }
}
