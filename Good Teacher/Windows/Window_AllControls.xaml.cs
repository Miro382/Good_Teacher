using Good_Teacher.Class.Workers;
using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher.Windows
{
    /// <summary>
    /// Interaction logic for Window_AllControls.xaml
    /// </summary>
    public partial class Window_AllControls : Window
    {
        public delegate void ClickControlHandler(object obj);
        public event ClickControlHandler ClickControl;

        string property = "";

        public Window_AllControls(Canvas canvas, string Text)
        {
            InitializeComponent();

            Title += " - " + Text;

            int i = 0;
            foreach (FrameworkElement elem in canvas.Children)
            {
                if (elem.Tag == null || elem.Tag.ToString() != "!S!")
                {
                    MenuItem item = new MenuItem();
                    StackPanel panel = new StackPanel();
                    Label lab = new Label();
                    lab.Content = ControlWorker.GetTypeName(elem, out property);
                    lab.FontWeight = FontWeights.Bold;
                    Label lab2 = new Label();
                    lab2.Content = "X: " + elem.GetValue(Canvas.LeftProperty) + "   Y: " + elem.GetValue(Canvas.TopProperty) + "   Z: " + Panel.GetZIndex(elem) + "    " + property;
                    panel.Children.Add(lab);
                    panel.Children.Add(lab2);
                    panel.Orientation = Orientation.Horizontal;

                    item.Header = panel;
                    item.Click += Item_Click;
                    item.Tag = elem;
                    Menu_list.Items.Add(item);
                    i++;
                }
            }

            property = "";
        }

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            if (ClickControl != null)
                ClickControl(((MenuItem)sender).Tag);
        }


    }
}
