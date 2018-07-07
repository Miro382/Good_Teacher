using System.IO;
using System.Windows.Controls;
using System.Windows.Ink;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for DrawingImage_Control.xaml
    /// </summary>
    public partial class DrawingImage_Control : UserControl
    {

        public byte[] strokeCollection
        {
            get { return strokeCollections; }
            set { strokeCollections = value; }
        }


        public DrawingImage_Control()
        {
            InitializeComponent();
        }


        private byte[] strokeCollections;

        public void UpdateImage()
        {
            using (Stream stream = new MemoryStream(strokeCollection))
            {
                //converted back to StrokeCollection
                StrokeCollection strokes = new StrokeCollection(stream);
            }
        }

    }
}
