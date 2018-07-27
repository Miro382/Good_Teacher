using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_CodeEditor.xaml
    /// </summary>
    public partial class Window_CodeEditor : Window
    {

        int currsel = 0;
        DataStore data;
        Canvas can;

        const string defaultcode = @"function main()
{
	Alert('Caption','Text');

    //Remove first element
	//Element.RemoveAt(0);

    //Hide element with ID 0
    //FindByID(0,Canvas).Visibility = Collapsed;

    //After click on first element - removes second element
    //Element[0].add_MouseLeftButtonDown(function(sender, eventArgs) { Element.RemoveAt(1); });
};

main();";

        public Window_CodeEditor(DataStore datastore, Canvas canvas , int CurrSelected)
        {
            InitializeComponent();

            data = datastore;
            currsel = CurrSelected;
            can = canvas;

            L_Page.Content = Strings.ResStrings.Page + ": " + (currsel+1);

            codeeditor.textEditor.Text = data.pages[currsel].ScriptCode;
        }


        private void ButtonCloseOnly_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonSaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            data.pages[currsel].ScriptCode = codeeditor.textEditor.Text;
            Close();
        }

        private void ButtonSample_Click(object sender, RoutedEventArgs e)
        {
            codeeditor.textEditor.Text = defaultcode;
        }

        private void ButtonGetID_Click(object sender, RoutedEventArgs e)
        {
            Window_ControlsID window_ControlsID = new Window_ControlsID(can);
            window_ControlsID.Owner = this;
            window_ControlsID.Show();
        }
    }
}
