using Good_Teacher.Class.Serialization.Content_Ser;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for ComboBox_Control.xaml
    /// </summary>
    public partial class ComboBox_Control : UserControl
    {

        public List<ContentCreator> contents
        {
            get { return contentsp; }
            set { contentsp = value; }
        }

        private List<ContentCreator> contentsp = new List<ContentCreator>();

        public ComboBox_Control()
        {
            InitializeComponent();
        }


        public void Create(DataStore data)
        {
            combobox.Items.Clear();

            foreach(ContentCreator cont in contents)
            {
                combobox.Items.Add(new ContentViewer() { Content = cont.Create(data), MaxHeight = 200 });
            }
        }


        public int GetSelectedIndex()
        {
            return combobox.SelectedIndex;
        }

        public void SetSelectedIndex(int index)
        {
            combobox.SelectedIndex = index;
        }

    }
}
