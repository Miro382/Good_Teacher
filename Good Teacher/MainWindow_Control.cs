using HelixToolkit.Wpf;
using LiveCharts.Wpf;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Good_Teacher.Controls;
using Good_Teacher.Pages;
using Good_Teacher.Pages.Special;
using WpfMath.Controls;

namespace Good_Teacher
{
    public partial class MainWindow : Window
    {

        object OldPage = null;


        public void EditControl(object sender)
        {

            if(sender!=null)
            Debug.WriteLine("Clicked on: " + sender.ToString());


            /*
            if (SelectedControl != null && SelectedControl is Control)
                if (ChangeColor(SelectedControl))
                    ((Control)SelectedControl).Background = selbackground;
                    */

            if(sender is FrameworkElement)
            {
                Debug.WriteLine("Clicked on: " + ((FrameworkElement)sender).Name);
            }

            if (sender is Label)
            {
                Value_Label value = new Value_Label(data,(Label)sender);
                ValueEditor.Content = value;

            }
            else if (sender is RichTextBox)
            {
                Value_Text value = new Value_Text(data,(RichTextBox)sender);
                ValueEditor.Content = value;

                if(OldPage == null)
                OldPage = FrameEditor.Content;

                FrameEditor.Content = new Page_TextEdit(this,OldPage,(RichTextBox)sender);
            }
            else if (sender is TextBox)
            {
                Value_TextBox value = new Value_TextBox(data,(TextBox)sender);
                ValueEditor.Content = value;
            }
            else if (sender is Image)
            {
                Value_Image value = new Value_Image(data,(Image)sender);
                ValueEditor.Content = value;
            }
            else if (sender is Line)
            {
                Value_Line value = new Value_Line((Line)sender);
                ValueEditor.Content = value;
            }
            else if (sender is Shape)
            {
                Value_Shapes value = new Value_Shapes(data, (Shape)sender);
                ValueEditor.Content = value;
            }
            else if (sender is CButton)
            {
                Value_Button value = new Value_Button(data,(CButton)sender, SelectedPosition);
                ValueEditor.Content = value;
            }
            else if (sender is PieChart)
            {
                Value_PieChart value = new Value_PieChart(data,(PieChart)sender);
                ValueEditor.Content = value;
            }
            else if (sender is CartesianChart)
            {
                Value_CartesianChart value = new Value_CartesianChart(data,(CartesianChart)sender);
                ValueEditor.Content = value;
            }
            else if (sender is HelixViewport3D)
            {
                Value_Model value = new Value_Model(data,(HelixViewport3D)sender);
                ValueEditor.Content = value;
            }
            else if (sender is WebPage_Control)
            {
                Value_WebBrowser value = new Value_WebBrowser(data, (WebPage_Control)sender);
                ValueEditor.Content = value;
            }
            else if (sender is Barcode)
            {
                Value_Barcode value = new Value_Barcode((Barcode)sender);
                ValueEditor.Content = value;
            }
            else if (sender is FormulaControl)
            {
                Value_Formula value = new Value_Formula(data,(FormulaControl)sender);
                ValueEditor.Content = value;
            }
            else if (sender is MediaPlayer_Control)
            {
                Value_Media value = new Value_Media(data, (MediaPlayer_Control)sender);
                ValueEditor.Content = value;
            }
            else if (sender is MediaPlayerController_Control)
            {
                Value_MediaPlayer value = new Value_MediaPlayer(data, (MediaPlayerController_Control)sender);
                ValueEditor.Content = value;
            }
            else if (sender is InkCanvas_Control)
            {
                Value_InkCanvas value = new Value_InkCanvas(data, (InkCanvas_Control)sender);
                ValueEditor.Content = value;
            }
            else if (sender is AnswerButton)
            {
                Value_AnswerButton value = new Value_AnswerButton(data, (AnswerButton)sender);
                ValueEditor.Content = value;
            }
            else if (sender is ContentViewer)
            {
                Value_ContentViewer value = new Value_ContentViewer(data, (ContentViewer)sender);
                ValueEditor.Content = value;
            }
            else if (sender is Gallery)
            {
                Value_Gallery value = new Value_Gallery(data, (Gallery)sender);
                ValueEditor.Content = value;
            }
            else if (sender is CheckBox)
            {
                Value_Checkbox value = new Value_Checkbox(data, (CheckBox)sender);
                ValueEditor.Content = value;
            }
            else if (sender is RadioButton)
            {
                Value_RadioButton value = new Value_RadioButton(data, (RadioButton)sender);
                ValueEditor.Content = value;
            }
            else if (sender is ComboBox_Control)
            {
                Value_ComboBox value = new Value_ComboBox(data, (ComboBox_Control)sender);
                ValueEditor.Content = value;
            }
            else if (sender is ScalableImage)
            {
                Value_ScalableImage value = new Value_ScalableImage(data, (ScalableImage)sender);
                ValueEditor.Content = value;
            }
            else if (sender is ToggleButton_Control)
            {
                Value_ToggleButton value = new Value_ToggleButton(data, (ToggleButton_Control)sender, SelectedPosition);
                ValueEditor.Content = value;
            }

            SelectedControl = sender;
            SelectedControlID = DesignCanvas.Children.IndexOf((UIElement)sender);

            DrawSelectedItem((FrameworkElement)SelectedControl);
        }


        void AddEvents(FrameworkElement cont)
        {
            if (string.IsNullOrWhiteSpace(cont.Name))
            {
                cont.Name = "ID_" + data.pages[SelectedPosition].LastID++;
            }

            MainWindow.IsChanged = true;

            if (cont is RichTextBox)
            {
                cont.GotFocus += Control_GotFocus;
                cont.GotKeyboardFocus += Control_GotFocus;
                AddAllDefaultEvents(cont);
            }
            else if (cont is TextBox)
            {
                AddAllDefaultEvents(cont);
                cont.PreviewMouseDown += DesignControl_MouseDown;
                cont.PreviewMouseLeftButtonUp += Control_MouseLeftButtonUp;
            }
            else if (cont is CheckBox ||cont is RadioButton ||cont is ComboBox_Control ||cont is ScalableImage)
            {
                AddAllDefaultEvents(cont);
                cont.PreviewMouseDown += DesignControl_MouseDown;
                cont.PreviewMouseLeftButtonUp += Control_MouseLeftButtonUp;
                cont.PreviewMouseMove += Control_MouseMove;
                cont.MouseLeave += Control_MouseLeave;
                cont.PreviewMouseLeftButtonDown += Control_MouseLeftButtonDown;
            }
            else if (cont is WebPage_Control)
            {
                AddAllDefaultEvents(cont);
                ((WebPage_Control)cont).GotFocus += MainWindow_GotFocus;
            }
            else
            {
                cont.MouseDown += (s, em) => { ((UIElement)s).Focus(); };
                AddAllDefaultEvents(cont);
            }
        }


        private void MainWindow_GotFocus(object sender, RoutedEventArgs e)
        {
            EditControl(sender);
        }


        private void AddAllDefaultEvents(FrameworkElement cont)
        {
            cont.MouseLeftButtonDown += Control_MouseLeftButtonDown;
            cont.MouseLeftButtonUp += Control_MouseLeftButtonUp;
            cont.MouseMove += Control_MouseMove;
            cont.MouseLeave += Control_MouseLeave;
            cont.MouseDown += DesignControl_MouseDown;
            cont.Unloaded += Control_Unloaded;
        }


    }
}
