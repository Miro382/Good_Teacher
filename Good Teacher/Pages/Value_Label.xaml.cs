using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Good_Teacher.Class;
using Good_Teacher.Class.Save;
using Good_Teacher.Windows.Special;

namespace Good_Teacher
{
    /// <summary>
    /// Interaction logic for Value_Label.xaml
    /// </summary>
    public partial class Value_Label : System.Windows.Controls.Page
    {
        Label lab;
        DataStore data;

        public Value_Label(DataStore datas ,Label label)
        {
            InitializeComponent();

            lab = label;
            data = datas;

            positionselector.STP_WH.Visibility = Visibility.Collapsed;
            positionselector.SetData(label);
            positionselector.LoadData();

            effectselector.SetData(lab);
            effectselector.LoadData();

            brushselectorBack.SetData(lab, data, false);
            brushselectorBack.LoadData(lab.Background);
            brushselectorBack.ChangedBrush -= BrushselectorBack_ChangedBrush;
            brushselectorBack.ChangedBrush += BrushselectorBack_ChangedBrush;

            brushselectorFor.SetData(lab, data, false);
            brushselectorFor.LoadData(lab.Foreground);
            brushselectorFor.ChangedBrush -= BrushselectorFor_ChangedBrush;
            brushselectorFor.ChangedBrush += BrushselectorFor_ChangedBrush;

            fontEditorPanel.Load(lab,data);


            Box_text.Text = (string)label.Content;

            fontEditorPanel.AlignmentPanel.Visibility = Visibility.Collapsed;
        }

        private void BrushselectorFor_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            lab.Foreground = Sbrush;

            if (Sbrush is ImageBrush)
            {
                if (lab.Tag != null)
                {
                    DesignSave designSave = DesignSave.Deserialize(lab.Tag.ToString());
                    designSave.Foreground = new ImageRepresentation(brushSelector.LastSelectedImageKey, ((ImageBrush)lab.Foreground).Stretch);
                    lab.Tag = designSave.Serialize();
                }
                else
                    lab.Tag = new DesignSave(new ImageRepresentation("", Stretch.UniformToFill), new ImageRepresentation(brushSelector.LastSelectedImageKey, ((ImageBrush)lab.Foreground).Stretch)).Serialize();
            }
        }


        private void BrushselectorBack_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            lab.Background = Sbrush;

            if (Sbrush is ImageBrush)
            {
                if (lab.Tag != null)
                {
                    DesignSave designSave = DesignSave.Deserialize(lab.Tag.ToString());
                    designSave.Background = new ImageRepresentation(brushSelector.LastSelectedImageKey, ((ImageBrush)lab.Background).Stretch);
                    lab.Tag = designSave.Serialize();
                }
                else
                    lab.Tag = new DesignSave(new ImageRepresentation(brushSelector.LastSelectedImageKey, ((ImageBrush)lab.Background).Stretch)).Serialize();
            }
        }

        private static String ColorToHexConverter(Color c)
        {
            return "#" + c.A.ToString("X2")  + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        void SetText()
        {
            try
            {
                lab.Content = Box_text.Text;
            }
            catch
            {
                Box_text.Text = "";
            }
        }



        void DetectSender(object senderob)
        {
            if (senderob == Box_text)
                SetText();
        }


        private void Control_LostFocus(object sender, RoutedEventArgs e)
        {
            DetectSender(sender);
        }



        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                DetectSender(sender);
            }else if(e.Key == Key.Escape)
            {
                Keyboard.ClearFocus();
                DetectSender(sender);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((Canvas)((Control)lab).Parent).Children.Remove((Control)lab);
                NavigationService.Content = "";
                ((MainWindow)Window.GetWindow(this)).RemoveSelectedItemEffect();
            }
            catch
            {

            }
        }


        private void Box_text_KeyUp(object sender, KeyEventArgs e)
        {
            DetectSender(sender);
        }


        private void WriteText_Click(object sender, RoutedEventArgs e)
        {
            Window_TextEditor textEditor = new Window_TextEditor(Box_text);
            textEditor.Owner = Window.GetWindow(this);
            textEditor.ShowDialog();
            SetText();
        }

    }
}
