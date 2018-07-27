using Good_Teacher.Class.Animations;
using Good_Teacher.Class.Workers;
using Good_Teacher.Pages.AnimationSettings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for DWindow_Animations.xaml
    /// </summary>
    public partial class DWindow_Animations : Window
    {
        DataStore data;
        Canvas canvas;
        string txt;

        public delegate void ClickControlHandler(object obj);
        public event ClickControlHandler ClickControl;

        int SelectedI = 0;
        object OBJSelected = null;
        int Pid = 0;

        public DWindow_Animations(int sind,Canvas canvasA, DataStore dataA, object SelectedOBJ)
        {
            InitializeComponent();

            Pid = sind;

            canvas = canvasA;
            data = dataA;

            CB_Type.IsEnabled = false;

            OBJSelected = SelectedOBJ;

            GetControls();
           // CB_Type_SelectionChanged(null, null);
           if(data.pages.Count>0)
            RefreshAnimList();

            //Debug.WriteLine(data.pages[sind].AnimationList.Count);
        }


        public void GetControls()
        {
            for (int i=0;i<canvas.Children.Count;i++)
            {
                if (((FrameworkElement)canvas.Children[i]).Tag == null || ((FrameworkElement)canvas.Children[i]).Tag.ToString() != "!S!")
                {
                    StackPanel item = new StackPanel();
                    item.Tag = i;
                    Label lab = new Label() { Content = "[" + ControlWorker.GetID(((FrameworkElement)canvas.Children[i]).Name) + "] "+ ControlNameWorker.GetTypeName(canvas.Children[i], out txt) };
                    item.Children.Add(lab);
                    ListControls.Items.Add(item);

                    if(OBJSelected != null)
                    {
                        Debug.WriteLine(""+OBJSelected);
                        if(OBJSelected == canvas.Children[i])
                        {
                            ListControls.SelectedIndex = i;
                            Sel_Label.Content = "[" + ControlWorker.GetID(((FrameworkElement)canvas.Children[i]).Name) + "] " + ControlNameWorker.GetTypeName(canvas.Children[i], out txt);
                            SelectedI = i;
                        }
                    }
                }
            }
        }



        private void ListControls_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListControls.SelectedIndex >= 0)
            {
                StackPanel stc = ((StackPanel)ListControls.SelectedItem);

                int iC = int.Parse(stc.Tag.ToString());

                if (ClickControl != null)
                    ClickControl(canvas.Children[iC]);

                Sel_Label.Content = "[" + ControlWorker.GetID(((FrameworkElement)canvas.Children[iC]).Name) + "] " + ControlNameWorker.GetTypeName(canvas.Children[iC], out txt);
                SelectedI = iC;
                CB_Type_SelectionChanged(null, null);

                AnimPanel.SelectedIndex = -1;
            }
        }


        private void CB_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AnimationSettings != null)
            {
                CB_Type.IsEnabled = true;

                if (CB_Type.SelectedIndex == 0)
                {
                    AnimationSettings.Content = null;
                    Anim_Position anim_Position = new Anim_Position(ControlWorker.GetID(((FrameworkElement)canvas.Children[SelectedI]).Name), Canvas.GetLeft( canvas.Children[SelectedI]), Canvas.GetTop(canvas.Children[SelectedI]) );
                    anim_Position.AddAnimation += Anim_Position_AddAnimation;
                    AnimationSettings.Content = anim_Position;
                }
                else if(CB_Type.SelectedIndex == 1)
                {
                    AnimationSettings.Content = null;
                    Anim_Opacity anim_Position = new Anim_Opacity(ControlWorker.GetID(((FrameworkElement)canvas.Children[SelectedI]).Name), canvas.Children[SelectedI].Opacity);
                    anim_Position.AddAnimation += Anim_Position_AddAnimation;
                    AnimationSettings.Content = anim_Position;
                }
                else
                {
                    AnimationSettings.Content = null;
                    Anim_Size anim_Position = new Anim_Size(ControlWorker.GetID(((FrameworkElement)canvas.Children[SelectedI]).Name), ((FrameworkElement)canvas.Children[SelectedI]).Width, ((FrameworkElement)canvas.Children[SelectedI]).Height);
                    anim_Position.AddAnimation += Anim_Position_AddAnimation;
                    AnimationSettings.Content = anim_Position;
                }
            }
        }


        private void Anim_Position_AddAnimation(Class.Animations.IAnimation created)
        {
            data.pages[Pid].AnimationList.Add(created);
            RefreshAnimList();
        }

        void RefreshAnimList()
        {
            if (Pid >= 0)
            {
                AnimPanel.Items.Clear();
                int k = 0;

                List<IAnimation> Removelist = new List<IAnimation>();

                foreach (IAnimation ian in data.pages[Pid].AnimationList)
                {
                    FrameworkElement felm = ControlWorker.FindChild<FrameworkElement>(canvas, "ID_" + ian.GetID());

                    if (felm != null)
                    {
                        StackPanel stc = new StackPanel();
                        stc.Orientation = Orientation.Horizontal;

                        /*
                        if ((k % 2) == 0)
                            stc.Background = new SolidColorBrush(Color.FromRgb(189, 195, 199));
                        else
                            stc.Background = new SolidColorBrush(Color.FromRgb(236, 240, 241));
                        */


                        Label lbl = new Label();
                        lbl.Content =  (k+1) + ".   [" + ian.GetID() + "] " + ControlNameWorker.GetTypeName(felm, out txt);

                        Image img = new Image();

                        if (ian is Animation_Position)
                            img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Animations/MoveAnim.png"));
                        else if (ian is Animation_Opacity)
                            img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Animations/OpacityAnimation.png"));
                        else if (ian is Animation_Size)
                            img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Animations/Scale.png"));

                        img.Width = 32;
                        img.Height = 32;

                        stc.Children.Add(img);
                        stc.Children.Add(lbl);

                        //stc.MouseLeftButtonDown += Stc_MouseLeftButtonDown;

                        stc.Tag = ian;

                        AnimPanel.Items.Add(stc);
                        k++;
                    }
                    else
                    {
                        Removelist.Add(ian);
                    }
                }

                foreach(IAnimation ian in Removelist)
                {
                    data.pages[Pid].AnimationList.Remove(ian);
                }

            }
        }


        private void Anim_Position_Delete(IAnimation current)
        {
            data.pages[Pid].AnimationList.Remove(current);
            RefreshAnimList();
            AnimationSettings.Content = null;
        }

        private void AnimPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AnimPanel.SelectedIndex >= 0)
            {
                //IAnimation ian = (IAnimation)((StackPanel)sender).Tag;

                IAnimation ian = (IAnimation)((StackPanel)AnimPanel.SelectedItem).Tag;

                ListControls.SelectedIndex = -1;

                if (ian is Animation_Position)
                {
                    CB_Type.SelectedIndex = 0;
                    CB_Type.IsEnabled = false;
                    AnimationSettings.Content = null;
                    Anim_Position anim_Position = new Anim_Position(SelectedI, Canvas.GetLeft(canvas.Children[SelectedI]), Canvas.GetTop(canvas.Children[SelectedI]),(Animation_Position)ian);
                    anim_Position.AddAnimation += Anim_Position_AddAnimation;
                    anim_Position.Delete += Anim_Position_Delete;
                    AnimationSettings.Content = anim_Position;
                }
                else if (ian is Animation_Opacity)
                {
                    CB_Type.SelectedIndex = 1;
                    CB_Type.IsEnabled = false;
                    AnimationSettings.Content = null;
                    Anim_Opacity anim = new Anim_Opacity(SelectedI, canvas.Children[SelectedI].Opacity, (Animation_Opacity)ian);
                    anim.AddAnimation += Anim_Position_AddAnimation;
                    anim.Delete += Anim_Position_Delete;
                    AnimationSettings.Content = anim;
                }
                else if (ian is Animation_Size)
                {
                    CB_Type.SelectedIndex = 2;
                    CB_Type.IsEnabled = false;
                    AnimationSettings.Content = null;
                    Anim_Size anim = new Anim_Size(SelectedI, Canvas.GetLeft(canvas.Children[SelectedI]), Canvas.GetTop(canvas.Children[SelectedI]), (Animation_Size)ian);
                    anim.AddAnimation += Anim_Position_AddAnimation;
                    anim.Delete += Anim_Position_Delete;
                    AnimationSettings.Content = anim;
                }

            }
        }

    }
}
