using Good_Teacher.Class;
using Good_Teacher.Class.Save;
using Good_Teacher.Controls;
using Good_Teacher.Pages;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_Repeating.xaml
    /// </summary>
    public partial class Window_Repeating : Window
    {
        DataStore data;

        int editingC = 0;

        public Window_Repeating(DataStore datas)
        {
            InitializeComponent();

            data = datas;

            TB_Name.Text = Strings.ResStrings.Repeating + " " + (data.RepeatingControls.Count+1);

            ShowContentViewer.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            Value_ContentViewer contentViewer = new Value_ContentViewer(data, ShowContentViewer);

            contentViewer.ChangedContentViewer -= ContentViewer_ChangedContentViewer;
            contentViewer.ChangedContentViewer += ContentViewer_ChangedContentViewer;

            contentViewer.Button_DeleteControl.Visibility = Visibility.Collapsed;
            contentViewer.Button_MakeRepeating.Visibility = Visibility.Collapsed;

            CVEditor.Content = contentViewer;

            AddListeners();

            UpdateAllControls();

            R_DefaultCanvas.Width = MainWindow.CanvasW;
            R_DefaultCanvas.Height = MainWindow.CanvasH;
        }



        public Window_Repeating(DataStore datas, ContentViewer CcontentViewer)
        {
            InitializeComponent();

            data = datas;

            TB_Name.Text = Strings.ResStrings.Repeating + " " + (data.RepeatingControls.Count + 1);

            ShowContentViewer.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            ShowContentViewer = (ContentViewer)ClassWorker.CloneObject(CcontentViewer);

            ShowContentViewer.MaxHeight = 150;

            SCV_ContentViewer.Content = ShowContentViewer;

            Canvas.SetLeft(ShowContentViewer, Canvas.GetLeft(CcontentViewer));
            Canvas.SetTop(ShowContentViewer, Canvas.GetTop(CcontentViewer));

            Value_ContentViewer contentViewer = new Value_ContentViewer(data, ShowContentViewer);

            contentViewer.ChangedContentViewer -= ContentViewer_ChangedContentViewer;
            contentViewer.ChangedContentViewer += ContentViewer_ChangedContentViewer;

            contentViewer.Button_DeleteControl.Visibility = Visibility.Collapsed;
            contentViewer.Button_MakeRepeating.Visibility = Visibility.Collapsed;

            CVEditor.Content = contentViewer;

            AddListeners();

            DrawToCanvas();

            UpdateAllControls();
        }




        private void AddListeners()
        {

            DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(
            Canvas.LeftProperty, typeof(ContentViewer));

            descriptor.AddValueChanged(ShowContentViewer, OnContentViewerPropertyChanged);

            DependencyPropertyDescriptor descriptorY = DependencyPropertyDescriptor.FromProperty(
            Canvas.TopProperty, typeof(ContentViewer));

            descriptorY.AddValueChanged(ShowContentViewer, OnContentViewerPropertyChanged);


            DependencyPropertyDescriptor descriptorF = DependencyPropertyDescriptor.FromProperty(
            BackgroundProperty, typeof(ContentViewer));

            descriptorF.AddValueChanged(ShowContentViewer, OnContentViewerPropertyChanged);


            DependencyPropertyDescriptor descriptorW = DependencyPropertyDescriptor.FromProperty(
            WidthProperty, typeof(ContentViewer));

            descriptorW.AddValueChanged(ShowContentViewer, OnContentViewerPropertyChanged);

            DependencyPropertyDescriptor descriptorH = DependencyPropertyDescriptor.FromProperty(
            HeightProperty, typeof(ContentViewer));

            descriptorH.AddValueChanged(ShowContentViewer, OnContentViewerPropertyChanged);
        }

        private void OnContentViewerPropertyChanged(object sender, EventArgs e)
        {
            DrawToCanvas();
        }

        private void ContentViewer_ChangedContentViewer()
        {
            DrawToCanvas();
        }

        private void DrawToCanvas()
        {
            /*
            ShowContentViewerV.contentCreator.contents = new List<Class.Serialization.Content_Ser.Content_Default>(ShowContentViewer.contentCreator.contents);
            ShowContentViewerV.Content = ShowContentViewerV.contentCreator.Create(data);
            */

            CanvasShow.Children.Clear();
            ContentViewer ShowContentViewerV = (ContentViewer)ClassWorker.CloneObject(ShowContentViewer);
            ShowContentViewerV.Content = ShowContentViewerV.contentCreator.Create(data);

            ShowContentViewerV.Width = ShowContentViewer.Width;
            ShowContentViewerV.Height = ShowContentViewer.Height;
            ShowContentViewerV.MaxHeight = Double.PositiveInfinity;
            Canvas.SetLeft(ShowContentViewerV,Canvas.GetLeft(ShowContentViewer));
            Canvas.SetTop(ShowContentViewerV, Canvas.GetTop(ShowContentViewer));

            CanvasShow.Children.Add(ShowContentViewerV);
        }

        private void UpdateAllControls()
        {
            VPanel_Repeaters.Children.Clear();

            int k = 0;
            foreach(RepeatingData rdata in data.RepeatingControls)
            {
                RepeatingInfoControl repeatingInfoControl = new RepeatingInfoControl();
                repeatingInfoControl.Margin = new Thickness(10);
                repeatingInfoControl.TB_Name.Text = rdata.Name;
                repeatingInfoControl.MouseLeftButtonUp += RepeatingInfoControl_MouseLeftButtonUp;
                repeatingInfoControl.Tag = k;
                repeatingInfoControl.RemoveClick += RepeatingInfoControl_RemoveClick;
                VPanel_Repeaters.Children.Add(repeatingInfoControl);
                k++;
            }

            if (data.RepeatingControls.Count > 0)
            {
                OpacityButton opacityButton = new OpacityButton();
                opacityButton.Content = new Image() { Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Icons/AddPlus.png")) };
                RenderOptions.SetBitmapScalingMode(opacityButton, BitmapScalingMode.Fant);
                opacityButton.Width = 42;
                opacityButton.Height = 42;
                opacityButton.NormalOpacity = 1;
                opacityButton.HoverOpacity = 0.7f;
                opacityButton.ClickOpacity = 0.5f;
                opacityButton.Margin = new Thickness(0, 5, 0, 10);

                opacityButton.MouseLeftButtonUp += OpacityButton_MouseLeftButtonUp;

                VPanel_Repeaters.Children.Add(opacityButton);
            }
        }

        //Add new
        private void OpacityButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ShowContentViewer = new ContentViewer();

            ShowContentViewer.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            TB_Name.Text = Strings.ResStrings.Repeating + " " + (data.RepeatingControls.Count + 1);

            ShowContentViewer.MaxHeight = 150;

            SCV_ContentViewer.Content = ShowContentViewer;

            Value_ContentViewer contentViewer = new Value_ContentViewer(data, ShowContentViewer);

            CVEditor.Content = null;
            CVEditor.Content = contentViewer;

            contentViewer.Button_DeleteControl.Visibility = Visibility.Collapsed;
            contentViewer.Button_MakeRepeating.Visibility = Visibility.Collapsed;

            contentViewer.ChangedContentViewer -= ContentViewer_ChangedContentViewer;
            contentViewer.ChangedContentViewer += ContentViewer_ChangedContentViewer;

            TypeLabel.Content = Strings.ResStrings.AddNew;
            B_Edit.Visibility = Visibility.Collapsed;

            SP_EditName.Visibility = Visibility.Collapsed;
            TBL_EditName.Text = "";

            DrawToCanvas();

            AddListeners();

            LB_IgnorePages.Items.Clear();
        }

        private void RepeatingInfoControl_RemoveClick(RepeatingInfoControl sender)
        {
            data.RepeatingControls.RemoveAt((int)sender.Tag);
            UpdateAllControls();
        }

        //Edit
        private void RepeatingInfoControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RepeatingData repeatingData = data.RepeatingControls[(int)((RepeatingInfoControl)sender).Tag];
            TB_Name.Text = repeatingData.Name;

            ShowContentViewer = repeatingData.contentViewer.CreateControl(data);

            ShowContentViewer.MaxHeight = 150;

            SCV_ContentViewer.Content = ShowContentViewer;

            Value_ContentViewer contentViewer = new Value_ContentViewer(data, ShowContentViewer);

            CVEditor.Content = null;
            CVEditor.Content = contentViewer;

            contentViewer.Button_DeleteControl.Visibility = Visibility.Collapsed;
            contentViewer.Button_MakeRepeating.Visibility = Visibility.Collapsed;

            contentViewer.ChangedContentViewer -= ContentViewer_ChangedContentViewer;
            contentViewer.ChangedContentViewer += ContentViewer_ChangedContentViewer;

            TypeLabel.Content = Strings.ResStrings.Edit;
            B_Edit.Visibility = Visibility.Visible;

            SP_EditName.Visibility = Visibility.Visible;
            TBL_EditName.Text = repeatingData.Name;

            DrawToCanvas();

            AddListeners();

            editingC = (int)((RepeatingInfoControl)sender).Tag;

            AddIgnorePagesToList(editingC);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            data.RepeatingControls.Add(new RepeatingData(ShowContentViewer,TB_Name.Text,data));
            AddIgnorePages(data.RepeatingControls.Count-1);
            UpdateAllControls();
            OpacityButton_MouseLeftButtonUp(this, null);
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            data.RepeatingControls[editingC] = new RepeatingData(ShowContentViewer, TB_Name.Text, data);
            AddIgnorePages(editingC);
            UpdateAllControls();
        }

        private void OpacityButtonUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int page = 0;
            if(int.TryParse(NTB_Page.Text,out page))
            {
                if (page < data.pages.Count)
                    page++;
                else
                    page = data.pages.Count;

                if (page < 1)
                    page = 1;

                NTB_Page.Text = "" + page;
            }
            else
            {
                NTB_Page.Text = "1";
            }
        }

        private void OpacityButtonDown_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int page = 0;
            if (int.TryParse(NTB_Page.Text, out page))
            {
                if (page > 1)
                    page--;
                else
                    page = 1;

                if (page > data.pages.Count)
                    page = data.pages.Count;

                NTB_Page.Text = "" + page;
            }
            else
            {
                NTB_Page.Text = "1";
            }
        }


        private void Button_Click_AddToList(object sender, RoutedEventArgs e)
        {
            int page = 0;
            if (int.TryParse(NTB_Page.Text, out page))
            {
                if (page < 1)
                    page = 1;

                if (page > data.pages.Count)
                    page = data.pages.Count;

                NTB_Page.Text = "" + page;

                if (!LB_IgnorePages.Items.Contains(page))
                LB_IgnorePages.Items.Add(page);
            }
            else
            {
                NTB_Page.Text = "1";
            }
        }

        private void Button_Click_RemoveFromList(object sender, RoutedEventArgs e)
        {
            if(LB_IgnorePages.SelectedIndex>=0)
            {
                LB_IgnorePages.Items.RemoveAt(LB_IgnorePages.SelectedIndex);
            }
        }


        void AddIgnorePages(int page)
        {
            data.RepeatingControls[page].IgnorePages.Clear();

            foreach(int li in LB_IgnorePages.Items)
            {
                data.RepeatingControls[page].IgnorePages.Add(li);
            }
        }

        void AddIgnorePagesToList(int page)
        {
            LB_IgnorePages.Items.Clear();

            foreach (int li in data.RepeatingControls[page].IgnorePages)
            {
                LB_IgnorePages.Items.Add(li);
            }
        }


    }
}
