using Good_Teacher.Class.Workers;
using Good_Teacher.Windows;
using Good_Teacher.Windows.Special;
using Good_Teacher_Repairo.Class;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Good_Teacher_Repairo
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

                    if (code > Good_Teacher.MainWindow.VersionCode)
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


        private void ExportToImage_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPosition >= 0)
            {

                if (((FrameworkElement)sender).Tag.ToString() == "OnePng")
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = Good_Teacher.Strings.ResStrings.Image + " (*.png) | *.png|" + Good_Teacher.Strings.ResStrings.AllFiles + "| *.*";
                    saveFileDialog.FileName = "GoodTeacher_" + SelectedPosition + ".png";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        BitmapSource bs = (BitmapSource)ImageLoader.GetImage(output.Pages[SelectedPosition].Image);

                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bs));

                        using (var fileStream = new System.IO.FileStream(saveFileDialog.FileName, System.IO.FileMode.Create))
                        {
                            encoder.Save(fileStream);
                        }
                        ShowNotification(Good_Teacher.Strings.ResStrings.Exported);

                    }
                }
                else if (((FrameworkElement)sender).Tag.ToString() == "OneJpg")
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = Good_Teacher.Strings.ResStrings.Image + " (*.jpg) | *.jpg|" + Good_Teacher.Strings.ResStrings.AllFiles + "| *.*";
                    saveFileDialog.FileName = "GoodTeacher_" + SelectedPosition + ".jpg";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        BitmapSource bs = (BitmapSource)ImageLoader.GetImage(output.Pages[SelectedPosition].Image);

                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bs));

                        using (var fileStream = new System.IO.FileStream(saveFileDialog.FileName, System.IO.FileMode.Create))
                        {
                            encoder.Save(fileStream);
                        }
                        ShowNotification(Good_Teacher.Strings.ResStrings.Exported);

                    }
                }
                else if (((FrameworkElement)sender).Tag.ToString() == "AllPng")
                {
                    System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                    folderBrowserDialog.ShowNewFolderButton = true;
                    folderBrowserDialog.Description = Good_Teacher.Strings.ResStrings.ExportToAllImages;
                    if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        for (int i = 0; i < output.Pages.Count; i++)
                        {
                            BitmapSource bs = (BitmapSource)ImageLoader.GetImage(output.Pages[i].Image);

                            BitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(bs));

                            if (File.Exists(folderBrowserDialog.SelectedPath + "\\GoodTeacher_" + i + ".png"))
                                File.Delete(folderBrowserDialog.SelectedPath + "\\GoodTeacher_" + i + ".png");

                            using (var fileStream = new System.IO.FileStream(folderBrowserDialog.SelectedPath + "\\GoodTeacher_" + i + ".png", System.IO.FileMode.Create))
                            {
                                encoder.Save(fileStream);
                            }
                        }
                        ShowNotification(Good_Teacher.Strings.ResStrings.Exported);
                    }
                }
                else if (((FrameworkElement)sender).Tag.ToString() == "AllJpg")
                {
                    System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                    folderBrowserDialog.ShowNewFolderButton = true;
                    folderBrowserDialog.Description = Good_Teacher.Strings.ResStrings.ExportToAllImages;
                    if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        for (int i = 0; i < output.Pages.Count; i++)
                        {
                            BitmapSource bs = (BitmapSource)ImageLoader.GetImage(output.Pages[i].Image);

                            BitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(bs));

                            if (File.Exists(folderBrowserDialog.SelectedPath + "\\GoodTeacher_" + i + ".jpg"))
                                File.Delete(folderBrowserDialog.SelectedPath + "\\GoodTeacher_" + i + ".jpg");

                            using (var fileStream = new System.IO.FileStream(folderBrowserDialog.SelectedPath + "\\GoodTeacher_" + i + ".jpg", System.IO.FileMode.Create))
                            {
                                encoder.Save(fileStream);
                            }
                        }
                        ShowNotification(Good_Teacher.Strings.ResStrings.Exported);
                    }
                }
                else if (((FrameworkElement)sender).Tag.ToString() == "OneClipboard")
                {
                    BitmapSource bs = (BitmapSource)ImageLoader.GetImage(output.Pages[SelectedPosition].Image);

                    Clipboard.SetImage(bs);
                    ShowNotification(Good_Teacher.Strings.ResStrings.CopiedToClipboard);
                }

            }
        }

        private void MenuItemExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPosition >= 0)
            {
                SaveCanvas();

                Window_PDFExport window_PDFExport = new Window_PDFExport(output);
                window_PDFExport.Owner = this;
                window_PDFExport.ShowDialog();

                if (window_PDFExport.IsExported)
                    ShowNotification(Good_Teacher.Strings.ResStrings.Exported);
            }
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








        private void ButtonColor_Click(object sender, RoutedEventArgs e)
        {
            Window_ColorPicker colorp = new Window_ColorPicker(Rect_BackColor.Fill);
            colorp.Owner = Window.GetWindow(this);
            colorp.ShowDialog();
            if (colorp.IsOK() == true)
            {
                Rect_BackColor.Fill = new SolidColorBrush(colorp.GetColor());

                DesignCanvas.DefaultDrawingAttributes.Color = colorp.GetColor();
                Debug.WriteLine("W: " + DesignCanvas.DefaultDrawingAttributes.Width + "  H: " + DesignCanvas.DefaultDrawingAttributes.Height);
            }
        }

        private void Button_EraseAll_Click(object sender, MouseEventArgs e)
        {
            DesignCanvas.Strokes.Clear();
        }


        private void CB_PaintSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DesignCanvas != null)
            {
                InkCanvasEditingMode emode = InkCanvasEditingMode.Ink;

                if (DesignCanvas.EditingMode == InkCanvasEditingMode.EraseByPoint)
                    emode = InkCanvasEditingMode.EraseByPoint;


                DesignCanvas.EditingMode = InkCanvasEditingMode.None;

                switch (CB_PaintSize.SelectedIndex)
                {
                    case 0:
                        DesignCanvas.DefaultDrawingAttributes.Width = 2;
                        DesignCanvas.DefaultDrawingAttributes.Height = 2;
                        DesignCanvas.EraserShape = new RectangleStylusShape(2, 2);
                        break;
                    case 1:
                        DesignCanvas.DefaultDrawingAttributes.Width = 4;
                        DesignCanvas.DefaultDrawingAttributes.Height = 4;
                        DesignCanvas.EraserShape = new RectangleStylusShape(4, 4);
                        break;
                    case 2:
                        DesignCanvas.DefaultDrawingAttributes.Width = 8;
                        DesignCanvas.DefaultDrawingAttributes.Height = 8;
                        DesignCanvas.EraserShape = new RectangleStylusShape(8, 8);
                        break;
                    case 3:
                        DesignCanvas.DefaultDrawingAttributes.Width = 12;
                        DesignCanvas.DefaultDrawingAttributes.Height = 12;
                        DesignCanvas.EraserShape = new RectangleStylusShape(12, 12);
                        break;
                    case 4:
                        DesignCanvas.DefaultDrawingAttributes.Width = 15;
                        DesignCanvas.DefaultDrawingAttributes.Height = 15;
                        DesignCanvas.EraserShape = new RectangleStylusShape(15, 15);
                        break;
                    case 5:
                        DesignCanvas.DefaultDrawingAttributes.Width = 18;
                        DesignCanvas.DefaultDrawingAttributes.Height = 18;
                        DesignCanvas.EraserShape = new RectangleStylusShape(18, 18);
                        break;
                    case 6:
                        DesignCanvas.DefaultDrawingAttributes.Width = 24;
                        DesignCanvas.DefaultDrawingAttributes.Height = 24;
                        DesignCanvas.EraserShape = new RectangleStylusShape(24, 24);
                        break;
                    case 7:
                        DesignCanvas.DefaultDrawingAttributes.Width = 30;
                        DesignCanvas.DefaultDrawingAttributes.Height = 30;
                        DesignCanvas.EraserShape = new RectangleStylusShape(30, 30);
                        break;
                    default:
                        DesignCanvas.DefaultDrawingAttributes.Width = 2;
                        DesignCanvas.DefaultDrawingAttributes.Height = 2;
                        DesignCanvas.EraserShape = new RectangleStylusShape(2, 2);
                        break;
                }


                DesignCanvas.EditingMode = emode;
            }
        }

        private void Button_Draw_OnCheckChanged(object sender, bool IsChecked)
        {
            Button_Draw.SetCheckedNoCall(true);
            Button_Eraser.SetCheckedNoCall(false);
            Button_Cursor.SetCheckedNoCall(false);
            DesignCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Button_Eraser_OnCheckChanged(object sender, bool IsChecked)
        {
            Button_Eraser.SetCheckedNoCall(true);
            Button_Draw.SetCheckedNoCall(false);
            Button_Cursor.SetCheckedNoCall(false);
            DesignCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void Button_Cursor_OnCheckChanged(object sender, bool IsChecked)
        {
            Button_Eraser.SetCheckedNoCall(false);
            Button_Draw.SetCheckedNoCall(false);
            Button_Cursor.SetCheckedNoCall(true);
            DesignCanvas.EditingMode = InkCanvasEditingMode.None;
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


    }
}
