using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Good_Teacher.Windows;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_Shapes.xaml
    /// </summary>
    public partial class Value_Shapes : System.Windows.Controls.Page
    {

        Shape cont;
        DataStore data;

        public Value_Shapes(DataStore dataStore, Shape shape)
        {
            InitializeComponent();
            cont = shape;

            data = dataStore;

            position_selector.SetData(cont);
            position_selector.LoadData();

            brushselector.SetData(cont, data, true);
            brushselector.LoadData(cont.Fill);
            brushselector.ChangedBrush -= Brushselector_ChangedBrush;
            brushselector.ChangedBrush += Brushselector_ChangedBrush;

            effectselector.SetData(cont);
            effectselector.LoadData();

            StackPanel_Radius.Visibility = Visibility.Collapsed;

            if (shape is Ellipse)
                Label_SettingsName.Text = Strings.ResStrings.EllipseSett;
            else if(shape is Rectangle)
            {
                StackPanel_Radius.Visibility = Visibility.Visible;
                Box_CRX.Text = "" + ((Rectangle)cont).RadiusX;
                Box_CRY.Text = "" + ((Rectangle)cont).RadiusY;
            }
            else
            {
                Label_SettingsName.Text = Strings.ResStrings.ShapeSett;
            }


            if (cont.Stroke == null || cont.Stroke.ToString() == "#00FFFFFF")
                Rect_BColor.Fill = null;
            else
                Rect_BColor.Fill = (SolidColorBrush)cont.Stroke;


            Box_BSize.Text = "" + cont.StrokeThickness;
           
        }



        private void Brushselector_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            cont.Fill = Sbrush;
        }



        private static String ColorToHexConverter(Color c)
        {
            return "#" + c.A.ToString("X2") + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }


        

        void DetectSender(object senderob)
        {
            if (senderob == Box_BSize)
                SetBorderSize();
            else if (senderob == Box_CRX)
                SetRadiusX();
            else if (senderob == Box_CRY)
                SetRadiusY();
        }


        void SetRadiusX()
        {
            try
            {
                ((Rectangle)cont).RadiusX = double.Parse(Box_CRX.Text);
            }
            catch
            {
                ((Rectangle)cont).RadiusX = 0;
                Box_CRX.Text = "0";
            }
        }


        void SetRadiusY()
        {
            try
            {
                ((Rectangle)cont).RadiusY = double.Parse(Box_CRY.Text);
            }
            catch
            {
                ((Rectangle)cont).RadiusY = 0;
                Box_CRY.Text = "0";
            }
        }

        void SetBorderSize()
        {
            try
            {
                cont.StrokeThickness = double.Parse(Box_BSize.Text);
            }
            catch
            {
                cont.StrokeThickness = 1;
                Box_BSize.Text = "1";
            }
        }


        private void Control_LostFocus(object sender, RoutedEventArgs e)
        {
            DetectSender(sender);
        }



        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DetectSender(sender);
            }
            else if (e.Key == Key.Escape)
            {
                Keyboard.ClearFocus();
                DetectSender(sender);
            }
        }



        private void ButtonColorBorder_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_BColor.Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                cont.Stroke = new SolidColorBrush(colorp.GetColor());
                Rect_BColor.Fill = new SolidColorBrush(colorp.GetColor());
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


    }
}
