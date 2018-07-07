using Good_Teacher.Windows.Special;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Good_Teacher.Pages.Manual
{
    /// <summary>
    /// Interaction logic for ManualP_Main.xaml
    /// </summary>
    public partial class ManualP_Main : Page
    {

        public ManualP_Main()
        {
            InitializeComponent();
        }

        private void TextBlockRegister_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Window_Manual window_Manual = (Window_Manual)Window.GetWindow(this);
                int ToPage = int.Parse(((TextElement)e.OriginalSource).Tag.ToString());
                window_Manual.CurrentSelected = ToPage;
                window_Manual.F_Manual.Content = window_Manual.pages[ToPage];
                window_Manual.SC_Frame.ScrollToHome();

            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
