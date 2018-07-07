using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Good_Teacher.Class;
using Good_Teacher.Class.Animations;
using Good_Teacher.Class.Serialization;
using Good_Teacher.Class.TestClass;
using Good_Teacher.Controls;
using Good_Teacher.Windows;
using Good_Teacher.Windows.Dialogs;

namespace Good_Teacher
{
    public partial class MainWindow : Window
    {

        /*
        string GetPageTypeName(TestTypeID.Test_Type type)
        {
            switch (type)
            {
                case TestTypeID.Test_Type.Classic:
                    return Strings.ResStrings.Answers;
                case TestTypeID.Test_Type.Form:
                    return Strings.ResStrings.Page;
                default:
                    return "";
            }
        }*/


        private void TestTypeCall_OnClick(int Pos, object sender)
        {
            SaveCanvas();

            Debug.WriteLine("POS:  " + Pos + "    Sender:" + ((TestType_ControlN)sender).test_type);

            FrameEditor.NavigationService.LoadCompleted -= NavigationService_LoadCompleted;
            FrameEditor.NavigationService.LoadCompleted += NavigationService_LoadCompleted;

            FrameEditor.Content = new Page_Form();
            FrameEditor.NavigationService.RemoveBackEntry();
 
            if (LastSelected != null)
                LastSelected.Select(false);

            ((TestType_ControlN)sender).Select(true);
            LastSelected = (TestType_ControlN)sender;

            SelectedPosition = Pos;

            ValueEditor.Content = "";

            HistoryRedo.Clear();
            HistoryUndo.Clear();

            Undo_MI.IsEnabled = false;
            Redo_MI.IsEnabled = false;

            LoadCanvas();
        }


        public void MovePage(int oldLoc,int newLoc)
        {
            SaveCanvas();
            Test_DefaultAbstract item = data.pages[oldLoc];
            data.pages.RemoveAt(oldLoc);
            data.pages.Insert(newLoc, item);


            UIElement tstl = Stack_TestList.Children[oldLoc];
            Stack_TestList.Children.RemoveAt(oldLoc);
            Stack_TestList.Children.Insert(newLoc, tstl);

            for (int i = 0; i < data.pages.Count; i++)
            {
                data.pages[i].Position = i;
                ((TestType_ControlN)Stack_TestList.Children[i]).Position = i;
                ((TestType_ControlN)Stack_TestList.Children[i]).LabelPage.Content = (i + 1);
                ((TestType_ControlN)Stack_TestList.Children[i]).UpdateData();
            }


            UpdateCanvasIcon(oldLoc);
            UpdateCanvasIcon(newLoc);

            
            LoadCanvas();
            LastSelected.Select(false);
            LastSelected = (TestType_ControlN)Stack_TestList.Children[SelectedPosition];
            LastSelected.Select(true);
        }


        private void TestTypeCall_OnMenuClick(TestType_ControlN.MenuEventType type, object sender)
        {
            Debug.WriteLine(type);
            int posTypeC = ((TestType_ControlN)sender).Position;
            switch (type)
            {
                case TestType_ControlN.MenuEventType.Up:
                    if (posTypeC>0)
                        MovePage(posTypeC, posTypeC - 1);
                    break;
                case TestType_ControlN.MenuEventType.Down:
                    if((posTypeC+1)<data.pages.Count)
                    MovePage(posTypeC, posTypeC + 1);
                    break;
                case TestType_ControlN.MenuEventType.Duplicate:
                    DuplicateCurrentPage(posTypeC);
                    break;
                case TestType_ControlN.MenuEventType.Delete:
                    if (data.pages[posTypeC].isUnlocked)
                    {
                        DeletePage(posTypeC);
                    }
                    else
                    {
                        if (MessageBox.Show(Strings.ResStrings.RemoveLockedPage, Strings.ResStrings.RemoveLockedPageTitle, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            DeletePage(posTypeC);
                        }
                    }
                    break;
                case TestType_ControlN.MenuEventType.Customize:

                    Canvas canvas = CanvasSaveLoad.LoadSpecificCanvas(data, posTypeC);
                    Window_Settings Wsettings = new Window_Settings(canvas, data, posTypeC, BorderWindowSize);
                    Wsettings.Owner = this;
                    Wsettings.ShowDialog();
                    UpdateCanvasIcon(posTypeC);
                    break;
                case TestType_ControlN.MenuEventType.MoveTo:

                    DWindow_MoveTo movetoW = new DWindow_MoveTo(data.pages.Count);
                    movetoW.Owner = this;
                    movetoW.ShowDialog();

                    if(movetoW.isOK)
                    {
                        MovePage(posTypeC, movetoW.MoveTo);
                    }

                    break;
                case TestType_ControlN.MenuEventType.Lock:
                    data.pages[posTypeC].isUnlocked = !data.pages[posTypeC].isUnlocked;
                    ((TestType_ControlN)sender).UpdateLocked(!data.pages[posTypeC].isUnlocked);

                    if (SelectedPosition == posTypeC)
                    {
                        SetLockOnCanvas(!data.pages[posTypeC].isUnlocked);
                        LockButton.SetCheckedNoCall(!data.pages[posTypeC].isUnlocked);
                    }

                    break;
                case TestType_ControlN.MenuEventType.Hide:
                    data.pages[posTypeC].isHidden = !data.pages[posTypeC].isHidden;
                    ((TestType_ControlN)sender).UpdateHidden(data.pages[posTypeC].isHidden);

                    if (SelectedPosition == posTypeC)
                    {
                        HiddenPageButton.SetCheckedNoCall(data.pages[posTypeC].isHidden);
                    }
                    break;

            }
        }


        private void Add_Page_Click(object sender, RoutedEventArgs e)
        {
            string tag = "";
            if (((Control)sender).Tag != null)
                tag = ((Control)sender).Tag.ToString();


            TestType_ControlN tst_type = new TestType_ControlN(Strings.ResStrings.Page, Stack_TestList.Children.Count, false,false);
            tst_type.OnClick += TestTypeCall_OnClick;
            tst_type.OnMenuClick += TestTypeCall_OnMenuClick;
            Stack_TestList.Children.Add(tst_type);

            Test_Form frm = new Test_Form();
            frm.Position = tst_type.Position;
            data.pages.Add(frm);

            UpdateCanvasIcon(data.pages.Count - 1);

            TestTypeCall_OnClick(tst_type.Position, tst_type);
        }



        private void Insert_Page_Click(object sender, RoutedEventArgs e)
        {
            int toPos = SelectedPosition;

            string tag = "";
            if (((Control)sender).Tag != null)
                tag = ((Control)sender).Tag.ToString();


            TestType_ControlN tst_type = new TestType_ControlN(Strings.ResStrings.Page, Stack_TestList.Children.Count, false, false);
            tst_type.OnClick += TestTypeCall_OnClick;
            tst_type.OnMenuClick += TestTypeCall_OnMenuClick;
            Stack_TestList.Children.Add(tst_type);

            Test_Form frm = new Test_Form();
            frm.Position = tst_type.Position;
            data.pages.Add(frm);

            UpdateCanvasIcon(data.pages.Count - 1);

            if(toPos>=0)
            {
                MovePage(tst_type.Position, toPos);
            }

            TestTypeCall_OnClick(toPos, tst_type);
        }


        void DuplicateCurrentPage(int pos)
        {
            SaveCanvas();
            LoadCanvas();
            TestType_ControlN tst_type = new TestType_ControlN(Strings.ResStrings.Page, Stack_TestList.Children.Count, false,false);
            tst_type.OnClick += TestTypeCall_OnClick;
            tst_type.OnMenuClick += TestTypeCall_OnMenuClick;
            Stack_TestList.Children.Add(tst_type);

            data.pages.Add((Test_DefaultAbstract)ClassWorker.Clone(data.pages[pos]));
            data.pages.Last().Position = tst_type.Position;
            data.pages.Last().isUnlocked = true;
            data.pages.Last().CustomControls = new System.Collections.Generic.List<object>(data.pages[pos].CustomControls);
            data.pages.Last().AnimationList = new System.Collections.Generic.List<Class.Animations.IAnimation>();

            data.pages[pos].AnimationList.ForEach((item) =>
            {
                data.pages.Last().AnimationList.Add((IAnimation)ClassWorker.CloneObject(item));
            });

            UpdateCanvasIcon(tst_type.Position);
            ScrollViewer_TestList.ScrollToEnd();
            TestTypeCall_OnClick(tst_type.Position, tst_type);
        }


        void DeletePage(int pos)
        {

            Debug.WriteLine("Pos: " + pos + "    Sel: " + SelectedPosition);

            CallAllUnloadEvent(CanvasSaveLoad.LoadSpecificCanvas(data, pos));
            Stack_TestList.Children.RemoveAt(pos);
            data.pages.RemoveAt(pos);
            UpdateAllDataPosition();
            UpdateAllStackListPosition();

            if (pos == SelectedPosition)
            {
                SelectedControl = null;
                SelectedPosition = -1;

                if (Stack_TestList.Children.Count > 0)
                {
                    TestTypeCall_OnClick(0, Stack_TestList.Children[0]);
                    ScrollViewer_TestList.ScrollToHome();
                }
                else
                {
                    BorderWindowSize.Visibility = Visibility.Collapsed;
                }

            }

        }


        private void PageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedPosition>=0)
            TestTypeCall_OnMenuClick((TestType_ControlN.MenuEventType)int.Parse(((Control)sender).Tag.ToString()), Stack_TestList.Children[SelectedPosition]);
        }


    }
}
