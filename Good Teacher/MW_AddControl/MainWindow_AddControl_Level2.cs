using Good_Teacher.Controls;
using Good_Teacher.Controls.Shapes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Good_Teacher
{
    public partial class MainWindow : Window
    {
        public void AddLevel2Control(Point p)
        {
            switch (ControlTag)
            {
                case 21:
                    {
                        Rectangle rect = new Rectangle();

                        rect.Fill = Brushes.LightGray;
                        rect.Stroke = Brushes.Black;

                        rect.Width = 128;
                        rect.Height = 128;

                        rect.Focusable = true;

                        rect.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(rect, -1);
                        Canvas.SetLeft(rect, p.X - rect.DesiredSize.Width / 2);
                        Canvas.SetTop(rect, p.Y - rect.DesiredSize.Height / 2);

                        AddEvents(rect);
                        DesignCanvas.Children.Add(rect);
                    }
                    break;

                case 22:
                    {
                        Ellipse ellipse = new Ellipse();

                        ellipse.Fill = Brushes.LightGray;
                        ellipse.Stroke = Brushes.Black;

                        ellipse.Width = 128;
                        ellipse.Height = 128;

                        ellipse.Focusable = true;


                        ellipse.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(ellipse, -1);
                        Canvas.SetLeft(ellipse, p.X - ellipse.DesiredSize.Width / 2);
                        Canvas.SetTop(ellipse, p.Y - ellipse.DesiredSize.Height / 2);

                        AddEvents(ellipse);
                        DesignCanvas.Children.Add(ellipse);
                    }
                    break;
                case 23:
                    {
                        Line line = new Line();

                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 3;
                        line.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        line.X1 = Math.Round(p.X, 1) - 100;
                        line.Y1 = Math.Round(p.Y, 1);
                        line.X2 = Math.Round(p.X, 1) + 100;
                        line.Y2 = Math.Round(p.Y, 1) + 0;
                        Panel.SetZIndex(line, 0);

                        AddEvents(line);
                        DesignCanvas.Children.Add(line);
                    }
                    break;


                case 24:
                    {
                        Hexagon con = new Hexagon();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 144;
                        con.Height = 128;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;

                case 25:
                    {
                        Controls.Triangle con = new Controls.Triangle();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 192;
                        con.Height = 128;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;


                case 216:
                    {
                        RightAngledTriangle con = new RightAngledTriangle();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 192;
                        con.Height = 128;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;

                case 217:
                    {
                        RightAngledTriangleSE con = new RightAngledTriangleSE();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 192;
                        con.Height = 128;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;

                case 218:
                    {
                        Chevron con = new Chevron();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 192;
                        con.Height = 128;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;

                case 219:
                    {
                        Drop con = new Drop();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 112;
                        con.Height = 192;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;

                case 26:
                    {
                        Star con = new Star();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 128;
                        con.Height = 128;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;

                case 27:
                    {
                        Diamond con = new Diamond();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 192;
                        con.Height = 128;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;

                case 28:
                    {
                        Heart con = new Heart();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 160;
                        con.Height = 128;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;

                case 29:
                    {
                        Cloud con = new Cloud();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 192;
                        con.Height = 128;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;
                case 210:
                    {
                        Arrow con = new Arrow();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 192;
                        con.Height = 128;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;
                case 211:
                    {
                        SmileFace con = new SmileFace();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 128;
                        con.Height = 128;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;
                case 212:
                    {
                        Speech con = new Speech();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 192;
                        con.Height = 128;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;
                case 213:
                    {
                        Ribbon con = new Ribbon();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 128;
                        con.Height = 192;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;
                case 214:
                    {
                        CheckMark con = new CheckMark();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 192;
                        con.Height = 128;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
                        Canvas.SetLeft(con, p.X - con.DesiredSize.Width / 2);
                        Canvas.SetTop(con, p.Y - con.DesiredSize.Height / 2);

                        AddEvents(con);
                        DesignCanvas.Children.Add(con);
                    }
                    break;
                case 215:
                    {
                        Cross con = new Cross();

                        con.Fill = Brushes.LightGray;
                        con.Stroke = Brushes.Black;

                        con.Width = 128;
                        con.Height = 128;

                        con.Focusable = true;

                        con.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        Panel.SetZIndex(con, -1);
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
