using Good_Teacher.Class;
using Good_Teacher.Class.History;
using Good_Teacher.Class.Save;
using Good_Teacher.Class.Workers;
using Good_Teacher.Controls;
using Good_Teacher.Pages.Pages;
using Good_Teacher.Pages.Special;
using Good_Teacher.Windows;
using Good_Teacher.Windows.Dialogs;
using Good_Teacher.Windows.Popup;
using Good_Teacher.Windows.Special;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Good_Teacher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public EventList<HistoryCommand> HistoryUndo = new EventList<HistoryCommand>();
        public List<HistoryCommand> HistoryRedo = new List<HistoryCommand>();

        /// <summary>
        /// Good Teacher Version Code
        /// </summary>
        public const float VersionCode = 0.94f;

        /// <summary>
        /// Text Version code
        /// </summary>
        public const string VersionSpecialIdentificationName = "Beta REV 13";

        /// <summary>
        /// Good Teacher Home Web URL
        /// </summary>
        public const string HomeWebURL = "http://goodteacher.diodegames.eu/";

        /// <summary>
        /// is in optimized mode? if true images will be resized to required size
        /// </summary>
        public static bool OPTIMIZEDMODE = false;

        public static string OpenFileARGPath = "";
        public static bool OpenFileARG = false;

        public static uint GridSize = 10;

        public static double CanvasW = 1280, CanvasH = 720;

        public static uint HistoryLimit = 30;

        /// <summary>
        /// Size impact of points for scaling
        /// </summary>
        public static float ControlAreaSize = 0f;

        public static AppSettings appSettings = new AppSettings();

        /// <summary>
        /// Is something in presentation changed? For unsaved changes dialog
        /// </summary>
        public static bool IsChanged = false;

        /// <summary>
        /// Actual page of presentation
        /// </summary>
        public static int ActualPage = 0;

        /// <summary>
        /// Count of good answered questions
        /// </summary>
        public static int GoodAnswersCount = 0;

        /// <summary>
        /// Count of wrong answered questions
        /// </summary>
        public static int WrongAnswersCount = 0;

        /// <summary>
        /// Is running presentation?
        /// </summary>
        public static bool InPresentationMode = false;
        
        /// <summary>
        /// Save current page while in presentation mode
        /// </summary>
        public static int PresentationModeSaveStatePage = 0;

        /// <summary>
        /// Maximum limit of screen
        /// </summary>
        public const int LimitScreenMaxX = 1000000, LimitScreenMinX = -1000000, LimitScreenMaxY = 1000000, LimitScreenMinY = -1000000, LimitSize = 100000, TextLimitSize=10000;
        const int CanvasImgX = 145, CanvasImgY = 82;

        /// <summary>
        /// Extension of Good Teacher (gtch)
        /// </summary>
        const string TestMakerFileExtension = "gtch";


        double WINwidth = 1024;


        bool BMoveOnGrid = false;

        DataStore data = new DataStore();

        /// <summary>
        /// Last selected TestType Button
        /// </summary>
        TestType_ControlN LastSelected;

        /// <summary>
        /// Current selected page position
        /// </summary>
        int SelectedPosition = -1;


        /// <summary>
        /// Selected editing unit (px, cm, pt...)
        /// </summary>
        public static int CUnit = 0;

        object CopyObject = null;

        /// <summary>
        /// Add control mode
        /// </summary>
        bool ControlSelect = false;

        /// <summary>
        /// Add control mode tag to identify control ID
        /// </summary>
        int ControlTag = 0;

        /// <summary>
        /// Path to opened presentation
        /// </summary>
        public static string pathtofile = "";

        DispatcherTimer timer;

        bool LoadedSettings = false;


        public MainWindow()
        {
            if (appSettings.Load())
            {
                LoadedSettings = true;
            }

            if (LoadedSettings)
                appSettings.ApplyLanguage();

            InitializeComponent();

            if (LoadedSettings)
            appSettings.ApplyComponentData(MainWindow_Border);

            Loaded += MainWindow_Loaded;
            StateChanged += MainWindow_StateChanged;
            ContentRendered += MainWindow_ContentRendered;

            HistoryUndo.OnAdd += HistoryUndo_OnAdd;
        }

        private void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            CheckVersion();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CheckState();
            ((FrameworkElement)Toolbar_Main.Template.FindName("OverflowGrid", Toolbar_Main)).Visibility = Visibility.Collapsed;
            //PreviewKeyDown += MainWindow_KeyDown;

            BorderWindowSize.Visibility = Visibility.Collapsed;

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 5);
            timer.Start();

            FontWorker.RemoveTemporaryFolder();

            TempFilesWorker.RemoveUpdateFolder();

            if(OpenFileARG)
            {
                OpenFile(OpenFileARGPath);
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (SelectedPosition >= 0)
            {
                try
                {
                    UpdateCurrentCanvasIcon();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

        }

        void NoAddControlsMouse()
        {
            Mouse.OverrideCursor = null;
            ControlSelect = false;
        }


        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Escape)
            {
                if (ControlSelect)
                {
                    NoAddControlsMouse();
                }

                if (OldPage != null)
                {
                    FrameEditor.Content = OldPage;
                    OldPage = null;
                }

                ReleaseMouseCapture();

                UnselectControl();
            }

            if (e.Key == Key.F5 && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                Debug.WriteLine("Pressed F5 - Play Mode Now");
                NoAddControlsMouse();
                Item_RunNow_Click(null, null);
            }
            else if (e.Key == Key.F5)
            {
                Debug.WriteLine("Pressed F5 - Play Mode");
                NoAddControlsMouse();
                Item_Run_Click(null, null);
            }
            else if(e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                MenuItem_Save_Click(MenuItem_Save, null);
            }
            else if (e.Key == Key.O && Keyboard.Modifiers == ModifierKeys.Control)
            {
                MenuItem_Open_Click(MenuItem_Open, null);
            }
            else if (e.Key == Key.N && Keyboard.Modifiers == ModifierKeys.Control)
            {
                MenuItem_New_Click(MenuItem_New, null);
            }
            else if (e.Key == Key.P && Keyboard.Modifiers == ModifierKeys.Control)
            {
                MenuItem_Print_Click(MenuItem_Print, null);
            }
            else if (e.Key == Key.F11)
            {
                Item_RealView_Click(null, null);
            }
            else if (e.Key == Key.F1)
            {
                Item_Help_Click(null, null);
            }



            if (e.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                IInputElement focusedControl = Keyboard.FocusedElement;

                if (!(focusedControl is TextBoxBase))
                {
                    Debug.WriteLine("CTRL + C pressed - COPY");
                    Copy();
                }
            }

            if (e.Key == Key.X && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                IInputElement focusedControl = Keyboard.FocusedElement;

                if (!(focusedControl is TextBoxBase))
                {
                    Debug.WriteLine("CTRL + X pressed - CUT");
                    Cut();
                }
            }

            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                IInputElement focusedControl = Keyboard.FocusedElement;

                if (!(focusedControl is TextBoxBase))
                {
                    Debug.WriteLine("CTRL + V pressed - Paste");
                    Paste();
                }
            }


            if (e.Key == Key.Z && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                Item_Undo_Click(null, null);
            }

            if (e.Key == Key.Y && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                Item_Redo_Click(null, null);
            }


            if(e.Key == Key.Right && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                ScrollViewerZoom.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                ScrollViewerZoom.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                viewboxDC.Margin = new Thickness(viewboxDC.Margin.Left+20, viewboxDC.Margin.Top, 0, viewboxDC.Margin.Bottom);
                B_DefaultCanvasPosition.Visibility = Visibility.Visible;
            }
            else if (e.Key == Key.Left && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                ScrollViewerZoom.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                ScrollViewerZoom.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                viewboxDC.Margin = new Thickness(0 , viewboxDC.Margin.Top, viewboxDC.Margin.Right+20, viewboxDC.Margin.Bottom);
                B_DefaultCanvasPosition.Visibility = Visibility.Visible;
            }
            else if (e.Key == Key.Up && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                ScrollViewerZoom.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                ScrollViewerZoom.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                viewboxDC.Margin = new Thickness(viewboxDC.Margin.Left, 0, viewboxDC.Margin.Right, viewboxDC.Margin.Bottom+20);
                B_DefaultCanvasPosition.Visibility = Visibility.Visible;
            }
            else if (e.Key == Key.Down && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                ScrollViewerZoom.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                ScrollViewerZoom.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                viewboxDC.Margin = new Thickness(viewboxDC.Margin.Left, viewboxDC.Margin.Top + 20, viewboxDC.Margin.Right, 0);
                B_DefaultCanvasPosition.Visibility = Visibility.Visible;
            }


            if(e.Key == Key.PageDown)
            {
                if (SelectedPosition + 1 < data.pages.Count)
                {
                    TestTypeCall_OnClick(SelectedPosition + 1, (TestType_ControlN)Stack_TestList.Children[SelectedPosition + 1]);
                }
            }
            else if (e.Key == Key.PageUp)
            {
                if (SelectedPosition > 0)
                {
                    TestTypeCall_OnClick(SelectedPosition - 1, (TestType_ControlN)Stack_TestList.Children[SelectedPosition - 1]);
                }
            }


            if (e.Key == Key.Delete && MSelectedItemEffect != null && SelectedAreaElements.Count > 0)
            {
                Debug.WriteLine("Delete");

                foreach (FrameworkElement elm in SelectedAreaElements)
                {
                    DesignCanvas.Children.Remove(elm);
                }
                ValueEditor.Content = "";
                RemoveSelectedItemEffect();
                SelectedAreaElements.Clear();
                MainWindow.IsChanged = true;
            }

            if (SelectedControl != null && e.Key == Key.Delete)
            {
                Debug.WriteLine("Delete");

                DesignCanvas.Children.Remove((FrameworkElement)SelectedControl);
                ValueEditor.Content = "";
                RemoveSelectedItemEffect();
                MainWindow.IsChanged = true;
            }

        }


        private void FlatButtonDefaultCanvasPosition_Click(object sender, MouseEventArgs e)
        {
            viewboxDC.Margin = new Thickness(0, 0, 0, 0);
            B_DefaultCanvasPosition.Visibility = Visibility.Collapsed;
            ScrollViewerZoom.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            ScrollViewerZoom.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            SliderZoom_ValueChanged(SliderZoom,null);
        }


        FrameworkElement CreateCopyObject(object ToCopy,int add)
        {
            FrameworkElement copy = (FrameworkElement)ClassWorker.CloneObject(ToCopy);

            Canvas.SetLeft(copy, Canvas.GetLeft((UIElement)ToCopy) + add);
            Canvas.SetTop(copy, Canvas.GetTop((UIElement)ToCopy) + add);
            Panel.SetZIndex(copy, Panel.GetZIndex((UIElement)ToCopy));

            AddEvents(copy);

            /*
            string guid = Guid.NewGuid().ToString();
            guid = guid.Replace("-", "");
            guid = "G" + guid;
            */

            return copy;
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            CheckState();
            SCRV_MenuOptions.ScrollToHorizontalOffset(0);
        }


        private void HeaderLine_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();

            if (e.ClickCount == 2)
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                    MaximizeButton.ToolTip = Strings.ResStrings.Maximize;
                }
                else
                {
                    WindowState = WindowState.Maximized;
                    MaximizeButton.ToolTip = Strings.ResStrings.RestoreDown;
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
            CloseApplication();
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
                MaximizeButton.ToolTip = Strings.ResStrings.Maximize;
            }
            else
            {
                WindowState = WindowState.Maximized;
                MaximizeButton.ToolTip = Strings.ResStrings.RestoreDown;
            }
        }


        void UpdateAllStackListPosition()
        {
            for (int i = 0; i < Stack_TestList.Children.Count; i++)
            {
                ((TestType_ControlN)Stack_TestList.Children[i]).SetPosition(i);
            }
        }


        void UpdateAllDataPosition()
        {
            for(int i=0;i<data.pages.Count;i++)
            {
                data.pages[i].Position = i;
            }
        }



        private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (!(FrameEditor.Content is Page_TextEdit))
            {
                ((Page_Interface)FrameEditor.Content).AddControlEvent -= Page_AddControlEvent;
                ((Page_Interface)FrameEditor.Content).AddControlEvent += Page_AddControlEvent;
            }
        }


        private void Item_ControlList_Click(object sender, RoutedEventArgs e)
        {
           string txt = "";
            /*
            if (CurrSelected == TestTypeID.Test_Type.Form)
                txt = Strings.ResStrings.Page;
                */

            txt = Strings.ResStrings.Page;

            txt += " (" + (SelectedPosition+1) +")";

            Window_AllControls windowall = new Window_AllControls(DesignCanvas,txt);
            windowall.ClickControl -= Windowall_ClickControl;
            windowall.ClickControl += Windowall_ClickControl;
            windowall.ShowDialog();
        }

        private void Windowall_ClickControl(object obj)
        {
            DesignControl_MouseDown(obj,null);
        }



        private void Item_Settings_Click(object sender, RoutedEventArgs e)
        {
            Window_Settings Wsettings = new Window_Settings(DesignCanvas, data,SelectedPosition, BorderWindowSize);
            Wsettings.Owner = App.Current.MainWindow;
            Wsettings.ShowDialog();
        }


        private void MenuItem_New_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Strings.ResStrings.NewProject, Strings.ResStrings.New, System.Windows.MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                BorderWindowSize.Visibility = Visibility.Collapsed;
                HistoryRedo.Clear();
                HistoryUndo.Clear();
                SelectedPosition = -1;
                SelectedControl = null;
                ValueEditor.Content = "";
                data = new DataStore();
                RemoveUnloadEvent();
                DesignCanvas.Children.Clear();
                Stack_TestList.Children.Clear();
                DesignCanvas.Background = new SolidColorBrush(Colors.White);
                pathtofile = "";
                BorderWindowSize.Width = 1282;
                BorderWindowSize.Height = 722;
                L_FileName.Content = "";
                FontWorker.RemoveTemporaryFolder();
                MainWindow.IsChanged = false;
            }
        }


        private void MenuItem_Print_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPosition >= 0)
            {
                SaveCanvas();
                LoadCanvas();

                Window_PrintPreview printPreview = new Window_PrintPreview(data);
                printPreview.Owner = this;
                printPreview.ShowDialog();
            }
        }

        private void CanvasOnlyButton_OnCheckChanged(object sender, bool IsChecked)
        {
            DesignCanvas.ClipToBounds = IsChecked;
        }

        private void Item_Build_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            if (LocalPath.GetDirectoryPath(out path))
            {
                using (System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog())
                {

                    dialog.ShowNewFolderButton = true;
                    dialog.Description = Strings.ResStrings.ExportToFolder;
                    System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {
                            SaveCanvas();
                            LoadCanvas();

                            SaveEditor save = new SaveEditor();

                            if (save.SaveWithCompression(dialog.SelectedPath+"/Data.gtchp", data))
                            {

                                if(Directory.Exists(path + "/Resources/"))
                                LocalPath.CopyDirectory(LocalPath.GetResourcesPath(), dialog.SelectedPath + "/Resources/");

                                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "Data/Good Teacher Presentation.exe", dialog.SelectedPath + "/Good Teacher Presentation.exe");

                                MessageBox.Show(Strings.ResStrings.SuccessExport + " " + dialog.SelectedPath, Strings.ResStrings.Success);
                            }
                            else
                            {
                                MessageBox.Show(Strings.ResStrings.FailedExport + " " + dialog.SelectedPath, Strings.ResStrings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(""+ex);
                            MessageBox.Show(Strings.ResStrings.FailedExport + " " + dialog.SelectedPath, Strings.ResStrings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                //LocalPath.CopyDirectory
            }
            else
            {
                MessageBox.Show(Strings.ResStrings.NotSaved, Strings.ResStrings.NotSavedTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = null;
            ControlSelect = false;
        }


        private void Item_Animations_Click(object sender, RoutedEventArgs e)
        {
            if (data.pages.Count > 0)
            {
                DWindow_Animations dWindow_Animations = new DWindow_Animations(SelectedPosition, DesignCanvas, data, SelectedControl);
                dWindow_Animations.Owner = this;

                dWindow_Animations.ClickControl -= Windowall_ClickControl;
                dWindow_Animations.ClickControl += Windowall_ClickControl;

                dWindow_Animations.ShowDialog();
            }
        }


        private void AppSettings_Click(object sender, RoutedEventArgs e)
        {
            Window_AppSettings window_AppSettings = new Window_AppSettings(MainWindow_Border);
            window_AppSettings.Show();
        }


        private void RunRepairo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("Good Teacher Repairo.exe");
            }catch(Exception ex)
            {
                Debug.WriteLine(""+ex);
            }
        }

        private void MoveOnGrid_OnCheckChanged(object sender, bool IsChecked)
        {
            BMoveOnGrid = MoveOnGrid.IsChecked();
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (!handle)
                return;

            if(e.Delta>0)
                SliderZoom.Value += 10;
            else
                SliderZoom.Value -= 10;
                
        }

        private void ScrollViewerZoom_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;

            if (handle)
            {
                e.Handled = handle;
                return;
            }
        }

        private void CheckForUpdates_Click(object sender, RoutedEventArgs e)
        {
            Window_CheckForUpdates checkForUpdates = new Window_CheckForUpdates();
            checkForUpdates.Owner = this;
            checkForUpdates.ShowDialog();
        }

        private void Item_Repeating_Click(object sender, RoutedEventArgs e)
        {
            if (data.pages.Count > 0)
            {
                Window_Repeating window_Repeating = new Window_Repeating(data);
                window_Repeating.Owner = this;
                window_Repeating.ShowDialog();
            }
        }

        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {
            Window_AboutApp wabout = new Window_AboutApp();
            wabout.Owner = this;
            wabout.ShowDialog();
        }

        private void Item_FileInfo_Click(object sender, RoutedEventArgs e)
        {
            Window_FileInformations window_FileInformations = new Window_FileInformations(data, pathtofile);
            window_FileInformations.Owner = this;
            window_FileInformations.ShowDialog();
        }

        private void Item_RealView_Click(object sender, RoutedEventArgs e)
        {
            if (data.pages.Count > 0 && SelectedPosition >=0)
            {
                SaveCanvas();
                LoadCanvas();

                Window_RealView window_RealView = new Window_RealView(data, SelectedPosition);
                window_RealView.Owner = this;
                window_RealView.ShowDialog();
            }
        }

        private void Item_Help_Click(object sender, RoutedEventArgs e)
        {
            Window_Manual window_Manual = new Window_Manual();
            window_Manual.Owner = this;
            window_Manual.Show();
        }

        private void Item_Script_Click(object sender, RoutedEventArgs e)
        {
            if (data.pages.Count > 0 && SelectedPosition >= 0)
            {
                Window_CodeEditor window_CodeEditor = new Window_CodeEditor(data, DesignCanvas, SelectedPosition);
                window_CodeEditor.Owner = this;
                window_CodeEditor.ShowDialog();
            }
        }

        private void ImageButtonNextActions_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine(SCRV_MenuOptions.HorizontalOffset);
            SCRV_MenuOptions.ScrollToHorizontalOffset(SCRV_MenuOptions.HorizontalOffset + 300);

            CheckActionButtons(300);
        }

        private void ImageButtonPreviousActions_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine(SCRV_MenuOptions.HorizontalOffset);
            SCRV_MenuOptions.ScrollToHorizontalOffset(SCRV_MenuOptions.HorizontalOffset - 300);

            CheckActionButtons(-300);
        }

        private void Item_FontManager_Click(object sender, RoutedEventArgs e)
        {
            Window_FontManager window_FontManager = new Window_FontManager(data);
            window_FontManager.Owner = this;
            window_FontManager.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FontWorker.RemoveTemporaryFolder();
        }

        private void Item_Transition_Click(object sender, RoutedEventArgs e)
        {
            if (data.pages.Count > 0)
            {
                Window_Transitions window_Transitions = new Window_Transitions(ActualPage, data);
                window_Transitions.Owner = this;
                window_Transitions.ShowDialog();
            }
        }

        private void Item_Cut_Click(object sender, RoutedEventArgs e)
        {
            Cut();
        }

        private void Item_Copy_Click(object sender, RoutedEventArgs e)
        {
            Copy();
        }

        private void Item_Paste_Click(object sender, RoutedEventArgs e)
        {
            Paste();
        }

        private void Item_Timers_Click(object sender, RoutedEventArgs e)
        {
            if (data.pages.Count > 0 && SelectedPosition >= 0)
            {
                Window_Timers window_Timers = new Window_Timers(data, SelectedPosition);
                window_Timers.Owner = this;
                window_Timers.ShowDialog();
            }
        }

        private void ExportToVideo_Click(object sender, RoutedEventArgs e)
        {
            if (data.pages.Count > 0 && SelectedPosition >= 0)
            {
                SaveCanvas();
                LoadCanvas();

                Window_ExportToVideo window_ExportToVideo = new Window_ExportToVideo(data);
                window_ExportToVideo.Owner = this;
                window_ExportToVideo.ShowDialog();
            }
        }

        private void Item_Archive_Click(object sender, RoutedEventArgs e)
        {
            Window_Archive archive = new Window_Archive(data);
            archive.Owner = this;
            archive.Show();
        }

        private void Item_Run_Click(object sender, RoutedEventArgs e)
        {
            if (data.pages.Count>0)
            {
                NoAddControlsMouse();
                SaveCanvas();
                Debug.WriteLine("Starting presentation...");

                ToPresentationMode();

                string Mpath = pathtofile;

                Window_Presentation presentation = new Window_Presentation(data,0);
                presentation.Owner = this;
                presentation.Closing += Presentation_Closing;
                presentation.ShowDialog();

                pathtofile = Mpath;
            }
        }


        private void Item_RunNow_Click(object sender, RoutedEventArgs e)
        {
            if (data.pages.Count > 0)
            {
                NoAddControlsMouse();
                SaveCanvas();
                Debug.WriteLine("Starting presentation...");

                string Mpath = pathtofile;

                Window_Presentation presentation = new Window_Presentation(data, SelectedPosition);

                ToPresentationMode();

                presentation.Owner = this;
                presentation.Closing += Presentation_Closing;
                presentation.ShowDialog();

                pathtofile = Mpath;
            }
        }


        private void Presentation_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BackFromPresentationMode();
        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WINwidth = Width;
            }

            SliderZoom_ValueChanged(null, null);

            CheckControlPanelSize();
        }


        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            CloseApplication();
        }


        private void Item_ClearList_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPosition >= 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(Strings.ResStrings.ClearCanvas, Strings.ResStrings.Clear, System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                    DesignCanvas.Children.Clear();
            }
        }



    }
}
