using Good_Teacher.Class.Actions;
using Good_Teacher.Class.Special;
using Good_Teacher.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_Timers.xaml
    /// </summary>
    public partial class Window_Timers : Window
    {

        DataStore data;
        int selpos;


        public Window_Timers(DataStore datastore, int selectedPosition)
        {
            InitializeComponent();

            data = datastore;
            selpos = selectedPosition;

            AddActions();
        }


        void AddActions()
        {
            SP_Actions.Children.Clear();

            int i = 1;
            foreach (TimerAction timer in data.pages[selpos].Timers)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;

                Label labelT = new Label();
                labelT.Content = Strings.ResStrings.Timer+" "+i;
                labelT.Margin = new Thickness(0, 5, 0, 0);
                labelT.FontWeight = FontWeights.Bold;

                Label labeltim = new Label();
                labeltim.Content = Strings.ResStrings.Time;
                labeltim.Margin = new Thickness(5, 5, 0, 0);

                NumberBox numberBox = new NumberBox();
                numberBox.Width = 100;
                numberBox.Padding = new Thickness(1);
                numberBox.Text = ""+timer.DefaultTime;
                numberBox.Tag = timer;
                numberBox.VerticalContentAlignment = VerticalAlignment.Center;
                numberBox.LostFocus += NumberBox_LostFocus;
                numberBox.KeyDown += NumberBox_KeyDown;

                Label label = new Label();
                label.Content = Strings.ResStrings.ActionsCount + " " + timer.Actions.Count;
                label.Margin = new Thickness(5, 5, 0, 0);


                FlatButton edit = new FlatButton();
                edit.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/Buttons/Edit.png")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 20,
                    Height = 20
                };
                edit.Click += EditAction_Click;
                edit.Margin = new Thickness(10, 0, 0, 0);
                edit.DefaultBrush = null;
                edit.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                edit.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                edit.Height = 24;
                edit.Width = 24;
                edit.Tag = timer;
                edit.VerticalContentAlignment = VerticalAlignment.Center;

                FlatButton Delete = new FlatButton();
                Delete.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/DeleteFill.png")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 20,
                    Height = 20
                };
                Delete.Click += DeleteAction_Click;
                Delete.Margin = new Thickness(10, 0, 0, 0);
                Delete.DefaultBrush = null;
                Delete.Hover = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                Delete.ClickBrush = new SolidColorBrush(Color.FromRgb(233, 30, 99));
                Delete.Height = 24;
                Delete.Width = 24;
                Delete.Tag = timer;
                Delete.VerticalContentAlignment = VerticalAlignment.Center;


                stackPanel.Children.Add(labelT);
                stackPanel.Children.Add(labeltim);
                stackPanel.Children.Add(numberBox);
                stackPanel.Children.Add(label);
                stackPanel.Children.Add(edit);
                stackPanel.Children.Add(Delete);

                stackPanel.Margin = new Thickness(5);

                SP_Actions.Children.Add(stackPanel);
                i++;
            }
        }

        private void NumberBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter || e.Key == Key.Escape)
            {
                SetTimeValue((NumberBox)sender);
            }
        }

        private void NumberBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SetTimeValue((NumberBox)sender);
        }

        void SetTimeValue(NumberBox sender)
        {
            TimerAction timer = (TimerAction)sender.Tag;

            float time = sender.GetFloat();

            if(!sender.Success)
            {
                sender.Text = "1";
                time = 1;
            }

            timer.DefaultTime = (float)Math.Round(time,1);
            sender.Text = "" + timer.DefaultTime;
        }

        private void EditAction_Click(object sender, MouseEventArgs e)
        {
            Window_ClickActionsList clickActionsList = new Window_ClickActionsList(data, ((TimerAction)((FlatButton)sender).Tag).Actions, selpos);
            clickActionsList.Owner = this;
            clickActionsList.ShowDialog();
            AddActions();
        }

        private void DeleteAction_Click(object sender, MouseEventArgs e)
        {
            data.pages[selpos].Timers.Remove((TimerAction)((FlatButton)sender).Tag);
            AddActions();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            data.pages[selpos].Timers.Add(new TimerAction(1,new List<IActions>()));
            AddActions();
        }
    }
}
