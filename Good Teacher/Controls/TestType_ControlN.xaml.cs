using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for TestType_ControlN.xaml
    /// </summary>
    public partial class TestType_ControlN : UserControl
    {

        public enum MenuEventType{
           Up,Down,Delete,Duplicate,Customize,MoveTo,Lock,Hide
        }

        public delegate void ClickHandler(int Pos, Object sender);
        public event ClickHandler OnClick;

        public delegate void MenuEventHandler(MenuEventType type, Object sender);
        public event MenuEventHandler OnMenuClick;

        public int Position = 0;
        public TestTypeID.Test_Type test_type;

        private bool selected = false;

        public SolidColorBrush DefaultBrush = new SolidColorBrush(Color.FromArgb(200, 255, 255, 255));
        public SolidColorBrush HoverBrush = new SolidColorBrush(Color.FromArgb(200, 244, 143, 177));
        public SolidColorBrush SelectBrush = new SolidColorBrush(Color.FromArgb(200, 236, 64, 122));
        public SolidColorBrush CBorderBrush = new SolidColorBrush(Color.FromRgb(216, 27, 96));

        public TestType_ControlN()
        {
            InitializeComponent();
            InitializeHandlers();
        }

        public TestType_ControlN(string Desc, int position, bool Locked, bool Hidden, TestTypeID.Test_Type Type = TestTypeID.Test_Type.Page)
        {
            InitializeComponent();

            Position = position;
            test_type = Type;
            Description.Content = Desc;
            LabelPage.Content = "" + (position+1);

            if(Locked)
                LockedI.Visibility = Visibility.Visible;
            else
                LockedI.Visibility = Visibility.Collapsed;

            UpdateHidden(Hidden);

            ToolTip = Desc + " ("+(position + 1)+")";
            InitializeHandlers();
        }

        void InitializeHandlers()
        {
            MouseEnter += TestType_MouseEnter;
            MouseLeave += TestType_MouseLeave;
            MouseLeftButtonDown += TestType_MouseLeftButtonDown;
        }


        public void SetPosition(int NewPosition)
        {
            Position = NewPosition;
            LabelPage.Content = "" + (NewPosition + 1);
            ToolTip = (string)Description.Content + " (" + (NewPosition + 1) + ")";
        }

        public void UpdateData()
        {
            LabelPage.Content = "" + (Position + 1);
            ToolTip = (string)Description.Content + " (" + (Position + 1) + ")";
        }

        public void UpdateDescription(string desc)
        {
            Description.Content = desc;
            LabelPage.Content = "" + (Position + 1);
            ToolTip = desc + " (" + (Position + 1) + ")";
        }

        public void UpdateLocked(bool locked)
        {
            if (locked)
                LockedI.Visibility = Visibility.Visible;
            else
                LockedI.Visibility = Visibility.Collapsed;
        }

        public void UpdateHidden(bool locked)
        {
            if (locked)
                HiddenI.Visibility = Visibility.Visible;
            else
                HiddenI.Visibility = Visibility.Collapsed;
        }


        public void SetCanvasImage(ImageSource image)
        {
            CanvasImage.Source = image;
        }



        private void TestType_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (OnClick != null)
                OnClick(Position, this);
        }


        private void TestType_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!selected)
                InfoGrid.Background = DefaultBrush;
        }

        private void TestType_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!selected)
                InfoGrid.Background = HoverBrush;
        }


        public bool IsSelected()
        {
            return selected;
        }


        public void Select(bool Select)
        {
            selected = Select;

            if (!selected)
            {
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1);
                InfoGrid.Background = DefaultBrush;
            }
            else
            {
                border.BorderThickness = new Thickness(2);
                border.BorderBrush = CBorderBrush; //(Color.FromRgb(231, 76, 60));
                InfoGrid.Background = SelectBrush;
            }

        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(sender == MenuItem_Copy)
            {
                if (OnMenuClick != null)
                    OnMenuClick(MenuEventType.Duplicate, this);
            }
            else if(sender == MenuItem_Delete)
            {
                if (OnMenuClick != null)
                    OnMenuClick(MenuEventType.Delete, this);
            }
            else if (sender == MenuItem_Up)
            {
                if (OnMenuClick != null)
                    OnMenuClick(MenuEventType.Up, this);
            }
            else if (sender == MenuItem_Down)
            {
                if (OnMenuClick != null)
                    OnMenuClick(MenuEventType.Down, this);
            }
            else if (sender == MenuItem_Settings)
            {
                if (OnMenuClick != null)
                    OnMenuClick(MenuEventType.Customize, this);
            }
            else if (sender == MenuItem_MoveTo)
            {
                if (OnMenuClick != null)
                    OnMenuClick(MenuEventType.MoveTo, this);
            }
            else if (sender == MenuItem_Lock)
            {
                if (OnMenuClick != null)
                    OnMenuClick(MenuEventType.Lock, this);
            }
            else if (sender == MenuItem_Hide)
            {
                if (OnMenuClick != null)
                    OnMenuClick(MenuEventType.Hide, this);
            }
        }


    }
}
