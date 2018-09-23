using Good_Teacher.Class.Actions;
using Good_Teacher.Class.Actions.Conditions;
using Good_Teacher.Class.Enumerators;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Window_AddCondition.xaml
    /// </summary>
    public partial class Window_AddCondition : Window
    {

        public bool isOK = false;
        public ActionType.Action_Type action_type;
        public IActions actions;
        public DataStore data;
        int selpos;

        public Window_AddCondition()
        {
            InitializeComponent();
        }


        public Window_AddCondition(List<IActions> actionlist, DataStore dataStore, int EditID, int selectedPosition)
        {
            InitializeComponent();
            data = dataStore;

            selpos = selectedPosition;

            Edit(actionlist, EditID);
        }


        void Edit(List<IActions> actionlist, int pos)
        {

            if (actionlist[pos] != null)
            {
                switch (actionlist[pos].GetActionType())
                {
                    case ActionType.Action_Type.CONDITION_IsChecked:
                        tabcontrol_actions.SelectedIndex = 0;

                        NB_CheckedID.Text = ""+((Condition_IsChecked)actionlist[pos]).ID;
                        RB_CheckedFalse.IsChecked = !((Condition_IsChecked)actionlist[pos]).CheckIfChecked;
                        RB_CheckedTrue.IsChecked = ((Condition_IsChecked)actionlist[pos]).CheckIfChecked;

                        break;
                    case ActionType.Action_Type.CONDITION_Else:
                        tabcontrol_actions.SelectedIndex = 1;

                        break;
                    case ActionType.Action_Type.CONDITION_CancelCondition:
                        tabcontrol_actions.SelectedIndex = 2;

                        break;

                }
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (tabcontrol_actions.SelectedIndex == 0) //is checked
            {
                action_type = ActionType.Action_Type.CONDITION_IsChecked;

                actions = new Condition_IsChecked();

                ((Condition_IsChecked)actions).ID = NB_CheckedID.GetInt();
                ((Condition_IsChecked)actions).CheckIfChecked = RB_CheckedTrue.IsChecked == true;

                isOK = true;
                Close();
            }
            else if(tabcontrol_actions.SelectedIndex == 1) //else
            {
                action_type = ActionType.Action_Type.CONDITION_Else;

                actions = new Condition_Else();

                isOK = true;
                Close();
            }
            else if (tabcontrol_actions.SelectedIndex == 2) //Cancel condition
            {
                action_type = ActionType.Action_Type.CONDITION_CancelCondition;

                actions = new Condition_CancelCondition();

                isOK = true;
                Close();
            }
        }


    }
}
