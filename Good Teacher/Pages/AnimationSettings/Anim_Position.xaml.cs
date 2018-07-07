using Good_Teacher.Class.Animations;
using System;
using System.Windows;

namespace Good_Teacher.Pages.AnimationSettings
{
    /// <summary>
    /// Interaction logic for Anim_Position.xaml
    /// </summary>
    public partial class Anim_Position : System.Windows.Controls.Page
    {
        public delegate void DeleteD(IAnimation current);
        public event DeleteD Delete;

        public delegate void AddAnim(IAnimation created);
        public event AddAnim AddAnimation;
        int id;
        bool Edit = false;

        Animation_Position animation_Position;

        public Anim_Position(int Cid, double x, double y)
        {
            InitializeComponent();

            T_ValueX.Text = "" + Math.Round(x, 3);
            T_ValueY.Text = "" + Math.Round(y, 3);

            Button_OKEdit.Content = Strings.ResStrings.OK;
            id = Cid;
            Edit = false;

            Button_Delete.Visibility = Visibility.Collapsed;
        }

        public Anim_Position(int Cid, double x, double y, Animation_Position Canimation_Position)
        {
            InitializeComponent();

            id = Cid;

            T_ValueX.Text = "" + Math.Round(x,3);
            T_ValueY.Text = "" + Math.Round(y, 3);

            TB_X.Text = ""+Canimation_Position.ToX;
            TB_Y.Text = "" + Canimation_Position.ToY;
            TB_XD.Text = "" + Canimation_Position.TimeX;
            TB_YD.Text = "" + Canimation_Position.TimeY;
            CB_X.IsChecked = Canimation_Position.MX;
            CB_Y.IsChecked = Canimation_Position.MY;
            SL_XAR.Value = (Canimation_Position.accX*100);
            SL_YAR.Value = (Canimation_Position.accY * 100);
            SL_XDR.Value = (Canimation_Position.decX * 100);
            SL_YDR.Value = (Canimation_Position.decY * 100);
            TB_TimeX.Text = "" + Canimation_Position.BTimeX;
            TB_TimeY.Text = "" + Canimation_Position.BTimeY;
            CB_Repeat.IsChecked = Canimation_Position.Repeat;
            CB_Reverse.IsChecked = Canimation_Position.Reverse;
            Button_OKEdit.Content = Strings.ResStrings.Edit;
            animation_Position = Canimation_Position;
            Edit = true;

            Button_Delete.Visibility = Visibility.Visible;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double x, y, dx, dy;
            bool doAn = true;
            double Btx, Bty;

            if (!double.TryParse(TB_X.Text, out x))
            {
                doAn = false;
                TB_X.Text = "0";
            }

            if (!double.TryParse(TB_Y.Text, out y))
            {
                doAn = false;
                TB_Y.Text = "0";
            }

            if (!double.TryParse(TB_XD.Text, out dx))
            {
                doAn = false;
                TB_XD.Text = "0";
            }

            if (!double.TryParse(TB_YD.Text, out dy))
            {
                doAn = false;
                TB_YD.Text = "0";
            }

            if (!double.TryParse(TB_TimeX.Text, out Btx))
            {
                doAn = false;
                TB_TimeX.Text = "0";
            }

            if (!double.TryParse(TB_TimeY.Text, out Bty))
            {
                doAn = false;
                TB_TimeY.Text = "0";
            }

            if (doAn)
            {
                if (Edit)
                {
                    animation_Position.MX = CB_X.IsChecked == true;
                    animation_Position.MY = CB_Y.IsChecked == true;
                    animation_Position.TimeX = dx;
                    animation_Position.TimeY = dy;
                    animation_Position.ToX = x;
                    animation_Position.ToY = y;
                    animation_Position.accX = (SL_XAR.Value / 100);
                    animation_Position.accY = (SL_YAR.Value / 100);
                    animation_Position.decX = (SL_XDR.Value / 100);
                    animation_Position.decY = (SL_YDR.Value / 100);
                    animation_Position.BTimeX = Btx;
                    animation_Position.BTimeY = Bty;
                    animation_Position.Repeat = CB_Repeat.IsChecked == true;
                    animation_Position.Reverse = CB_Reverse.IsChecked == true;
                }
                else
                {
                    AddAnimation?.Invoke(new Animation_Position(id, CB_X.IsChecked == true, CB_Y.IsChecked == true,
                        x, y, dx, dy, (SL_XAR.Value / 100), (SL_YAR.Value / 100), (SL_XDR.Value / 100), (SL_YDR.Value / 100),Btx,Bty, (CB_Repeat.IsChecked == true), (CB_Reverse.IsChecked == true)));
                }
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            Delete?.Invoke(animation_Position);
        }


    }
}
