using Good_Teacher.Class.Serialization.Content_Ser;
using Good_Teacher.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Good_Teacher
{
    public partial class MainWindow : Window
    {
        public void AddLevel4Control(Point p)
        {
            switch (ControlTag)
            {
                case 41:
                    {

                        CButton con = new CButton();

                        con.DefaultBrush = new SolidColorBrush(Color.FromRgb(189, 195, 199));
                        con.Hover = new SolidColorBrush(Color.FromRgb(189, 195, 199));
                        con.ClickBrush = new SolidColorBrush(Color.FromRgb(189, 195, 199));

                        con.ChangeHover = false;
                        con.OpacityHover = 0.8;

                        con.ChangeClick = false;
                        con.OpacityClick = 0.6;

                        con.contentCreator.contents.Add(new Content_Text(Strings.ResStrings.Button, 20, 0));
                        con.Content = con.contentCreator.Create(data);

                        con.Width = 150;
                        con.Height = 50;

                        Panel.SetZIndex(con, 10);

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);

                    }
                    break;

                case 42:
                    {

                        AnswerButton con = new AnswerButton();

                        Panel.SetZIndex(con, 5);

                        con.Width = 250;
                        con.Height = 60;

                        con.contentCreator.contents.Add(new Content_Text(Strings.ResStrings.AnswerButton, 20, 0));
                        con.AnswerPanel.Children.Add(con.contentCreator.Create(data));

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);

                    }
                    break;

                case 43:
                    {
                        TextBox textbox = new TextBox();
                        textbox.Text = Strings.ResStrings.EditBox;
                        textbox.IsReadOnly = true;
                        textbox.Width = 100;
                        textbox.Height = 20;
                        textbox.VerticalContentAlignment = VerticalAlignment.Center;
                        textbox.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        textbox.Focusable = false;


                        Canvas.SetLeft(textbox, p.X - textbox.DesiredSize.Width / 2);
                        Canvas.SetTop(textbox, p.Y - textbox.DesiredSize.Height / 2);

                        AddEvents(textbox);
                        DesignCanvas.Children.Add(textbox);
                    }
                    break;

                case 44:
                    {
                        CheckBox con = new CheckBox();
                        con.IsChecked = false;

                        con.Width = 150;
                        con.Height = 30;

                        con.Content = Strings.ResStrings.Checkbox;

                        con.Focusable = false;

                        con.VerticalContentAlignment = VerticalAlignment.Center;

                        Panel.SetZIndex(con, 2);

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;

                case 45:
                    {
                        RadioButton con = new RadioButton();

                        con.Width = 150;
                        con.Height = 30;

                        con.Content = Strings.ResStrings.RadioButton;

                        con.Focusable = false;

                        con.GroupName = "1";
                        con.IsChecked = false;

                        con.VerticalContentAlignment = VerticalAlignment.Center;

                        Panel.SetZIndex(con, 2);

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;

                case 46:
                    {
                        ComboBox_Control con = new ComboBox_Control();

                        con.Width = 190;
                        con.Height = 40;

                        con.combobox.VerticalContentAlignment = VerticalAlignment.Center;

                        ContentCreator contentCreator = new ContentCreator();
                        contentCreator.contents.Add(new Content_Text(Strings.ResStrings.ComboBox, 16, 0));

                        con.contents.Add(contentCreator);
                        con.Create(data);

                        con.Focusable = false;

                        Panel.SetZIndex(con, 2);

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;


                case 47:
                    {
                        ToggleButton_Control con = new ToggleButton_Control();

                        con.Width = 150;
                        con.Height = 50;

                        con.contentCreatorUnchecked.contents.Add(new Content_Text(Strings.ResStrings.Off, 20, 10));

                        con.contentCreatorChecked.contents.Add(new Content_Text(Strings.ResStrings.On, 20, 10));

                        con.SetData(data);

                        con.SetChecked(false, true);

                        Panel.SetZIndex(con, 2);

                        con.Ccontent.VerticalAlignment = VerticalAlignment.Center;
                        con.Ccontent.HorizontalAlignment = HorizontalAlignment.Left;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;

            }
        }
    }
}
