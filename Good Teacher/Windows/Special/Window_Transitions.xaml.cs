using System.Windows;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_Transitions.xaml
    /// </summary>
    public partial class Window_Transitions : Window
    {
        DataStore data;
        int curp;

        public Window_Transitions(int CurrPage, DataStore datastore)
        {
            InitializeComponent();

            data = datastore;
            curp = CurrPage;

            TB_Time.Text = "" + ((float)data.pages[curp].TransitionMove/1000);

            if(data.pages[curp].transitionType == Class.Enumerators.TransitionTypeEnum.TransitionType.Automatic)
            {
                RB_Automatic.IsChecked = true;
            }
            else if (data.pages[curp].transitionType == Class.Enumerators.TransitionTypeEnum.TransitionType.AutomaticClose)
            {
                RB_AutomaticClose.IsChecked = true;
            }
            else
            {
                RB_Manual.IsChecked = true;
            }
        }


        private void ButtonSetUp_Click(object sender, RoutedEventArgs e)
        {
            data.pages[curp].TransitionMove = (int)(TB_Time.GetFloat() * 1000);
            
            if(RB_Manual.IsChecked == true)
            {
                data.pages[curp].transitionType = Class.Enumerators.TransitionTypeEnum.TransitionType.Manual;
            }
            else if (RB_Automatic.IsChecked == true)
            {
                data.pages[curp].transitionType = Class.Enumerators.TransitionTypeEnum.TransitionType.Automatic;
            }
            else if (RB_AutomaticClose.IsChecked == true)
            {
                data.pages[curp].transitionType = Class.Enumerators.TransitionTypeEnum.TransitionType.AutomaticClose;
            }

            Close();
        }


        private void ButtonSetUpAll_Click(object sender, RoutedEventArgs e)
        {
            int time = (int)(TB_Time.GetFloat() * 1000);

            Class.Enumerators.TransitionTypeEnum.TransitionType transitionType = Class.Enumerators.TransitionTypeEnum.TransitionType.Manual;

            if (RB_Manual.IsChecked == true)
            {
                transitionType = Class.Enumerators.TransitionTypeEnum.TransitionType.Manual;
            }
            else if (RB_Automatic.IsChecked == true)
            {
                transitionType = Class.Enumerators.TransitionTypeEnum.TransitionType.Automatic;
            }
            else if (RB_AutomaticClose.IsChecked == true)
            {
                transitionType = Class.Enumerators.TransitionTypeEnum.TransitionType.AutomaticClose;
            }


            for (int i = 0; i < data.pages.Count; i++)
            {
                data.pages[i].TransitionMove = time;
                data.pages[i].transitionType = transitionType;
            }

            Close();
        }


    }
}
