using Good_Teacher.Class.Serialization.Content_Ser;
using System.Windows.Controls;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for ContentViewer.xaml
    /// </summary>
    public partial class ContentViewer : UserControl
    {
        public ContentCreator contentCreator
        {
            get { return contentCreators; }
            set { contentCreators = value; }
        }

        private ContentCreator contentCreators = new ContentCreator();
        

        public ContentViewer()
        {
            InitializeComponent();
        }
    }
}
