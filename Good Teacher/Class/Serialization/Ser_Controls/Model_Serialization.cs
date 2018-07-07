using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Good_Teacher.Class.Enumerators;
using Good_Teacher.Class.Save;

namespace Good_Teacher.Class.Serialization.Ser_Controls
{
    public class Model_Serialization : ControlSerializer
    {
        public Brush_Serializer brush = new Brush_Serializer();
        public ModelPath modelp = new ModelPath();
        public Camera_Serializer camera = new Camera_Serializer();
        public ModelLightType.LightType lightType = ModelLightType.LightType.DefaultLight;
        public bool CameraControl = true;

        public Model_Serialization()
        {

        }

        public Model_Serialization(HelixViewport3D control, DataStore data)
        {
            Serialize(control,data);
        }


        public void Serialize(HelixViewport3D control, DataStore data)
        {
            SerializeDefault(control);

            List<object> list = new List<object>(2);

            if (control.Tag!= null)
            list = (List<object>)control.Tag;

            if (list.Count>1 && list[1] != null)
                control.Tag = list[1];

            brush.Serialize(control, control.Background, data);

            if (list.Count > 0 && list[0] != null)
                control.Tag = list[0];

            if (control.Tag!=null)
            {
                if(control.Tag is ModelPath)
                modelp = (ModelPath)control.Tag;
            }
            camera.Serialize(control.Camera);

            if (control.CameraController != null)
                CameraControl = control.CameraController.IsEnabled;
            else
                CameraControl = true;

            foreach (object obj in control.Children)
            {
                if (obj is LightSetup)
                {
                    if (obj is SunLight)
                        lightType = ModelLightType.LightType.SunLight;
                    else if (obj is SpotHeadLight)
                        lightType = ModelLightType.LightType.SpotHeadlight;
                    else
                        lightType = ModelLightType.LightType.DefaultLight;

                    break;
                }
            }

        }



        public void Deserialize(HelixViewport3D control, DataStore data)
        {
            List<object> list = new List<object>(2);
            list.Add(null);
            list.Add(null);

            DeserializeDefault(control);
            brush.Deserialize(control, data);

            list[1] = control.Tag;

            Debug.WriteLine("ModelP:  Path: "+modelp.LocalPathToModel);

            if(modelp!=null)
            {

                if(!String.IsNullOrWhiteSpace(modelp.LocalPathToModel))
                {
                    string lpath = System.IO.Path.Combine(LocalPath.GetResourcesPath(), modelp.LocalPathToModel);
                    if(File.Exists(lpath))
                    {
                        LoadModel(control,lpath);
                        list[0] = control.Tag;
                    }
                    else
                    {
                        DefaultModel(control);
                    }
                }
                else
                {
                    DefaultModel(control);
                }
            }
            else
            {
                DefaultModel(control);
            }

            if(camera!=null)
            {
                camera.Deserialize(control.Camera);
            }

            if (lightType == ModelLightType.LightType.SpotHeadlight)
            {
                SpotHeadLight spotHeadLight = new SpotHeadLight();
                control.Children.Add(spotHeadLight);
            }
            else if (lightType == ModelLightType.LightType.SunLight)
            {
                SunLight sunLight = new SunLight();
                control.Children.Add(sunLight);
            }
            else
            {
                DefaultLights defaultLights = new DefaultLights();
                control.Children.Add(defaultLights);
            }

            control.Tag = list;


            control.Loaded += (s, e) => {

                if(control.CameraController!=null)
                control.CameraController.IsEnabled = CameraControl;

                control.ShowViewCube = CameraControl;

            };
        }



        private void DefaultModel(HelixViewport3D cont)
        {
            Teapot teaPot = new Teapot();

            cont.Children.Add(teaPot);
        }


        private void LoadModel(HelixViewport3D cont,string path)
        {
            try
            {
                Model3DGroup mgroup = new Model3DGroup();

                string ext = System.IO.Path.GetExtension(path);
                if (ext == ".obj")
                {
                    ObjReader reader = new HelixToolkit.Wpf.ObjReader();
                    try
                    {

                        reader.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(modelp.DefaultColor));


                        if (modelp.LoadTexture)
                        {
                            mgroup = reader.Read(path);
                        }
                        else
                        {
                            reader.TexturePath = ".";
                            mgroup = reader.Read(RichTextBoxWorker.StreamFromString(File.ReadAllText(path)));
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Error Load obj First Try: Loading texture error?: " + modelp.LoadTexture);
                        if (modelp.LoadTexture)
                        {
                            reader = new HelixToolkit.Wpf.ObjReader();
                            reader.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(modelp.DefaultColor));
                            reader.TexturePath = ".";
                            mgroup = reader.Read(RichTextBoxWorker.StreamFromString(File.ReadAllText(path)));
                        }
                        else
                        {
                            Debug.WriteLine("Error Load 3D model: " + ex);
                        }

                    }

                }
                else
                {
                    ModelImporter modelImporter = new ModelImporter();
                    modelImporter.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(modelp.DefaultColor));
                    mgroup = modelImporter.Load(path);
                }

                ModelVisual3D model = new ModelVisual3D();
                model.Content = mgroup;

                cont.Children.Clear();
                cont.Children.Add(model);

                cont.Tag = modelp;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(""+ex);
            }
        }


        public HelixViewport3D CreateControl(DataStore data)
        {
            HelixViewport3D control = new HelixViewport3D();

            Deserialize(control, data);

            return control;
        }


    }
}
