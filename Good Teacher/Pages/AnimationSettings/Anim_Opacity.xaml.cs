using Good_Teacher.Class.Animations;
using System.Windows;

namespace Good_Teacher.Pages.AnimationSettings
{
    /// <summary>
    /// Interaction logic for Anim_Opacity.xaml
    /// </summary>
    public partial class Anim_Opacity : System.Windows.Controls.Page
    {

        public delegate void DeleteD(IAnimation current);
        public event DeleteD Delete;

        public delegate void AddAnim(IAnimation created);
        public event AddAnim AddAnimation;
        int id;
        bool Edit = false;

        Animation_Opacity animation_Position;

        public Anim_Opacity(int Cid, double opacity)
        {
            InitializeComponent();

            T_ValueOp.Text = "" + ((int)(opacity * 100))+"%";

            Button_OKEdit.Content = Strings.ResStrings.OK;
            id = Cid;
            Edit = false;

            Button_Delete.Visibility = Visibility.Collapsed;
        }


        public Anim_Opacity(int Cid, double opacity, Animation_Opacity Canimation_Position)
        {
            InitializeComponent();

            T_ValueOp.Text = "" + ((int)(opacity * 100))+"%";

            id = Cid;
            TB_Duration.Text = "" + Canimation_Position.Time;
            TB_Time.Text = "" + Canimation_Position.FromSec;
            SL_ToOP.Value = (Canimation_Position.opacity* 100);
            animation_Position = Canimation_Position;
            CB_Repeat.IsChecked = Canimation_Position.Repeat;
            CB_Reverse.IsChecked = Canimation_Position.Reverse;
            Edit = true;
            Button_OKEdit.Content = Strings.ResStrings.Edit;

            Button_Delete.Visibility = Visibility.Visible;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            double dur,btime;
            bool doAn = true;

            if (!double.TryParse(TB_Duration.Text, out dur))
            {
                doAn = false;
                TB_Duration.Text = "1";
            }

            if (!double.TryParse(TB_Time.Text, out btime))
            {
                doAn = false;
                TB_Time.Text = "0";
            }

            if (doAn)
            {
                if (Edit)
                {
                    animation_Position.Time = dur;
                    animation_Position.FromSec = btime;
                    animation_Position.opacity = (SL_ToOP.Value / 100);
                    animation_Position.Repeat = (CB_Repeat.IsChecked == true);
                    animation_Position.Reverse = (CB_Reverse.IsChecked == true);
                }
                else
                {
                    AddAnimation?.Invoke(new Animation_Opacity(id, (SL_ToOP.Value / 100),dur,btime, (CB_Repeat.IsChecked==true), (CB_Reverse.IsChecked == true)));
                }
            }
            
        }


        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            Delete?.Invoke(animation_Position);
        }

        private void SL_ToOP_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(TB_Op != null)
            {
                TB_Op.Text = (int)SL_ToOP.Value + "%";
            }
        }

    }
}
