using Good_Teacher.Class.Serialization.Content_Ser;
using Good_Teacher.Controls;
using Good_Teacher.Pages.Content;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Good_Teacher.Windows
{
    /// <summary>
    /// Interaction logic for DWindow_Content.xaml
    /// </summary>
    public partial class DWindow_Content : Window
    {
        SelectButton lastChecked;

        public ContentCreator content = new ContentCreator();

        DataStore data;

        public bool IsOK = false;


        public DWindow_Content(DataStore dataStore,ContentCreator contentCreator)
        {
            InitializeComponent();

            data = dataStore;

            content.contents = contentCreator.contents.ToList();

            Debug.WriteLine("Count: "+content.contents.Count);

            foreach (Content_Default conD in content.contents)
            {
                SelectButton button = new SelectButton()
                {
                    MinWidth = 10,
                    MinHeight = 10,
                    Background = new SolidColorBrush(Color.FromRgb(236, 240, 241)),
                    DefaultBrush = new SolidColorBrush(Color.FromRgb(236, 240, 241)),
                    Hover = new SolidColorBrush(Color.FromArgb(60, 233, 30, 99)),
                    OnChecked = new SolidColorBrush(Color.FromRgb(240, 98, 146))
                };

                button.VerticalContentAlignment = VerticalAlignment.Center;

                if (conD.ContentType() == 0)
                {
                    button.Content = new Image()
                    {
                        Source = data.archive.GetImage(((Content_Image)conD).ImageKey),
                        Stretch = ((Content_Image)conD).stretch
                    };

                    RenderOptions.SetBitmapScalingMode(button, ((Content_Image)conD).scalingMode);

                    button.Width = ((Content_Image)conD).W;
                    button.Height = ((Content_Image)conD).H;
                    button.Margin = new Thickness(((Content_Image)conD).MarginLeft, 0, 0, 0);
                    button.Tag = ((Content_Image)conD);
                }
                else if (conD.ContentType() == 1)
                {
                    Content_Text text = (Content_Text)conD;
                    button.Content = text.text;
                    button.Margin = new Thickness(text.MarginLeft,0,0,0);
                    button.FontSize = text.fontsize;
                    button.Tag = text;
                    button.Foreground = text.foreground.DeserializeToBrushWithKey(data);
                    if(text.fontFamily!=null)
                    button.FontFamily = text.fontFamily;

                    if (text.Bold)
                        button.FontWeight = FontWeights.Bold;

                    if (text.Italic)
                        button.FontStyle = FontStyles.Italic;
                }
                else if (conD.ContentType() == 2)
                {
                    Content_Splitter spl = (Content_Splitter)conD;
                    button.Margin = new Thickness(spl.MarginLeft, 0, 0, 0);
                    button.Tag = spl;
                    button.Content = new Rectangle()
                    {
                        Width = spl.W,
                        Height = 1000,
                        Fill = spl.fill.DeserializeToBrushWithKey(data)
                    };
                }
                else if (conD.ContentType() == 3)
                {
                    Content_PageNumber acontent = (Content_PageNumber)conD;
                    button.Content = (MainWindow.ActualPage + 1) - acontent.SubtractPages;
                    button.Margin = new Thickness(acontent.MarginLeft, 0, 0, 0);
                    button.FontSize = acontent.fontsize;
                    button.Tag = acontent;
                    button.Foreground = acontent.foreground.DeserializeToBrushWithKey(data);
                    if (acontent.fontFamily != null)
                        button.FontFamily = acontent.fontFamily;

                    if (acontent.Bold)
                        button.FontWeight = FontWeights.Bold;

                    if (acontent.Italic)
                        button.FontStyle = FontStyles.Italic;
                }
                else if (conD.ContentType() == 4)
                {
                    Content_Answers acontent = (Content_Answers)conD;
                    button.Content = "A";
                    button.Margin = new Thickness(acontent.MarginLeft, 0, 0, 0);
                    button.FontSize = acontent.fontsize;
                    button.Tag = acontent;
                    button.Foreground = acontent.foreground.DeserializeToBrushWithKey(data);
                    if (acontent.fontFamily != null)
                        button.FontFamily = acontent.fontFamily;

                    if (acontent.Bold)
                        button.FontWeight = FontWeights.Bold;

                    if (acontent.Italic)
                        button.FontStyle = FontStyles.Italic;
                }
                else if (conD.ContentType() == 5)
                {
                    Content_Date acontent = (Content_Date)conD;
                    button.Content = acontent.Create(data).Content;
                    button.Margin = new Thickness(acontent.MarginLeft, 0, 0, 0);
                    button.FontSize = acontent.fontsize;
                    button.Tag = acontent;
                    button.Foreground = acontent.foreground.DeserializeToBrushWithKey(data);
                    if (acontent.fontFamily != null)
                        button.FontFamily = acontent.fontFamily;

                    if (acontent.Bold)
                        button.FontWeight = FontWeights.Bold;

                    if (acontent.Italic)
                        button.FontStyle = FontStyles.Italic;
                }

                ContentPanel.Children.Add(button);

                button.OnCheckChanged += Button_OnCheckChanged;

            }

            if(ContentPanel.Children.Count>0)
            ((SelectButton)ContentPanel.Children[0]).SetChecked(true);
        }


        private void ButtonAddImage_Click(object sender, RoutedEventArgs e)
        {
            SelectButton button = new SelectButton()
            {
                MinWidth = 10,
                MinHeight = 10,
                DefaultBrush = new SolidColorBrush(Color.FromRgb(236, 240, 241)),
                Hover = new SolidColorBrush(Color.FromArgb(60,233, 30, 99)),
                OnChecked = new SolidColorBrush(Color.FromRgb(240, 98, 146))
            };

            button.Content = new Image()
            {
                Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Controls/image.png")),
            };
            ContentPanel.Children.Add(button);

            if (lastChecked != null)
                lastChecked.SetCheckedNoCall(false);

            Content_Image content_Image = new Content_Image();
            content.contents.Add(content_Image);

            button.Tag = content_Image;

            button.OnCheckChanged += Button_OnCheckChanged;

            button.SetChecked(true);

            lastChecked = button;
        }

        private void ButtonAddText_Click(object sender, RoutedEventArgs e)
        {
            SelectButton button = new SelectButton()
            {
                DefaultBrush = new SolidColorBrush(Color.FromRgb(236, 240, 241)),
                Hover = new SolidColorBrush(Color.FromArgb(60, 233, 30, 99)),
                OnChecked = new SolidColorBrush(Color.FromRgb(240, 98, 146))
            };

            button.Content = Strings.ResStrings.Text;
            button.FontSize = 20;
            button.VerticalContentAlignment = VerticalAlignment.Center;
            ContentPanel.Children.Add(button);

            if (lastChecked != null)
                lastChecked.SetCheckedNoCall(false);


            Content_Text content_Text = new Content_Text();
            content.contents.Add(content_Text);

            button.Tag = content_Text;

            button.OnCheckChanged += Button_OnCheckChanged;

            button.SetChecked(true);

            lastChecked = button;
        }


        private void ButtonAddSplitter_Click(object sender, RoutedEventArgs e)
        {
            SelectButton button = new SelectButton()
            {
                MinWidth = 10,
                MinHeight = 10,
                DefaultBrush = new SolidColorBrush(Color.FromRgb(236, 240, 241)),
                Hover = new SolidColorBrush(Color.FromArgb(60, 233, 30, 99)),
                OnChecked = new SolidColorBrush(Color.FromRgb(240, 98, 146))
            };

            button.Content = new Rectangle()
            {
                Fill = new SolidColorBrush(Colors.Black),
                Width = 2,
                Height = Double.NaN
            };
            ContentPanel.Children.Add(button);

            if (lastChecked != null)
                lastChecked.SetCheckedNoCall(false);

            Content_Splitter content_Spl = new Content_Splitter();
            content.contents.Add(content_Spl);

            button.Tag = content_Spl;

            button.OnCheckChanged += Button_OnCheckChanged;

            button.SetChecked(true);

            lastChecked = button;
        }


        private void ButtonAddPageNumber_Click(object sender, RoutedEventArgs e)
        {
            SelectButton button = new SelectButton()
            {
                DefaultBrush = new SolidColorBrush(Color.FromRgb(236, 240, 241)),
                Hover = new SolidColorBrush(Color.FromArgb(60, 233, 30, 99)),
                OnChecked = new SolidColorBrush(Color.FromRgb(240, 98, 146))
            };

            button.Content = "#";
            button.FontSize = 20;
            button.VerticalContentAlignment = VerticalAlignment.Center;
            ContentPanel.Children.Add(button);

            if (lastChecked != null)
                lastChecked.SetCheckedNoCall(false);


            Content_PageNumber Acontent = new Content_PageNumber();
            content.contents.Add(Acontent);

            button.Tag = Acontent;

            button.OnCheckChanged += Button_OnCheckChanged;

            button.SetChecked(true);

            lastChecked = button;
        }


        private void ButtonAddAnswersCount_Click(object sender, RoutedEventArgs e)
        {
            SelectButton button = new SelectButton()
            {
                DefaultBrush = new SolidColorBrush(Color.FromRgb(236, 240, 241)),
                Hover = new SolidColorBrush(Color.FromArgb(60, 233, 30, 99)),
                OnChecked = new SolidColorBrush(Color.FromRgb(240, 98, 146))
            };

            button.Content = "A";
            button.FontSize = 20;
            button.VerticalContentAlignment = VerticalAlignment.Center;
            ContentPanel.Children.Add(button);

            if (lastChecked != null)
                lastChecked.SetCheckedNoCall(false);

            Content_Answers Acontent = new Content_Answers();
            content.contents.Add(Acontent);

            button.Tag = Acontent;

            button.OnCheckChanged += Button_OnCheckChanged;

            button.SetChecked(true);

            lastChecked = button;
        }


        private void ButtonAddDateCount_Click(object sender, RoutedEventArgs e)
        {
            SelectButton button = new SelectButton()
            {
                DefaultBrush = new SolidColorBrush(Color.FromRgb(236, 240, 241)),
                Hover = new SolidColorBrush(Color.FromArgb(60, 233, 30, 99)),
                OnChecked = new SolidColorBrush(Color.FromRgb(240, 98, 146))
            };

            button.Content = "D";
            button.FontSize = 20;
            button.VerticalContentAlignment = VerticalAlignment.Center;
            ContentPanel.Children.Add(button);

            if (lastChecked != null)
                lastChecked.SetCheckedNoCall(false);

            Content_Date Acontent = new Content_Date();
            content.contents.Add(Acontent);

            button.Tag = Acontent;

            button.OnCheckChanged += Button_OnCheckChanged;

            button.SetChecked(true);

            lastChecked = button;
        }

        private void Button_OnCheckChanged(object sender, bool IsChecked)
        {
            if (lastChecked != null && lastChecked!=sender)
                lastChecked.SetCheckedNoCall(false);

            SelectButton ths = (SelectButton)sender;

            ths.SetCheckedNoCall(true);

            lastChecked = ths;

            if (ths != null)
            {

                if (((Content_Default)ths.Tag).ContentType() == 0)
                {
                    editor.Content = new ContentEditor_Image(data, ths);
                }
                else if (((Content_Default)ths.Tag).ContentType() == 1)
                {
                    editor.Content = new ContentEditor_Text(data, ths);
                }
                else if (((Content_Default)ths.Tag).ContentType() == 2)
                {
                    editor.Content = new ContentEditor_Splitter(data, ths);
                }
                else if (((Content_Default)ths.Tag).ContentType() == 3)
                {
                    editor.Content = new ContentEditor_PageNumber(data, ths);
                }
                else if (((Content_Default)ths.Tag).ContentType() == 4)
                {
                    editor.Content = new ContentEditor_Answers(data, ths);
                }
                else if (((Content_Default)ths.Tag).ContentType() == 5)
                {
                    editor.Content = new ContentEditor_Date(data, ths);
                }
            }
        }



        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            IsOK = true;
            Close();
        }


        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lastChecked != null)
            {
                content.contents.Remove(((Content_Default)lastChecked.Tag));
                ContentPanel.Children.Remove(lastChecked);
                lastChecked = null;
                editor.Content = "";
            }
        }


        private void ButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedIndex = ContentPanel.Children.IndexOf(lastChecked);
                if (selectedIndex - 1 >= 0)
                {
                    content.contents.Remove(((Content_Default)lastChecked.Tag));
                    content.contents.Insert(selectedIndex - 1, ((Content_Default)lastChecked.Tag));

                    ContentPanel.Children.RemoveAt(selectedIndex);
                    ContentPanel.Children.Insert(selectedIndex - 1, lastChecked);
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


        private void ButtonRight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedIndex = ContentPanel.Children.IndexOf(lastChecked);
                if (selectedIndex + 1 < ContentPanel.Children.Count)
                {
                    content.contents.Remove(((Content_Default)lastChecked.Tag));
                    content.contents.Insert(selectedIndex + 1, ((Content_Default)lastChecked.Tag));

                    ContentPanel.Children.RemoveAt(selectedIndex);
                    ContentPanel.Children.Insert(selectedIndex + 1, lastChecked);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


    }
}
