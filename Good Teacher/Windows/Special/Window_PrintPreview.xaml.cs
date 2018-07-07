using Good_Teacher.Class;
using Good_Teacher.Class.Serialization;
using System.Diagnostics;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_PrintPreview.xaml
    /// </summary>
    public partial class Window_PrintPreview : Window
    {

        DataStore data;

        int show = 0;

        public Window_PrintPreview(DataStore datas)
        {
            InitializeComponent();

            data = datas;

            SB_Fit.SetChecked(true);
            SB_OnePage.SetChecked(true);

            SB_ScaleView.SetChecked(true);

            L_Page.Content = "1/" + data.pages.Count;

            //RenderPreview(show);
        }

        void RenderPreviewAll(int from, int to)
        {
            SPanel.Children.Clear();

            for(int i=from;i<to;i++)
            {
                Canvas can = CanvasSaveLoad.LoadSpecificCanvas(data, i);
                CanvasSaveLoad.ToPresentationMode(can);
                CanvasSaveLoad.SimulateCanvas(can);

                can.UpdateLayout();
                Size size = new Size();
                can.Measure(size);
                can.Arrange(new Rect(size));

                ImageSource bs = ImageWorker.ByteDataToImage(CanvasWriter.SaveCanvasToImgSimulateFullPng(can));

                Border pageP = new Border();
                pageP.Background = new ImageBrush(bs);
                pageP.BorderBrush = new SolidColorBrush(Color.FromRgb(44, 62, 80));
                pageP.BorderThickness = new Thickness(3);
                pageP.Width = data.CanvasW;
                pageP.Height = data.CanvasH;
                pageP.Margin = new Thickness(10, 10, 10, 15);

                SPanel.Children.Add(pageP);
            }

            L_Page.Content = "-/"+data.pages.Count;
        }


        void RenderPreview(int i)
        {
            SPanel.Children.Clear();

            Canvas can = CanvasSaveLoad.LoadSpecificCanvas(data, i);
            CanvasSaveLoad.ToPresentationMode(can);
            CanvasSaveLoad.SimulateCanvas(can);

            can.UpdateLayout();
            Size size = new Size();
            can.Measure(size);
            can.Arrange(new Rect(size));

            ImageSource bs = ImageWorker.ByteDataToImage(CanvasWriter.SaveCanvasToImgSimulateFullPng(can));

            Border pageP = new Border();
            pageP.Background = new ImageBrush(bs);
            pageP.BorderBrush = new SolidColorBrush(Color.FromRgb(44, 62, 80));
            pageP.BorderThickness = new Thickness(3);
            pageP.Width = data.CanvasW;
            pageP.Height = data.CanvasH;
            pageP.Margin = new Thickness(10, 10, 10, 15);

            SPanel.Children.Add(pageP);

            L_Page.Content = (i+1)+"/"+data.pages.Count;
        }
        

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if(show<data.pages.Count-1)
            show++;

            SB_OnePage.SetCheckedNoCall(true);
            SB_AllPage.SetCheckedNoCall(false);

            RenderPreview(show);
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (show > 0)
                show--;

            SB_OnePage.SetCheckedNoCall(true);
            SB_AllPage.SetCheckedNoCall(false);

            RenderPreview(show);
        }


        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog prnt = new PrintDialog();
            prnt.UserPageRangeEnabled = true;
            prnt.MaxPage = (uint)data.pages.Count;
            prnt.MinPage = 1;

            if (prnt.ShowDialog() == true)
            {

                if (CB_OutPutColor.SelectedIndex > 0)
                    prnt.PrintTicket.OutputColor = (OutputColor)(CB_OutPutColor.SelectedIndex);

                if (CB_Orientation.SelectedIndex > 0)
                    prnt.PrintTicket.PageOrientation = (PageOrientation)(CB_Orientation.SelectedIndex);


                FixedDocument document = new FixedDocument();

                PrintCapabilities capabilities = prnt.PrintQueue.GetPrintCapabilities(prnt.PrintTicket);
                Size visibleSize = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
                Size Margins = new Size(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight);

                if (SB_Fit.IsChecked())
                {
                    document.DocumentPaginator.PageSize = new Size(prnt.PrintableAreaWidth, prnt.PrintableAreaHeight);
                }
                else
                {
                    document.DocumentPaginator.PageSize = new Size(data.CanvasW,data.CanvasH);
                }

                Debug.WriteLine("Page Range:  From: " + prnt.PageRange.PageFrom + "  To: " + prnt.PageRange.PageTo);


                if (prnt.PageRange.PageTo < 1)
                {
                    for (int i = 0; i < data.pages.Count; i++)
                    {
                        PrintAddPage(i, document,visibleSize,Margins);
                    }
                }
                else
                {
                    for (int i = (prnt.PageRange.PageFrom - 1); i < prnt.PageRange.PageTo; i++)
                    {
                        PrintAddPage(i, document,visibleSize,Margins);
                    }
                }

                prnt.PrintDocument(document.DocumentPaginator, "Good Teacher");
            }
        }
        

        
        private void PrintAddPage(int i, FixedDocument document,Size sizeContent,Size margins)
        {

            Canvas can = CanvasSaveLoad.LoadSpecificCanvas(data, i);
            CanvasSaveLoad.ToPresentationMode(can);
            CanvasSaveLoad.SimulateCanvas(can);


            can.UpdateLayout();
            Size size = new Size();
            can.Measure(size);
            can.Arrange(new Rect(size));

            ImageSource bs = ImageWorker.ByteDataToImage(CanvasWriter.SaveCanvasToImgSimulateFullPng(can));
            Image image = new Image();
            if (SB_Fit.IsChecked())
            {
                image.Width = sizeContent.Width;
                image.Height = sizeContent.Height;
            }
            else
            {
                image.Width = data.CanvasW;
                image.Height = data.CanvasH;
            }
            image.Stretch = Stretch.Uniform;
            image.Source = bs;


            FixedPage page = new FixedPage();
            page.Width = document.DocumentPaginator.PageSize.Width;
            page.Height = document.DocumentPaginator.PageSize.Width;

            if (SB_Fit.IsChecked())
            {
                page.Margin = new Thickness(margins.Width, margins.Height, 0, 0);
            }


            page.Children.Clear();
            page.Children.Add(image);
            
            PageContent pageContent = new PageContent();
            ((IAddChild)pageContent).AddChild(page);
            document.Pages.Add(pageContent);
        }

        private void SB_Real_OnCheckChanged(object sender, bool IsChecked)
        {
            SB_Real.SetCheckedNoCall(true);
            SB_Fit.SetCheckedNoCall(false);
        }

        private void SB_Fit_OnCheckChanged(object sender, bool IsChecked)
        {
            SB_Fit.SetCheckedNoCall(true);
            SB_Real.SetCheckedNoCall(false);
        }

        private void SB_OnePage_OnCheckChanged(object sender, bool IsChecked)
        {
            SB_OnePage.SetCheckedNoCall(true);
            SB_AllPage.SetCheckedNoCall(false);
            RenderPreview(show);
        }

        private void SB_AllPage_OnCheckChanged(object sender, bool IsChecked)
        {
            SB_AllPage.SetCheckedNoCall(true);
            SB_OnePage.SetCheckedNoCall(false);
            RenderPreviewAll(0, data.pages.Count);
        }

        private void SB_ScaleView_OnCheckChanged(object sender, bool IsChecked)
        {
            SB_ScaleView.SetCheckedNoCall(true);
            SB_RealView.SetCheckedNoCall(false);

            VB_Image.Stretch = Stretch.Uniform;
            SV_Preview.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
        }

        private void SB_RealView_OnCheckChanged(object sender, bool IsChecked)
        {
            SB_RealView.SetCheckedNoCall(true);
            SB_ScaleView.SetCheckedNoCall(false);

            VB_Image.Stretch = Stretch.None;
            SV_Preview.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

    }
}
