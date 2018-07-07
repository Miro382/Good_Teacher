using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher.Controls.Special
{
    /// <summary>
    /// Interaction logic for AlignmentSelector.xaml
    /// </summary>
    public partial class AlignmentSelector : UserControl
    {

        public delegate void ChangedAlignDelegate(HorizontalAlignment horizontal, VerticalAlignment vertical);
        public event ChangedAlignDelegate ChangedAlign;

        public HorizontalAlignment hal = HorizontalAlignment.Center;
        public VerticalAlignment val = VerticalAlignment.Center;

        public AlignmentSelector()
        {
            InitializeComponent();
        }


        public void SetData(HorizontalAlignment horAl, VerticalAlignment verAl)
        {

            if (horAl == HorizontalAlignment.Left)
                Button_LeftAlign.SetCheckedNoCall(true);
            else if (horAl == HorizontalAlignment.Center)
                Button_CenterAlign.SetCheckedNoCall(true);
            else if (horAl == HorizontalAlignment.Right)
                Button_RightAlign.SetCheckedNoCall(true);


            if (verAl == VerticalAlignment.Top)
                Button_VTopAlign.SetCheckedNoCall(true);
            else if (verAl == VerticalAlignment.Center)
                Button_VCenterAlign.SetCheckedNoCall(true);
            else if (verAl == VerticalAlignment.Bottom)
                Button_VBottomAlign.SetCheckedNoCall(true);

            val = verAl;
            hal = horAl;
        }

        private void Button_Align_OnCheckChanged(object sender, bool IsChecked)
        {
                if (sender == Button_LeftAlign)
                {
                    hal = HorizontalAlignment.Left;
                    Button_LeftAlign.SetCheckedNoCall(true);
                    Button_RightAlign.SetCheckedNoCall(false);
                    Button_CenterAlign.SetCheckedNoCall(false);
                }
                else if (sender == Button_CenterAlign)
                {
                    hal = HorizontalAlignment.Center;
                    Button_RightAlign.SetCheckedNoCall(false);
                    Button_LeftAlign.SetCheckedNoCall(false);
                    Button_CenterAlign.SetCheckedNoCall(true);
                }
                else if (sender == Button_RightAlign)
                {
                    hal = HorizontalAlignment.Right;
                    Button_CenterAlign.SetCheckedNoCall(false);
                    Button_LeftAlign.SetCheckedNoCall(false);
                    Button_RightAlign.SetCheckedNoCall(true);
                }
                else if (sender == Button_VTopAlign)
                {
                    val = VerticalAlignment.Top;
                    Button_VCenterAlign.SetCheckedNoCall(false);
                    Button_VBottomAlign.SetCheckedNoCall(false);
                    Button_VTopAlign.SetCheckedNoCall(true);
                }
                else if (sender == Button_VCenterAlign)
                {
                    val = VerticalAlignment.Center;
                    Button_VTopAlign.SetCheckedNoCall(false);
                    Button_VBottomAlign.SetCheckedNoCall(false);
                    Button_VCenterAlign.SetCheckedNoCall(true);
                }
                else if (sender == Button_VBottomAlign)
                {
                    val = VerticalAlignment.Bottom;
                    Button_VTopAlign.SetCheckedNoCall(false);
                    Button_VCenterAlign.SetCheckedNoCall(false);
                    Button_VBottomAlign.SetCheckedNoCall(true);
                }

            if (ChangedAlign != null)
                ChangedAlign(hal,val);
        }


    }
}
