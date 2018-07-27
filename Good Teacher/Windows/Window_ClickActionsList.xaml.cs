using Good_Teacher.Class.Actions;
using Good_Teacher.Class.Enumerators;
using Good_Teacher.Class.Workers;
using Good_Teacher.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Good_Teacher.Windows
{
    /// <summary>
    /// Interaction logic for Window_ClickActionsList.xaml
    /// </summary>
    public partial class Window_ClickActionsList : Window
    {
        CButton cont;
        DataStore data;
        int selpos;

        public Window_ClickActionsList(DataStore datastore, CButton button, int selectedPosition)
        {
            InitializeComponent();

            cont = button;
            data = datastore;
            selpos = selectedPosition;

            AddActions();
        }


        void AddActions()
        {
            SP_Actions.Children.Clear();

            foreach (IActions action in cont.actions)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;

                Label labelT = new Label();
                labelT.Content = GetActionName(action);
                labelT.Margin = new Thickness(0, 5, 0, 0);
                labelT.FontWeight = FontWeights.Bold;

                Label label = new Label();
                label.Content = GetActionInfo(action);
                label.Margin = new Thickness(0, 5, 0, 0);


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


                stackPanel.Children.Add(labelT);
                stackPanel.Children.Add(label);
                stackPanel.Children.Add(edit);
                stackPanel.Children.Add(Delete);

                SP_Actions.Children.Add(stackPanel);
            }
        }

        private void EditAction_Click(object sender, MouseEventArgs e)
        {
            int id = cont.actions.IndexOf((IActions)((FlatButton)sender).Tag);
            Window_SetOnClickActions window_SetOnClickActions = new Window_SetOnClickActions(cont, data, id, selpos);
            window_SetOnClickActions.Owner = Window.GetWindow(this);
            window_SetOnClickActions.ShowDialog();

            if (window_SetOnClickActions.isOK)
            {
                if (window_SetOnClickActions.actions != null)
                {
                    cont.actions[id] = window_SetOnClickActions.actions;
                    Debug.WriteLine(window_SetOnClickActions.actions);
                    AddActions();
                }
            }
        }

        private void DeleteAction_Click(object sender, MouseEventArgs e)
        {
            cont.actions.Remove((IActions)((FlatButton)sender).Tag);
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

                    case ActionType.Action_Type.NoAction:
                        return "";
                }
            }
            return "-";
        }


        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Window_SetOnClickActions window_SetOnClickActions = new Window_SetOnClickActions(cont, data, selpos);
            window_SetOnClickActions.Owner = Window.GetWindow(this);
            window_SetOnClickActions.ShowDialog();

            if (window_SetOnClickActions.isOK)
            {
                if (window_SetOnClickActions.actions != null)
                {
                    cont.actions.Add(window_SetOnClickActions.actions);
                    Debug.WriteLine(window_SetOnClickActions.actions);
                    AddActions();
                }
            }
        }



    }
}
