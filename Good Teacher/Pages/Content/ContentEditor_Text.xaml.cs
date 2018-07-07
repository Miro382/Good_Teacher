using Good_Teacher.Class.Serialization.Content_Ser;
using Good_Teacher.Controls;
using Good_Teacher.Windows;
using Good_Teacher.Windows.Dialogs;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Pages.Content
{
    /// <summary>
    /// Interaction logic for ContentEditor_Text.xaml
    /// </summary>
    public partial class ContentEditor_Text : System.Windows.Controls.Page
    {
        DataStore data;
        SelectButton cont;

        public ContentEditor_Text(DataStore dataStore, SelectButton control)
        {
            InitializeComponent();

            data = dataStore;
            cont = control;

            TB_Text.Text = cont.Content.ToString();
            ComboBox_FontSize.Text = ""+cont.FontSize;
            TB_MarginLeft.Text = "" + cont.Margin.Left;

            if(cont.Foreground is SolidColorBrush)
            Rect_BackColor.Fill = cont.Foreground;


            if (cont.FontWeight == FontWeights.Bold)
                SButton_Bold.SetCheckedNoCall(true);

            if (cont.FontStyle == FontStyles.Italic)
                SButton_Italic.SetCheckedNoCall(true);

            try
            {
                ComboBox_FontName.Text = cont.FontFamily.ToString();


                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\"))
                {
                    foreach (System.Windows.Media.FontFamily fm in Fonts.GetFontFamilies(new Uri(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\")))
                    {
                        Label labelf = new Label();
                        labelf.Content = fm.ToString().Substring(3);
                        labelf.FontFamily = fm;
                        labelf.FontSize = 14;
                        labelf.ToolTip = fm.ToString();
                        labelf.Foreground = new SolidColorBrush(Color.FromRgb(19, 87, 48));
                        ComboBox_FontName.Items.Add(labelf);
                    }
                }


                foreach (System.Windows.Media.FontFamily fm in Fonts.SystemFontFamilies)
                {
                    Label labelf = new Label();
                    labelf.Content = fm;
                    labelf.FontFamily = fm;
                    labelf.FontSize = 14;
                    labelf.ToolTip = fm.ToString();
                    ComboBox_FontName.Items.Add(labelf);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Font not found: " + ex);
            }
        }

        private void TB_Text_KeyUp(object sender, KeyEventArgs e)
        {
            ((Content_Text)cont.Tag).text = TB_Text.Text;
            cont.Content = TB_Text.Text;
        }


        private void ComboBox_FontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cont != null)
            {
                double size = 0;
                if (double.TryParse(ComboBox_FontSize.Text, out size))
                {
                    if (size > 3 && size < MainWindow.TextLimitSize)
                    {
                        cont.FontSize = size;
                        ((Content_Text)cont.Tag).fontsize = size;
                    }
                }
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
                    ((Content_Text)cont.Tag).MarginLeft = val;
                }
                else
                {
                    cont.Margin = new Thickness(0, 0, 0, 0);
                    ((Content_Text)cont.Tag).MarginLeft = 0;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                cont.Margin = new Thickness(0, 0, 0, 0);
                ((Content_Text)cont.Tag).MarginLeft = 0;
            }
        }

        private void ButtonBrush_Click(object sender, RoutedEventArgs e)
        {
            DWindow_Brush window_Brush = new DWindow_Brush(cont,data);
            window_Brush.Owner = Window.GetWindow(this);
            window_Brush.ChangedBrush += Window_Brush_ChangedBrush;
            window_Brush.ShowDialog();
            window_Brush.ChangedBrush -= Window_Brush_ChangedBrush;
        }

        private void Window_Brush_ChangedBrush(string Key)
        {
            ((Content_Text)cont.Tag).foreground.SerializeWithKey(cont.Foreground, data, Key);
        }

        private void ButtonColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_BackColor.Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                cont.Foreground = new SolidColorBrush(colorp.GetColor());
                ((Content_Text)cont.Tag).foreground.SerializeWithKey(cont.Foreground, data, "");
                Rect_BackColor.Fill = new SolidColorBrush(colorp.GetColor());
            }
        }



        private void ComboBox_FontName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cont != null)
            {
                try
                {
                    cont.FontFamily = ((Label)ComboBox_FontName.SelectedItem).FontFamily;
                    ((Content_Text)cont.Tag).fontFamily = cont.FontFamily;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error set font family: " + ex);
                }
            }
        }

        private void SelectButton_OnCheckChanged(object sender, bool IsChecked)
        {
            if (sender == SButton_Bold)
            {
                if (IsChecked)
                {
                    cont.FontWeight = FontWeights.Bold;
                    ((Content_Text)cont.Tag).Bold = true;
                }
                else
                {
                    cont.FontWeight = FontWeights.Normal;
                    ((Content_Text)cont.Tag).Bold = false;
                }

            }
            else if (sender == SButton_Italic)
            {
                if (IsChecked)
                {
                    cont.FontStyle = FontStyles.Italic;
                    ((Content_Text)cont.Tag).Italic = true;
                }
                else
                {
                    cont.FontStyle = FontStyles.Normal;
                    ((Content_Text)cont.Tag).Italic = false;
                }
            }
        }


    }
}
