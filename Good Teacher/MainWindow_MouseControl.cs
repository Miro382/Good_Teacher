using Good_Teacher.Pages.Special;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Good_Teacher
{
    public partial class MainWindow : Window
    {

        FrameworkElement SelectionBox;
        bool DraggingM = false;


        bool isDown = false;
        bool DrawArea = false;
        Point pointS;
        const int DiffToStart = 10;

        double lowestX = 0, lowestY = 0;
        double maxX = 0, maxY = 0;

        double CLowX = 0, CLowY = 0;
        double CMaxX = 0, CMaxY = 0;

        Rectangle MSelectedItemEffect;

        List<FrameworkElement> SelectedAreaElements = new List<FrameworkElement>();



        private void DrawSelectionBox()
        {
            Point p = Mouse.GetPosition(DesignCanvas);


            if (SelectionBox != null)
            {
                Canvas.SetLeft(SelectionBox, lowestX);
                Canvas.SetTop(SelectionBox, lowestY);
                SelectionBox.Width = maxX - lowestX;
                SelectionBox.Height = maxY - lowestY;

            }
            else
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Tag = "!S!";
                rectangle.Width = maxX - lowestX;
                rectangle.Height = maxY - lowestY;
                rectangle.IsHitTestVisible = false;
                Canvas.SetLeft(rectangle, lowestX);
                Canvas.SetTop(rectangle, lowestY);
                Panel.SetZIndex(rectangle, int.MaxValue);
                rectangle.Fill = new SolidColorBrush(Color.FromArgb(100, 52, 152, 219));
                rectangle.Stroke = new SolidColorBrush(Color.FromArgb(200, 52, 152, 219));
                rectangle.StrokeThickness = 2;
                DesignCanvas.Children.Add(rectangle);
                SelectionBox = rectangle;
            }
            
        }

        public void DrawMultipleControlsArea()
        {
            if (SelectedAreaElements.Count > 0)
            {
                double adjust = CalcSizeAdjust();
                SizeAdjust = adjust;

                if (MSelectedItemEffect == null)
                {
                    Rectangle border = new Rectangle();
                    border.Tag = "!S!";
                    border.Width = (CMaxX - CLowX + 10 * adjust);
                    border.Height = (CMaxY - CLowY + 10 * adjust);
                    border.StrokeThickness = 2 * adjust;
                    border.StrokeDashArray = new DoubleCollection(new Double[] { 3, 3 });
                    border.Stroke = new SolidColorBrush(Color.FromRgb(192, 57, 43));
                    Canvas.SetLeft(border, CLowX - 5 * adjust);
                    Canvas.SetTop(border, CLowY - 5 * adjust);
                    Panel.SetZIndex(border, int.MaxValue);
                    border.IsHitTestVisible = false;
                    MSelectedItemEffect = border;

                    DesignCanvas.Children.Add(border);

                    StackPanel move = new StackPanel();
                    move.Tag = "!S!";
                    move.Width = 24 * adjust;
                    move.Height = 24 * adjust;
                    move.Background = new SolidColorBrush(Color.FromArgb(180, 149, 165, 166));


                    move.MouseEnter += MoveMulti_MouseEnter;
                    move.MouseLeave += MoveMulti_MouseLeave;
                    move.MouseMove += MoveMulti_MouseMove;
                    move.MouseDown += MoveMulti_MouseDown;
                    move.MouseUp += MoveMulti_MouseUp;


                    move.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                    move.MouseLeftButtonUp += Control_MouseLeftButtonUp;

                    Image img = new Image();
                    img.Margin = new Thickness(2);
                    img.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Specific/Move.png"));
                    move.Children.Add(img);

                    move.UpdateLayout();

                    MoveButton = move;

                    Canvas.SetLeft(move, CLowX + (CMaxX - CLowX) / 2 - (12 * adjust));
                    Canvas.SetTop(move, CLowY - (45 * adjust));
                    Panel.SetZIndex(move, int.MaxValue);

                    DesignCanvas.Children.Add(move);



                    rotCanvas.Children.Clear();

                    rotCanvas.Tag = "!S!";

                    rotCanvas.Width = (CMaxX - CLowX + 10 * adjust);
                    rotCanvas.Height = (CMaxY - CLowY + 10 * adjust );
                    Canvas.SetLeft(rotCanvas, CLowX - 10 * adjust);
                    Canvas.SetTop(rotCanvas, CLowY - 10 * adjust);
                    Panel.SetZIndex(rotCanvas, int.MaxValue);


                    //Bottom right
                    {
                        Ellipse scale = new Ellipse();
                        scale.Width = 10 * SizeAdjust;
                        scale.Height = 10 * SizeAdjust;
                        scale.Fill = new SolidColorBrush(Color.FromRgb(46, 204, 113));
                        scale.Stroke = new SolidColorBrush(Color.FromRgb(25, 111, 61));
                        scale.StrokeThickness = 2 * SizeAdjust;

                        scale.MouseEnter += Move_MouseEnter;
                        scale.MouseLeave += Move_MouseLeave;
                        scale.MouseMove += ScaleBR_MouseMove;

                        scale.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                        scale.MouseLeftButtonUp += Control_MouseLeftButtonUp;

                        ScaleButton[0] = scale;

                        Canvas.SetLeft(scale, rotCanvas.Width);
                        Canvas.SetTop(scale, rotCanvas.Height);
                        Panel.SetZIndex(scale, int.MaxValue);

                        //DesignCanvas.Children.Add(scale);
                        rotCanvas.Children.Add(scale);
                    }

                    //Top Left
                    {
                        Ellipse scale = new Ellipse();
                        scale.Width = 10 * SizeAdjust;
                        scale.Height = 10 * SizeAdjust;
                        scale.Fill = new SolidColorBrush(Color.FromRgb(46, 204, 113));
                        scale.Stroke = new SolidColorBrush(Color.FromRgb(25, 111, 61));
                        scale.StrokeThickness = 2 * SizeAdjust;

                        scale.MouseEnter += Move_MouseEnter;
                        scale.MouseLeave += Move_MouseLeave;
                        scale.MouseMove += ScaleTL_MouseMove;

                        scale.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                        scale.MouseLeftButtonUp += Control_MouseLeftButtonUp;

                        ScaleButton[1] = scale;

                        Canvas.SetLeft(scale, 0);
                        Canvas.SetTop(scale, 0);
                        Panel.SetZIndex(scale, int.MaxValue);

                        //DesignCanvas.Children.Add(scale);
                        rotCanvas.Children.Add(scale);
                    }

                    //Top Right
                    {
                        Ellipse scale = new Ellipse();
                        scale.Width = 10 * SizeAdjust;
                        scale.Height = 10 * SizeAdjust;
                        scale.Fill = new SolidColorBrush(Color.FromRgb(46, 204, 113));
                        scale.Stroke = new SolidColorBrush(Color.FromRgb(25, 111, 61));
                        scale.StrokeThickness = 2 * SizeAdjust;

                        scale.MouseEnter += Move_MouseEnter;
                        scale.MouseLeave += Move_MouseLeave;
                        scale.MouseMove += ScaleTR_MouseMove;

                        scale.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                        scale.MouseLeftButtonUp += Control_MouseLeftButtonUp;

                        ScaleButton[2] = scale;

                        Canvas.SetLeft(scale, rotCanvas.Width);
                        Canvas.SetTop(scale, 0);
                        Panel.SetZIndex(scale, int.MaxValue);

                        //DesignCanvas.Children.Add(scale);
                        rotCanvas.Children.Add(scale);
                    }


                    //Bottom left
                    {
                        Ellipse scale = new Ellipse();
                        scale.Width = 10 * SizeAdjust;
                        scale.Height = 10 * SizeAdjust;
                        scale.Fill = new SolidColorBrush(Color.FromRgb(46, 204, 113));
                        scale.Stroke = new SolidColorBrush(Color.FromRgb(25, 111, 61));
                        scale.StrokeThickness = 2 * SizeAdjust;

                        scale.MouseEnter += Move_MouseEnter;
                        scale.MouseLeave += Move_MouseLeave;
                        scale.MouseMove += ScaleBL_MouseMove;

                        scale.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                        scale.MouseLeftButtonUp += Control_MouseLeftButtonUp;

                        ScaleButton[3] = scale;

                        Canvas.SetLeft(scale, 0);
                        Canvas.SetTop(scale, rotCanvas.Height);
                        Panel.SetZIndex(scale, int.MaxValue);

                        //DesignCanvas.Children.Add(scale);
                        rotCanvas.Children.Add(scale);
                    }



                    //Top
                    {
                        Ellipse scale = new Ellipse();
                        scale.Width = 10 * SizeAdjust;
                        scale.Height = 10 * SizeAdjust;
                        scale.Fill = new SolidColorBrush(Color.FromRgb(52, 152, 219));
                        scale.Stroke = new SolidColorBrush(Color.FromRgb(25, 79, 115));
                        scale.StrokeThickness = 2 * SizeAdjust;

                        scale.MouseEnter += Move_MouseEnter;
                        scale.MouseLeave += Move_MouseLeave;
                        scale.MouseMove += ScaleT_MouseMove;

                        scale.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                        scale.MouseLeftButtonUp += Control_MouseLeftButtonUp;

                        ScaleButton[4] = scale;

                        Canvas.SetLeft(scale, rotCanvas.Width / 2);
                        Canvas.SetTop(scale, -((10 * SizeAdjust) / 2));
                        Panel.SetZIndex(scale, int.MaxValue);

                        //DesignCanvas.Children.Add(scale);
                        rotCanvas.Children.Add(scale);
                    }

                    //Left
                    {
                        Ellipse scale = new Ellipse();
                        scale.Width = 10 * SizeAdjust;
                        scale.Height = 10 * SizeAdjust;
                        scale.Fill = new SolidColorBrush(Color.FromRgb(52, 152, 219));
                        scale.Stroke = new SolidColorBrush(Color.FromRgb(25, 79, 115));
                        scale.StrokeThickness = 2 * SizeAdjust;

                        scale.MouseEnter += Move_MouseEnter;
                        scale.MouseLeave += Move_MouseLeave;
                        scale.MouseMove += ScaleL_MouseMove;

                        scale.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                        scale.MouseLeftButtonUp += Control_MouseLeftButtonUp;

                        ScaleButton[5] = scale;

                        Canvas.SetLeft(scale, -((10 * SizeAdjust) / 2));
                        Canvas.SetTop(scale, rotCanvas.Height / 2);
                        Panel.SetZIndex(scale, int.MaxValue);

                        //DesignCanvas.Children.Add(scale);
                        rotCanvas.Children.Add(scale);
                    }


                    //Bottom
                    {
                        Ellipse scale = new Ellipse();
                        scale.Width = 10 * SizeAdjust;
                        scale.Height = 10 * SizeAdjust;
                        scale.Fill = new SolidColorBrush(Color.FromRgb(52, 152, 219));
                        scale.Stroke = new SolidColorBrush(Color.FromRgb(25, 79, 115));
                        scale.StrokeThickness = 2 * SizeAdjust;

                        scale.MouseEnter += Move_MouseEnter;
                        scale.MouseLeave += Move_MouseLeave;
                        scale.MouseMove += ScaleB_MouseMove;

                        scale.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                        scale.MouseLeftButtonUp += Control_MouseLeftButtonUp;

                        ScaleButton[6] = scale;

                        Canvas.SetLeft(scale, rotCanvas.Width / 2);
                        Canvas.SetTop(scale, rotCanvas.Height + ((10 * SizeAdjust) / 2));
                        Panel.SetZIndex(scale, int.MaxValue);

                        //DesignCanvas.Children.Add(scale);
                        rotCanvas.Children.Add(scale);
                    }


                    //Right
                    {
                        Ellipse scale = new Ellipse();
                        scale.Width = 10 * SizeAdjust;
                        scale.Height = 10 * SizeAdjust;
                        scale.Fill = new SolidColorBrush(Color.FromRgb(52, 152, 219));
                        scale.Stroke = new SolidColorBrush(Color.FromRgb(25, 79, 115));
                        scale.StrokeThickness = 2 * SizeAdjust;

                        scale.MouseEnter += Move_MouseEnter;
                        scale.MouseLeave += Move_MouseLeave;
                        scale.MouseMove += ScaleR_MouseMove;

                        scale.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                        scale.MouseLeftButtonUp += Control_MouseLeftButtonUp;

                        ScaleButton[7] = scale;

                        Canvas.SetLeft(scale, rotCanvas.Width + ((10 * SizeAdjust) / 2));
                        Canvas.SetTop(scale, rotCanvas.Height / 2);
                        Panel.SetZIndex(scale, int.MaxValue);

                        //DesignCanvas.Children.Add(scale);
                        rotCanvas.Children.Add(scale);
                    }

                    DesignCanvas.Children.Add(rotCanvas);

                }
                else
                {
                    MSelectedItemEffect.Width = (CMaxX - CLowX + 10 * adjust);
                    MSelectedItemEffect.Height = (CMaxY - CLowY + 10 * adjust);
                    Canvas.SetLeft(MSelectedItemEffect, CLowX - 5 * adjust);
                    Canvas.SetTop(MSelectedItemEffect, CLowY - 5 * adjust);

                    Canvas.SetLeft(MoveButton, CLowX + (CMaxX - CLowX) / 2 - (12 * adjust));
                    Canvas.SetTop(MoveButton, CLowY - (45 * adjust));

                    rotCanvas.Width = (CMaxX - CLowX + 10 * adjust);
                    rotCanvas.Height = (CMaxY - CLowY + 10 * adjust);
                    Canvas.SetLeft(rotCanvas, CLowX - 10 * adjust);
                    Canvas.SetTop(rotCanvas, CLowY - 10 * adjust);

                    //Bottom right
                    Canvas.SetLeft(ScaleButton[0], rotCanvas.Width);
                    Canvas.SetTop(ScaleButton[0], rotCanvas.Height);

                    //Top Left
                    Canvas.SetLeft(ScaleButton[1], 0);
                    Canvas.SetTop(ScaleButton[1], 0);

                    //Top Right
                    Canvas.SetLeft(ScaleButton[2], rotCanvas.Width);
                    Canvas.SetTop(ScaleButton[2], 0);

                    //Bottom left
                    Canvas.SetLeft(ScaleButton[3], 0);
                    Canvas.SetTop(ScaleButton[3], rotCanvas.Height);


                    //Top
                    Canvas.SetLeft(ScaleButton[4], rotCanvas.Width / 2);
                    Canvas.SetTop(ScaleButton[4], -((10 * SizeAdjust) / 2));

                    //Left
                    Canvas.SetLeft(ScaleButton[5], -((10 * SizeAdjust) / 2));
                    Canvas.SetTop(ScaleButton[5], rotCanvas.Height / 2);

                    //Bottom
                    Canvas.SetLeft(ScaleButton[6], rotCanvas.Width / 2);
                    Canvas.SetTop(ScaleButton[6], rotCanvas.Height + ((10 * SizeAdjust) / 2));

                    //Right
                    Canvas.SetLeft(ScaleButton[7], rotCanvas.Width + ((10 * SizeAdjust) / 2));
                    Canvas.SetTop(ScaleButton[7], rotCanvas.Height / 2);
                }

            }
        }

        private void MoveMulti_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DraggingM = false;

            if(BMoveOnGrid)
            {
                foreach (FrameworkElement element in SelectedAreaElements)
                {
                    Canvas.SetLeft(element, ((int)Canvas.GetLeft(element) / GridSize) * GridSize);
                    Canvas.SetTop(element, ((int)Canvas.GetTop(element) / GridSize) * GridSize);
                }

                CalcNewAreaSizes();
                DrawMultipleControlsArea();
            }
        }

        private void MoveMulti_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DraggingM = true;
        }

        private void MoveMulti_MouseMove(object sender, MouseEventArgs e)
        {
            if(DraggingM)
            {
                if (SelectedAreaElements.Count > 0)
                {

                    Point canp = e.GetPosition(DesignCanvas);

                    CMaxX = (CMaxX + (canp.X - LastPoint.X));
                    CMaxY = (CMaxY + (canp.Y - LastPoint.Y));

                    CLowX = (CLowX + (canp.X - LastPoint.X));
                    CLowY = (CLowY + (canp.Y - LastPoint.Y));

                    foreach (FrameworkElement selcol in SelectedAreaElements)
                    {
                        double changex = (Canvas.GetLeft(selcol) + (canp.X - LastPoint.X));
                        double changey = (Canvas.GetTop(selcol) + (canp.Y - LastPoint.Y));

                        if (changex < LimitScreenMaxX && changex >= LimitScreenMinX)
                            Canvas.SetLeft(selcol, changex);
                        if (changey < LimitScreenMaxY && changey >= LimitScreenMinY)
                            Canvas.SetTop(selcol, changey);
                    }

                    LastPoint = canp;

                    DrawMultipleControlsArea();
                }
            }
        }

        private void MoveMulti_MouseLeave(object sender, MouseEventArgs e)
        {
            ((FrameworkElement)sender).Opacity = 1;
            DraggingM = false;
        }

        private void MoveMulti_MouseEnter(object sender, MouseEventArgs e)
        {
            ((FrameworkElement)sender).Opacity = 0.7f;
        }

        public void CalcNewAreaSizes()
        {
            CLowX = double.MaxValue;
            CLowY = double.MaxValue;
            CMaxX = double.MinValue;
            CMaxY = double.MinValue;

            foreach (FrameworkElement element in SelectedAreaElements)
            {
                double x = Canvas.GetLeft(element);
                double y = Canvas.GetTop(element);

                if (x < CLowX)
                    CLowX = x;

                if (y < CLowY)
                    CLowY = y;

                if (x + element.Width > CMaxX)
                    CMaxX = x + element.Width;

                if (y + element.Height > CMaxY)
                    CMaxY = y + element.Height;
            }
        }

        private void DisableSelection()
        {
            if (isDown && DrawArea)
            {
                SelectedAreaElements.Clear();

                CLowX = double.MaxValue;
                CLowY = double.MaxValue;
                CMaxX = double.MinValue;
                CMaxY = double.MinValue;

                foreach (FrameworkElement element in DesignCanvas.Children)
                {
                    double x = Canvas.GetLeft(element);
                    double y = Canvas.GetTop(element);
                    double w = element.Width;
                    double h = element.Height;

                    if(element is Label)
                    {
                        w = element.ActualWidth;
                        h = element.ActualHeight;
                    }

                    if (x >= lowestX && x + w < maxX && y >= lowestY && y + h < maxY)
                    {
                        if (x < CLowX)
                            CLowX = x;

                        if (y < CLowY)
                            CLowY = y;

                        if (x + w > CMaxX)
                            CMaxX = x + w;

                        if (y + h > CMaxY)
                            CMaxY = y + h;

                        SelectedAreaElements.Add(element);
                    }
                }

                if (SelectedAreaElements.Count > 1)
                {
                    ValueEditor.Content = new Page_MultiSelect(DesignCanvas, SelectedAreaElements, CLowX, CLowY, CMaxX, CMaxY);

                }else if(SelectedAreaElements.Count > 0)
                {
                    EditControl(SelectedAreaElements[0]);
                    SelectedAreaElements.Clear();
                }

                DrawMultipleControlsArea();

            }

            isDown = false;

            if (SelectionBox != null)
            {
                DesignCanvas.Children.Remove(SelectionBox);
                SelectionBox = null;
            }

        }


        private void ScrollViewerZoom_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource == DesignCanvas && ControlSelect == false)
            {
                RemoveSelectedItemEffect();
                isDown = true;
                pointS = Mouse.GetPosition(DesignCanvas);
                DrawArea = false;
            }
        }

        private void ScrollViewerZoom_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DisableSelection();
        }

        private void ScrollViewerZoom_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (isDown)
            {
                Point curp = Mouse.GetPosition(DesignCanvas);
                double diffX = Math.Abs(pointS.X - curp.X);
                double diffY = Math.Abs(pointS.Y - curp.Y);

                if (diffX > DiffToStart || diffY > DiffToStart)
                {


                    if (pointS.X < curp.X)
                    {
                        lowestX = pointS.X;
                        maxX = curp.X;
                    }
                    else
                    {
                        lowestX = curp.X;
                        maxX = pointS.X;
                    }


                    if (pointS.Y < curp.Y)
                    {
                        lowestY = pointS.Y;
                        maxY = curp.Y;
                    }
                    else
                    {
                        lowestY = curp.Y;
                        maxY = pointS.Y;
                    }

                    DrawArea = true;
                    DrawSelectionBox();
                }
            }

        }


        private void ScrollViewerZoom_MouseLeave(object sender, MouseEventArgs e)
        {
            DisableSelection();
        }


    }
}
