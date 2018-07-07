using System.Windows;
using System.Windows.Media;

namespace Good_Teacher.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for DWindow_ExistingModel.xaml
    /// </summary>
    public partial class DWindow_ExistingModel : Window
    {
        public bool IsOK = false;
        public bool Texture = true;
        public Color color = Colors.LightGray;


        public DWindow_ExistingModel(string ModelName)
        {
            InitializeComponent();
            ObjectName.Content = ModelName;
        }


        private void AddNewModel_Click(object sender, RoutedEventArgs e)
        {
            IsOK = true;
            Texture = (Checkbox_Texture.IsChecked == true);
            color = ((SolidColorBrush)Rect_BackColor.Fill).Color;
            Close();
        }


        private void ButtonColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_BackColor.Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                Rect_BackColor.Fill = new SolidColorBrush(colorp.GetColor());
            }
        }

    }
}
