using System.Windows;
using System.Windows.Media.Effects;
using Good_Teacher.Controls;

namespace Good_Teacher.Class.Serialization
{
    public class WebPage_Serialization : ControlSerializer
    {
        public Visibility Buttons, Toolbar;
        public string URL;
        public Brush_Serializer CPbrush = new Brush_Serializer();

        public WebPage_Serialization()
        {

        }


        public WebPage_Serialization(WebPage_Control control, DataStore data)
        {
            position = new PositionSize(control);
            Buttons = control.BackForwardVisibility;
            URL = control.WebUrl;
            Toolbar = control.ToolbarPanel.Visibility;
            ContID = control.Name;
            ControlVisibility = control.Visibility;

            if (control.Effect != null)
                effect = new Effect_Serializer((DropShadowEffect)control.Effect);
            else
                effect = null;

            CPbrush.SerializeWithKey(control.ControlPanelBack, data, control.PathToCPImage);
            CPbrush.SerializeQuality(control);
        }


        public WebPage_Control CreateControl(DataStore data)
        {
            WebPage_Control control = new WebPage_Control();
            position.SetControlPositionSize(control);
            control.BackForwardVisibility = Buttons;
            control.webBrowser.Navigate(URL);
            control.WebUrl = URL;
            control.ToolbarPanel.Visibility = Toolbar;


            if (effect != null)
                control.Effect = effect.CreateShadow();

            string ikey = "";

            control.ControlPanelBack = CPbrush.DeserializeToBrushWithKey(data, out ikey);
            control.PathToCPImage = ikey;
            control.CPStretch = CPbrush.GetStretch();

            control.Name = ContID;

            control.Visibility = ControlVisibility;

            CPbrush.DeserializeQuality(control);

            return control;
        }


    }
}
