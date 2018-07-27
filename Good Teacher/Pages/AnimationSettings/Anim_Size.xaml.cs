using Good_Teacher.Class.Animations;
using System;
using System.Windows;

namespace Good_Teacher.Pages.AnimationSettings
{
    /// <summary>
    /// Interaction logic for Anim_Size.xaml
    /// </summary>
    public partial class Anim_Size : System.Windows.Controls.Page
    {


        public delegate void DeleteD(IAnimation current);
        public event DeleteD Delete;

        public delegate void AddAnim(IAnimation created);
        public event AddAnim AddAnimation;
        int id;
        bool Edit = false;

        Animation_Size animation;



        public Anim_Size(int Cid, double w, double h)
        {
            InitializeComponent();


            T_ValueX.Text = "" + Math.Round(w, 3);
            T_ValueY.Text = "" + Math.Round(h, 3);

            Button_OKEdit.Content = Strings.ResStrings.OK;
            id = Cid;
            Edit = false;

            Button_Delete.Visibility = Visibility.Collapsed;
        }

        public Anim_Size(int Cid, double w, double h, Animation_Size Canimation)
        {
            InitializeComponent();

            id = Cid;

            T_ValueX.Text = "" + Math.Round(w, 3);
            T_ValueY.Text = "" + Math.Round(h, 3);

            TB_W.Text = "" + Canimation.ToX;
            TB_H.Text = "" + Canimation.ToY;
            TB_XD.Text = "" + Canimation.TimeX;
            TB_YD.Text = "" + Canimation.TimeY;
            CB_X.IsChecked = Canimation.MX;
            CB_Y.IsChecked = Canimation.MY;
            SL_XAR.Value = (Canimation.accX * 100);
            SL_YAR.Value = (Canimation.accY * 100);
            SL_XDR.Value = (Canimation.decX * 100);
            SL_YDR.Value = (Canimation.decY * 100);
            TB_TimeX.Text = "" + Canimation.BTimeX;
            TB_TimeY.Text = "" + Canimation.BTimeY;
            CB_Repeat.IsChecked = Canimation.Repeat;
            CB_Reverse.IsChecked = Canimation.Reverse;
            CB_OnStart.IsChecked = Canimation.DoAnimationOnStart;
            Button_OKEdit.Content = Strings.ResStrings.Edit;
            animation = Canimation;
            Edit = true;

            Button_Delete.Visibility = Visibility.Visible;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double x, y, dx, dy;
            bool doAn = true;
            double Btx, Bty;

            if (!double.TryParse(TB_W.Text, out x))
            {
                doAn = false;
                TB_W.Text = "0";
            }

            if (!double.TryParse(TB_H.Text, out y))
            {
                doAn = false;
                TB_H.Text = "0";
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
                    animation.MX = CB_X.IsChecked == true;
                    animation.MY = CB_Y.IsChecked == true;
                    animation.TimeX = dx;
                    animation.TimeY = dy;
                    animation.ToX = x;
                    animation.ToY = y;
                    animation.accX = (SL_XAR.Value / 100);
                    animation.accY = (SL_YAR.Value / 100);
                    animation.decX = (SL_XDR.Value / 100);
                    animation.decY = (SL_YDR.Value / 100);
                    animation.BTimeX = Btx;
                    animation.BTimeY = Bty;
                    animation.Repeat = CB_Repeat.IsChecked == true;
                    animation.Reverse = CB_Reverse.IsChecked == true;
                    animation.DoAnimationOnStart = (CB_OnStart.IsChecked == true);
                }
                else
                {
                    AddAnimation?.Invoke(new Animation_Size(id, CB_OnStart.IsChecked == true, CB_X.IsChecked == true, CB_Y.IsChecked == true,
                        x, y, dx, dy, (SL_XAR.Value / 100), (SL_YAR.Value / 100), (SL_XDR.Value / 100), (SL_YDR.Value / 100), Btx, Bty, (CB_Repeat.IsChecked == true), (CB_Reverse.IsChecked == true)));
                }
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            Delete?.Invoke(animation);
        }




    }
}
