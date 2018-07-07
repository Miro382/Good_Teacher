using Good_Teacher.Class.Serialization.Content_Ser;
using Good_Teacher.Controls;
using Good_Teacher.Windows;
using Good_Teacher.Windows.Dialogs;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher.Pages.Content
{
    /// <summary>
    /// Interaction logic for ContentEditor_Splitter.xaml
    /// </summary>
    public partial class ContentEditor_Splitter : System.Windows.Controls.Page
    {

        DataStore data;
        SelectButton cont;

        public ContentEditor_Splitter(DataStore dataStore, SelectButton selectButton)
        {
            InitializeComponent();

            data = dataStore;
            cont = selectButton;

            TB_Width.Text = "" + ((Rectangle)cont.Content).Width;
            TB_MarginLeft.Text = "" + cont.Margin.Left;

            if (cont.Foreground is SolidColorBrush)
                Rect_BackColor.Fill = cont.Foreground;
        }


        private void TB_Width_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                double val = 0;
                if (double.TryParse(TB_Width.Text, out val))
                {
                    ((Rectangle)cont.Content).Width = val;
                    ((Content_Splitter)cont.Tag).W = val;
                }
                else
                {
                    cont.Width = Double.NaN;
                    ((Content_Splitter)cont.Tag).W = Double.NaN;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                cont.Width = Double.NaN;
            }
        }


        private void TB_MarginLeft_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                double val = 0;
                if (double.TryParse(TB_MarginLeft.Text, out val))
                {
                    cont.Margin = new Thickness(val, 0, 0, 0);
                    ((Content_Splitter)cont.Tag).MarginLeft = val;
                }
                else
                {
                    cont.Margin = new Thickness(0, 0, 0, 0);
                    ((Content_Splitter)cont.Tag).MarginLeft = 0;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                cont.Margin = new Thickness(0, 0, 0, 0);
                ((Content_Splitter)cont.Tag).MarginLeft = 0;
            }
        }



        private void ButtonBrush_Click(object sender, RoutedEventArgs e)
        {
            DWindow_Brush window_Brush = new DWindow_Brush(cont, data);
            window_Brush.Owner = Window.GetWindow(this);
            window_Brush.ChangedBrush += Window_Brush_ChangedBrush;
            window_Brush.ShowDialog();
            window_Brush.ChangedBrush -= Window_Brush_ChangedBrush;
        }

        private void Window_Brush_ChangedBrush(string Key)
        {
            ((Content_Splitter)cont.Tag).fill.SerializeWithKey(cont.Foreground, data, Key);
        }

        private void ButtonColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_BackColor.Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                ((Rectangle)cont.Content).Fill = new SolidColorBrush(colorp.GetColor());
                ((Content_Splitter)cont.Tag).fill.SerializeWithKey(cont.Foreground, data, "");
                Rect_BackColor.Fill = new SolidColorBrush(colorp.GetColor());
            }
        }



    }
}
