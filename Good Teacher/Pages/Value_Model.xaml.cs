using HelixToolkit.Wpf;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Good_Teacher.Class;
using Good_Teacher.Class.Save;
using Good_Teacher.Windows.Dialogs;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_Model.xaml
    /// </summary>
    public partial class Value_Model : System.Windows.Controls.Page
    {
        HelixViewport3D cont;

        DataStore data;

        public Value_Model(DataStore datas,HelixViewport3D webbrowser)
        {
            InitializeComponent();
            cont = webbrowser;

            data = datas;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();

            brushselector.SetData(cont,data,false);
            brushselector.LoadData(cont.Background);
            brushselector.ChangedBrush -= Brushselector_ChangedBrush;
            brushselector.ChangedBrush += Brushselector_ChangedBrush;


            foreach (object obj in cont.Children)
            {
                if (obj is LightSetup)
                {
                    if (obj is SunLight)
                        RB_SunLight.IsChecked = true;
                    else if (obj is SpotHeadLight)
                        RB_SpotLight.IsChecked = true;
                    else
                        RB_DefaultLight.IsChecked = true;

                    break;
                }
            }


            CB_FixedModel.IsChecked = !cont.CameraController.IsEnabled;
        }

        private void Brushselector_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            cont.Background = Sbrush;

            if (Sbrush is ImageBrush)
            {
                if (cont.Tag == null)
                {
                    List<object> list = new List<object>(2);
                    list.Add(null);
                    list.Add(null);
                    cont.Tag = list;
                    ((List<object>)cont.Tag)[1] = new DesignSave(new ImageRepresentation(brushselector.LastSelectedImageKey, ((ImageBrush)cont.Background).Stretch)).Serialize();
                }
                else
                {
                    ((List<object>)cont.Tag)[1] = new DesignSave(new ImageRepresentation(brushselector.LastSelectedImageKey, ((ImageBrush)cont.Background).Stretch)).Serialize();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((Canvas)cont.Parent).Children.Remove(cont);
                NavigationService.Content = "";
                ((MainWindow)Window.GetWindow(this)).RemoveSelectedItemEffect();
            }
            catch
            {

            }
        }


        private void SetModel_Click(object sender, RoutedEventArgs e)
        {
            DWindow_ModelSelector modelSelector = new DWindow_ModelSelector(cont);
            modelSelector.Owner = Window.GetWindow(this);
            modelSelector.ShowDialog();
        }




        private void RadioButtonLight_Checked(object sender, RoutedEventArgs e)
        {
            if(cont != null)
            {
                List<LightSetup> lights = new List<LightSetup>();

                foreach (object obj in cont.Children)
                {
                    if (obj is LightSetup)
                        lights.Add((LightSetup)obj);
                }

                foreach(LightSetup lsetup in lights)
                {
                    cont.Children.Remove(lsetup);
                }


                if (RB_DefaultLight.IsChecked==true)
                {
                    Debug.WriteLine("DEFAULTLIGHT");
                    DefaultLights defaultLights = new DefaultLights();
                    cont.Children.Add(defaultLights);
                }
                else if (RB_SunLight.IsChecked==true)
                {
                    Debug.WriteLine("SUNLIGHT");
                    SunLight sunLight = new SunLight();
                    cont.Children.Add(sunLight);
                }
                else if(RB_SpotLight.IsChecked == true)
                {
                    Debug.WriteLine("SPOTLIGHT");
                    SpotHeadLight spotHeadLight = new SpotHeadLight();
                    cont.Children.Add(spotHeadLight);
                }
            }

        }


        private void CB_FixedModel_Checked(object sender, RoutedEventArgs e)
        {
            if(cont!=null)
            {
                cont.CameraController.IsEnabled = !(CB_FixedModel.IsChecked == true);
                cont.ShowViewCube = !(CB_FixedModel.IsChecked == true);
            }
        }

    }
}
