using Good_Teacher.Class.History;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Good_Teacher
{

    public partial class MainWindow : Window
    {


        private void DesignCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.RightButton)
            {
                Mouse.OverrideCursor = null;
                ControlSelect = false;
            }

            if (ControlSelect)
            {
                Point p = Mouse.GetPosition(DesignCanvas);

                int CID = 0;

                string textCT = "" + ControlTag;

                int.TryParse("" + textCT[0],out CID);


                switch (CID)
                {
                    case 1:
                        AddLevel1Control(p); //main
                    break;
                    case 2:
                        AddLevel2Control(p); //shapes
                    break;
                    case 3:
                        AddLevel3Control(p); //charts
                        break;
                    case 4:
                        AddLevel4Control(p); //controls (buttons, textbox...)
                        break;
                    case 5:
                        AddLevel5Control(p); //special
                        break;
                }

                if(DesignCanvas.Children.Count>0)
                HistoryUndo.AddRaise(new His_AddControl(DesignCanvas.Children[DesignCanvas.Children.Count - 1], DesignCanvas.Children.Count - 1));

            }
            else if (e.OriginalSource is Canvas)
            {
                /*
                if (SelectedControl != null && SelectedControl is Control)
                {
                    if (ChangeColor(SelectedControl))
                        ((Control)SelectedControl).Background = selbackground;
                }
                */

                //Keyboard.ClearFocus();

                UnselectControl();

            }
        }


        void UnselectControl()
        {
            ValueEditor.Content = "";

            if (OldPage != null)
            {
                FrameEditor.Content = OldPage;
                OldPage = null;
                FrameEditor.NavigationService.RemoveBackEntry();
            }

            SelectedControl = null;

            RemoveSelectedItemEffect();
        }



    }

}
