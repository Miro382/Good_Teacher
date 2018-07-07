using Good_Teacher.Class.History;
using Good_Teacher.Class.Serialization;
using Good_Teacher.Class.Workers;
using Good_Teacher.Windows.Special;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Good_Teacher
{
    public partial class MainWindow : Window
    {


        private void CheckVersion()
        {
            try
            {
                if (TempFilesWorker.CheckVersion())
                {
                    float code = 0;
                    float important = 0;
                    string versiont = "";
                    DateTime date;

                    WebWorker.GetVersionInfo(out code, out important, out versiont, out date);

                    if (code > VersionCode)
                    {
                        Window_CheckForUpdates checkForUpdates = new Window_CheckForUpdates(code, important, versiont, date);
                        checkForUpdates.Owner = this;
                        checkForUpdates.ShowDialog();
                    }


                    TempFilesWorker.WriteCurrentDay();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }


        private void ToPresentationMode()
        {
            InPresentationMode = true;
            PresentationModeSaveStatePage = SelectedPosition;
            SelectedPosition = -1;
            DesignCanvas.Children.Clear();
        }

        private void BackFromPresentationMode()
        {
            InPresentationMode = false;
            SelectedPosition = PresentationModeSaveStatePage;
            LoadCanvas();
        }



        private void ExportToImage_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPosition >= 0)
            {
                SaveCanvas();
                LoadCanvas();

                if (((FrameworkElement)sender).Tag.ToString() == "OnePng")
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = Strings.ResStrings.Image + " (*.png) | *.png|" + Strings.ResStrings.AllFiles + "| *.*";
                    saveFileDialog.FileName = "GoodTeacher_" + SelectedPosition + ".png";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        Canvas can = CanvasSaveLoad.LoadSpecificCanvas(data, SelectedPosition);
                        CanvasSaveLoad.ToPresentationMode(can);
                        CanvasSaveLoad.SimulateCanvas(can);
                        BitmapSource bs = (BitmapSource)new ImageSourceConverter().ConvertFrom(CanvasWriter.SaveCanvasToImgSimulateFullPng(can));

                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bs));

                        using (var fileStream = new System.IO.FileStream(saveFileDialog.FileName, System.IO.FileMode.Create))
                        {
                            encoder.Save(fileStream);
                        }
                        ShowNotification(Strings.ResStrings.Exported);

                    }
                }
                else if (((FrameworkElement)sender).Tag.ToString() == "OneJpg")
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = Strings.ResStrings.Image + " (*.jpg) | *.jpg|" + Strings.ResStrings.AllFiles + "| *.*";
                    saveFileDialog.FileName = "GoodTeacher_" + SelectedPosition + ".jpg";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        Canvas can = CanvasSaveLoad.LoadSpecificCanvas(data, SelectedPosition);
                        CanvasSaveLoad.ToPresentationMode(can);
                        CanvasSaveLoad.SimulateCanvas(can);
                        BitmapSource bs = (BitmapSource)new ImageSourceConverter().ConvertFrom(CanvasWriter.SaveCanvasToImgSimulateFullJpg(can));

                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bs));

                        using (var fileStream = new System.IO.FileStream(saveFileDialog.FileName, System.IO.FileMode.Create))
                        {
                            encoder.Save(fileStream);
                        }
                        ShowNotification(Strings.ResStrings.Exported);

                    }
                }
                else if (((FrameworkElement)sender).Tag.ToString() == "AllPng")
                {
                    System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                    folderBrowserDialog.ShowNewFolderButton = true;
                    folderBrowserDialog.Description = Strings.ResStrings.ExportToAllImages;
                    if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        for (int i = 0; i < data.pages.Count; i++)
                        {
                            Canvas can = CanvasSaveLoad.LoadSpecificCanvas(data, i);
                            CanvasSaveLoad.ToPresentationMode(can);
                            CanvasSaveLoad.SimulateCanvas(can);
                            BitmapSource bs = (BitmapSource)new ImageSourceConverter().ConvertFrom(CanvasWriter.SaveCanvasToImgSimulateFullPng(can));

                            BitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(bs));

                            if (File.Exists(folderBrowserDialog.SelectedPath + "\\GoodTeacher_" + i + ".png"))
                                File.Delete(folderBrowserDialog.SelectedPath + "\\GoodTeacher_" + i + ".png");

                            using (var fileStream = new System.IO.FileStream(folderBrowserDialog.SelectedPath + "\\GoodTeacher_" + i + ".png", System.IO.FileMode.Create))
                            {
                                encoder.Save(fileStream);
                            }
                        }
                        ShowNotification(Strings.ResStrings.Exported);
                    }
                }
                else if (((FrameworkElement)sender).Tag.ToString() == "AllJpg")
                {
                    System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                    folderBrowserDialog.ShowNewFolderButton = true;
                    folderBrowserDialog.Description = Strings.ResStrings.ExportToAllImages;
                    if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        for (int i = 0; i < data.pages.Count; i++)
                        {
                            Canvas can = CanvasSaveLoad.LoadSpecificCanvas(data, i);
                            CanvasSaveLoad.ToPresentationMode(can);
                            CanvasSaveLoad.SimulateCanvas(can);
                            BitmapSource bs = (BitmapSource)new ImageSourceConverter().ConvertFrom(CanvasWriter.SaveCanvasToImgSimulateFullJpg(can));

                            BitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(bs));

                            if (File.Exists(folderBrowserDialog.SelectedPath + "\\GoodTeacher_" + i + ".jpg"))
                                File.Delete(folderBrowserDialog.SelectedPath + "\\GoodTeacher_" + i + ".jpg");

                            using (var fileStream = new System.IO.FileStream(folderBrowserDialog.SelectedPath + "\\GoodTeacher_" + i + ".jpg", System.IO.FileMode.Create))
                            {
                                encoder.Save(fileStream);
                            }
                        }
                        ShowNotification(Strings.ResStrings.Exported);
                    }
                }
                else if (((FrameworkElement)sender).Tag.ToString() == "OneClipboard")
                {
                    Canvas can = CanvasSaveLoad.LoadSpecificCanvas(data, SelectedPosition);
                    CanvasSaveLoad.ToPresentationMode(can);
                    CanvasSaveLoad.SimulateCanvas(can);
                    BitmapSource bs = (BitmapSource)new ImageSourceConverter().ConvertFrom(CanvasWriter.SaveCanvasToImgSimulateFullPng(can));

                    Clipboard.SetImage(bs);
                    ShowNotification(Strings.ResStrings.CopiedToClipboard);
                }



            }
        }





        private void MenuItemExportToPDF_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedPosition >= 0)
            {

                SaveCanvas();
                LoadCanvas();

                Window_PDFExport window_PDFExport = new Window_PDFExport(data);
                window_PDFExport.Owner = this;
                window_PDFExport.ShowDialog();

                if(window_PDFExport.IsExported)
                ShowNotification(Strings.ResStrings.Exported);
            }
        }


        private void SecretPageButton_OnCheckChanged(object sender, bool IsChecked)
        {
            if (SelectedPosition >= 0)
            {
                data.pages[SelectedPosition].isHidden = IsChecked;
                LastSelected.UpdateHidden(IsChecked);
            }
            else
                HiddenPageButton.SetCheckedNoCall(false);
        }


        private void LockButton_OnCheckChanged(object sender, bool IsChecked)
        {
            if (SelectedPosition >= 0)
            {
                SetLockOnCanvas(IsChecked);
                LastSelected.UpdateLocked(IsChecked);
            }
            else
                LockButton.SetCheckedNoCall(false);
        }


        private void SliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ScrollViewerZoom.ActualWidth > 1)
            {
                //Debug.WriteLine("Viewbox W: " + viewboxDC.Width + "   AW: " + viewboxDC.ActualWidth + "    Viewbox H: " + viewboxDC.Height + "   AH: " + viewboxDC.ActualHeight);
                //Debug.WriteLine("ScrollViewerZoom W: " + ScrollViewerZoom.Width + "   AW: " + ScrollViewerZoom.ActualWidth + "    Viewbox H: " + ScrollViewerZoom.Height + "   AH: " + ScrollViewerZoom.ActualHeight);

                if (SliderZoom.Value == 100)
                {
                    ScrollViewerZoom.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    ScrollViewerZoom.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    viewboxDC.Width = double.NaN;
                    viewboxDC.Height = double.NaN;
                }
                else if (SliderZoom.Value < 100)
                {
                    ScrollViewerZoom.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    ScrollViewerZoom.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    viewboxDC.Width = ScrollViewerZoom.ActualWidth * (SliderZoom.Value / 100);
                    viewboxDC.Height = ScrollViewerZoom.ActualHeight * (SliderZoom.Value / 100);
                }
                else
                {
                    ScrollViewerZoom.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                    ScrollViewerZoom.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                    viewboxDC.Width = ScrollViewerZoom.ActualWidth * (SliderZoom.Value / 100);
                    viewboxDC.Height = ScrollViewerZoom.ActualHeight * (SliderZoom.Value / 100);
                }

                TextBox_Zoom.Text = "" + ((int)SliderZoom.Value);
            }
        }


        private void FlatButtonZoomCancel_Click(object sender, MouseEventArgs e)
        {
            SliderZoom.Value = 100;
            ScrollViewerZoom.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            ScrollViewerZoom.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            viewboxDC.Width = double.NaN;
            viewboxDC.Height = double.NaN;
            TextBox_Zoom.Text = "100";
        }

        private void TB_Zoom_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateZoomValueByZoomTextBox();
        }


        private void TB_Zoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Enter)
            {
                UpdateZoomValueByZoomTextBox();
                Keyboard.ClearFocus();
            }
        }


        void UpdateZoomValueByZoomTextBox()
        {
            try
            {
                int zm = int.Parse(TextBox_Zoom.Text);
                SliderZoom.Value = zm;
            }catch(Exception ex)
            {
                Debug.WriteLine("Zoom edit: "+ex);
                TextBox_Zoom.Text = ""+ SliderZoom.Value;
            }
        }



        void SetLockOnCanvas(bool IsLocked)
        {
            ValueEditor.Content = "";

            LockButton.SetCheckedNoCall(IsLocked);
            if (IsLocked)
            {
                Label labellock = new Label();
                labellock.Margin = new Thickness(5, 10, 5, 10);
                labellock.Content = Strings.ResStrings.Locked;
                labellock.HorizontalAlignment = HorizontalAlignment.Stretch;
                labellock.HorizontalContentAlignment = HorizontalAlignment.Center;
                labellock.FontSize = 15;
                labellock.FontWeight = FontWeights.Bold;
                ValueEditor.Content = labellock;
            }
            DesignCanvas.IsEnabled = !IsLocked;
        }



        public void ShowNotification(string Text)
        {
            NotificationLabel.Content = Text;
            Notification.Visibility = Visibility.Visible;
            DoubleAnimation moveAnimH = new DoubleAnimation(0, 35, new Duration(TimeSpan.FromSeconds(0.3)));

            Notification.BeginAnimation(HeightProperty, moveAnimH);

            Task.Factory.StartNew(() => Thread.Sleep(3000))
            .ContinueWith((t) =>
            {
                DoubleAnimation BmoveAnimH = new DoubleAnimation(35, 1, new Duration(TimeSpan.FromSeconds(0.3)));

                Notification.BeginAnimation(HeightProperty, BmoveAnimH);

            }, TaskScheduler.FromCurrentSynchronizationContext());


            Task.Factory.StartNew(() => Thread.Sleep(3350))
            .ContinueWith((ts) =>
            {
                Notification.Visibility = Visibility.Collapsed;

            }, TaskScheduler.FromCurrentSynchronizationContext());

        }


        private void HistoryUndo_OnAdd(object sender, EventArgs e)
        {
            HistoryRedo.Clear();
            Redo_MI.IsEnabled = false;
            Undo_MI.IsEnabled = true;

            if(HistoryUndo.Count>=HistoryLimit)
            {
                HistoryUndo.RemoveAt(0);
            }
        }


        private void Item_Undo_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (HistoryUndo.Count > 0)
                {
                    int last = HistoryUndo.Count - 1;

                    if (HistoryUndo[last] is His_AddControl)
                    {
                        SpecialRemoved = true;
                    }

                    HistoryUndo[last].DoUndoAction(DesignCanvas, data);
                    HistoryCommand historyCommand = HistoryUndo[last];
                    HistoryUndo.RemoveAt(last);
                    HistoryRedo.Add(historyCommand);
                    Redo_MI.IsEnabled = true;

                    if (HistoryUndo.Count == 0)
                        Undo_MI.IsEnabled = false;
                }
                else
                {
                    Undo_MI.IsEnabled = false;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Undo Error: "+ex);
            }
        }



        private void Item_Redo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (HistoryRedo.Count > 0)
                {
                    int last = HistoryRedo.Count - 1;

                    if (HistoryRedo[last] is His_RemovedControl)
                    {
                        SpecialRemoved = true;
                    }

                    HistoryRedo[last].DoRedoAction(DesignCanvas, data);
                    HistoryCommand historyCommand = HistoryRedo[last];
                    HistoryRedo.RemoveAt(last);
                    HistoryUndo.Add(historyCommand);
                    Undo_MI.IsEnabled = true;

                    if(HistoryRedo.Count==0)
                        Redo_MI.IsEnabled = false;
                }
                else
                {
                    Redo_MI.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Redo Error: " + ex);
            }
        }



        public void SetWorkingFileLabel(string path)
        {
            string fileWE = System.IO.Path.GetFileNameWithoutExtension(path);
            if (fileWE.Length > 70)
            {
                fileWE = fileWE.Substring(0, 70);
                fileWE += "...";
            }

            L_FileName.Content = "- " + fileWE;
        }


        void CheckControlPanelSize()
        {
            //Debug.WriteLine("SCR AW: " + SCRV_MenuOptions.ActualWidth + "    SP AW: " + SP_ActionControlPanel.ActualWidth + "   offset: " + SCRV_MenuOptions.HorizontalOffset);

            /*
            if (SCRV_MenuOptions.ScrollableWidth > 0)
            {
                SP_MenuPanelControls.Visibility = Visibility.Visible;
            }
            else
            {
                SP_MenuPanelControls.Visibility = Visibility.Collapsed;
            }
            */

            if (SCRV_MenuOptions.ActualWidth >= SP_ActionControlPanel.ActualWidth)
            {
                SP_MenuPanelControls.Visibility = Visibility.Collapsed;
                SCRV_MenuOptions.ScrollToHorizontalOffset(0);
            }
            else
            {
                SP_MenuPanelControls.Visibility = Visibility.Visible;
            }

            CheckActionButtons();
        }


        void CheckActionButtons(int AddedHOffset = 0)
        {
            if (SCRV_MenuOptions.HorizontalOffset + AddedHOffset >= SCRV_MenuOptions.ScrollableWidth)
            {
                ImgB_NextActions.Visibility = Visibility.Hidden;
                ImgB_PreviousActions.Visibility = Visibility.Visible;

            }

            if (SCRV_MenuOptions.HorizontalOffset + AddedHOffset <= 0)
            {
                ImgB_PreviousActions.Visibility = Visibility.Hidden;
                ImgB_NextActions.Visibility = Visibility.Visible;
            }
        }


    }
}
