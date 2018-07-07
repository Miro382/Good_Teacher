using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Good_Teacher.Windows;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_TextBox.xaml
    /// </summary>
    public partial class Value_TextBox : System.Windows.Controls.Page
    {
        TextBox cont;
        DataStore data;

        public Value_TextBox(DataStore dataStore, TextBox textbox)
        {
            InitializeComponent();
            cont = textbox;
            data = dataStore;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();

            brushselector.SetData(cont,data, true);
            brushselector.LoadData(cont.Background);
            brushselector.ChangedBrush -= Brushselector_ChangedBrush;
            brushselector.ChangedBrush += Brushselector_ChangedBrush;

            Box_ID.Text = (string)cont.Tag;

            Rect_ForColor.Fill = (SolidColorBrush)cont.Foreground;

            fontEditorPanel.Load(cont,data);

            CB_TextWrapping.IsChecked = cont.TextWrapping == TextWrapping.Wrap;
            CB_MultiLine.IsChecked = cont.AcceptsReturn;

            Box_ID.Text = cont.Text;
            TB_MaxLength.Text = ""+cont.MaxLength;
        }

        private void Brushselector_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            cont.Background = Sbrush;
        }

        private static String ColorToHexConverter(Color c)
        {
            return "#" + c.A.ToString("X2") + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }


        void SetID()
        {
            cont.Text = Box_ID.Text;
        }


        void DetectSender(object senderob)
        {
            if (senderob == Box_ID)
                SetID();
            else if (senderob == TB_MaxLength)
                SetMaxLength();
        }


        void SetMaxLength()
        {
            int length = 0;

            if (int.TryParse(TB_MaxLength.Text, out length))
            {
                cont.MaxLength = length;
            }
            else
            {
                TB_MaxLength.Text = "0";
                cont.MaxLength = 0;
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


        private void Align_top_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cont.VerticalContentAlignment = VerticalAlignment.Top;
            }
            catch
            {

            }
        }

        private void Align_center_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cont.VerticalContentAlignment = VerticalAlignment.Center;
            }
            catch
            {

            }
        }


        private void Align_bottom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cont.VerticalContentAlignment = VerticalAlignment.Bottom;
            }
            catch
            {

            }
        }


        private void ButtonColorFont_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_ForColor.Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                cont.Foreground = new SolidColorBrush(colorp.GetColor());
                Rect_ForColor.Fill = new SolidColorBrush(colorp.GetColor());
            }
        }



        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            if(cont != null)
            {
                if (sender == CB_TextWrapping)
                {
                    if (CB_TextWrapping.IsChecked == true)
                    {
                        cont.TextWrapping = TextWrapping.Wrap;
                    }
                    else
                    {
                        cont.TextWrapping = TextWrapping.NoWrap;
                    }
                }else if (sender == CB_MultiLine)
                {
                    cont.AcceptsReturn = CB_MultiLine.IsChecked == true;
                }
            }
        }



        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }


        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9,-]+");
            return !regex.IsMatch(text);
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {

            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }


    }
}
