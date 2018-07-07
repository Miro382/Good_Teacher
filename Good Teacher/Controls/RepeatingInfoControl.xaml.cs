using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for RepeatingInfoControl.xaml
    /// </summary>
    public partial class RepeatingInfoControl : UserControl
    {
        public delegate void RemoveClickDelegate(RepeatingInfoControl sender);
        public event RemoveClickDelegate RemoveClick;

        public RepeatingInfoControl()
        {
            InitializeComponent();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            MGrid.Background = new SolidColorBrush(Color.FromRgb(112, 111, 211));
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            MGrid.Background = new SolidColorBrush(Color.FromRgb(71, 71, 135));
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MGrid.Background = new SolidColorBrush(Color.FromRgb(129, 127, 242));
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MGrid.Background = new SolidColorBrush(Color.FromRgb(112, 111, 211));
        }

        private void FlatButton_Click(object sender, MouseEventArgs e)
        {
            RemoveClick?.Invoke(this);
        }
    }
}
