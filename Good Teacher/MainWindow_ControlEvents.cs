using Good_Teacher.Class.History;
using Good_Teacher.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Good_Teacher
{
    public partial class MainWindow : Window
    {
        object SelectedControl;
        int SelectedControlID = -1;
        //Brush selbackground;
        UIElement DragC;
        Point LastPoint;
        bool Dragging = false;
        bool IsSelectedControl = false;

        private double StartX = double.NaN, StartY = double.NaN;

        private bool SpecialRemoved = false;


        private void Control_GotFocus(object sender, RoutedEventArgs e)
        {
            EditControl(sender);
        }

        private void Control_MouseLeave(object sender, MouseEventArgs e)
        {
            Dragging = false;
            ((UIElement)sender).ReleaseMouseCapture();
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging && DragC != null)
            {
                double changex = ((double)DragC.GetValue(Canvas.LeftProperty) + (e.GetPosition(DesignCanvas).X - LastPoint.X));
                double changey = ((double)DragC.GetValue(Canvas.TopProperty) + (e.GetPosition(DesignCanvas).Y - LastPoint.Y));
                if (changex < LimitScreenMaxX && changex >= LimitScreenMinX)
                    Canvas.SetLeft(DragC, changex);
                if (changey < LimitScreenMaxY && changey >= LimitScreenMinY)
                    Canvas.SetTop(DragC, changey);
                LastPoint = e.GetPosition(DesignCanvas);

                UpdateSelectedEffect();
            }
        }



        private void Control_Unloaded(object sender, RoutedEventArgs e)
        {
            if (!SpecialRemoved && SelectedControl != null && !(sender is Gallery))
            {
                try
                {
                    //Debug.WriteLine("Add Removed ID: " + SelectedControlID + "    Type: " + SelectedControl);
                    HistoryUndo.AddRaise(new His_RemovedControl(CreateCopyObject(SelectedControl, 0), SelectedControlID));
                }catch(Exception ex)
                {
                    Debug.WriteLine("Cant create copy: "+ex);
                }
            }

            SpecialRemoved = false;
        }


        private void Control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Dragging = false;
            ((UIElement)sender).ReleaseMouseCapture();

            if (!double.IsNaN(StartX) && StartX != Canvas.GetLeft(((UIElement)sender)) && !MovedByMoveButton)
            {
                //Debug.WriteLine("Added to History Undo: "+HistoryUndo.Count);
                HistoryUndo.AddRaise(new His_PositionChanged(DesignCanvas.Children.IndexOf(((UIElement)sender)), Canvas.GetLeft(((UIElement)sender)), Canvas.GetTop(((UIElement)sender)), StartX, StartY));
            }

            if (!MovedByMoveButton)
            {
                StartX = double.NaN;
                StartY = double.NaN;
            }

            if(BMoveOnGrid)
            {
                FrameworkElement element = ((FrameworkElement)sender);
                Canvas.SetLeft(element, ((int)Canvas.GetLeft(element)/ GridSize)*GridSize);
                Canvas.SetTop(element, ((int)Canvas.GetTop(element) / GridSize)*GridSize);

                if(IsSelectedControl)
                DrawSelectedItem((FrameworkElement)SelectedControl);
            }

            IsSelectedControl = false;
        }

        private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LastPoint = e.GetPosition(DesignCanvas);
            DragC = (UIElement)sender;

            StartX = Canvas.GetLeft(DragC);
            StartY = Canvas.GetTop(DragC);

            Dragging = true;
            ((UIElement)sender).CaptureMouse();
            IsSelectedControl = true;
        }


        private void DesignControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            EditControl(sender);
        }


        private void Page_AddControlEvent(int Tag)
        {
            Debug.WriteLine("Tag: " + Tag);
            ControlSelect = true;
            ControlTag = Tag;
        }


        private void Control_ChangedBackground()
        {
            //selbackground = (Brush)((Control)SelectedControl).Background;
        }


    }
}
