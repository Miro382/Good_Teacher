using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Good_Teacher.Class;
using Good_Teacher.Windows;
using Good_Teacher.Windows.Dialogs;
using Good_Teacher.Windows.Popup;
using System.Threading.Tasks;
using System.Threading;

namespace Good_Teacher.Pages.Special
{
    /// <summary>
    /// Interaction logic for Page_TextEdit.xaml
    /// </summary>
    public partial class Page_TextEdit : System.Windows.Controls.Page
    {
        object OldPage;
        RichTextBox txt;
        DispatcherTimer timer;
        Window windowM;

        public Page_TextEdit(Window window ,object oldPage ,RichTextBox text)
        {
            InitializeComponent();
            OldPage = oldPage;
            txt = text;

            txt.SelectionChanged -= Txt_SelectionChanged;
            txt.SelectionChanged += Txt_SelectionChanged;

            txt.LostFocus -= Txt_LostFocus;
            txt.LostFocus += Txt_LostFocus;

            Loaded -= Page_TextEdit_Loaded;
            Loaded += Page_TextEdit_Loaded;
            Unloaded -= Page_TextEdit_Unloaded;
            Unloaded += Page_TextEdit_Unloaded;

            windowM = window;
            window.KeyDown += new KeyEventHandler(Page_KeyDown);
        }


        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            txt.Selection.Select(txt.Document.ContentStart, txt.Document.ContentStart);
        }

        private void Page_TextEdit_Loaded(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement)Toolbar_Editor.Template.FindName("OverflowGrid", Toolbar_Editor)).Visibility = Visibility.Collapsed;

            try
            {
                ComboBox_FontName.Text = Label_font.FontFamily.ToString();

                foreach (System.Windows.Media.FontFamily fm in Fonts.SystemFontFamilies)
                {
                    Label label = new Label();
                    label.Content = fm;
                    label.FontFamily = fm;
                    label.FontSize = 14;
                    label.ToolTip = fm.ToString();
                    ComboBox_FontName.Items.Add(label);
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine("Font not found: "+ex);
            }

            Timer_Tick(null, null);


            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!txt.CanRedo)
            {
                FButton_Redo.IsEnabled = false;
                FButton_Redo.Opacity = 0.3f;
            }
            else
            {
                FButton_Redo.IsEnabled = true;
                FButton_Redo.Opacity = 1;
            }

            if (!txt.CanUndo)
            {
                FButton_Undo.IsEnabled = false;
                FButton_Undo.Opacity = 0.3f;
            }
            else
            {
                FButton_Undo.IsEnabled = true;
                FButton_Undo.Opacity = 1;
            }
        }


        private void Page_TextEdit_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }


        private void Txt_SelectionChanged(object sender, RoutedEventArgs e)
        {
                try
                {
                    if (txt.Selection.GetPropertyValue(FontSizeProperty) == DependencyProperty.UnsetValue)
                        ComboBox_FontSize.Text = "---";
                    else
                    {
                        ComboBox_FontSize.Text = "" + txt.Selection.GetPropertyValue(FontSizeProperty);
                    }

                    ComboBox_FontName.Text = txt.Selection.GetPropertyValue(FontFamilyProperty).ToString();

                    if (((FontWeight)txt.Selection.GetPropertyValue(TextElement.FontWeightProperty)) == FontWeights.Bold)
                        SButton_Bold.SetChecked(true);
                    else
                        SButton_Bold.SetChecked(false);


                    if (((System.Windows.FontStyle)txt.Selection.GetPropertyValue(TextElement.FontStyleProperty)) == FontStyles.Italic)
                        SButton_Italic.SetChecked(true);
                    else
                        SButton_Italic.SetChecked(false);

                    FontVariants fv = (FontVariants)txt.Selection.GetPropertyValue(Typography.VariantsProperty);

                    if (fv == FontVariants.Subscript)
                    {
                        SButton_SubScript.SetCheckedNoCall(true);
                        SButton_SuperScript.SetCheckedNoCall(false);
                    }
                    else if (fv == FontVariants.Superscript)
                    {
                        SButton_SuperScript.SetCheckedNoCall(true);
                        SButton_SubScript.SetCheckedNoCall(false);
                    }
                    else
                    {
                        SButton_SubScript.SetCheckedNoCall(false);
                        SButton_SuperScript.SetCheckedNoCall(false);
                    }


                    TextDecorationCollection td = (TextDecorationCollection)txt.Selection.GetPropertyValue(Inline.TextDecorationsProperty);

                    if (td == TextDecorations.Underline)
                    {
                        SButton_Underline.SetCheckedNoCall(true);
                        SButton_Strikeline.SetCheckedNoCall(false);
                    }
                    else if (td == TextDecorations.Strikethrough)
                    {
                        SButton_Underline.SetCheckedNoCall(false);
                        SButton_Strikeline.SetCheckedNoCall(true);
                    }
                    else
                    {
                        SButton_Underline.SetCheckedNoCall(false);
                        SButton_Strikeline.SetCheckedNoCall(false);
                    }


                    TextAlignment textalign = (TextAlignment)txt.Selection.GetPropertyValue(Paragraph.TextAlignmentProperty);

                    Button_LeftAlign.SetCheckedNoCall(false);
                    Button_RightAlign.SetCheckedNoCall(false);
                    Button_CenterAlign.SetCheckedNoCall(false);
                    Button_JustifyAlign.SetCheckedNoCall(false);

                    if (textalign == TextAlignment.Left)
                        Button_LeftAlign.SetCheckedNoCall(true);
                    else if (textalign == TextAlignment.Center)
                        Button_CenterAlign.SetCheckedNoCall(true);
                    else if (textalign == TextAlignment.Right)
                        Button_RightAlign.SetCheckedNoCall(true);
                    else if (textalign == TextAlignment.Justify)
                        Button_JustifyAlign.SetCheckedNoCall(true);



                    BackColor.Fill = (SolidColorBrush)txt.Selection.GetPropertyValue(Inline.BackgroundProperty);
                    ForColor.Fill = (SolidColorBrush)txt.Selection.GetPropertyValue(Inline.ForegroundProperty);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Selection Changed: " + ex);
                }
        }




        private void ComboBox_FontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txt.Selection.IsEmpty)
            {
                double size = 0;
                if (double.TryParse(ComboBox_FontSize.Text, out size))
                {
                    if (size > 3 && size < 1200)
                    {
                        txt.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, size);
                    }
                }
            }
        }


        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = OldPage;
        }

        private void ComboBox_FontName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!txt.Selection.IsEmpty)
                {
                    txt.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, ((Label)ComboBox_FontName.SelectedItem).Content);
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void SelectButton_OnCheckChanged(object sender, bool IsChecked)
        {
            if (!txt.Selection.IsEmpty)
            {
                if (sender == SButton_Bold)
                {
                    if (IsChecked)
                        txt.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                    else
                        txt.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);

                }
                else if (sender == SButton_Italic)
                {
                    if (IsChecked)
                        txt.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
                    else
                        txt.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                        
                }else if(sender == SButton_Underline)
                {
                    if (IsChecked)
                    {
                        txt.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                        SButton_Strikeline.SetCheckedNoCall(false);
                    }
                    else
                        txt.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                }
                else if (sender == SButton_Strikeline)
                {
                    if (IsChecked)
                    {
                        txt.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Strikethrough);
                         SButton_Underline.SetCheckedNoCall(false);
                    }
                    else
                        txt.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                }
                else if(sender == SButton_SubScript)
                {
                    if (IsChecked)
                    {
                        txt.Selection.ApplyPropertyValue(Typography.VariantsProperty, FontVariants.Subscript);
                        SButton_SuperScript.SetCheckedNoCall(false);
                    }
                    else
                        txt.Selection.ApplyPropertyValue(Typography.VariantsProperty, FontVariants.Normal);
                }
                else if (sender == SButton_SuperScript)
                {
                    if (IsChecked)
                    {
                        txt.Selection.ApplyPropertyValue(Typography.VariantsProperty, FontVariants.Superscript);
                        SButton_SubScript.SetCheckedNoCall(false);
                    }
                    else
                        txt.Selection.ApplyPropertyValue(Typography.VariantsProperty, FontVariants.Normal);
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

                    if (nm > 3 && nm < 1200)
                    {
                        ComboBox_FontSize.Text = "" + nm;
                    }

                    //  ComboBox_FontSize.Text = "" + nm;
                }
                else
                {
                    nm -= 2;

                    if (nm > 3 && nm < 1200)
                    {
                        ComboBox_FontSize.Text = "" + nm;
                    }
                }
            }
        }



        private void FButton_RTF_Click(object sender, MouseEventArgs e)
        {
            if(sender == FButton_RTFSave)
            {

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = "Document.rtf";
                savefile.Filter = "Rich Text Format (*.rtf)|*.rtf|"+Strings.ResStrings.AllFiles+"|*.*";

                if (savefile.ShowDialog() == true)
                {
                    RichTextBoxWorker.SaveToRTF(txt,savefile.FileName);
                }

            }
            else if(sender == FButton_RTFLoad)
            {
                OpenFileDialog openfile = new OpenFileDialog();
                openfile.Filter = "Rich Text Format (*.rtf)|*.rtf|" + Strings.ResStrings.AllFiles + "|*.*";

                if (openfile.ShowDialog() == true)
                {
                    RichTextBoxWorker.LoadFromRTF(txt,openfile.FileName);
                }

            }

        }


        private void FButton_UndoRendo_Click(object sender, MouseEventArgs e)
        {
            if(sender == FButton_Undo)
            {
                if (txt.CanUndo)
                    txt.Undo();
            }
            else if (sender == FButton_Redo)
            {
                if(txt.CanRedo)
                txt.Redo();
            }
        }




        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Z && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (txt.CanUndo)
                    txt.Undo();
            }
            else if (e.Key == Key.Y && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (txt.CanRedo)
                    txt.Redo();
            }
        }


        private void Button_Align_OnCheckChanged(object sender, bool IsChecked)
        {
            if (sender == Button_LeftAlign)
            {
                txt.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Left);
                Button_LeftAlign.SetCheckedNoCall(true);
                Button_RightAlign.SetCheckedNoCall(false);
                Button_CenterAlign.SetCheckedNoCall(false);
                Button_JustifyAlign.SetCheckedNoCall(false);
            }
            else if (sender == Button_CenterAlign)
            {
                txt.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Center);
                Button_RightAlign.SetCheckedNoCall(false);
                Button_JustifyAlign.SetCheckedNoCall(false);
                Button_LeftAlign.SetCheckedNoCall(false);
                Button_CenterAlign.SetCheckedNoCall(true);
            }
            else if (sender == Button_RightAlign)
            {
                txt.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Right);
                Button_CenterAlign.SetCheckedNoCall(false);
                Button_JustifyAlign.SetCheckedNoCall(false);
                Button_LeftAlign.SetCheckedNoCall(false);
                Button_RightAlign.SetCheckedNoCall(true);
            }
            else if (sender == Button_JustifyAlign)
            {
                txt.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Justify);
                Button_RightAlign.SetCheckedNoCall(false);
                Button_CenterAlign.SetCheckedNoCall(false);
                Button_LeftAlign.SetCheckedNoCall(false);
                Button_JustifyAlign.SetCheckedNoCall(true);
            }
        }


        private void FButton_Add_Click(object sender, MouseEventArgs e)
        {
            try
            {

                if (sender == FButton_Hyperlink)
                {
                    DWindow_AddFromWeb dhyp = new DWindow_AddFromWeb();
                    dhyp.Owner = windowM;
                    dhyp.ShowDialog();

                    if (dhyp.OK)
                    {
                        TextRange tr = new TextRange(txt.Selection.Start, txt.Selection.End);
                        Hyperlink hlink;
                        if (String.IsNullOrWhiteSpace(txt.Selection.Text))
                        {
                            hlink = new Hyperlink();
                            hlink.Inlines.Add(dhyp.Address);
                            txt.CaretPosition.Paragraph.Inlines.Add(hlink);
                        }
                        else
                            hlink = new Hyperlink(tr.Start, tr.End);

                        hlink.NavigateUri = new Uri(dhyp.Address);
                        hlink.RequestNavigate += Hlink_RequestNavigate;
                        hlink.IsEnabled = true;
                    }

                }else if(sender == FButton_AddImage)
                {


                    DWindow_AddImage aimg = new DWindow_AddImage();
                    aimg.Owner = windowM;
                    aimg.ShowDialog();

                    if (aimg.OK)
                    {
                        BitmapSource bitmap = new BitmapImage(new Uri(aimg.Path));
                        Paragraph para = new Paragraph();
                        Image image = new Image();
                        image.Source = bitmap;
                        image.Width = txt.Width;
                        para.Inlines.Add(image);
                        txt.Document.Blocks.Add(para);
                    }

                }


            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }




        private void Hlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }



        private void FButton_ColorPicker(object sender, MouseEventArgs e)
        {
            if (sender == FButton_CPickerB)
            {
                Point location = FButton_CPickerB.PointToScreen(new Point(0,0));

                PWindow_ColorPicker pcl = new PWindow_ColorPicker(true);
                pcl.Left = location.X;
                pcl.Top = location.Y + FButton_CPickerB.Height;
                pcl.obj = BackColor;
                pcl.Closing += ColorPickerPW_Closing;
                pcl.Show();
            }
            if (sender == FButton_CPickerF)
            {
                Point location = FButton_CPickerF.PointToScreen(new Point(0, 0));

                PWindow_ColorPicker pcl = new PWindow_ColorPicker();
                pcl.Left = location.X;
                pcl.Top = location.Y + FButton_CPickerF.Height;
                pcl.obj = ForColor;
                pcl.Closing += ColorPickerPW_Closing;
                pcl.Show();
            }
        }


        private void ColorPickerPW_Closing(object sender, EventArgs e)
        {
            PWindow_ColorPicker pcl = (PWindow_ColorPicker)sender;
            if (pcl.OK)
            {
                if (pcl.DestroyColorB == false)
                {
                    ((Rectangle)pcl.obj).Fill = new SolidColorBrush(pcl.color);

                    if (pcl.obj == BackColor)
                        txt.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(pcl.color));
                    else
                        txt.Selection.ApplyPropertyValue(ForegroundProperty, new SolidColorBrush(pcl.color));
                }
                else
                {
                    txt.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, null);
                }

                TextPointer textPointer1 = txt.Selection.Start;
                TextPointer textPointer2 = txt.Selection.End;
                txt.Selection.Select(txt.Document.ContentStart, txt.Document.ContentStart);

                Task.Factory.StartNew(() => Thread.Sleep(30))
                    .ContinueWith((t) =>
                    {
                        txt.Selection.Select(textPointer1, textPointer2);
                    }, TaskScheduler.FromCurrentSynchronizationContext());

            }
        }


        private void FlatButtonCustomColor_Click(object sender, MouseEventArgs e)
        {
            if(sender == FButton_CustomColorFor)
            {
                Window_ColorPicker colorp;

                if ((SolidColorBrush)ForColor.Fill == null)
                    colorp = new Window_ColorPicker(Colors.White);
                else
                    colorp = new Window_ColorPicker(((SolidColorBrush)ForColor.Fill).Color);

                colorp.Owner = Window.GetWindow(this);
                colorp.ShowDialog();
                if (colorp.IsOK() == true)
                {
                    ForColor.Fill = new SolidColorBrush(colorp.GetColor());
                    txt.Selection.ApplyPropertyValue(Inline.ForegroundProperty, new SolidColorBrush(colorp.GetColor()));
                }
            }
            else if(sender == FButton_CustomColorBack)
            {
                Window_ColorPicker colorp;

                if( (SolidColorBrush)BackColor.Fill == null )
                colorp = new Window_ColorPicker(Colors.White);
                else
                colorp = new Window_ColorPicker(((SolidColorBrush)BackColor.Fill).Color);

                colorp.Owner = Window.GetWindow(this);
                colorp.ShowDialog();
                if (colorp.IsOK() == true)
                {
                    BackColor.Fill = new SolidColorBrush(colorp.GetColor());
                    txt.Selection.ApplyPropertyValue(Inline.BackgroundProperty, new SolidColorBrush(colorp.GetColor()));
                }
            }
        }


    }
}
