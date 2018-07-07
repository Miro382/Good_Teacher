using Good_Teacher.Class.History;
using System;
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
        Rectangle SelectedItemEffect = null;
        StackPanel MoveButton = null;
        Ellipse[] ScaleButton = new Ellipse[8];
        Canvas rotCanvas = new Canvas();
        bool MovedByMoveButton = false;
        double SizeAdjust;

        bool IsSpecialNoDrawEffectControl(FrameworkElement elm)
        {
            if (elm is Label || elm is Line)
                return true;
            else
                return false;
        }


        double CalcSizeAdjust()
        {
            double bigger = MainWindow.CanvasW;
            double sadj = 0;
            double DivideNum = 1280;

            if (MainWindow.CanvasH > bigger)
            {
                bigger = MainWindow.CanvasH;
                DivideNum = 720;
            }

            sadj = bigger / DivideNum;

            if (sadj < 0.6)
                sadj = 0.6;

            sadj += (ControlAreaSize * sadj);
            return sadj;
        }



        void DrawSelectedItem(FrameworkElement FRelm)
        {
            if (FRelm != null)
            {
                RemoveSelectedItemEffect();

                FrameworkElement elm = FRelm;

                SizeAdjust = CalcSizeAdjust();

                if (IsSpecialNoDrawEffectControl(elm) == false)
                {

                    Rectangle border = new Rectangle();
                    border.Tag = "!S!";
                    border.Width = elm.Width + 10 * SizeAdjust;
                    border.Height = elm.Height + 10 * SizeAdjust;
                    border.StrokeThickness = 2 * SizeAdjust;
                    border.StrokeDashArray = new DoubleCollection(new Double[] { 3, 3 });
                    border.Stroke = new SolidColorBrush(Color.FromRgb(192, 57, 43));
                    Canvas.SetLeft(border, Canvas.GetLeft(elm) - 5 * SizeAdjust);
                    Canvas.SetTop(border, Canvas.GetTop(elm) - 5 * SizeAdjust);
                    Panel.SetZIndex(border, int.MaxValue);
                    border.IsHitTestVisible = false;
                    SelectedItemEffect = border;
                    DesignCanvas.Children.Add(border);

                    StackPanel move = new StackPanel();
                    move.Tag = "!S!";
                    move.Width = 24 * SizeAdjust;
                    move.Height = 24 * SizeAdjust;
                    move.Background = new SolidColorBrush(Color.FromArgb(180, 149, 165, 166));
                    move.MouseEnter += Move_MouseEnter;
                    move.MouseLeave += Move_MouseLeave;
                    move.MouseMove += Move_MouseMove;
                    move.MouseDown += Move_MouseDown;
                    move.MouseUp += Move_MouseUp;

                    move.MouseLeftButtonDown += Control_MouseLeftButtonDown;
                    move.MouseLeftButtonUp += Control_MouseLeftButtonUp;

                    Image img = new Image();
                    img.Margin = new Thickness(2);
                    img.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Specific/Move.png"));
                    move.Children.Add(img);

                    MoveButton = move;

                    Canvas.SetLeft(move, Canvas.GetLeft(elm) + elm.Width / 2 - (12 * SizeAdjust));
                    Canvas.SetTop(move, Canvas.GetTop(elm) - (45 * SizeAdjust));
                    Panel.SetZIndex(move, int.MaxValue);

                    DesignCanvas.Children.Add(move);

                    rotCanvas.Children.Clear();

                    rotCanvas.Tag = "!S!";

                    rotCanvas.Width = elm.Width + 10 * SizeAdjust;
                    rotCanvas.Height = elm.Height + 10 * SizeAdjust;
                    Canvas.SetLeft(rotCanvas, Canvas.GetLeft(elm) - 10 * SizeAdjust);
                    Canvas.SetTop(rotCanvas, Canvas.GetTop(elm) - 10 * SizeAdjust);
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
            }

        }


        private void ScaleBR_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging && DragC != null)
            {
                double changex = e.GetPosition(DesignCanvas).X - LastPoint.X;
                double changey = e.GetPosition(DesignCanvas).Y - LastPoint.Y;

                if (MSelectedItemEffect != null)
                {
                    if(CMaxX + changex > CLowX)
                        CMaxX += changex;

                    if (CMaxY + changey > CLowY)
                        CMaxY += changey;

                    foreach (FrameworkElement selcol in SelectedAreaElements)
                    {

                        if (selcol.Width + changex > 3)
                        {
                            selcol.Width += changex;
                        }
                        if (selcol.Height + changey > 3)
                        {
                            selcol.Height += changey;
                        }
                    }

                    LastPoint = e.GetPosition(DesignCanvas);
                    DrawMultipleControlsArea();
                }
                else
                {
                    FrameworkElement selcol = ((FrameworkElement)SelectedControl);

                    if (selcol.Width + changex > 3)
                        selcol.Width += changex;
                    if (selcol.Height + changey > 3)
                        selcol.Height += changey;
                    LastPoint = e.GetPosition(DesignCanvas);


                    UpdateSelectedEffect();
                }
            }
        }

        private void ScaleBL_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging && DragC != null)
            {
                double changex = e.GetPosition(DesignCanvas).X - LastPoint.X;
                double changey = e.GetPosition(DesignCanvas).Y - LastPoint.Y;

                if (MSelectedItemEffect != null)
                {

                    if (CMaxY + changey > CLowY)
                        CMaxY += changey;

                    if (CLowX + (e.GetPosition(DesignCanvas).X - LastPoint.X) < CMaxX)
                        CLowX = CLowX + (e.GetPosition(DesignCanvas).X - LastPoint.X);


                    foreach (FrameworkElement selcol in SelectedAreaElements)
                    {

                        double posx = ((double)selcol.GetValue(Canvas.LeftProperty) + (e.GetPosition(DesignCanvas).X - LastPoint.X));
                        if (selcol.Width - changex > 3)
                        {
                            selcol.Width -= changex;
                            Canvas.SetLeft(selcol, posx);
                        }
                        if (selcol.Height + changey > 3)
                        {
                            selcol.Height += changey;
                        }
                    }

                    LastPoint = e.GetPosition(DesignCanvas);
                    DrawMultipleControlsArea();
                }
                else
                {
                    FrameworkElement selcol = ((FrameworkElement)SelectedControl);

                    double posx = ((double)selcol.GetValue(Canvas.LeftProperty) + (e.GetPosition(DesignCanvas).X - LastPoint.X));

                    if (selcol.Width - changex > 3)
                    {
                        selcol.Width -= changex;
                        Canvas.SetLeft(selcol, posx);
                    }
                    if (selcol.Height + changey > 3)
                    {
                        selcol.Height += changey;
                    }
                    LastPoint = e.GetPosition(DesignCanvas);

                    UpdateSelectedEffect();
                }
            }
        }


        private void ScaleTL_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging && DragC != null)
            {

                double changex = e.GetPosition(DesignCanvas).X - LastPoint.X;
                double changey = e.GetPosition(DesignCanvas).Y - LastPoint.Y;

                if (MSelectedItemEffect != null)
                {


                    if(CLowX + (e.GetPosition(DesignCanvas).X - LastPoint.X)<CMaxX)
                        CLowX = CLowX + (e.GetPosition(DesignCanvas).X - LastPoint.X);

                    if (CLowY + (e.GetPosition(DesignCanvas).Y - LastPoint.Y) < CMaxY)
                        CLowY = CLowY + (e.GetPosition(DesignCanvas).Y - LastPoint.Y);


                    foreach (FrameworkElement selcol in SelectedAreaElements)
                    {

                        double posx = ((double)selcol.GetValue(Canvas.LeftProperty) + (e.GetPosition(DesignCanvas).X - LastPoint.X));
                        double posy = ((double)selcol.GetValue(Canvas.TopProperty) + (e.GetPosition(DesignCanvas).Y - LastPoint.Y));


                        if (selcol.Width - changex > 3)
                        {
                            selcol.Width -= changex;
                            Canvas.SetLeft(selcol, posx);
                        }
                        if (selcol.Height - changey > 3)
                        {
                            selcol.Height -= changey;
                            Canvas.SetTop(selcol, posy);
                        }
                    }

                    LastPoint = e.GetPosition(DesignCanvas);
                    DrawMultipleControlsArea();
                }
                else
                {
                    FrameworkElement selcol = ((FrameworkElement)SelectedControl);

                    double posx = ((double)selcol.GetValue(Canvas.LeftProperty) + (e.GetPosition(DesignCanvas).X - LastPoint.X));
                    double posy = ((double)selcol.GetValue(Canvas.TopProperty) + (e.GetPosition(DesignCanvas).Y - LastPoint.Y));


                    if (selcol.Width - changex > 3)
                    {
                        selcol.Width -= changex;
                        Canvas.SetLeft(selcol, posx);
                    }
                    if (selcol.Height - changey > 3)
                    {
                        selcol.Height -= changey;
                        Canvas.SetTop(selcol, posy);
                    }
                    LastPoint = e.GetPosition(DesignCanvas);

                    UpdateSelectedEffect();
                }
            }
        }



        private void ScaleTR_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging && DragC != null)
            {
                double changex = e.GetPosition(DesignCanvas).X - LastPoint.X;
                double changey = e.GetPosition(DesignCanvas).Y - LastPoint.Y;

                if (MSelectedItemEffect != null)
                {
                    if (CMaxX + changex > CLowX)
                        CMaxX += changex;

                    if (CLowY + (e.GetPosition(DesignCanvas).Y - LastPoint.Y) < CMaxY)
                        CLowY = CLowY + (e.GetPosition(DesignCanvas).Y - LastPoint.Y);

                    foreach (FrameworkElement selcol in SelectedAreaElements)
                    {

                        double posy = ((double)selcol.GetValue(Canvas.TopProperty) + (e.GetPosition(DesignCanvas).Y - LastPoint.Y));


                        if (selcol.Width + changex > 3)
                        {
                            selcol.Width += changex;
                        }
                        if (selcol.Height - changey > 3)
                        {
                            selcol.Height -= changey;
                            Canvas.SetTop(selcol, posy);
                        }
                    }

                    LastPoint = e.GetPosition(DesignCanvas);
                    DrawMultipleControlsArea();
                }
                else
                {
                    FrameworkElement selcol = ((FrameworkElement)SelectedControl);

                    double posy = ((double)selcol.GetValue(Canvas.TopProperty) + (e.GetPosition(DesignCanvas).Y - LastPoint.Y));


                    if (selcol.Width + changex > 3)
                    {
                        selcol.Width += changex;
                    }
                    if (selcol.Height - changey > 3)
                    {
                        selcol.Height -= changey;
                        Canvas.SetTop(selcol, posy);
                    }
                    LastPoint = e.GetPosition(DesignCanvas);

                    UpdateSelectedEffect();
                }
            }
        }




        private void ScaleT_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging && DragC != null)
            {
                double changey = e.GetPosition(DesignCanvas).Y - LastPoint.Y;

                if (MSelectedItemEffect != null)
                {

                    if (CLowY + (e.GetPosition(DesignCanvas).Y - LastPoint.Y) < CMaxY)
                        CLowY = CLowY + (e.GetPosition(DesignCanvas).Y - LastPoint.Y);

                    foreach (FrameworkElement selcol in SelectedAreaElements)
                    {

                        double posy = ((double)selcol.GetValue(Canvas.TopProperty) + (e.GetPosition(DesignCanvas).Y - LastPoint.Y));

                        if (selcol.Height - changey > 3)
                        {
                            selcol.Height -= changey;
                            Canvas.SetTop(selcol, posy);
                        }
                    }

                    LastPoint = e.GetPosition(DesignCanvas);
                    DrawMultipleControlsArea();
                }
                else
                {
                    FrameworkElement selcol = ((FrameworkElement)SelectedControl);

                    double posy = ((double)selcol.GetValue(Canvas.TopProperty) + (e.GetPosition(DesignCanvas).Y - LastPoint.Y));

                    if (selcol.Height - changey > 3)
                    {
                        selcol.Height -= changey;
                        Canvas.SetTop(selcol, posy);
                    }
                    LastPoint = e.GetPosition(DesignCanvas);

                    UpdateSelectedEffect();
                }
            }
        }



        private void ScaleL_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging && DragC != null)
            {
                double changex = e.GetPosition(DesignCanvas).X - LastPoint.X;

                if (MSelectedItemEffect != null)
                {
                    if (CLowX + (e.GetPosition(DesignCanvas).X - LastPoint.X) < CMaxX)
                        CLowX = CLowX + (e.GetPosition(DesignCanvas).X - LastPoint.X);

                    foreach (FrameworkElement selcol in SelectedAreaElements)
                    {

                        double posx = ((double)selcol.GetValue(Canvas.LeftProperty) + (e.GetPosition(DesignCanvas).X - LastPoint.X));

                        if (selcol.Width - changex > 3)
                        {
                            selcol.Width -= changex;
                            Canvas.SetLeft(selcol, posx);
                        }
                    }

                    LastPoint = e.GetPosition(DesignCanvas);
                    DrawMultipleControlsArea();
                }
                else
                {
                    FrameworkElement selcol = ((FrameworkElement)SelectedControl);

                    double posx = ((double)selcol.GetValue(Canvas.LeftProperty) + (e.GetPosition(DesignCanvas).X - LastPoint.X));

                    if (selcol.Width - changex > 3)
                    {
                        selcol.Width -= changex;
                        Canvas.SetLeft(selcol, posx);
                    }
                    LastPoint = e.GetPosition(DesignCanvas);

                    UpdateSelectedEffect();
                }
            }
        }



        private void ScaleB_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging && DragC != null)
            {
                double changey = e.GetPosition(DesignCanvas).Y - LastPoint.Y;

                if (MSelectedItemEffect != null)
                {

                    if (CMaxY + changey > CLowY)
                        CMaxY += changey;

                    foreach (FrameworkElement selcol in SelectedAreaElements)
                    {

                        if (selcol.Height + changey > 3)
                        {
                            selcol.Height += changey;
                        }
                    }

                    LastPoint = e.GetPosition(DesignCanvas);
                    DrawMultipleControlsArea();
                }
                else
                {
                    FrameworkElement selcol = ((FrameworkElement)SelectedControl);

                    if (selcol.Height + changey > 3)
                    {
                        selcol.Height += changey;
                    }
                    LastPoint = e.GetPosition(DesignCanvas);

                    UpdateSelectedEffect();
                }
            }
        }



        private void ScaleR_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging && DragC != null)
            {
                double changex = e.GetPosition(DesignCanvas).X - LastPoint.X;

                if (MSelectedItemEffect != null)
                {
                    if (CMaxX + changex > CLowX)
                        CMaxX += changex;

                    foreach (FrameworkElement selcol in SelectedAreaElements)
                    {

                        if (selcol.Width + changex > 3)
                        {
                            selcol.Width += changex;
                        }
                    }

                    LastPoint = e.GetPosition(DesignCanvas);
                    DrawMultipleControlsArea();
                }
                else
                {
                    FrameworkElement selcol = ((FrameworkElement)SelectedControl);

                    if (selcol.Width + changex > 3)
                    {
                        selcol.Width += changex;
                    }
                    LastPoint = e.GetPosition(DesignCanvas);

                    UpdateSelectedEffect();
                }
            }
        }


        private void Move_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging && DragC != null)
            {
                Point canp = e.GetPosition(DesignCanvas);
                FrameworkElement selcol = ((FrameworkElement)SelectedControl);
                double changex = ((double)selcol.GetValue(Canvas.LeftProperty) + (canp.X - LastPoint.X));
                double changey = ((double)selcol.GetValue(Canvas.TopProperty) + (canp.Y - LastPoint.Y));
                if (changex < LimitScreenMaxX && changex >= LimitScreenMinX)
                    Canvas.SetLeft(selcol, changex);
                if (changey < LimitScreenMaxY && changey >= LimitScreenMinY)
                    Canvas.SetTop(selcol, changey);
                LastPoint = canp;

                UpdateSelectedEffect();
            }
        }

        private void Move_MouseLeave(object sender, MouseEventArgs e)
        {
            ((FrameworkElement)sender).Opacity = 1;
            Dragging = false;

            MovedByMoveButton = false;
        }

        private void Move_MouseEnter(object sender, MouseEventArgs e)
        {
            ((FrameworkElement)sender).Opacity = 0.7f;
        }

        private void Move_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StartX = Canvas.GetLeft(((FrameworkElement)SelectedControl));
            StartY = Canvas.GetTop(((FrameworkElement)SelectedControl));
            MovedByMoveButton = true;
        }


        private void Move_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!double.IsNaN(StartX) && StartX != Canvas.GetLeft(((UIElement)SelectedControl)))
            {
                //Debug.WriteLine("Added: "+DesignCanvas.Children[SelectedControlID]);
                HistoryUndo.AddRaise(new His_PositionChanged(SelectedControlID, Canvas.GetLeft((UIElement)SelectedControl), Canvas.GetTop((UIElement)SelectedControl), StartX, StartY));
            }

            StartX = double.NaN;
            StartY = double.NaN;
            MovedByMoveButton = false;

            if (BMoveOnGrid)
            {
                FrameworkElement element = ((FrameworkElement)SelectedControl);
                Canvas.SetLeft(element, ((int)Canvas.GetLeft(element) / GridSize) * GridSize);
                Canvas.SetTop(element, ((int)Canvas.GetTop(element) / GridSize) * GridSize);
                UpdateSelectedEffect();
            }
        }



        public void UpdateSelectedEffect()
        {
            if (SelectedControl != null)
            {
                FrameworkElement elm = (FrameworkElement)SelectedControl;

                if (SelectedItemEffect != null)
                {
                    SelectedItemEffect.Width = elm.Width + 10 * SizeAdjust;
                    SelectedItemEffect.Height = elm.Height + 10 * SizeAdjust;
                    Canvas.SetLeft(SelectedItemEffect, Canvas.GetLeft(elm) - 5 * SizeAdjust);
                    Canvas.SetTop(SelectedItemEffect, Canvas.GetTop(elm) - 5 * SizeAdjust);
                }

                if (MoveButton != null)
                {
                    Canvas.SetLeft(MoveButton, Canvas.GetLeft(elm) + elm.Width / 2 - (12 * SizeAdjust));
                    Canvas.SetTop(MoveButton, Canvas.GetTop(elm) - (45*SizeAdjust));

                    
                    rotCanvas.Width = elm.Width + 10 * SizeAdjust;
                    rotCanvas.Height = elm.Height + 10 * SizeAdjust;
                    Canvas.SetLeft(rotCanvas, Canvas.GetLeft(elm) - 10 * SizeAdjust);
                    Canvas.SetTop(rotCanvas, Canvas.GetTop(elm) - 10 * SizeAdjust);

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
                    Canvas.SetLeft(ScaleButton[4], rotCanvas.Width/2);
                    Canvas.SetTop(ScaleButton[4], -((10 * SizeAdjust) / 2));

                    //Left
                    Canvas.SetLeft(ScaleButton[5], -((10 * SizeAdjust) / 2));
                    Canvas.SetTop(ScaleButton[5], rotCanvas.Height/2);

                    //Bottom
                    Canvas.SetLeft(ScaleButton[6], rotCanvas.Width/2);
                    Canvas.SetTop(ScaleButton[6], rotCanvas.Height + ((10 * SizeAdjust) / 2));

                    //Right
                    Canvas.SetLeft(ScaleButton[7],rotCanvas.Width + ((10 * SizeAdjust) / 2));
                    Canvas.SetTop(ScaleButton[7], rotCanvas.Height/2);
                }

            }
        }


        public void RemoveSelectedItemEffect()
        {
            if (SelectedItemEffect != null)
            {
                DesignCanvas.Children.Remove(SelectedItemEffect);
            }

            if(MSelectedItemEffect != null)
            {
                DesignCanvas.Children.Remove(MSelectedItemEffect);
                MSelectedItemEffect = null;
            }


            if (MoveButton != null)
            {
                DesignCanvas.Children.Remove(MoveButton);

                foreach (Ellipse ellipse in ScaleButton)
                    DesignCanvas.Children.Remove(ellipse);

                rotCanvas.Children.Clear();
                DesignCanvas.Children.Remove(rotCanvas);
            }

            if (SelectionBox != null)
            {
                DesignCanvas.Children.Remove(SelectionBox);
                SelectionBox = null;
            }
        }



    }
}
