using System.Windows;

namespace Good_Teacher.Windows.Popup
{
    /// <summary>
    /// Interaction logic for PWindow_SpecialDialog.xaml
    /// </summary>
    public partial class PWindow_SpecialDialog : Window
    {

        public enum ButtonOption
        {
            None,
            Button1,
            Button2,
            Button3
        };

        public ButtonOption option = ButtonOption.None;

        /// <summary>
        /// Make dialog window
        /// </summary>
        /// <param name="STitle">Title</param>
        /// <param name="SText">Message text</param>
        /// <param name="SButton1">Button 1 - Left = text</param>
        /// <param name="SButton2">Button 2 - middle = text</param>
        /// <param name="SButton3">Button 3 - right = text</param>
        public PWindow_SpecialDialog(string STitle, string SText, string SButton1, string SButton2, string SButton3)
        {
            InitializeComponent();

            option = ButtonOption.None;
            Title = STitle;
            TB_Text.Text = SText;
            TB_B1.Text = SButton1;
            TB_B2.Text = SButton2;
            TB_B3.Text = SButton3;
        }

        /// <summary>
        /// Make dialog window with default button
        /// </summary>
        /// <param name="STitle">Title</param>
        /// <param name="SText">Message text</param>
        /// <param name="SButton1">Button 1 - Left = text</param>
        /// <param name="SButton2">Button 2 - middle = text</param>
        /// <param name="SButton3">Button 3 - right = text</param>
        /// <param name="SetDefault">Set default button = 1 is Button 1; 2 is Button 2; 3 is Button 3</param>
        public PWindow_SpecialDialog(string STitle, string SText, string SButton1, string SButton2, string SButton3, int SetDefault)
        {
            InitializeComponent();

            option = ButtonOption.None;
            Title = STitle;
            TB_Text.Text = SText;
            TB_B1.Text = SButton1;
            TB_B2.Text = SButton2;
            TB_B3.Text = SButton3;

            if(SetDefault == 1)
            {
                B_Opt1.IsDefault = true;
            }
            else if (SetDefault == 2)
            {
                B_Opt2.IsDefault = true;
            }
            else if (SetDefault == 3)
            {
                B_Opt3.IsDefault = true;
            }
        }

        private void B_Opt1_Click(object sender, RoutedEventArgs e)
        {
            option = ButtonOption.Button1;
            Close();
        }

        private void B_Opt2_Click(object sender, RoutedEventArgs e)
        {
            option = ButtonOption.Button2;
            Close();
        }

        private void B_Opt3_Click(object sender, RoutedEventArgs e)
        {
            option = ButtonOption.Button3;
            Close();
        }
    }
}
