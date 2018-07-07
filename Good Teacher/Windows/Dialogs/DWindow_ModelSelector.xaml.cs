using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Good_Teacher.Class;
using Good_Teacher.Class.Save;
using Good_Teacher.Class.Serialization;

namespace Good_Teacher.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for DWindow_ModelSelector.xaml
    /// </summary>
    public partial class DWindow_ModelSelector : Window
    {
        HelixViewport3D cont;

        public DWindow_ModelSelector(HelixViewport3D viewport3D)
        {
            InitializeComponent();
            cont = viewport3D;

            string path = "";
            if (LocalPath.GetDirectoryPath(out path))
            {
                    path = System.IO.Path.Combine(path, "Resources");

                if (Directory.Exists(path))
                {
                    Debug.WriteLine("Path: " + path);

                    IEnumerable<string> mfiles = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                        .Where(s => s.EndsWith(".obj", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".3ds", StringComparison.OrdinalIgnoreCase)
                        || s.EndsWith(".lwo", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".stl", StringComparison.OrdinalIgnoreCase)
                        || s.EndsWith(".off", StringComparison.OrdinalIgnoreCase));


                    foreach (string file in mfiles)
                    {
                        Button button = new Button();
                        button.Background = new SolidColorBrush(Color.FromRgb(52, 152, 219));
                        button.Tag = file;
                        button.Content = System.IO.Path.GetFileNameWithoutExtension(file);
                        button.FontSize = 15;
                        button.Foreground = new SolidColorBrush(Colors.White);

                        button.Click += ButtonModelExist_Click;

                        button.Padding = new Thickness(10, 3, 10, 3);
                        button.MinHeight = 40;
                        button.MinWidth = 70;
                        ModelList.Items.Add(button);
                    }
                }
            }
        }


        private void ButtonModelExist_Click(object sender, RoutedEventArgs e)
        {
            DWindow_ExistingModel ExModel = new DWindow_ExistingModel(System.IO.Path.GetFileName( ((Button)sender).Tag.ToString() ));
            ExModel.Owner = this;
            ExModel.ShowDialog();

            if (ExModel.IsOK)
            {
                if (LoadModel(((Button)sender).Tag.ToString(), ExModel.color, ExModel.Texture, false))
                    Close();
            }
        }


        private void ImportModel_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            if (LocalPath.GetDirectoryPath(out path))
            {

                DWindow_NewModel newModel = new DWindow_NewModel();
                newModel.Owner = this;
                newModel.ShowDialog();


                if (newModel.IsOK)
                {
                    if (LoadModel(newModel.Model, newModel.color, newModel.Texture, true))
                        Close();
                }

            }
            else
            {
                MessageBox.Show(Strings.ResStrings.NotSaved, Strings.ResStrings.NotSavedTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        bool LoadModel(string PathToModel,Color DModelColor, bool LoadTexture, bool NewModel)
        {
            try
            {
                    if(NewModel)
                    LocalPath.CopyDirectoryToResources(System.IO.Path.GetDirectoryName(PathToModel));

                    Debug.WriteLine("Dir Name: " + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(PathToModel)) + "   FileName: " + System.IO.Path.GetFileName(PathToModel));

                string Lpathtomod = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(PathToModel)) + "\\" + System.IO.Path.GetFileName(PathToModel);
                    string pathtomod = System.IO.Path.Combine(LocalPath.GetResourcesPath(),Lpathtomod);

                Debug.WriteLine("Lpath: "+Lpathtomod+" path: "+pathtomod);

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

                    cont.Children.Clear();
                    cont.Children.Add(model);
                    cont.Children.Add(defaultLights);

                    Debug.WriteLine("Adding model tag: "+ Lpathtomod);

                    if (cont.Tag == null)
                    {
                        List<object> list = new List<object>(2);
                        list.Add(null);
                        list.Add(null);
                        cont.Tag = list;
                        ((List<object>)cont.Tag)[0] = new ModelPath(Lpathtomod, LoadTexture, DModelColor);
                    }
                    else
                    {
                        ((List<object>)cont.Tag)[0] = new ModelPath(Lpathtomod, LoadTexture, DModelColor);
                    }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Load 3D model: " + ex);
                MessageBox.Show(Strings.ResStrings.ErrorLoadModel, Strings.ResStrings.Error, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }


    }
}
