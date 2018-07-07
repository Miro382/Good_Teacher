using Good_Teacher.Class.Serialization.Content_Ser;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for AnswerButton.xaml
    /// </summary>
    public partial class AnswerButton : UserControl
    {

        public ContentCreator contentCreator
        {
            get { return contentCreators; }
            set { contentCreators = value; }
        }

        public bool Good
        {
            get { return Goods; }
            set { Goods = value; }
        }

        public bool ShowGood
        {
            get { return ShowGoods; }
            set { ShowGoods = value; }
        }

        public string SelectedBrushKey
        {
            get { return SelectedBrushKeys; }
            set { SelectedBrushKeys = value; }
        }

        public string ID
        {
            get { return IDs; }
            set { IDs = value; }
        }

        public Brush SelectedBrush
        {
            get { return SelectedBrushs; }
            set { SelectedBrushs = value; }
        }

        public Brush NormalBr
        {
            get { return NormalBrs; }
            set { NormalBrs = value; }
        }

        public double NormalOp
        {
            get { return NormalOps; }
            set { NormalOps = value; }
        }

        public double HoverOp
        {
            get { return HoverOps; }
            set { HoverOps = value; }
        }

        public double ClickOp
        {
            get { return ClickOps; }
            set { ClickOps = value; }
        }

        private Brush NormalBrs;
        private double NormalOps = 1, HoverOps = 0.8f, ClickOps = 0.6f;
        private Brush SelectedBrushs = new SolidColorBrush(Colors.Red);
        private string SelectedBrushKeys = "";
        private ContentCreator contentCreators = new ContentCreator();
        private bool Goods = true;
        private bool ShowGoods = false;

        private string IDs = "1";

        public delegate void ClickDelegate(AnswerButton sender, MouseButtonEventArgs e);
        public event ClickDelegate Click;

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Opacity = HoverOp;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Opacity = NormalOp;
        }

        private void UserControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Opacity = ClickOp;
        }

        public AnswerButton()
        {
            InitializeComponent();
        }

        private void UserControl_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Opacity = HoverOp;

            if (Click != null)
                Click(this,e);
        }

        public void SetSelected(bool selected)
        {
            if (selected)
            {
                NormalBr = Background;
                Background = SelectedBrush;
            }else if(!selected && NormalBr!=null)
            {
                Background = NormalBr;
            }
        }

    }
}
