using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_Text.xaml
    /// </summary>
    public partial class Value_Text : System.Windows.Controls.Page
    {
        RichTextBox cont;
        DataStore data;

        public Value_Text(DataStore datas ,RichTextBox rich)
        {
            InitializeComponent();

            data = datas;

            cont = rich;

            positionselector.SetData(cont);
            positionselector.LoadData();

            Brushselector.SetData(cont,data, true);
            Brushselector.LoadData(cont.Background);
            Brushselector.ChangedBrush -= Brushselector_ChangedBrush;
            Brushselector.ChangedBrush += Brushselector_ChangedBrush;

            effectselector.SetData(cont);
            effectselector.LoadData();

            if (cont.VerticalScrollBarVisibility == ScrollBarVisibility.Visible)
                ComboBox_VScrollBar.SelectedIndex = 1;

            CB_BorderVisible.IsChecked = cont.BorderThickness.Top > 0;

            TB_HPadding.Text = "" + cont.Padding.Left;
            TB_VPadding.Text = "" + cont.Padding.Top;

        }

        private void Brushselector_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            cont.Background = Sbrush;
        }

        private static String ColorToHexConverter(Color c)
        {
            return "#" + c.A.ToString("X2") + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
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


        private void ComboBox_VScrollBar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cont != null)
            {
                if (ComboBox_VScrollBar.SelectedIndex == 0)
                    cont.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                else
                    cont.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
        }



        private void CB_BorderVisible_Checked(object sender, RoutedEventArgs e)
        {
            if(CB_BorderVisible.IsChecked == false)
            cont.BorderThickness = new Thickness(0);
            else
                cont.BorderThickness = new Thickness(1);
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

        private void Control_LostFocus(object sender, RoutedEventArgs e)
        {
            DetectSender(sender);
        }
        
        void DetectSender(object sender)
        {
            if(sender == TB_HPadding)
            {
                SetHPadding();
            }
            else if (sender == TB_VPadding)
            {
                SetVPadding();
            }
        }



        void SetHPadding()
        {
            try
            {
                double pos = 0;

                if (double.TryParse(TB_HPadding.Text, out pos))
                {
                    cont.Padding = new Thickness(pos, cont.Padding.Top, pos, cont.Padding.Bottom);
                }
                else
                {
                    TB_HPadding.Text = "0";
                }
            }
            catch
            {
                TB_HPadding.Text = "0";
            }
        }


        void SetVPadding()
        {
            try
            {
                double pos = 0;

                if (double.TryParse(TB_VPadding.Text, out pos))
                {
                    cont.Padding = new Thickness(cont.Padding.Left, pos, cont.Padding.Right, pos);
                }
                else
                {
                    TB_VPadding.Text = "0";
                }
            }
            catch
            {
                TB_VPadding.Text = "0";
            }
        }

    }
}
