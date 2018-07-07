using System.Windows.Controls;
using System.Windows.Input;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for OpacityButton.xaml
    /// </summary>
    public partial class OpacityButton : UserControl
    {
        public float NormalOpacity { get; set; } = 1;
        public float HoverOpacity { get; set; } = 0.7f;
        public float ClickOpacity { get; set; } = 0.5f;

        public OpacityButton()
        {
            InitializeComponent();
            Opacity = NormalOpacity;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Opacity = HoverOpacity;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Opacity = NormalOpacity;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Opacity = ClickOpacity;
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Opacity = HoverOpacity;
        }
    }
}
