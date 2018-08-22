using Good_Teacher.Controls;
using Good_Teacher.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_ToggleButton.xaml
    /// </summary>
    public partial class Value_ToggleButton : Page
    {

        ToggleButton_Control cont;
        DataStore data;

        int selpos;

        public Value_ToggleButton(DataStore datas,ToggleButton_Control toggle, int SelectedPosition)
        {
            InitializeComponent();

            data = datas;

            cont = toggle;

            selpos = SelectedPosition;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();

            effectselector.OpacityPanel.Visibility = Visibility.Collapsed;
            effectselector.Slider_ImgOpacity.Visibility = Visibility.Collapsed;

            BrushSelector_Unchecked.SetData(cont.CBorder, data, false, cont.keyN);
            BrushSelector_Unchecked.LoadData(cont.UncheckedBrush);
            BrushSelector_Unchecked.ChangedBrush -= BrushSelector_Unchecked_ChangedBrush;
            BrushSelector_Unchecked.ChangedBrush += BrushSelector_Unchecked_ChangedBrush;

            BrushSelector_Checked.SetData(cont.CBorder, data, false, cont.keyC);
            BrushSelector_Checked.LoadData(cont.CheckedBrush);
            BrushSelector_Checked.ChangedBrush -= BrushSelector_Checked_ChangedBrush;
            BrushSelector_Checked.ChangedBrush += BrushSelector_Checked_ChangedBrush;

            alignmentSelector.SetData(cont.Ccontent.HorizontalAlignment, cont.Ccontent.VerticalAlignment);
            alignmentSelector.ChangedAlign += AlignmentSelector_ChangedAlign;

            Slider_ImgOpacityNormal.Value = cont.OpacityN * 100;
            Slider_ImgOpacityClick.Value = cont.OpacityClick * 100;

            if (cont.ToolTip != null)
            {
                TB_Tooltip.Text = cont.ToolTip.ToString();
            }

            TB_Radius.Text = "" + cont.CBorder.CornerRadius.TopLeft;

            CurrentActionsCount.Text = "" + cont.UncheckedActions.Count;
            CurrentActionsCountC.Text = "" + cont.CheckedActions.Count;

            if (cont.Cursor != null)
            {
                CB_Cursor.IsChecked = true;
            }
            else
            {
                CB_Cursor.IsChecked = false;
            }

            CB_Animation.IsChecked = cont.DoAnimation;

            CB_IsCheckedDefault.IsChecked = cont.DefaultIsChecked;
        }


        private void AlignmentSelector_ChangedAlign(HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            cont.Ccontent.VerticalAlignment = vertical;
            cont.Ccontent.HorizontalAlignment = horizontal;
        }


        private void BrushSelector_Checked_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            cont.CheckedBrush = Sbrush;

            if (Sbrush is ImageBrush)
            {
                cont.keyC = brushSelector.LastSelectedImageKey;
                cont.stretchC = ((ImageBrush)Sbrush).Stretch;
            }

            cont.SetChecked(cont.IsChecked);
        }

        private void BrushSelector_Unchecked_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            cont.UncheckedBrush = Sbrush;

            if (Sbrush is ImageBrush)
            {
                cont.keyN = brushSelector.LastSelectedImageKey;
                cont.stretchN = ((ImageBrush)Sbrush).Stretch;
            }

            cont.SetChecked(cont.IsChecked);
        }


        private static String ColorToHexConverter(Color c)
        {
            return "#" + c.A.ToString("X2") + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }


        void DetectSender(object senderob)
        {
            if (senderob == TB_Tooltip)
            {
                if (String.IsNullOrWhiteSpace(TB_Tooltip.Text))
                    cont.ToolTip = null;
                else
                    cont.ToolTip = TB_Tooltip.Text;

            }else if(senderob == TB_Radius)
            {
                SetRadius();
            }
        }


        private void SetRadius()
        {
            cont.CBorder.CornerRadius = new CornerRadius(TB_Radius.GetDouble());
        }


        private void Control_LostFocus(object sender, RoutedEventArgs e)
        {
            DetectSender(sender);
        }



        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DetectSender(sender);
            }
            else if (e.Key == Key.Escape)
            {
                Keyboard.ClearFocus();
                DetectSender(sender);
            }
        }




        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((Canvas)cont.Parent).Children.Remove(cont);
                NavigationService.Content = "";
                ((MainWindow)Window.GetWindow(this)).RemoveSelectedItemEffect();
            }
            catch
            {

            }
        }

        private void Slider_ImgOpacityClick_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Label_ImageOpacityClick.Content = (int)Slider_ImgOpacityClick.Value + " %";
            if (cont != null)
            {
                cont.OpacityClick = Slider_ImgOpacityClick.Value / 100;
            }
        }

        private void Slider_ImgOpacityNormal_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Label_ImageOpacityNormal.Content = (int)Slider_ImgOpacityNormal.Value + " %";
            if (cont != null)
            {
                cont.OpacityN = Slider_ImgOpacityNormal.Value / 100;
                cont.Opacity = Slider_ImgOpacityNormal.Value / 100;
            }
        }

        private void Content_Click(object sender, RoutedEventArgs e)
        {
            DWindow_Content window_Content = new DWindow_Content(data, cont.contentCreatorChecked);
            window_Content.Owner = Window.GetWindow(this);
            window_Content.ShowDialog();

            if (window_Content.IsOK == true)
            {
                cont.contentCreatorChecked = window_Content.content;
                cont.SetChecked(cont.IsChecked);
            }
        }

        private void ContentUn_Click(object sender, RoutedEventArgs e)
        {
            DWindow_Content window_Content = new DWindow_Content(data, cont.contentCreatorUnchecked);
            window_Content.Owner = Window.GetWindow(this);
            window_Content.ShowDialog();

            if (window_Content.IsOK == true)
            {
                cont.contentCreatorUnchecked = window_Content.content;
                cont.SetChecked(cont.IsChecked);
            }
        }


        private void CheckBoxCursor_Checked(object sender, RoutedEventArgs e)
        {
            if (cont != null)
            {
                if (CB_Cursor.IsChecked == true)
                {
                    cont.Cursor = Cursors.Hand;
                }
                else
                {
                    cont.Cursor = null;
                }
            }
        }

        private void CheckBoxAnim_Checked(object sender, RoutedEventArgs e)
        {
            if (cont != null)
            {
                cont.DoAnimation = CB_Animation.IsChecked == true;
            }
        }

        private void Button_OnClickActions_Click(object sender, RoutedEventArgs e)
        {
            Window_ClickActionsList window_ClickActionsList = new Window_ClickActionsList(data, cont.UncheckedActions, selpos);
            window_ClickActionsList.Owner = Window.GetWindow(this);
            window_ClickActionsList.ShowDialog();
            CurrentActionsCount.Text = "" + cont.UncheckedActions.Count;
        }

        private void Button_OnClickActionsCH_Click(object sender, RoutedEventArgs e)
        {
            Window_ClickActionsList window_ClickActionsList = new Window_ClickActionsList(data, cont.CheckedActions, selpos);
            window_ClickActionsList.Owner = Window.GetWindow(this);
            window_ClickActionsList.ShowDialog();
            CurrentActionsCountC.Text = "" + cont.CheckedActions.Count;
        }

        private void CB_IsCheckedDefault_Checked(object sender, RoutedEventArgs e)
        {
            if(cont != null)
            {
                cont.DefaultIsChecked = CB_IsCheckedDefault.IsChecked == true;
            }
        }

    }
}
