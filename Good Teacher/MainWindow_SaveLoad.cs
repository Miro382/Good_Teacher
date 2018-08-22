using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Good_Teacher.Class;
using Good_Teacher.Class.Serialization;
using Good_Teacher.Class.TestClass;
using Good_Teacher.Controls;
using Good_Teacher.Class.Workers;

namespace Good_Teacher
{
    public partial class MainWindow : Window
    {

        int IMG_StackCount;
        int IMG_Current;
        bool IMG_Work = false;
        DispatcherTimer IMG_dispatcherTimer;

        Canvas IMG_can;

        void LoadFileData()
        {
            DesignCanvas.Children.Clear();
            Stack_TestList.Children.Clear();

            int k = 0;
            foreach (Test_DefaultAbstract tst in data.pages)
            {
                // TestType_Control type = new TestType_Control(tst.TestType, tst.Position, Strings.ResStrings.Form, Strings.ResStrings.Form, "Resources/Menu/form.png", Stretch.Uniform);
                TestType_ControlN type = new TestType_ControlN(Strings.ResStrings.Page, Stack_TestList.Children.Count, !data.pages[k].isUnlocked, data.pages[k].isHidden);
                type.OnClick += TestTypeCall_OnClick;
                type.OnMenuClick += TestTypeCall_OnMenuClick;
                Stack_TestList.Children.Add(type);
                k++;
            }

            SelectedPosition = -1;

            if(Stack_TestList.Children[0]!=null)
            TestTypeCall_OnClick(0, Stack_TestList.Children[0]);
        }


        void LoadCanvas()
        {
            if (SelectedPosition >= 0)
            {
                BorderWindowSize.Visibility = Visibility.Visible;
                MainWindow.OPTIMIZEDMODE = false;
                RemoveUnloadEvent();
                DesignCanvas.Children.Clear();

                LockButton.SetCheckedNoCall(false);
                HiddenPageButton.SetCheckedNoCall(false);
                DesignCanvas.IsEnabled = true;

                ActualPage = SelectedPosition;

                if (!String.IsNullOrWhiteSpace(data.pages[SelectedPosition].canvas))
                {
                    CanvasWriter.LoadCanvas(DesignCanvas, data.pages[SelectedPosition].canvas);
                }
                SetLockOnCanvas(!data.pages[SelectedPosition].isUnlocked);
                HiddenPageButton.SetCheckedNoCall(data.pages[SelectedPosition].isHidden);
                CanvasSaveLoad.ExtractHiddenData(data, DesignCanvas, SelectedPosition);

                AddEventsToNewCanvas();
            }
        }


            
        void SaveCanvas()
        {

            /*
            if (SelectedControl != null && SelectedControl is Control)
            {
                
                if (ChangeColor(SelectedControl))
                    ((Control)SelectedControl).Background = selbackground;
                    
            }
            */
            RemoveSelectedItemEffect();

            SelectedControl = null;

            if (SelectedPosition >= 0)
            {

                if(!InPresentationMode)
                LastSelected.SetCanvasImage((BitmapSource)new ImageSourceConverter().ConvertFrom(CanvasWriter.SaveCanvasToImg(DesignCanvas, CanvasImgX, CanvasImgY )));

                //SaveRichTextbox(DesignCanvas);

                RemoveUnloadEvent();

                CanvasSaveLoad.ToSerializableCanvas(data,DesignCanvas,SelectedPosition);

                string can = CanvasWriter.SerializeToXAML(DesignCanvas);
                data.pages[SelectedPosition].canvas = can;
                data.pages[SelectedPosition].isUnlocked = DesignCanvas.IsEnabled;
                data.SavedVersionCode = MainWindow.VersionCode;
            }
        }


        private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {

            SaveFile(sender);
          //  RefreshRichTextBox();
        }


        public bool SaveFile(object sender)
        {
            if (data.pages.Count > 0)
            {
                if (sender == MenuItem_SaveAs || string.IsNullOrWhiteSpace(pathtofile))
                {

                    SaveEditor save = new SaveEditor();

                    SaveFileDialog savefile = new SaveFileDialog();
                    savefile.FileName = Strings.ResStrings.File + "." + TestMakerFileExtension;
                    savefile.Filter = Strings.ResStrings.GoodTeacherTestFormat + "|*." + TestMakerFileExtension + "|" + Strings.ResStrings.AllFiles + "|*.*";

                    if (savefile.ShowDialog() == true)
                    {
                        SaveCanvas();
                        LoadCanvas();
                        if (save.SaveWithCompression(savefile.FileName, data))
                        {
                            pathtofile = savefile.FileName;
                            SetWorkingFileLabel(pathtofile);
                            ShowNotification(Strings.ResStrings.Saved);
                            MainWindow.IsChanged = false;
                            return true;
                        }
                        else
                        {
                            MessageBox.Show(Strings.ResStrings.ErrorSave, Strings.ResStrings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    SaveEditor save = new SaveEditor();

                    SaveCanvas();
                    LoadCanvas();

                    if (!save.SaveWithCompression(pathtofile, data))
                    {
                        MessageBox.Show(Strings.ResStrings.ErrorSave, Strings.ResStrings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    else
                    {
                        ShowNotification(Strings.ResStrings.Saved);
                        MainWindow.IsChanged = false;
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public void OpenFile(string path)
        {
            try
            {
                SaveEditor save = new SaveEditor();
                data = save.LoadWithCompression(path);

                SetWorkingFileLabel(path);

                LoadFileData();
                pathtofile = path;
                BorderWindowSize.Width = data.CanvasW + 2;
                BorderWindowSize.Height = data.CanvasH + 2;
                CanvasW = data.CanvasW;
                CanvasH = data.CanvasH;

                FontWorker.LoadFonts(data);

                LoadCanvas();

                IMG_StackCount = Stack_TestList.Children.Count;
                IMG_Work = false;
                IMG_Current = 0;

                IMG_dispatcherTimer = new DispatcherTimer();
                IMG_dispatcherTimer.Tick += new EventHandler(IMG_dispatcherTimer_Tick);
                IMG_dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
                IMG_dispatcherTimer.Start();

                //UpdateCanvasIcon();

                ScrollViewer_TestList.ScrollToHome();

                MainWindow.IsChanged = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(Strings.ResStrings.ErrorLoad, Strings.ResStrings.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openfile = new OpenFileDialog();
            openfile.FileName = "File."+TestMakerFileExtension;
            openfile.Filter = Strings.ResStrings.GoodTeacherTestFormat + "|*."+TestMakerFileExtension+"|" + Strings.ResStrings.AllFiles + "|*.*";

            if (openfile.ShowDialog() == true)
            {
                OpenFile(openfile.FileName);
            }
        }


        private void IMG_dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (IMG_Current >= IMG_StackCount)
            {
                IMG_dispatcherTimer.IsEnabled = false;
                IMG_Work = true;
            }

            if (!IMG_Work)
            {
                IMG_Work = true;
                if (UpdateCanvasIcon(IMG_Current))
                    IMG_Current++;

                IMG_Work = false;
            }
        }


        bool UpdateCanvasIcon(int i)
        {
            if (i < data.pages.Count)
            {
                IMG_can = CanvasSaveLoad.LoadSpecificCanvas(data,i);
                IMG_can.Width = CanvasW;
                IMG_can.Height = CanvasH;
                ((TestType_ControlN)Stack_TestList.Children[i]).SetCanvasImage((BitmapSource)new ImageSourceConverter().ConvertFrom(CanvasWriter.SaveCanvasToImgSimulate(IMG_can, CanvasImgX, CanvasImgY)));
                return true;
            }
            return false;
        }

        void UpdateCurrentCanvasIcon()
        {
            ((TestType_ControlN)Stack_TestList.Children[SelectedPosition]).SetCanvasImage((BitmapSource)new ImageSourceConverter().ConvertFrom(CanvasWriter.SaveCanvasToImg(DesignCanvas, CanvasImgX, CanvasImgY)));
        }


        void UpdateAllCanvasIcons()
        {
            Canvas can;

            for (int i = 0; i < Stack_TestList.Children.Count; i++)
            {
                can = CanvasSaveLoad.LoadSpecificCanvas(data,i);
                IMG_can.Width = CanvasW;
                IMG_can.Height = CanvasH;
                ((TestType_ControlN)Stack_TestList.Children[i]).SetCanvasImage((BitmapSource)new ImageSourceConverter().ConvertFrom(CanvasWriter.SaveCanvasToImgSimulate(can, CanvasImgX, CanvasImgY)));
            }

        }



    }
}
