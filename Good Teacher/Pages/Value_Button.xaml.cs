using Good_Teacher.Class.Actions;
using Good_Teacher.Class.Enumerators;
using Good_Teacher.Class.Workers;
using Good_Teacher.Controls;
using Good_Teacher.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_Button.xaml
    /// </summary>
    public partial class Value_Button : System.Windows.Controls.Page
    {
        CButton cont;
        DataStore data;
        int selpos;

        public Value_Button(DataStore datas, CButton button, int selectedPosition)
        {
            InitializeComponent();

            data = datas;

            cont = button;

            selpos = selectedPosition;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();

            effectselector.OpacityPanel.Visibility = Visibility.Collapsed;
            effectselector.Slider_ImgOpacity.Visibility = Visibility.Collapsed;

            BrushSelector_Normal.SetData(cont,data, false,cont.keyN);
            BrushSelector_Normal.LoadData(cont.DefaultBrush);
            BrushSelector_Normal.ChangedBrush -= BrushSelector_Normal_ChangedBrush;
            BrushSelector_Normal.ChangedBrush += BrushSelector_Normal_ChangedBrush;

            BrushSelector_Hover.SetData(cont, data, false,cont.keyH);
            BrushSelector_Hover.LoadData(cont.Hover);
            BrushSelector_Hover.ChangedBrush -= BrushSelector_Hover_ChangedBrush;
            BrushSelector_Hover.ChangedBrush += BrushSelector_Hover_ChangedBrush;

            BrushSelector_Click.SetData(cont, data, false,cont.keyC);
            BrushSelector_Click.LoadData(cont.ClickBrush);
            BrushSelector_Click.ChangedBrush -= BrushSelector_Click_ChangedBrush;
            BrushSelector_Click.ChangedBrush += BrushSelector_Click_ChangedBrush;

            alignmentSelector.SetData(cont.HorizontalContentAlignment, cont.VerticalContentAlignment);
            alignmentSelector.ChangedAlign += AlignmentSelector_ChangedAlign;

            Slider_ImgOpacityNormal.Value = cont.OpacityN * 100;
            Slider_ImgOpacityHover.Value = cont.OpacityHover * 100;
            Slider_ImgOpacityClick.Value = cont.OpacityClick * 100;

            CheckBoxClick.IsChecked = cont.ChangeClick;
            CheckBoxHover.IsChecked = cont.ChangeHover;

            CurrentActionsCount.Text = ""+cont.actions.Count;

            if(cont.ToolTip!=null)
            {
                TB_Tooltip.Text = cont.ToolTip.ToString();
            }

            if(cont.Cursor != null)
            {
                CB_Cursor.IsChecked = true;
            }
            else
            {
                CB_Cursor.IsChecked = false;
            }

        }

        private void AlignmentSelector_ChangedAlign(HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            cont.VerticalContentAlignment = vertical;
            cont.HorizontalContentAlignment = horizontal;
        }


        private void BrushSelector_Click_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            cont.ClickBrush = Sbrush;

            if (Sbrush is ImageBrush)
            {
                cont.keyC = brushSelector.LastSelectedImageKey;
                cont.stretchC = ((ImageBrush)Sbrush).Stretch;
            }
        }

        private void BrushSelector_Hover_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            cont.Hover = Sbrush;

            if (Sbrush is ImageBrush)
            {
                cont.keyH = brushSelector.LastSelectedImageKey;
                cont.stretchH = ((ImageBrush)Sbrush).Stretch;
            }
        }

        private void BrushSelector_Normal_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            cont.DefaultBrush = Sbrush;
            cont.Background = Sbrush;

            if (Sbrush is ImageBrush)
            {
                cont.keyN = brushSelector.LastSelectedImageKey;
                cont.stretchN = ((ImageBrush)Sbrush).Stretch;
            }
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
            }
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

        private void Slider_ImgOpacityHover_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Label_ImageOpacityHover.Content = (int)Slider_ImgOpacityHover.Value + " %";
            if (cont != null)
            {
                cont.OpacityHover = Slider_ImgOpacityHover.Value / 100;
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

        private void CheckBoxHover_Checked(object sender, RoutedEventArgs e)
        {
            if(cont!=null)
                cont.ChangeHover = CheckBoxHover.IsChecked == true;
        }

        private void CheckBoxClick_Checked(object sender, RoutedEventArgs e)
        {
            if (cont != null)
                cont.ChangeClick = CheckBoxClick.IsChecked == true;
        }

        private void Button_OnClickActions_Click(object sender, RoutedEventArgs e)
        {
            Window_ClickActionsList window_ClickActionsList = new Window_ClickActionsList(data, cont, selpos);
            window_ClickActionsList.Owner = Window.GetWindow(this);
            window_ClickActionsList.ShowDialog();
            CurrentActionsCount.Text = "" + cont.actions.Count;
        }

        private void Content_Click(object sender, RoutedEventArgs e)
        {
            DWindow_Content window_Content = new DWindow_Content(data, cont.contentCreator);
            window_Content.Owner = Window.GetWindow(this);
            window_Content.ShowDialog();

            if (window_Content.IsOK == true)
            {
                cont.contentCreator = window_Content.content;
                cont.Content = "";
                cont.Content = cont.contentCreator.Create(data);
            }
        }


        private void CheckBoxCursor_Checked(object sender, RoutedEventArgs e)
        {
            if(cont != null)
            {
                if(CB_Cursor.IsChecked == true)
                {
                    cont.Cursor = Cursors.Hand;
                }
                else
                {
                    cont.Cursor = null;
                }
            }
        }


    }
}
