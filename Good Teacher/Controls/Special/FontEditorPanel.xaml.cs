using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Controls.Special
{
    /// <summary>
    /// Interaction logic for FontEditorPanel.xaml
    /// </summary>
    public partial class FontEditorPanel : UserControl
    {

        public delegate void DelegateChangedFontSize(double fontsize, bool AddTo);
        public event DelegateChangedFontSize OnChangeFontSize;

        public delegate void DelegateChangedFontFamily(FontFamily fontFamily);
        public event DelegateChangedFontFamily OnChangeFontFamily;

        public Control cont;


        public FontEditorPanel()
        {
            InitializeComponent();
        }



        private void SelectButton_OnCheckChanged(object sender, bool IsChecked)
        {
            if (cont != null)
            {
                if (sender == SButton_Bold)
                {
                    if (IsChecked)
                        cont.FontWeight = FontWeights.Bold;
                    else
                        cont.FontWeight = FontWeights.Normal;

                }
                else if (sender == SButton_Italic)
                {
                    if (IsChecked)
                        cont.FontStyle = FontStyles.Italic;
                    else
                        cont.FontStyle = FontStyles.Normal;
                }
            }
        }


        public void Load(Control control, DataStore data)
        {
            cont = control;

            try
            {
                ComboBox_FontName.Text = cont.FontFamily.ToString();

                
                try
                {
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\"))
                    {
                        foreach (System.Windows.Media.FontFamily fm in Fonts.GetFontFamilies(new Uri(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoodTeacher\\Temp\\")))
                        {
                            Debug.WriteLine(fm.ToString().Substring(3));
                            Debug.WriteLine("EXIST: "+data.FontManager.Exists(item => item.FontFamilyName == fm.ToString().Substring(3)));
                            if (data.FontManager.Exists(item => item.FontFamilyName == fm.ToString().Substring(3)))
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
                    }
                }catch(Exception ex)
                {
                    Debug.WriteLine("!NotFoundFontFile: "+ex);
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

            ComboBox_FontSize.Text = "" + cont.FontSize;

            if (cont.FontWeight == FontWeights.Bold)
                SButton_Bold.SetCheckedNoCall(true);
            else
                SButton_Bold.SetCheckedNoCall(false);


            if (cont.FontStyle == FontStyles.Italic)
                SButton_Italic.SetCheckedNoCall(true);
            else
                SButton_Italic.SetCheckedNoCall(false);

            if (cont.HorizontalContentAlignment == HorizontalAlignment.Left)
                Button_LeftAlign.SetCheckedNoCall(true);
            else if (cont.HorizontalContentAlignment == HorizontalAlignment.Center)
                Button_CenterAlign.SetCheckedNoCall(true);
            else if (cont.HorizontalContentAlignment == HorizontalAlignment.Right)
                Button_RightAlign.SetCheckedNoCall(true);


            if (cont.VerticalContentAlignment == VerticalAlignment.Top)
                Button_VTopAlign.SetCheckedNoCall(true);
            else if (cont.VerticalContentAlignment == VerticalAlignment.Center)
                Button_VCenterAlign.SetCheckedNoCall(true);
            else if (cont.VerticalContentAlignment == VerticalAlignment.Bottom)
                Button_VBottomAlign.SetCheckedNoCall(true);
        }
      

        private void ComboBox_FontName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cont != null)
            {
                try
                {
                    
                    if (OnChangeFontFamily != null)
                        OnChangeFontFamily(((Label)ComboBox_FontName.SelectedItem).FontFamily);

                    cont.FontFamily = ((Label)ComboBox_FontName.SelectedItem).FontFamily;
                    
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error set font family: " + ex);
                }
            }
        }

        private void FontUp_Click(object sender, RoutedEventArgs e)
        {
            if (OnChangeFontSize != null)
                OnChangeFontSize(2, true);

            cont.FontSize += 2;
            ComboBox_FontSize.Text = "" + cont.FontSize;
        }

        private void FontDown_Click(object sender, RoutedEventArgs e)
        {
            if (OnChangeFontSize != null)
                OnChangeFontSize(-2, true);

            cont.FontSize -= 2;
            ComboBox_FontSize.Text = "" + cont.FontSize;
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
                        if (OnChangeFontSize != null)
                            OnChangeFontSize(size, false);

                        cont.FontSize = size;
                    }
                }
            }
        }

        private void Button_FastFont_Click(object sender, MouseEventArgs e)
        {
            double nm = 0;
            if (double.TryParse(ComboBox_FontSize.Text, out nm))
            {
                if (sender == Button_FontUp)
                {
                    nm += 2;

                    if (nm > 3 && nm <MainWindow.TextLimitSize)
                    {
                        ComboBox_FontSize.Text = "" + nm;
                    }
                }
                else
                {
                    nm -= 2;

                    if (nm > 3 && nm < MainWindow.TextLimitSize)
                    {
                        ComboBox_FontSize.Text = "" + nm;
                    }
                }
            }
        }

        private void Button_Align_OnCheckChanged(object sender, bool IsChecked)
        {
            if (cont != null)
            {
                if (sender == Button_LeftAlign)
                {
                    cont.HorizontalContentAlignment = HorizontalAlignment.Left;
                    Button_LeftAlign.SetCheckedNoCall(true);
                    Button_RightAlign.SetCheckedNoCall(false);
                    Button_CenterAlign.SetCheckedNoCall(false);
                }
                else if (sender == Button_CenterAlign)
                {
                    cont.HorizontalContentAlignment = HorizontalAlignment.Center;
                    Button_RightAlign.SetCheckedNoCall(false);
                    Button_LeftAlign.SetCheckedNoCall(false);
                    Button_CenterAlign.SetCheckedNoCall(true);
                }
                else if (sender == Button_RightAlign)
                {
                    cont.HorizontalContentAlignment = HorizontalAlignment.Right;
                    Button_CenterAlign.SetCheckedNoCall(false);
                    Button_LeftAlign.SetCheckedNoCall(false);
                    Button_RightAlign.SetCheckedNoCall(true);
                }
                else if (sender == Button_VTopAlign)
                {
                    cont.VerticalContentAlignment = VerticalAlignment.Top;
                    Button_VCenterAlign.SetCheckedNoCall(false);
                    Button_VBottomAlign.SetCheckedNoCall(false);
                    Button_VTopAlign.SetCheckedNoCall(true);
                }
                else if (sender == Button_VCenterAlign)
                {
                    cont.VerticalContentAlignment = VerticalAlignment.Center;
                    Button_VTopAlign.SetCheckedNoCall(false);
                    Button_VBottomAlign.SetCheckedNoCall(false);
                    Button_VCenterAlign.SetCheckedNoCall(true);
                }
                else if (sender == Button_VBottomAlign)
                {
                    cont.VerticalContentAlignment = VerticalAlignment.Bottom;
                    Button_VTopAlign.SetCheckedNoCall(false);
                    Button_VCenterAlign.SetCheckedNoCall(false);
                    Button_VBottomAlign.SetCheckedNoCall(true);
                }
            }
        }


    }
}
