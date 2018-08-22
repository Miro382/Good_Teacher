using Good_Teacher.Class;
using Good_Teacher.Class.Save.Output;
using Good_Teacher.Controls;
using Good_Teacher.Windows;
using Good_Teacher.Windows.Special;
using Good_Teacher_Repairo.Class;
using Good_Teacher_Repairo.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Good_Teacher_Repairo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int IMG_StackCount;
        int IMG_Current;
        bool IMG_Work = false;
        DispatcherTimer IMG_dispatcherTimer;

        double WINwidth = 1024;
        bool LoadedSettings = false;

        const string TestMakerFileOutExtension = "gtout";

        private string pathtofile = "";

        PresentationOutput output = new PresentationOutput();

        int SelectedPosition = -1;
        TestType_ControlN LastSelected;

        int CurrentPage = -1;

        public MainWindow()
        {

            if (Good_Teacher.MainWindow.appSettings.Load())
            {
                LoadedSettings = true;
            }

            if (LoadedSettings)
                Good_Teacher.MainWindow.appSettings.ApplyLanguage();

            InitializeComponent();


            DesignCanvas.EditingMode = InkCanvasEditingMode.None;


            if (LoadedSettings)
                Good_Teacher.MainWindow.appSettings.ApplyComponentData(MainWindow_Border);

            Loaded += MainWindow_Loaded;
            StateChanged += MainWindow_StateChanged;
            CheckState();

            Button_Cursor.SetCheckedNoCall(true);

            CB_PaintSize_SelectionChanged(CB_PaintSize, null);
            Button_Cursor_OnCheckChanged(Button_Cursor, true);

            ContentRendered += MainWindow_ContentRendered;
        }

        private void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            CheckVersion();
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            CheckState();
        }

        void CheckState()
        {
            if (this.WindowState == WindowState.Normal)
            {
                MaximizeButton.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Good Teacher;Component/Resources/maximize.png")),
                    VerticalAlignment = VerticalAlignment.Center
                };

                BorderThickness = new Thickness(0, 0, 0, 0);
            }
            else
            {
                MaximizeButton.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Good Teacher;Component/Resources/NormalWindow.png")),
                    VerticalAlignment = VerticalAlignment.Center
                };

                BorderThickness = new Thickness(5, 5, 5, 5);
            }
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement)Toolbar_Main.Template.FindName("OverflowGrid", Toolbar_Main)).Visibility = Visibility.Collapsed;

            if (Good_Teacher.MainWindow.OpenFileARG)
            {
                OpenFile(Good_Teacher.MainWindow.OpenFileARGPath);
            }
        }


        private void HeaderLine_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();

            if (e.ClickCount == 2)
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                    MaximizeButton.ToolTip = Good_Teacher.Strings.ResStrings.Maximize;
                }
                else
                {
                    WindowState = WindowState.Maximized;
                    MaximizeButton.ToolTip = Good_Teacher.Strings.ResStrings.RestoreDown;
                }
            }
        }


        private void HeaderLine_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
                if (WindowState == WindowState.Maximized)
                {
                    Point pointToWindow = Mouse.GetPosition(this);
                    Point pointToScreen = PointToScreen(pointToWindow);

                    Left = pointToScreen.X - WINwidth / 2;
                    Top = pointToScreen.Y;
                    WindowState = WindowState.Normal;
                    Left = pointToScreen.X - Width / 2;
                }
            }
        }


        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                MaximizeButton.ToolTip = Good_Teacher.Strings.ResStrings.Maximize;
            }
            else
            {
                WindowState = WindowState.Maximized;
                MaximizeButton.ToolTip = Good_Teacher.Strings.ResStrings.RestoreDown;
            }
        }



        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {

            if(e.Key == Key.Escape)
            {
                Button_Cursor_OnCheckChanged(Button_Cursor, true);
            }

            if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                MenuItem_Save_Click(MenuItem_Save, null);
            }
            else if (e.Key == Key.O && Keyboard.Modifiers == ModifierKeys.Control)
            {
                MenuItem_Open_Click(MenuItem_Open, null);
            }
            else if (e.Key == Key.P && Keyboard.Modifiers == ModifierKeys.Control)
            {
                MenuItem_Print_Click(MenuItem_Print, null);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WINwidth = Width;
            }
        }

        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {
            Window_AboutApp wabout = new Window_AboutApp();
            wabout.Owner = this;
            wabout.ShowDialog();
        }

        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Good_Teacher.Strings.ResStrings.CloseApp, Good_Teacher.Strings.ResStrings.Exit, System.Windows.MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void MenuItem_Print_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPosition >= 0)
            {

                PrintDialog prnt = new PrintDialog();
                prnt.UserPageRangeEnabled = true;
                prnt.MaxPage = (uint)output.Pages.Count;
                prnt.MinPage = 1;

                if (prnt.ShowDialog() == true)
                {
                    FixedDocument document = new FixedDocument();
                    document.DocumentPaginator.PageSize = new Size(
                    DesignCanvas.ActualWidth,
                    DesignCanvas.ActualHeight);

                    Debug.WriteLine("Page Range:  From: " + prnt.PageRange.PageFrom + "  To: " + prnt.PageRange.PageTo);


                    if (prnt.PageRange.PageTo < 1)
                    {
                        for (int i = 0; i < output.Pages.Count; i++)
                        {
                            PrintAddPage(i, document);
                        }
                    }
                    else
                    {
                        for (int i = (prnt.PageRange.PageFrom - 1); i < prnt.PageRange.PageTo; i++)
                        {
                            PrintAddPage(i, document);
                        }
                    }

                    prnt.PrintDocument(document.DocumentPaginator, "Good Teacher");
                }
            }
        }


        private void PrintAddPage(int i, FixedDocument document)
        {
   
            ImageSource bs = ImageLoader.GetImage(output.Pages[i].Image);
            Image image = new Image();
            image.Width = document.DocumentPaginator.PageSize.Width;
            image.Height = document.DocumentPaginator.PageSize.Height;
            image.Source = bs;


            FixedPage page = new FixedPage();
            page.Width = document.DocumentPaginator.PageSize.Width;
            page.Height = document.DocumentPaginator.PageSize.Height;

            page.Children.Clear();
            page.Children.Add(image);
            PageContent pageContent = new PageContent();
            ((IAddChild)pageContent).AddChild(page);
            document.Pages.Add(pageContent);
        }


        private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.FileName = "File." + TestMakerFileOutExtension;
            openfile.Filter = Good_Teacher.Strings.ResStrings.GoodTeacherOutFormat + "|*." + TestMakerFileOutExtension + "|" + Good_Teacher.Strings.ResStrings.AllFiles + "|*.*";

            if (openfile.ShowDialog() == true)
            {
                OpenFile(openfile.FileName);
            }
        }



        private void SliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ScrollViewerZoom.ActualWidth > 1)
            {
                if (SliderZoom.Value == 100)
                {
                    ScrollViewerZoom.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    ScrollViewerZoom.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    viewboxDC.Width = double.NaN;
                    viewboxDC.Height = double.NaN;
                }
                else if (SliderZoom.Value < 100)
                {
                    ScrollViewerZoom.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    ScrollViewerZoom.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    viewboxDC.Width = ScrollViewerZoom.ActualWidth * (SliderZoom.Value / 100);
                    viewboxDC.Height = ScrollViewerZoom.ActualHeight * (SliderZoom.Value / 100);
                }
                else
                {
                    ScrollViewerZoom.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                    ScrollViewerZoom.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                    viewboxDC.Width = ScrollViewerZoom.ActualWidth * (SliderZoom.Value / 100);
                    viewboxDC.Height = ScrollViewerZoom.ActualHeight * (SliderZoom.Value / 100);
                }

                TextBox_Zoom.Text = "" + ((int)SliderZoom.Value);
            }
        }


        private void FlatButtonZoomCancel_Click(object sender, MouseEventArgs e)
        {
            SliderZoom.Value = 100;
            ScrollViewerZoom.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            ScrollViewerZoom.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            viewboxDC.Width = double.NaN;
            viewboxDC.Height = double.NaN;
            TextBox_Zoom.Text = "100";
        }

        private void TB_Zoom_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateZoomValueByZoomTextBox();
        }


        private void TB_Zoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Enter)
            {
                UpdateZoomValueByZoomTextBox();
                Keyboard.ClearFocus();
            }
        }



        void UpdateZoomValueByZoomTextBox()
        {
            try
            {
                int zm = int.Parse(TextBox_Zoom.Text);
                SliderZoom.Value = zm;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Zoom edit: " + ex);
                TextBox_Zoom.Text = "" + SliderZoom.Value;
            }
        }

        private void AppSettings_Click(object sender, RoutedEventArgs e)
        {
            Window_AppSettings window_AppSettings = new Window_AppSettings(MainWindow_Border);
            window_AppSettings.Show();
        }



        public void OpenFile(string path)
        {
            try
            {
                
                SaveEditor save = new SaveEditor();
                output = (PresentationOutput)save.LoadWithCompressionO(path);

                pathtofile = path;

                SetWorkingFileLabel(path);

                DesignCanvas.Strokes.Clear();

                CurrentPage = -1;

                LoadFileData();

                BorderWindowSize.Width = output.W + 2;
                BorderWindowSize.Height = output.H + 2;
                
                IMG_StackCount = Stack_TestList.Children.Count;
                IMG_Work = false;
                IMG_Current = 0;
                
                IMG_dispatcherTimer = new DispatcherTimer();
                IMG_dispatcherTimer.Tick += new EventHandler(IMG_dispatcherTimer_Tick);
                IMG_dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
                IMG_dispatcherTimer.Start();
                

                //UpdateCanvasIcon();

                ScrollViewer_TestList.ScrollToHome();
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(Good_Teacher.Strings.ResStrings.ErrorLoad, Good_Teacher.Strings.ResStrings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void IMG_dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (IMG_Current >= IMG_StackCount)
            {
                IMG_dispatcherTimer.IsEnabled = false;
                IMG_Work = true;
            }

            if (!IMG_Work)
            {
                IMG_Work = true;
                if (UpdateCanvasIcon(IMG_Current))
                    IMG_Current++;

                IMG_Work = false;
            }
        }



        bool UpdateCanvasIcon(int i)
        {
            if (i < output.Pages.Count)
            {
                int toc = int.Parse( ((TestType_ControlN)Stack_TestList.Children[i]).Tag.ToString());
                ((TestType_ControlN)Stack_TestList.Children[i]).SetCanvasImage(ImageLoader.GetImageOptimal(147,84,output.Pages[toc].Image));
                return true;
            }
            return false;
        }


        void LoadFileData()
        {
            DesignCanvas.Children.Clear();
            Stack_TestList.Children.Clear();

            int k = 0;
            foreach (KeyValuePair<int, OutputPage> pair in output.Pages)
            {
                TestType_ControlN type = new TestType_ControlN(Good_Teacher.Strings.ResStrings.Page, Stack_TestList.Children.Count, false,false);
                type.Tag = pair.Key;
                type.OnClick += TestTypeCall_OnClick;
                type.ContextMenu.Visibility = Visibility.Collapsed;
                type.HoverBrush = new SolidColorBrush(Color.FromArgb(200, 121, 134, 203));
                type.SelectBrush = new SolidColorBrush(Color.FromRgb(57, 73, 171));
                type.CBorderBrush = new SolidColorBrush(Color.FromRgb(57, 73, 171));
                Stack_TestList.Children.Add(type);
                k++;
            }


            SelectedPosition = -1;

            if (Stack_TestList.Children[0] != null)
                TestTypeCall_OnClick(0, Stack_TestList.Children[0]);
        }




        private void TestTypeCall_OnClick(int Pos, object sender)
        {
            if (LastSelected != null)
                LastSelected.Select(false);

            ((TestType_ControlN)sender).Select(true);
            LastSelected = (TestType_ControlN)sender;

            SelectedPosition = Pos;

            LoadCanvas(int.Parse( ((TestType_ControlN)sender).Tag.ToString()) );
        }


        void SaveCanvas()
        {
            if (CurrentPage >= 0)
            {
                using (MemoryStream str = new MemoryStream())
                {
                    DesignCanvas.Strokes.Save(str, true);
                    output.Pages[CurrentPage].strokeCollection = str.ToArray();
                }
            }
        }



        void LoadCanvas(int can)
        {

            if(CurrentPage>=0)
            SaveCanvas();

            DesignCanvas.Children.Clear();
            ControlInputPanel.Children.Clear();
            DesignCanvas.Strokes.Clear();

            CurrentPage = can;

            if (output.Pages[can].strokeCollection != null)
            {
                using (Stream stream = new MemoryStream(output.Pages[can].strokeCollection))
                {
                    StrokeCollection strokes = new StrokeCollection(stream);
                    DesignCanvas.Strokes.Add(strokes);
                }
            }

            DesignCanvas.Background = new ImageBrush(ImageLoader.GetImage(output.Pages[can].Image));

            TextBlock pageb = new TextBlock();
            pageb.VerticalAlignment = VerticalAlignment.Center;
            pageb.TextWrapping = TextWrapping.Wrap;
            pageb.HorizontalAlignment = HorizontalAlignment.Center;
            pageb.Margin = new Thickness(0,10,0,15);
            pageb.FontWeight = FontWeights.Bold;
            pageb.Text = Good_Teacher.Strings.ResStrings.Page+": "+ (output.Pages[can].Page+1);

            ControlInputPanel.Children.Add(pageb);

            foreach (Good_Teacher.Class.Save.Output.InputType intype in output.Pages[can].InputList)
            {
                switch(intype.Type())
                {
                    case 0:
                        {
                            TextboxInput textboxInput = (TextboxInput)intype;


                            Image image = new Image();
                            image.Width = 24;
                            image.Height = 24;
                            image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Good Teacher;Component/Resources/Controls/editbox.png"));

                            TextBlock textBlock = new TextBlock();
                            textBlock.VerticalAlignment = VerticalAlignment.Center;
                            textBlock.TextWrapping = TextWrapping.Wrap;
                            textBlock.Margin = new Thickness(5, 0, 0, 0);
                            textBlock.Text = textboxInput.ID;

                            StackPanel idPanel = new StackPanel();
                            idPanel.Orientation = Orientation.Horizontal;
                            idPanel.Children.Add(image);
                            idPanel.Children.Add(textBlock);

                            idPanel.Margin = new Thickness(0, 0, 0, 5);

                            TextBox textBox = new TextBox();
                            textBox.Text = textboxInput.InputText;
                            textBox.MinHeight = 22;
                            textBox.IsReadOnly = true;
                            textBox.IsReadOnlyCaretVisible = true;

                            StackPanel stackPanel = new StackPanel();
                            stackPanel.Margin = new Thickness(0, 5, 0, 10);
                            stackPanel.Children.Add(idPanel);
                            stackPanel.Children.Add(textBox);

                            ControlInputPanel.Children.Add(stackPanel);

                            ControlInputPanel.Children.Add(new Separator() { Margin = new Thickness(0,0,0,10)});

                        }
                        break;
                    case 1:
                        {
                            CheckBoxInput Input = (CheckBoxInput)intype;


                            Image image = new Image();
                            image.Width = 24;
                            image.Height = 24;
                            image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Good Teacher;Component/Resources/Controls/Checkbox.png"));

                            TextBlock textBlock = new TextBlock();
                            textBlock.VerticalAlignment = VerticalAlignment.Center;
                            textBlock.TextWrapping = TextWrapping.Wrap;
                            textBlock.Margin = new Thickness(5, 0, 0, 0);
                            textBlock.Text = Input.ID;

                            StackPanel idPanel = new StackPanel();
                            idPanel.Orientation = Orientation.Horizontal;
                            idPanel.Children.Add(image);
                            idPanel.Children.Add(textBlock);

                            idPanel.Margin = new Thickness(0, 0, 0, 5);

                            CheckBox checkBox = new CheckBox();
                            checkBox.Content = Good_Teacher.Strings.ResStrings.IsChecked;
                            checkBox.IsChecked = Input.Check;
                            checkBox.IsEnabled = false;

                            StackPanel stackPanel = new StackPanel();
                            stackPanel.Margin = new Thickness(0, 5, 0, 10);
                            stackPanel.Children.Add(idPanel);
                            stackPanel.Children.Add(checkBox);

                            ControlInputPanel.Children.Add(stackPanel);

                            ControlInputPanel.Children.Add(new Separator() { Margin = new Thickness(0, 0, 0, 10) });

                        }
                        break;

                    case 2:
                        {
                            RadioButtonInput Input = (RadioButtonInput)intype;


                            Image image = new Image();
                            image.Width = 24;
                            image.Height = 24;
                            image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Good Teacher;Component/Resources/Controls/RadioButton.png"));

                            TextBlock textBlock = new TextBlock();
                            textBlock.VerticalAlignment = VerticalAlignment.Center;
                            textBlock.TextWrapping = TextWrapping.Wrap;
                            textBlock.Margin = new Thickness(5, 0, 0, 0);
                            textBlock.Text = Input.ID;

                            StackPanel idPanel = new StackPanel();
                            idPanel.Orientation = Orientation.Horizontal;
                            idPanel.Children.Add(image);
                            idPanel.Children.Add(textBlock);

                            idPanel.Margin = new Thickness(0, 0, 0, 5);

                            CheckBox checkBox = new CheckBox();
                            checkBox.Content = Good_Teacher.Strings.ResStrings.IsChecked;
                            checkBox.IsChecked = Input.Check;
                            checkBox.IsEnabled = false;


                            TextBlock textBlock2 = new TextBlock();
                            textBlock2.VerticalAlignment = VerticalAlignment.Center;
                            textBlock2.TextWrapping = TextWrapping.Wrap;
                            textBlock2.Text = Good_Teacher.Strings.ResStrings.GroupID + ": " +Input.RadioGroup;

                            StackPanel stackPanel = new StackPanel();
                            stackPanel.Margin = new Thickness(0, 5, 0, 10);
                            stackPanel.Children.Add(idPanel);
                            stackPanel.Children.Add(checkBox);
                            stackPanel.Children.Add(textBlock2);

                            ControlInputPanel.Children.Add(stackPanel);

                            ControlInputPanel.Children.Add(new Separator() { Margin = new Thickness(0, 0, 0, 10) });

                        }
                        break;

                    case 3:
                        {
                            ToggleButtonInput Input = (ToggleButtonInput)intype;


                            Image image = new Image();
                            image.Width = 24;
                            image.Height = 24;
                            image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Good Teacher;Component/Resources/Controls/ToggleButton.png"));

                            TextBlock textBlock = new TextBlock();
                            textBlock.VerticalAlignment = VerticalAlignment.Center;
                            textBlock.TextWrapping = TextWrapping.Wrap;
                            textBlock.Margin = new Thickness(5, 0, 0, 0);
                            textBlock.Text = Input.ID;

                            StackPanel idPanel = new StackPanel();
                            idPanel.Orientation = Orientation.Horizontal;
                            idPanel.Children.Add(image);
                            idPanel.Children.Add(textBlock);

                            idPanel.Margin = new Thickness(0, 0, 0, 5);

                            TextBlock textBlockTXT = new TextBlock();
                            textBlockTXT.VerticalAlignment = VerticalAlignment.Center;
                            textBlockTXT.TextWrapping = TextWrapping.Wrap;
                            textBlockTXT.Margin = new Thickness(0, 5, 0, 15);
                            textBlockTXT.Text = Input.Text;

                            CheckBox checkBox = new CheckBox();
                            checkBox.Content = Good_Teacher.Strings.ResStrings.IsChecked;
                            checkBox.IsChecked = Input.Check;
                            checkBox.IsEnabled = false;

                            StackPanel stackPanel = new StackPanel();
                            stackPanel.Margin = new Thickness(0, 5, 0, 10);
                            stackPanel.Children.Add(idPanel);

                            if (!String.IsNullOrWhiteSpace(Input.Text))
                            {
                                stackPanel.Children.Add(textBlockTXT);
                            }

                            stackPanel.Children.Add(checkBox);

                            ControlInputPanel.Children.Add(stackPanel);

                            ControlInputPanel.Children.Add(new Separator() { Margin = new Thickness(0, 0, 0, 10) });

                        }
                        break;
                }
            }
        }

        private void RunGoodTeacher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("Good Teacher.exe");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
            }
        }


        private void MenuItem_View_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPosition >= 0)
            {
                Window_FullView window_FullView = new Window_FullView();
                window_FullView.CanvasImage.Source = ImageLoader.GetImage(output.Pages[CurrentPage].Image);
                window_FullView.ShowDialog();
            }
        }

        private void MenuItem_Answer_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPosition >= 0)
            {
                Window_Answers window_Answers = new Window_Answers(output);
                window_Answers.Show();
            }
        }

        private void MenuItem_Info_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPosition >= 0)
            {
                Window_Info window_Info = new Window_Info(output);
                window_Info.Show();
            }
        }


        private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPosition >= 0)
            {
                if (sender == MenuItem_SaveAs || string.IsNullOrWhiteSpace(pathtofile))
                {

                    SaveEditor save = new SaveEditor();

                    SaveFileDialog savefile = new SaveFileDialog();


                    savefile.FileName = "File." + TestMakerFileOutExtension;
                    savefile.Filter = Good_Teacher.Strings.ResStrings.GoodTeacherOutFormat + "|*." + TestMakerFileOutExtension + "|" + Good_Teacher.Strings.ResStrings.AllFiles + "|*.*";

                    if (savefile.ShowDialog() == true)
                    {
                        SaveCanvas();

                        if (save.SaveWithCompressionO(savefile.FileName, output))
                        {
                            pathtofile = savefile.FileName;
                            ShowNotification(Good_Teacher.Strings.ResStrings.Saved);
                        }
                        else
                        {
                            MessageBox.Show(Good_Teacher.Strings.ResStrings.ErrorSave, Good_Teacher.Strings.ResStrings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                }
                else
                {
                    SaveEditor save = new SaveEditor();

                    SaveCanvas();

                    if (!save.SaveWithCompressionO(pathtofile, output))
                        MessageBox.Show(Good_Teacher.Strings.ResStrings.ErrorSave, Good_Teacher.Strings.ResStrings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                        ShowNotification(Good_Teacher.Strings.ResStrings.Saved);
                }
            }
        }

        private void CheckForUpdates_Click(object sender, RoutedEventArgs e)
        {
            Window_CheckForUpdates checkForUpdates = new Window_CheckForUpdates();
            checkForUpdates.Owner = this;
            checkForUpdates.ShowDialog();
        }


        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (!handle)
                return;

            if (e.Delta > 0)
                SliderZoom.Value += 10;
            else
                SliderZoom.Value -= 10;
        }

    }
}
