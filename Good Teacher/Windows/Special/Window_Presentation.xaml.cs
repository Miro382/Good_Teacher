using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Good_Teacher.Class.Serialization;
using Good_Teacher.Controls;
using Good_Teacher.Class.Actions;
using System.Collections.Generic;
using Good_Teacher.Class.Animations;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using Good_Teacher.Class.Save.Output;
using Good_Teacher.Class;
using System.IO;
using Good_Teacher.Class.Save.Output.Temporary;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Good_Teacher.Class.Save;
using System.Text.RegularExpressions;
using Good_Teacher.Class.Workers;
using Good_Teacher.Class.Enumerators;
using Good_Teacher.Class.Special;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_Presentation.xaml
    /// </summary>
    public partial class Window_Presentation : Window
    {
        private const int ToHoverVisible = 120;
        DataStore data;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DispatcherTimer dispatcherTimerTransition = new DispatcherTimer();
        DispatcherTimer dispatcherTimer_Timers = new DispatcherTimer();
        long Timer_TimersLast = 0;

        short t = 0,m=0;
        public int currentC = 0;
        private int LoadedPage = 0;
        int maxC = 0;
        List<AnswerButton> answersList = new List<AnswerButton>();
        List<int> answersClickedList = new List<int>();
        int currentLoaded = -1;
        PresentationOutput output = new PresentationOutput();
        Dictionary<int, TemporaryDataSave> tempsave = new Dictionary<int, TemporaryDataSave>();

        MediaPlayer mediaplayer = new MediaPlayer();
        bool MediaLoop = true;

        string MainPathToFile = "";

        private bool KeyDownP = false;

        /// <summary>
        /// Temporary save int for all answers
        /// </summary>
        private int TAllAnswers = 0;

        /// <summary>
        /// Temporary save int for good answers
        /// </summary>
        private int TGood = 0;

        /// <summary>
        /// Temporary save int for wrong answers
        /// </summary>
        private int TWrong = 0;

        private bool NoCount = false;

        ScriptingWorker scriptingWorker = new ScriptingWorker();

        bool AllLoaded = false;
        bool ScriptWarningBoxAllowed = false;

        public Window_Presentation(DataStore datastore, int loadCanvasI)
        {
            InitializeComponent();

            ScriptWarningBoxAllowed = false;

            AllLoaded = false;

            data = datastore;

            if (data.CacheCanvas)
                PlayCanvas.CacheMode = new BitmapCache(1);

            MainWindow.GoodAnswersCount = 0;
            MainWindow.WrongAnswersCount = 0;

            MainPathToFile = MainWindow.pathtofile;

            Border_CanvasSize.Width = data.CanvasW;
            Border_CanvasSize.Height = data.CanvasH;
            
            PreviewMouseMove += Window_Presentation_PreviewMouseMove;

            LoadedPage = loadCanvasI;

            maxC = datastore.pages.Count;

            Label_Number.Content = "/" + maxC;
            UpdateNumberLabel();

            LoadCanvas(loadCanvasI);

            dispatcherTimer.Tick -= DispatcherTimer_Tick;
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            dispatcherTimer.IsEnabled = true;

            Timer_TimersLast = DateTime.Now.Ticks;

            dispatcherTimer_Timers.Tick -= DispatcherTimer_Timers_Tick;
            dispatcherTimer_Timers.Tick += DispatcherTimer_Timers_Tick;
            dispatcherTimer_Timers.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer_Timers.IsEnabled = true;

            dispatcherTimerTransition.Tick -= DispatcherTimerTransition_Tick;
            dispatcherTimerTransition.Tick += DispatcherTimerTransition_Tick;
            dispatcherTimerTransition.Interval = TimeSpan.FromMilliseconds(data.pages[LoadedPage].TransitionMove);
            dispatcherTimerTransition.IsEnabled = (data.pages[LoadedPage].transitionType != Class.Enumerators.TransitionTypeEnum.TransitionType.Manual);


            if (data.BlockPresentationInput)
                ControlPanelC.Visibility = Visibility.Collapsed;

            if (data.HideInput)
                SP_InputControls.Visibility = Visibility.Collapsed;
           
            MainGrid.Background = data.OutsideBrush;


            this.ResizeMode = ResizeMode.NoResize;

            this.WindowStyle = WindowStyle.None;

            this.WindowState = WindowState.Maximized;

            Closing += Window_Presentation_Closing;

            mediaplayer.MediaEnded += Mediaplayer_MediaEnded;

            Activate();

            ContentRendered += Window_Presentation_ContentRendered;
        }

        private void DispatcherTimer_Timers_Tick(object sender, EventArgs e)
        {
            foreach (TimerAction timer in data.pages[currentLoaded].Timers) 
            {
                if (!timer.Stop)
                {
                    timer.ActualTime -= (float)((float)(DateTime.Now.Ticks-Timer_TimersLast) / TimeSpan.TicksPerMillisecond)/1000;

                    if (timer.ActualTime <= 0)
                    {
                        timer.Stop = true;
                        foreach (IActions action in timer.Actions)
                        {
                            DoAction(action);
                        }
                    }
                }
            }
            Timer_TimersLast = DateTime.Now.Ticks;
        }

        private void DispatcherTimerTransition_Tick(object sender, EventArgs e)
        {
            if(data.pages[LoadedPage].transitionType == Class.Enumerators.TransitionTypeEnum.TransitionType.Automatic)
            {
                GoForward();
            }
            else if(data.pages[LoadedPage].transitionType == Class.Enumerators.TransitionTypeEnum.TransitionType.AutomaticClose)
            {
                Close();
            }
        }

        private void Window_Presentation_ContentRendered(object sender, EventArgs e)
        {
            if (data.AreScriptsAllowed)
            {
                if (MessageBox.Show(Strings.ResStrings.ScriptsWarning + Environment.NewLine + Environment.NewLine + data.ScriptWarningMessage, Strings.ResStrings.AllowScripts, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //No
                    ScriptWarningBoxAllowed = false;
                }
                else
                {
                    //Yes
                    ScriptWarningBoxAllowed = true;
                }
            }

            AllLoaded = true;
            DoScript();
        }


        public void DoScript()
        {
            if (data.AreScriptsAllowed)
            {
                if (AllLoaded && ScriptWarningBoxAllowed)
                {
                    if (data.AreScriptsAllowed && !String.IsNullOrWhiteSpace(data.pages[currentLoaded].ScriptCode))
                    {
                        scriptingWorker.DoScript(data.pages[currentLoaded].ScriptCode, PlayCanvas, currentLoaded + 1, data.ScriptDebug);
                    }
                }
            }
        }


        private void Mediaplayer_MediaEnded(object sender, EventArgs e)
        {
            if (MediaLoop)
            {
                mediaplayer.Position = TimeSpan.Zero;
                mediaplayer.Play();
            }
        }

        public void UpdateNumberLabel()
        {
            TB_CNumber.Text = "" + (currentC + 1);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            t++;
            m++;

            if (ControlPanelC.IsMouseOver)
                t = 0;

            if (t > 2)
            {
                t = 3;
                ControlPanelC.Visibility = Visibility.Collapsed;
            }

            if (m > 2)
            {
                m = 3;
                Cursor = Cursors.None;
            }
        }


        private void Window_Presentation_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!data.BlockPresentationInput)
            {
                if (Math.Abs(e.GetPosition(MainGrid).Y) > MainGrid.ActualHeight - ToHoverVisible)
                {

                    if (ControlPanelC.Visibility == Visibility.Collapsed)
                    {
                        ThicknessAnimation ta = new ThicknessAnimation();
                        ta.From = new Thickness(0, 0, 0, -60);
                        ta.To = new Thickness(0, 0, 0, 0);
                        ta.Duration = new Duration(TimeSpan.FromSeconds(0.2f));
                        ControlPanelC.BeginAnimation(Grid.MarginProperty, ta);
                    }

                    ControlPanelC.Visibility = Visibility.Visible;
                    t = 0;
                }
            }

            m = 0;
            Cursor = null;
        }


        void PlaySound(int pos)
        {
            try
            {
                if (data.pages[pos].soundActionType == Class.Enumerators.SoundAction.SoundActionType.Play)
                {
                    Debug.WriteLine(LocalPath.GetResourcesPath() + "\\Media\\" + data.pages[pos].PathToPlaySound);
                    SetSoundToBePlayed(data.pages[pos].PathToPlaySound,data.pages[pos].SoundLoop);
                }
                else if (data.pages[pos].soundActionType == Class.Enumerators.SoundAction.SoundActionType.Stop)
                {
                    mediaplayer.Stop();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(""+ex);
            }
        }


        private void SetSoundToBePlayed(string pathPlay, bool SoundLoop)
        {
            mediaplayer.Open(new Uri(LocalPath.GetResourcesPath() + "\\Media\\" + pathPlay));
            mediaplayer.Play();

            MediaLoop = SoundLoop;
        }

        public void LoadCanvas(int pos, bool IgnoreHidden = false)
        {
            if (data.OptimizedMode)
                MainWindow.OPTIMIZEDMODE = true;
            else
                MainWindow.OPTIMIZEDMODE = false;


            //Hidden page
            if (!IgnoreHidden)
            {
                if (data.pages[pos].isHidden)
                {
                    bool finded = false;

                    if (LoadedPage > pos)
                    {
                        for (int i = pos; i >= 0; i--)
                        {
                            if (!data.pages[i].isHidden)
                            {
                                pos = i;
                                finded = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = pos; i < maxC; i++)
                        {
                            if (!data.pages[i].isHidden)
                            {
                                pos = i;
                                finded = true;
                                break;
                            }
                        }
                    }

                    if (!finded)
                        return;
                }
            }
            //hidden page

            MainWindow.ActualPage = pos;
            LoadedPage = pos;

            currentC = pos;

            if(data.SaveOutput)
            SaveOutputData();

            if (data.SaveTemporaryData)
            SaveTemporary();

            PlaySound(pos);

            TGood = 0;
            TWrong = 0;
            TAllAnswers = 0;

            PlayCanvas.Children.Clear();
            if (!String.IsNullOrWhiteSpace(data.pages[pos].canvas))
            {
                CanvasWriter.LoadCanvas(PlayCanvas, data.pages[pos].canvas);
            }

            CanvasSaveLoad.ExtractHiddenData(data, PlayCanvas, pos);
            CanvasSaveLoad.ToPresentationMode(PlayCanvas);
            ToSpecialPresentation();

            UpdateNumberLabel();
            MakeAnimations(pos);

            currentLoaded = pos;

            NoCount = true;

            if(data.SaveTemporaryData)
            LoadTempData();

            NoCount = false;

            foreach (RepeatingData repeat in data.RepeatingControls)
            {
                if (!repeat.IgnorePages.Contains((pos + 1)))
                {
                    ContentViewer contentViewer = repeat.contentViewer.CreateControl(data);
                    contentViewer.IsHitTestVisible = false;
                    PlayCanvas.Children.Add(contentViewer);
                }
            }

            foreach(TimerAction timer in data.pages[currentLoaded].Timers)
            {
                timer.SetActualTime();
                timer.Stop = false;
            }

            DoScript();

            dispatcherTimerTransition.Interval = TimeSpan.FromMilliseconds(data.pages[LoadedPage].TransitionMove);
            dispatcherTimerTransition.IsEnabled = (data.pages[LoadedPage].transitionType != Class.Enumerators.TransitionTypeEnum.TransitionType.Manual);
        }


        void LoadTempData()
        {
            if (currentLoaded >= 0)
            {
                if (tempsave.ContainsKey(currentLoaded))
                {
                    try
                    {
                        foreach(Temporary_Save temp in tempsave[currentLoaded].TempSave)
                        {
                            if (temp is TextBox_TempSave)
                                ((TextBox)PlayCanvas.Children[temp.GetOwnedControl()]).Text = ((TextBox_TempSave)temp).Text;
                            else if (temp is ComboBox_TempSave)
                                ((ComboBox_Control)PlayCanvas.Children[temp.GetOwnedControl()]).combobox.SelectedIndex = ((ComboBox_TempSave)temp).Selected;
                            else if (temp is CheckControl_TempSave)
                                ((ToggleButton)PlayCanvas.Children[temp.GetOwnedControl()]).IsChecked = ((CheckControl_TempSave)temp).IsChecked;
                            else if (temp is InkCanvas_TempSave)
                                ((InkCanvas_TempSave)temp).LoadStrokes(((InkCanvas_Control)PlayCanvas.Children[temp.GetOwnedControl()]));
                            else if (temp is AnswerButton_TempSave)
                                Window_Presentation_Answer_Click((AnswerButton)PlayCanvas.Children[temp.GetOwnedControl()], null);
                        }
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(""+ex);
                    }
                }
            }
        }


        void SaveOutputData()
        {
            if (currentLoaded >= 0)
            {
                if (!output.Pages.ContainsKey(currentLoaded))
                    output.Pages.Add(currentLoaded, new OutputPage());

                output.Pages[currentLoaded].Page = currentLoaded;
                output.Pages[currentLoaded].Image = CanvasWriter.SaveCanvasToImg(PlayCanvas);
                output.Pages[currentLoaded].AllAnswers = TAllAnswers;
                output.Pages[currentLoaded].GoodAnswers = TGood;
                output.Pages[currentLoaded].WrongAnswers = TWrong;
                output.CreatedTime = DateTime.Now;
                output.PresentationPagesCount = data.pages.Count;
                output.HaveScripts = data.AreScriptsAllowed;
                output.ScriptsAllowed = ScriptWarningBoxAllowed;
                SaveControlState();
            }
        }

        public void SaveControlState()
        {
            output.Pages[currentLoaded].InputList.Clear();
            foreach (FrameworkElement elm in PlayCanvas.Children)
            {
                if(elm is TextBox)
                {
                    output.Pages[currentLoaded].InputList.Add(new TextboxInput(elm.Tag.ToString(), ((TextBox)elm).Text));
                }
                else if (elm is CheckBox)
                {
                    output.Pages[currentLoaded].InputList.Add(new CheckBoxInput(((CheckBox)elm).Content?.ToString(), ((CheckBox)elm).IsChecked==true));
                }
                else if (elm is RadioButton)
                {
                    output.Pages[currentLoaded].InputList.Add(new RadioButtonInput(((RadioButton)elm).Content?.ToString(), ((RadioButton)elm).IsChecked == true, ((RadioButton)elm).GroupName));
                }
                else if (elm is ToggleButton_Control)
                {
                    string checkSTR = "";
                    if(((ToggleButton_Control)elm).IsChecked)
                    {
                        checkSTR = ((ToggleButton_Control)elm).contentCreatorChecked.GetText();
                    }
                    else
                    {
                        checkSTR = ((ToggleButton_Control)elm).contentCreatorUnchecked.GetText();
                    }

                    output.Pages[currentLoaded].InputList.Add(new ToggleButtonInput( ""+ControlWorker.GetID( ((ToggleButton_Control)elm).Name),checkSTR, ((ToggleButton_Control)elm).IsChecked));
                }
            }
        }


        public void SaveTemporary()
        {
            if (tempsave.ContainsKey(currentLoaded))
                tempsave[currentLoaded].TempSave.Clear();
            else
                tempsave.Add(currentLoaded, new TemporaryDataSave());


            int c = 0;
            foreach (FrameworkElement elm in PlayCanvas.Children)
            {
                if (elm is TextBox)
                {
                    tempsave[currentLoaded].TempSave.Add(new TextBox_TempSave(c,((TextBox)elm).Text));
                }
                else if (elm is ToggleButton)
                {
                    tempsave[currentLoaded].TempSave.Add(new CheckControl_TempSave(c, ((ToggleButton)elm).IsChecked==true));
                }
                else if (elm is ComboBox_Control)
                {
                    tempsave[currentLoaded].TempSave.Add(new ComboBox_TempSave(c, ((ComboBox_Control)elm).combobox.SelectedIndex));
                }
                else if (elm is InkCanvas_Control)
                {
                    tempsave[currentLoaded].TempSave.Add(new InkCanvas_TempSave(c, ((InkCanvas_Control)elm).inkCanvas.Strokes));
                }

                c++;
            }


            foreach (int con in answersClickedList)
            {
                tempsave[currentLoaded].TempSave.Add(new AnswerButton_TempSave(con, true));
            }
        }



        private void Window_Presentation_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (data.SaveOutput)
            {
                output.W = data.CanvasW;
                output.H = data.CanvasH;

                if (!String.IsNullOrWhiteSpace(MainWindow.pathtofile))
                {
                    SaveOutputData();
                    SaveEditor saveEditor = new SaveEditor();

                    string dirp = Path.GetDirectoryName(MainWindow.pathtofile);
                    Directory.CreateDirectory(dirp+"\\GT_Output\\");
                    saveEditor.SaveWithCompressionO(dirp + "\\GT_Output\\GT_" + DateTime.Now.Year + "_" + DateTime.Now.Month.ToString("00") + "_" + DateTime.Now.Day+"__"+DateTime.Now.Hour.ToString("00")+"_"+DateTime.Now.Minute.ToString("00")+ ".gtout", output);
                }
            }

            mediaplayer.Stop();

            MainWindow.pathtofile = MainPathToFile;

            MainWindow.GoodAnswersCount = 0;
            MainWindow.WrongAnswersCount = 0;

            dispatcherTimer_Timers.Stop();
            dispatcherTimer_Timers.IsEnabled = false;
        }


        void MakeAnimations(int pos)
        {
            foreach( IAnimation ian in  data.pages[pos].AnimationList)
            {
                FrameworkElement felm =  ControlWorker.FindChild<FrameworkElement>(PlayCanvas, "ID_" + ian.GetID());

                if (felm != null)
                {
                    if (ian.DoAnimationAtStart())
                    {
                        ian.MakeAnimation(felm);
                    }
                }
            }
        }


        void MakeAnimation(int pos, int ID)
        {
            if (ID >= 0 && ID < data.pages[pos].AnimationList.Count)
            {
                FrameworkElement felm = ControlWorker.FindChild<FrameworkElement>(PlayCanvas, "ID_" + data.pages[pos].AnimationList[ID].GetID());

                if (felm != null)
                {
                    data.pages[pos].AnimationList[ID].MakeAnimation(felm);
                }
            }
        }


        void ToSpecialPresentation()
        {
            answersList.Clear();
            answersClickedList.Clear();
            List<string> ids = new List<string>();
            for (int i=0;i<PlayCanvas.Children.Count;i++)
            {
                if(PlayCanvas.Children[i] is CButton)
                {
                    ((CButton)PlayCanvas.Children[i]).Click += Window_Presentation_Click;
                }
                else if (PlayCanvas.Children[i] is ToggleButton_Control)
                {
                    ((ToggleButton_Control)PlayCanvas.Children[i]).Click += Window_Presentation_Click;
                }
                else if (PlayCanvas.Children[i] is AnswerButton)
                {
                    ((AnswerButton)PlayCanvas.Children[i]).Click += Window_Presentation_Answer_Click;
                    answersList.Add((AnswerButton)PlayCanvas.Children[i]);

                    if (data.SaveOutput)
                    {
                        if (!ids.Contains(((AnswerButton)PlayCanvas.Children[i]).ID))
                        {
                            TAllAnswers++;
                            ids.Add(((AnswerButton)PlayCanvas.Children[i]).ID);
                        }
                    }
                }
            }
        }


        private void Window_Presentation_Answer_Click(AnswerButton sender, MouseButtonEventArgs e)
        {
            sender.SetSelected(true);

            answersClickedList.Add(PlayCanvas.Children.IndexOf(sender));

            bool showgood = false;

            if (!sender.Good)
            {
                showgood = sender.ShowGood;

                if (data.SaveOutput)
                    TWrong++;

                if(!NoCount)
                MainWindow.WrongAnswersCount++;
            }
            else
            {
                if (data.SaveOutput)
                    TGood++;

                if (!NoCount)
                    MainWindow.GoodAnswersCount++;
            }

            foreach (AnswerButton ans in answersList)
            {
                if(ans.ID == sender.ID)
                {
                    if(showgood)
                    {
                        if(ans.Good)
                        {
                            ans.SetSelected(true);
                        }
                    }

                    ans.IsEnabled = false;
                }
            }
        }


        private void Window_Presentation_Click(FrameworkElement sender, MouseButtonEventArgs e)
        {
            List<IActions> actions = new List<IActions>();

            if(sender is CButton)
            {
                actions = new List<IActions>(((CButton)sender).actions);
            }
            else if(sender is ToggleButton_Control)
            {
                if (((ToggleButton_Control)sender).IsChecked)
                {
                    actions = new List<IActions>(((ToggleButton_Control)sender).CheckedActions);
                }
                else
                {
                    actions = new List<IActions>(((ToggleButton_Control)sender).UncheckedActions);
                }
            }

            foreach (IActions action in actions)
            {
                DoAction(action);
            }
        }


        public void DoAction(IActions action)
        {
            int act = action.DoAction();

            switch (act)
            {
                case 1:
                    if (((Action_GoToPage)action).ToSpecific)
                    {
                        LoadCanvas(((Action_GoToPage)action).ToPage - 1, true);
                        UpdateNumberLabel();
                    }
                    else if (((Action_GoToPage)action).Next)
                    {
                        GoForward();
                    }
                    else
                    {
                        GoBack();
                    }
                    break;
                case 2:
                    Close();
                    break;
                case 3:
                    if (((Action_Sound)action).Stop)
                    {
                        mediaplayer.Stop();
                    }
                    else if (((Action_Sound)action).PlayAgain)
                    {
                        mediaplayer.Position = TimeSpan.Zero;
                        mediaplayer.Play();
                    }
                    else
                    {
                        if (!String.IsNullOrWhiteSpace(((Action_Sound)action).PathToPlay))
                            SetSoundToBePlayed(((Action_Sound)action).PathToPlay, ((Action_Sound)action).Repeat);
                    }
                    break;
                case 4:
                    LoadNewPresentation(((Action_LoadPresentation)action).PresentationPath);
                    break;
                case 5:
                    {
                        FrameworkElement felm = ControlWorker.FindChild<FrameworkElement>(PlayCanvas, "ID_" + ((Action_SetVisibility)action).ID);

                        if (felm != null)
                        {
                            if (((Action_SetVisibility)action).VisibilityValue == SetVisibilityEnum.SetVisibilityValue.SetToVisible)
                            {
                                felm.Visibility = Visibility.Visible;
                            }
                            else if (((Action_SetVisibility)action).VisibilityValue == SetVisibilityEnum.SetVisibilityValue.SetToInvisible)
                            {
                                felm.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                if (felm.Visibility == Visibility.Collapsed || felm.Visibility == Visibility.Hidden)
                                {
                                    felm.Visibility = Visibility.Visible;
                                }
                                else
                                {
                                    felm.Visibility = Visibility.Collapsed;
                                }
                            }
                        }
                        break;
                    }
                case 6:
                    MakeAnimation(currentLoaded, ((Action_DoAnimation)action).AnimationID);
                    break;
                case 7:
                    {
                        FrameworkElement felm = ControlWorker.FindChild<FrameworkElement>(PlayCanvas, "ID_" + ((Action_Position)action).ID);

                        if (felm != null)
                        {
                            if (((Action_Position)action).ChangeX)
                            {
                                MathSignEnum.SetPositionX(((Action_Position)action).CX, ((Action_Position)action).SignX, felm);
                            }

                            if (((Action_Position)action).ChangeY)
                            {
                                MathSignEnum.SetPositionY(((Action_Position)action).CY, ((Action_Position)action).SignY, felm);
                            }
                        }
                        break;
                    }
            }
        }


        void GoBack()
        {
            if (currentC > 0)
                LoadCanvas(currentC-1);
        }


        void GoForward()
        {
            if (currentC <= (maxC - 2))
                LoadCanvas(currentC+1);
        }


        private void FlatButton_Click(object sender, MouseEventArgs e)
        {
           if(sender == FB_Left)
            {
                GoBack();
            }
            else if(sender == FB_Right)
            {
                GoForward();
            }
            else if(sender == FB_Close)
            {
                Close();
            }
            else if(sender == FB_PageList)
            {
                Window_ShowAllPages window_ShowAllPages = new Window_ShowAllPages(data);
                window_ShowAllPages.Owner = this;
                window_ShowAllPages.ShowDialog();

                if(window_ShowAllPages.IsOK)
                {
                    LoadCanvas(window_ShowAllPages.ClickedID, true);
                }
            }
        }

        private void TB_CNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Debug.WriteLine("Enter on TB_CNUMBER");
                try
                {
                    int sn = 0;

                    if(int.TryParse(TB_CNumber.Text,out sn))
                    {
                        if (sn > maxC || sn<1)
                        {
                            UpdateNumberLabel();
                        }
                        else
                        {
                            LoadCanvas(sn - 1,true);
                        }
                    }
                    else
                    {
                        UpdateNumberLabel();
                    }
                }catch(Exception ex)
                {
                    Debug.WriteLine("Error parse number (presentation): "+ex);
                    UpdateNumberLabel();
                }
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
            else if (e.Key == Key.Right && !KeyDownP)
            {
                if (!data.BlockPresentationInput)
                    GoForward();

                KeyDownP = true;
            }
            else if (e.Key == Key.Left && !KeyDownP)
            {
                if (!data.BlockPresentationInput)
                    GoBack();

                KeyDownP = true;
            }
        }

        private void PlayCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (data.ClickToNext)
            {
                if (e.Source is Canvas)
                {
                    GoForward();
                }
            }
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            KeyDownP = false;
        }



        private void LoadNewPresentation(string PresentationName)
        {
            string localpath = "";

            if (LocalPath.GetDirectoryPath(out localpath))
            {

                if (!File.Exists(localpath + "\\" + PresentationName))
                {
                    MessageBox.Show(Good_Teacher.Strings.ResStrings.FileNotFound+": "+ Path.Combine(localpath, PresentationName), Good_Teacher.Strings.ResStrings.FileNotFound);
                }
                else
                {
                    MainWindow.pathtofile = localpath + "\\" + PresentationName;
                    SaveEditor save = new SaveEditor();
                    data = save.LoadWithCompression(localpath + "\\" + PresentationName);

                    MainWindow.GoodAnswersCount = 0;
                    MainWindow.WrongAnswersCount = 0;

                    Border_CanvasSize.Width = data.CanvasW;
                    Border_CanvasSize.Height = data.CanvasH;

                    LoadedPage = 0;

                    maxC = data.pages.Count;

                    Label_Number.Content = "/" + maxC;
                    UpdateNumberLabel();

                    LoadCanvas(0);
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


        private void SliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ScrollViewerZoom.ActualWidth > 1)
            {
                //Debug.WriteLine("Viewbox W: " + viewboxDC.Width + "   AW: " + viewboxDC.ActualWidth + "    Viewbox H: " + viewboxDC.Height + "   AH: " + viewboxDC.ActualHeight);
                //Debug.WriteLine("ScrollViewerZoom W: " + ScrollViewerZoom.Width + "   AW: " + ScrollViewerZoom.ActualWidth + "    Viewbox H: " + ScrollViewerZoom.Height + "   AH: " + ScrollViewerZoom.ActualHeight);

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

        private void TB_Zoom_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateZoomValueByZoomTextBox();
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

        private void ScrollViewerZoom_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;

            if (handle)
            {
                e.Handled = handle;
                return;
            }
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
