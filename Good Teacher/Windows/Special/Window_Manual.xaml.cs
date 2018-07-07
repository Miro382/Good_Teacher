using Good_Teacher.Pages.Manual;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_Manual.xaml
    /// </summary>
    public partial class Window_Manual : Window
    {

        public Page[] pages = { new ManualP_Main(), new ManualP_1Start(), new ManualP_2CanvasSettings(), new ManualP_3Image(), new ManualP_4EditControl(), new ManualP_5SaveAndExport() };
        public int CurrentSelected = 0;

        public Window_Manual()
        {
            InitializeComponent();
            F_Manual.Content = new ManualP_Main();
        }


        private void FlatButtonPrevious_Click(object sender, MouseEventArgs e)
        {
            if(CurrentSelected>0)
            {
                CurrentSelected--;
                F_Manual.Content = pages[CurrentSelected];
                SC_Frame.ScrollToHome();
            }
        }

        private void FlatButtonNext_Click(object sender, MouseEventArgs e)
        {
            if (CurrentSelected < pages.Length-1)
            {
                CurrentSelected++;
                F_Manual.Content = pages[CurrentSelected];
                SC_Frame.ScrollToHome();
            }
        }

        private void FlatButtonRegister_Click(object sender, MouseEventArgs e)
        {
            CurrentSelected = 0;
            F_Manual.Content = pages[0];
        }


        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Right)
            {
                FlatButtonNext_Click(null, null);
            }
            else if (e.Key == Key.Left)
            {
                FlatButtonPrevious_Click(null, null);
            }
            else if (e.Key == Key.Home)
            {
                FlatButtonRegister_Click(null, null);
            }
        }


    }
}
