using Good_Teacher.Class.Actions;
using Good_Teacher.Class.Actions.Conditions;
using Good_Teacher.Class.Enumerators;
using Good_Teacher.Class.Workers;
using Good_Teacher.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Windows
{
    /// <summary>
    /// Interaction logic for Window_ClickActionsList.xaml
    /// </summary>
    public partial class Window_ClickActionsList : Window
    {
        List<IActions> Actionlist;
        DataStore data;
        int selpos;

        private const int MarginCon = 25;

        public Window_ClickActionsList(DataStore datastore, List<IActions> actionlist, int selectedPosition)
        {
            InitializeComponent();

            Actionlist = actionlist;
            data = datastore;
            selpos = selectedPosition;

            AddActions();
        }


        void AddActions()
        {
            SP_Actions.Children.Clear();
            int marginL = 0;

            foreach (IActions action in Actionlist)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;

                Image imageC = new Image();

                if (action.IsCondition())
                {
                    if (action.DoAction() == 1)
                    {
                        imageC.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/Special/Else.png"));

                        if (marginL > 0)
                        {
                            marginL -= MarginCon;
                        }
                    }
                    else if(action.DoAction() == 2)
                    {
                        imageC.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/Special/Condition.png"));
                    }
                    else
                    {
                        imageC.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/Special/CancelCondition.png"));
                    }
                }
                else
                {
                    imageC.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/Special/Action.png"));
                }
                imageC.Margin = new Thickness(marginL, 5, 0, 0);
                imageC.Width = 20;
                imageC.Height = 20;
                RenderOptions.SetBitmapScalingMode(imageC, BitmapScalingMode.Fant);
                imageC.VerticalAlignment = VerticalAlignment.Center;


                Label labelT = new Label();
                labelT.Content = GetActionName(action);
                labelT.Margin = new Thickness(0, 5, 0, 0);
                labelT.FontWeight = FontWeights.Bold;
                labelT.VerticalAlignment = VerticalAlignment.Center;

                if(action.IsCondition())
                {
                    if (action.DoAction() == 1)
                    {
                        labelT.Foreground = new SolidColorBrush(Colors.DarkBlue);
                    }
                    else
                    {
                        labelT.Foreground = new SolidColorBrush(Colors.DarkRed);
                    }
                    marginL += MarginCon;
                }

                Label label = new Label();
                label.Content = GetActionInfo(action);
                label.Margin = new Thickness(0, 5, 0, 0);
                label.VerticalAlignment = VerticalAlignment.Center;


                FlatButton edit = new FlatButton();
                edit.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/Buttons/Edit.png")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 20,
                    Height = 20
                };
                edit.Click += EditAction_Click;
                edit.Margin = new Thickness(10, 0, 0, 0);
                edit.DefaultBrush = null;
                edit.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                edit.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                edit.Height = 24;
                edit.Width = 24;
                edit.Tag = action;
                edit.VerticalContentAlignment = VerticalAlignment.Center;
                edit.VerticalAlignment = VerticalAlignment.Center;

                FlatButton Delete = new FlatButton();
                Delete.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/DeleteFill.png")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 20,
                    Height = 20
                };
                Delete.Click += DeleteAction_Click;
                Delete.Margin = new Thickness(10, 0, 0, 0);
                Delete.DefaultBrush = null;
                Delete.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                Delete.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                Delete.Height = 24;
                Delete.Width = 24;
                Delete.Tag = action;
                Delete.VerticalContentAlignment = VerticalAlignment.Center;
                Delete.VerticalAlignment = VerticalAlignment.Center;


                stackPanel.Children.Add(imageC);
                stackPanel.Children.Add(labelT);
                stackPanel.Children.Add(label);
                stackPanel.Children.Add(edit);
                stackPanel.Children.Add(Delete);

                SP_Actions.Children.Add(stackPanel);
            }
        }

        private void EditAction_Click(object sender, MouseEventArgs e)
        {
            int id = Actionlist.IndexOf((IActions)((FlatButton)sender).Tag);

            if (((IActions)((FlatButton)sender).Tag).IsCondition())
            {
                Window_AddCondition window_AddCondition = new Window_AddCondition(Actionlist, data, id, selpos);
                window_AddCondition.Owner = Window.GetWindow(this);
                window_AddCondition.ShowDialog();

                if (window_AddCondition.isOK)
                {
                    if (window_AddCondition.actions != null)
                    {
                        Actionlist[id] = window_AddCondition.actions;
                        Debug.WriteLine(window_AddCondition.actions);
                        AddActions();
                    }
                }
            }
            else
            {
                Window_SetOnClickActions window_SetOnClickActions = new Window_SetOnClickActions(Actionlist, data, id, selpos);
                window_SetOnClickActions.Owner = Window.GetWindow(this);
                window_SetOnClickActions.ShowDialog();

                if (window_SetOnClickActions.isOK)
                {
                    if (window_SetOnClickActions.actions != null)
                    {
                        Actionlist[id] = window_SetOnClickActions.actions;
                        Debug.WriteLine(window_SetOnClickActions.actions);
                        AddActions();
                    }
                }
            }
        }

        private void DeleteAction_Click(object sender, MouseEventArgs e)
        {
            Actionlist.Remove((IActions)((FlatButton)sender).Tag);
            AddActions();
        }


        private string GetActionName(IActions action)
        {
            if (action != null)
            {
                switch (action.GetActionType())
                {
                    case ActionType.Action_Type.GoToPage:
                            return Strings.ResStrings.GoToPage;

                    case ActionType.Action_Type.OpenWeb:
                        return Strings.ResStrings.OpenLink;

                    case ActionType.Action_Type.ClosePresentation:
                        return Strings.ResStrings.ClosePresentation;

                    case ActionType.Action_Type.OpenApplication:
                        return Strings.ResStrings.OpenApplication;

                    case ActionType.Action_Type.ShowMessageBox:
                        return Strings.ResStrings.ShowMessageBox;

                    case ActionType.Action_Type.Sound:
                        return Strings.ResStrings.Sound;

                    case ActionType.Action_Type.LoadPresentation:
                        return Strings.ResStrings.LoadPresentation;

                    case ActionType.Action_Type.SetVisibility:
                        return Strings.ResStrings.Visibility;

                    case ActionType.Action_Type.DoAnimation:
                        return Strings.ResStrings.DoAnimation;

                    case ActionType.Action_Type.Position:
                        return Strings.ResStrings.Position;

                    case ActionType.Action_Type.CONDITION_IsChecked:
                        return Strings.ResStrings.IsChecked;

                    case ActionType.Action_Type.CONDITION_Else:
                        return Strings.ResStrings.Else;

                    case ActionType.Action_Type.NoAction:
                        return "-";
                }
            }
            return "-";
        }

        private string GetActionInfo(IActions action)
        {
            if (action != null)
            {
                switch (action.GetActionType())
                {
                    case ActionType.Action_Type.GoToPage:
                        if (((Action_GoToPage)action).ToSpecific)
                            return "- " + ((Action_GoToPage)action).ToPage;
                        else if (((Action_GoToPage)action).Next)
                            return "- " + Strings.ResStrings.NextPage;
                        else
                            return "- " + Strings.ResStrings.PreviousPage;

                    case ActionType.Action_Type.OpenWeb:
                        return "- " + ((Action_OpenWeb)action).Url;

                    case ActionType.Action_Type.ClosePresentation:
                        return "";

                    case ActionType.Action_Type.OpenApplication:
                        return "- " + ((Action_OpenApp)action).AppPath;

                    case ActionType.Action_Type.ShowMessageBox:
                        return "- " + ((Action_ShowMessageBox)action).Title + " -  [ " + ((Action_ShowMessageBox)action).Text + " ]";

                    case ActionType.Action_Type.Sound:

                        if (((Action_Sound)action).Stop)
                            return "- " + Strings.ResStrings.Stop;
                        else if (((Action_Sound)action).PlayAgain)
                            return "- " + Strings.ResStrings.PlayAgainStart;
                        else
                            return "- " + Strings.ResStrings.Play + ": [" + ((Action_Sound)action).PathToPlay + " , " +
                                Strings.ResStrings.Repeat + ": " + LocalizationWorker.BoolToYesNo(((Action_Sound)action).Repeat) + "]";

                    case ActionType.Action_Type.LoadPresentation:
                        return "- " + ((Action_LoadPresentation)action).PresentationPath;

                    case ActionType.Action_Type.SetVisibility:
                        return "- " + Strings.ResStrings.ID + ": " + ((Action_SetVisibility)action).ID + "  [" + SetVisibilityEnum.GetEnumString(((Action_SetVisibility)action).VisibilityValue) + "]";

                    case ActionType.Action_Type.DoAnimation:
                        return "- "+Strings.ResStrings.ID + ": "+ ((Action_DoAnimation)action).AnimationID;

                    case ActionType.Action_Type.Position:

                        string returnpos = Strings.ResStrings.ID + ": "+ ((Action_Position)action).ID+" ";

                        if(((Action_Position)action).ChangeX)
                        {
                            returnpos += "  " + (Strings.ResStrings.AxisX+": " + MathSignEnum.GetSign(((Action_Position)action).SignX)+" "+ ((Action_Position)action).CX+" ");
                        }

                        if (((Action_Position)action).ChangeY)
                        {
                            returnpos += "  " + (Strings.ResStrings.AxisY + ": " + MathSignEnum.GetSign(((Action_Position)action).SignY) + " " + ((Action_Position)action).CY);
                        }

                        return "- "+returnpos;

                    case ActionType.Action_Type.CONDITION_IsChecked:

                        return "- " + Strings.ResStrings.ID + ": " + ((Condition_IsChecked)action).ID + " "+Strings.ResStrings.CIf+": "+LocalizationWorker.BoolCheckedUnchecked(((Condition_IsChecked)action).CheckIfChecked);

                    case ActionType.Action_Type.CONDITION_Else:
                        return "";
                    case ActionType.Action_Type.NoAction:
                        return "";
                }
            }
            return "-";
        }


        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Window_SetOnClickActions window_SetOnClickActions = new Window_SetOnClickActions(data, selpos);
            window_SetOnClickActions.Owner = this;
            window_SetOnClickActions.ShowDialog();

            if (window_SetOnClickActions.isOK)
            {
                if (window_SetOnClickActions.actions != null)
                {
                    Actionlist.Add(window_SetOnClickActions.actions);
                    Debug.WriteLine(window_SetOnClickActions.actions);
                    AddActions();
                }
            }
        }


        private void ButtonAddCondition_Click(object sender, RoutedEventArgs e)
        {
            Window_AddCondition window_AddCondition = new Window_AddCondition();
            window_AddCondition.Owner = this;
            window_AddCondition.ShowDialog();

            if (window_AddCondition.isOK)
            {
                if (window_AddCondition.actions != null)
                {
                    Actionlist.Add(window_AddCondition.actions);
                    Debug.WriteLine(window_AddCondition.actions);
                    AddActions();
                }
            }
        }

    }
}
