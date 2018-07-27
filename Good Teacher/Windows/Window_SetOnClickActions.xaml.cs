using Good_Teacher.Class.Actions;
using Good_Teacher.Class.Animations;
using Good_Teacher.Class.Enumerators;
using Good_Teacher.Class.Save;
using Good_Teacher.Class.Workers;
using Good_Teacher.Controls;
using Good_Teacher.Windows.Dialogs;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Windows
{

    /// <summary>
    /// Interaction logic for Window_SetOnClickActions.xaml
    /// </summary>
    public partial class Window_SetOnClickActions : Window
    {
        DataStore data;
        public bool isOK = false;
        public ActionType.Action_Type action_type;
        public IActions actions;

        string dirpath = "";
        bool saved = false;

        int selpos;

        public Window_SetOnClickActions(CButton button,DataStore dataStore, int selectedPosition)
        {
            InitializeComponent();
            data = dataStore;

            MaxPages.Content = "/" + data.pages.Count;

            selpos = selectedPosition;


            if(LocalPath.GetDirectoryPath(out dirpath))
            {
                TBL_NotSaved.Visibility = Visibility.Collapsed;
                TBL_NotSavedP.Visibility = Visibility.Collapsed;
                saved = true;
                TBL_AddressToFile.Text = dirpath+"\\"+TB_OpenApp.Text;
                TBL_AddressToPres.Text = dirpath + "\\" + TB_LoadPres.Text;
            }
            else
            {
                TBL_NotSaved.Visibility = Visibility.Visible;
                TBL_NotSavedP.Visibility = Visibility.Visible;
                saved = false;
            }

            AddAnimationsToComboBox();
        }


        public Window_SetOnClickActions(CButton button, DataStore dataStore, int EditID, int selectedPosition)
        {
            InitializeComponent();
            data = dataStore;

            MaxPages.Content = "/" + data.pages.Count;

            selpos = selectedPosition;

            Edit(button, EditID);

            if (LocalPath.GetDirectoryPath(out dirpath))
            {
                TBL_NotSaved.Visibility = Visibility.Collapsed;
                TBL_NotSavedP.Visibility = Visibility.Collapsed;
                saved = true;
                TBL_AddressToFile.Text = dirpath + "\\" + TB_OpenApp.Text;
                TBL_AddressToPres.Text = dirpath + "\\" + TB_LoadPres.Text;
            }
            else
            {
                TBL_NotSaved.Visibility = Visibility.Visible;
                TBL_NotSavedP.Visibility = Visibility.Visible;
                saved = false;
            }

            AddAnimationsToComboBox();
        }


        public void AddAnimationsToComboBox()
        {
            int k = 0;
            foreach (IAnimation ian in data.pages[selpos].AnimationList)
            {
                StackPanel stc = new StackPanel();
                stc.Orientation = Orientation.Horizontal;

                Label lbl = new Label();
                lbl.Content = (k+1) + ".   [" + ian.GetID() + "]";
                lbl.VerticalAlignment = VerticalAlignment.Center;

                Image img = new Image();

                if (ian is Animation_Position)
                    img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Animations/MoveAnim.png"));
                else if (ian is Animation_Opacity)
                    img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Animations/OpacityAnimation.png"));
                else if (ian is Animation_Size)
                    img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Animations/Scale.png"));

                img.Width = 32;
                img.Height = 32;

                stc.Children.Add(img);
                stc.Children.Add(lbl);

                CB_Animation.Items.Add(stc);
                k++;
            }
        }


        void Edit(CButton button, int pos)
        {

            if (button.actions[pos] != null)
            {
                switch (button.actions[pos].GetActionType())
                {
                    case ActionType.Action_Type.GoToPage:
                        InputToPage.Text = "" + ((Action_GoToPage)button.actions[pos]).ToPage;
                        tabcontrol_actions.SelectedIndex = 0;

                        RB_Next.IsChecked = ((Action_GoToPage)button.actions[pos]).Next;
                        RB_Previous.IsChecked = ((Action_GoToPage)button.actions[pos]).Previous;
                        RB_Specific.IsChecked = ((Action_GoToPage)button.actions[pos]).ToSpecific;

                        break;
                    case ActionType.Action_Type.OpenWeb:
                        TB_OpenLink.Text = ((Action_OpenWeb)button.actions[pos]).Url;
                        tabcontrol_actions.SelectedIndex = 1;
                        break;
                    case ActionType.Action_Type.ClosePresentation:
                        tabcontrol_actions.SelectedIndex = 2;
                        break;
                    case ActionType.Action_Type.OpenApplication:
                        tabcontrol_actions.SelectedIndex = 3;
                        TB_OpenApp.Text = ((Action_OpenApp)button.actions[pos]).AppPath;
                        break;
                    case ActionType.Action_Type.ShowMessageBox:
                        tabcontrol_actions.SelectedIndex = 4;
                        TB_MBText.Text = ((Action_ShowMessageBox)button.actions[pos]).Text;
                        TB_MBTitle.Text = ((Action_ShowMessageBox)button.actions[pos]).Title;
                        break;
                    case ActionType.Action_Type.Sound:
                        tabcontrol_actions.SelectedIndex = 5;
                        L_SoundPlay.Content = ((Action_Sound)button.actions[pos]).PathToPlay;
                        RB_Stop.IsChecked = ((Action_Sound)button.actions[pos]).Stop;
                        RB_PlayAgain.IsChecked = ((Action_Sound)button.actions[pos]).PlayAgain;

                        if (RB_Stop.IsChecked == false && RB_PlayAgain.IsChecked == false)
                            RB_Play.IsChecked = true;

                        CB_SoundRepeat.IsChecked = ((Action_Sound)button.actions[pos]).Repeat;
                        break;
                    case ActionType.Action_Type.LoadPresentation:
                        tabcontrol_actions.SelectedIndex = 6;
                        TB_LoadPres.Text = ((Action_LoadPresentation)button.actions[pos]).PresentationPath;
                        break;
                    case ActionType.Action_Type.SetVisibility:
                        tabcontrol_actions.SelectedIndex = 7;
                        NB_ID.Text = "" + ((Action_SetVisibility)button.actions[pos]).ID;

                        if (((Action_SetVisibility)button.actions[pos]).VisibilityValue == SetVisibilityEnum.SetVisibilityValue.SetToVisible)
                        {
                            RB_SVisible.IsChecked = true;

                        }
                        else if (((Action_SetVisibility)button.actions[pos]).VisibilityValue == SetVisibilityEnum.SetVisibilityValue.SetToInvisible)
                        {
                            RB_SInvisible.IsChecked = true;
                        }
                        else
                        {
                            RB_TVisibility.IsChecked = true;
                        }

                        break;
                    case ActionType.Action_Type.DoAnimation:
                        tabcontrol_actions.SelectedIndex = 8;
                        CB_Animation.SelectedIndex = ((Action_DoAnimation)button.actions[pos]).AnimationID;
                        break;
                    case ActionType.Action_Type.Position:
                        tabcontrol_actions.SelectedIndex = 9;
                        CB_AXSign.SelectedIndex = (int)((Action_Position)button.actions[pos]).SignX;
                        CB_AYSign.SelectedIndex = (int)((Action_Position)button.actions[pos]).SignY;
                        CB_ChangeX.IsChecked = ((Action_Position)button.actions[pos]).ChangeX;
                        CB_ChangeY.IsChecked = ((Action_Position)button.actions[pos]).ChangeY;
                        NB_AX.Text = ""+((Action_Position)button.actions[pos]).CX;
                        NB_AY.Text = "" + ((Action_Position)button.actions[pos]).CY;
                        NB_PositionID.Text = "" + ((Action_Position)button.actions[pos]).ID;
                        break;

                }
            }
        }


        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (tabcontrol_actions.SelectedIndex == 0)
            {
                int page = 0;
                if (int.TryParse(InputToPage.Text, out page) && page > 0 && page <= data.pages.Count)
                {
                    action_type = ActionType.Action_Type.GoToPage;

                    actions = new Action_GoToPage();
                    ((Action_GoToPage)actions).ToPage = page;
                    ((Action_GoToPage)actions).Next = RB_Next.IsChecked == true;
                    ((Action_GoToPage)actions).Previous = RB_Previous.IsChecked == true;
                    ((Action_GoToPage)actions).ToSpecific = RB_Specific.IsChecked == true;

                    isOK = true;
                    Close();
                }
                else
                {
                    InputToPage.Text = "1";
                    MessageBox.Show(Strings.ResStrings.WrongInputData, Strings.ResStrings.Error);
                }
            }
            else if (tabcontrol_actions.SelectedIndex == 1)
            {

                if (IsUrlValid(TB_OpenLink.Text))
                {


                    action_type = ActionType.Action_Type.OpenWeb;

                    actions = new Action_OpenWeb();
                    ((Action_OpenWeb)actions).Url = TB_OpenLink.Text;

                    isOK = true;
                    Close();
                }
                else
                {
                    MessageBox.Show(Strings.ResStrings.WrongInputData, Strings.ResStrings.Error);
                }
            }
            else if (tabcontrol_actions.SelectedIndex == 2)
            {
                action_type = ActionType.Action_Type.ClosePresentation;

                actions = new Action_ClosePresentation();

                isOK = true;
                Close();
            }
            else if (tabcontrol_actions.SelectedIndex == 3)
            {
                if (saved)
                {
                    action_type = ActionType.Action_Type.OpenApplication;

                    actions = new Action_OpenApp();

                    ((Action_OpenApp)actions).AppPath = TB_OpenApp.Text;

                    isOK = true;
                    Close();
                }
                else
                {
                    MessageBox.Show(Strings.ResStrings.NotSaved, Strings.ResStrings.NotSavedTitle);
                }
            }
            else if (tabcontrol_actions.SelectedIndex == 4)
            {
                action_type = ActionType.Action_Type.ShowMessageBox;

                actions = new Action_ShowMessageBox();

                ((Action_ShowMessageBox)actions).Title = TB_MBTitle.Text;
                ((Action_ShowMessageBox)actions).Text = TB_MBText.Text;

                isOK = true;
                Close();
            }
            else if (tabcontrol_actions.SelectedIndex == 5)
            {
                action_type = ActionType.Action_Type.Sound;

                actions = new Action_Sound(RB_Stop.IsChecked == true, RB_PlayAgain.IsChecked == true, CB_SoundRepeat.IsChecked == true, L_SoundPlay.Content.ToString());

                isOK = true;
                Close();
            }
            else if (tabcontrol_actions.SelectedIndex == 6)
            {
                if (saved)
                {
                    action_type = ActionType.Action_Type.LoadPresentation;

                    actions = new Action_LoadPresentation();

                    ((Action_LoadPresentation)actions).PresentationPath = TB_LoadPres.Text;

                    isOK = true;
                    Close();
                }
                else
                {
                    MessageBox.Show(Strings.ResStrings.NotSaved, Strings.ResStrings.NotSavedTitle);
                }
            }
            else if (tabcontrol_actions.SelectedIndex == 7)
            {
                action_type = ActionType.Action_Type.SetVisibility;
                actions = new Action_SetVisibility();
                ((Action_SetVisibility)actions).ID =  NB_ID.GetInt();

                if(RB_SInvisible.IsChecked == true)
                {
                    ((Action_SetVisibility)actions).VisibilityValue = SetVisibilityEnum.SetVisibilityValue.SetToInvisible;
                }
                else if (RB_SVisible.IsChecked == true)
                {
                    ((Action_SetVisibility)actions).VisibilityValue = SetVisibilityEnum.SetVisibilityValue.SetToVisible;
                }
                else
                {
                    ((Action_SetVisibility)actions).VisibilityValue = SetVisibilityEnum.SetVisibilityValue.ToggleVisibility;
                }

                isOK = true;
                Close();
            }
            else if (tabcontrol_actions.SelectedIndex == 8)
            {
                action_type = ActionType.Action_Type.DoAnimation;

                actions = new Action_DoAnimation();

                ((Action_DoAnimation)actions).AnimationID = CB_Animation.SelectedIndex;

                isOK = true;
                Close();
            }
            else if (tabcontrol_actions.SelectedIndex == 9)
            {
                action_type = ActionType.Action_Type.Position;

                actions = new Action_Position();

                ((Action_Position)actions).ChangeX = CB_ChangeX.IsChecked == true;
                ((Action_Position)actions).ChangeY = CB_ChangeY.IsChecked == true;
                ((Action_Position)actions).SignX = (MathSignEnum.MathSignType)CB_AXSign.SelectedIndex;
                ((Action_Position)actions).SignY = (MathSignEnum.MathSignType)CB_AYSign.SelectedIndex;
                ((Action_Position)actions).CX = NB_AX.GetDouble();
                ((Action_Position)actions).CY = NB_AY.GetDouble();
                ((Action_Position)actions).ID = NB_PositionID.GetInt();

                isOK = true;
                Close();
            }
        }


        private bool IsUrlValid(string url)
        {

            string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            int page = 0;
            if (int.TryParse(InputToPage.Text, out page) && page <= data.pages.Count)
            {
                if(page < data.pages.Count)
                InputToPage.Text = "" + (page + 1);
            }
            else
            {
                InputToPage.Text = "1";
            }
        }


        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            int page = 0;
            if (int.TryParse(InputToPage.Text, out page) && page > 1)
            {
                InputToPage.Text = ""+(page-1);
            }
            else
            {
                InputToPage.Text = "1";
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (SetSpecificPage != null)
            {
                if (sender == RB_Specific)
                {
                    if(RB_Specific.IsChecked != true)
                    SetSpecificPage.Visibility = Visibility.Hidden;
                    else
                    SetSpecificPage.Visibility = Visibility.Visible;
                }
            }
        }


        private void TB_OpenApp_KeyUp(object sender, KeyEventArgs e)
        {
            TBL_AddressToFile.Text = dirpath+"\\" + TB_OpenApp.Text;
        }


        private void ButtonPlaySound_Click(object sender, RoutedEventArgs e)
        {
            DWindow_MediaSelector mediaSelector = new DWindow_MediaSelector();
            mediaSelector.Owner = Window.GetWindow(this);
            mediaSelector.ShowDialog();

            if (mediaSelector.OK)
            {
                L_SoundPlay.Content = mediaSelector.FileName;
            }
        }

        private void TB_LoadPres_KeyUp(object sender, KeyEventArgs e)
        {
            TBL_AddressToPres.Text = dirpath + "\\" + TB_LoadPres.Text;
        }
    }
}
