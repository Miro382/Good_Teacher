using System.Windows;
using System.Windows.Media;

namespace Good_Teacher.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for DWindow_NewModel.xaml
    /// </summary>
    public partial class DWindow_NewModel : Window
    {

        public bool IsOK = false;
        public string Model = "";
        public bool Texture = true;
        public Color color = Colors.LightGray;

        public DWindow_NewModel()
        {
            InitializeComponent();
            IsOK = false;
        }


        private void AddNewModel_Click(object sender, RoutedEventArgs e)
        {
            IsOK = true;
            Texture = (Checkbox_Texture.IsChecked == true);
            color = ((SolidColorBrush)Rect_BackColor.Fill).Color;
            Close();
        }

        private void SetModel_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = Strings.ResStrings.ModelObject+" (obj,3ds,lwo,stl,off)|*.obj;*.3ds;*.lwo;*.stl;*.off|"+Strings.ResStrings.AllFiles+"|*.*";
            if (openFileDialog.ShowDialog()==true)
            {
                Model = openFileDialog.FileName;
                TB_Model.Text = openFileDialog.FileName;
            }

            /*
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.ShowNewFolderButton = false;
                dialog.Description = Strings.ResStrings.SelectFolderModel;
                DialogResult result = dialog.ShowDialog();
                Model = dialog.SelectedPath;
                TB_Model.Text = dialog.SelectedPath;
            }
            */
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
