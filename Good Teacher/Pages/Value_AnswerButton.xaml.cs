using Good_Teacher.Controls;
using Good_Teacher.Controls.Special;
using Good_Teacher.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_AnswerButton.xaml
    /// </summary>
    public partial class Value_AnswerButton : System.Windows.Controls.Page
    {
        AnswerButton cont;
        DataStore data;

        public Value_AnswerButton(DataStore datas, AnswerButton answerButton)
        {
            InitializeComponent();

            cont = answerButton;

            data = datas;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();
            effectselector.OpacityPanel.Visibility = Visibility.Collapsed;
            effectselector.Slider_ImgOpacity.Visibility = Visibility.Collapsed;

            alignmentSelector.SetData(cont.AnswerPanel.HorizontalAlignment,cont.AnswerPanel.VerticalAlignment);
            alignmentSelector.ChangedAlign += AlignmentSelector_ChangedAlign;

            brushselector.SetData(cont, data, true);
            brushselector.LoadData(cont.Background);
            brushselector.ChangedBrush -= Brushselector_ChangedBrush;
            brushselector.ChangedBrush += Brushselector_ChangedBrush;

            SelectedBrushselector.SetData(cont, data, false, cont.SelectedBrushKey);
            SelectedBrushselector.LoadData(cont.SelectedBrush);
            SelectedBrushselector.ChangedBrush -= SelectedBrushselector_ChangedBrush;
            SelectedBrushselector.ChangedBrush += SelectedBrushselector_ChangedBrush;

            RB_Good.IsChecked = cont.Good;
            RB_Wrong.IsChecked = !cont.Good;
            CB_ShowGood.IsChecked = cont.ShowGood;

            TB_QuestionID.Text = cont.ID;
        }

        private void AlignmentSelector_ChangedAlign(HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            cont.AnswerPanel.HorizontalAlignment = horizontal;
            cont.AnswerPanel.VerticalAlignment = vertical;
        }

        private void SelectedBrushselector_ChangedBrush(BrushSelector brushSelector, Brush Sbrush)
        {
            cont.SelectedBrush = Sbrush;

            if(Sbrush is ImageBrush)
            {
                cont.SelectedBrushKey = SelectedBrushselector.LastSelectedImageKey;
                Debug.WriteLine("SLBK: "+ brushselector.LastSelectedImageKey);
            }
        }

        private void Brushselector_ChangedBrush(BrushSelector brushSelector, Brush Sbrush)
        {
            cont.Background = Sbrush;
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


        private void Slider_Opacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(cont!=null)
            {
                if(sender == Slider_NOpacity)
                {
                    Label_NOpacity.Content = (int)Slider_NOpacity.Value+ " %";
                    cont.Opacity = Slider_NOpacity.Value / 100;
                    cont.NormalOp = Slider_NOpacity.Value / 100;
                }
                else if (sender == Slider_HOpacity)
                {
                    Label_HOpacity.Content = (int)Slider_HOpacity.Value + " %";
                    cont.HoverOp = Slider_HOpacity.Value / 100;
                }
                else if (sender == Slider_COpacity)
                {
                    Label_COpacity.Content = (int)Slider_COpacity.Value + " %";
                    cont.ClickOp = Slider_COpacity.Value / 100;
                }
            }
        }

        private void TB_QuestionID_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            cont.ID = TB_QuestionID.Text;
        }


        private void Content_Click(object sender, RoutedEventArgs e)
        {
            DWindow_Content window_Content = new DWindow_Content(data,cont.contentCreator);
            window_Content.Owner = Window.GetWindow(this);
            window_Content.ShowDialog();

            if(window_Content.IsOK==true)
            {
                cont.contentCreator = window_Content.content;
                cont.AnswerPanel.Children.Clear();
                cont.AnswerPanel.Children.Add(cont.contentCreator.Create(data));
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (cont != null)
            {
                if (RB_Good.IsChecked == true)
                    cont.Good = true;
                else
                    cont.Good = false;
            }
        }


        private void CheckBoxShowGood_Checked(object sender, RoutedEventArgs e)
        {
            if (cont != null)
                cont.ShowGood = CB_ShowGood.IsChecked == true;
        }


    }
}
