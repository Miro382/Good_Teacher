using HelixToolkit.Wpf;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Good_Teacher.Class;
using Good_Teacher.Class.Save;
using Good_Teacher.Class.Serialization;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_ShowModel.xaml
    /// </summary>
    public partial class Window_ShowModel : Window
    {
        HelixViewport3D Mviewport = new HelixViewport3D();
        public bool CloseOnStart = false;


        public Window_ShowModel(string PathToModel)
        {
            InitializeComponent();

            DefaultLights lights = new DefaultLights();
            Teapot teaPot = new Teapot();
            Mviewport.Children.Add(lights);
            Mviewport.Children.Add(teaPot);

            GridModelViewer.Children.Add(Mviewport);

            LoadModel(PathToModel, Colors.LightGray, true, false);
        }

        private void Window_ShowModel_Initialized(object sender, EventArgs e)
        {
            if (CloseOnStart)
                Close();
        }

        void LoadModel(string PathToModel, Color DModelColor, bool LoadTexture, bool NewModel)
        {
            try
            {
                if (NewModel)
                    LocalPath.CopyDirectoryToResources(System.IO.Path.GetDirectoryName(PathToModel));

                Debug.WriteLine("Dir Name: " + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(PathToModel)) + "   FileName: " + System.IO.Path.GetFileName(PathToModel));

                string Lpathtomod = System.IO.Path.GetDirectoryName(PathToModel) + "\\" + System.IO.Path.GetFileName(PathToModel);
                string pathtomod = LocalPath.CombinePath(Lpathtomod);

                Model3DGroup mgroup = new Model3DGroup();

                string ext = System.IO.Path.GetExtension(PathToModel);
                if (ext == ".obj")
                {
                    ObjReader reader = new HelixToolkit.Wpf.ObjReader();
                    try
                    {

                        reader.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(DModelColor));


                        if (LoadTexture)
                        {
                            mgroup = reader.Read(pathtomod);
                        }
                        else
                        {
                            reader.TexturePath = ".";
                            mgroup = reader.Read(RichTextBoxWorker.StreamFromString(File.ReadAllText(pathtomod)));
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Error Load obj First Try: Loading texture error?: " + LoadTexture);
                        if (LoadTexture)
                        {
                            reader = new HelixToolkit.Wpf.ObjReader();
                            reader.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(DModelColor));
                            reader.TexturePath = ".";
                            mgroup = reader.Read(RichTextBoxWorker.StreamFromString(File.ReadAllText(pathtomod)));
                        }
                        else
                        {
                            Debug.WriteLine("Error Load 3D model: " + ex);
                            MessageBox.Show(Strings.ResStrings.NotSaved, Strings.ResStrings.NotSavedTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                            CloseOnStart = true;
                        }

                    }

                }
                else
                {
                    ModelImporter modelImporter = new ModelImporter();
                    modelImporter.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(DModelColor));
                    mgroup = modelImporter.Load(pathtomod);
                }

                ModelVisual3D model = new ModelVisual3D();
                model.Content = mgroup;

                DefaultLights defaultLights = new DefaultLights();

                Mviewport.Children.Clear();
                Mviewport.Children.Add(model);
                Mviewport.Children.Add(defaultLights);

                Mviewport.Tag = new ModelPath(Lpathtomod, LoadTexture, DModelColor);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Load 3D model: " + ex);
                MessageBox.Show(Strings.ResStrings.ErrorLoadModel, Strings.ResStrings.Error, MessageBoxButton.OK, MessageBoxImage.Warning);
                CloseOnStart = true;
            }
        }


    }
}
