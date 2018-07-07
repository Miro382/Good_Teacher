using Good_Teacher.Controls;
using Good_Teacher.Controls.Special;
using Good_Teacher.Windows;
using Good_Teacher.Windows.Special;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_ContentViewer.xaml
    /// </summary>
    public partial class Value_ContentViewer : System.Windows.Controls.Page
    {

        ContentViewer cont;
        DataStore data;

        public delegate void ChangedContent();
        public event ChangedContent ChangedContentViewer;


        public Value_ContentViewer(DataStore datas, ContentViewer contentViewer)
        {
            InitializeComponent();

            cont = contentViewer;

            data = datas;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();

            alignmentSelector.SetData(cont.HorizontalContentAlignment, cont.VerticalContentAlignment);
            alignmentSelector.ChangedAlign += AlignmentSelector_ChangedAlign;

            brushselector.SetData(cont, data, true);
            brushselector.LoadData(cont.Background);
            brushselector.ChangedBrush -= Brushselector_ChangedBrush;
            brushselector.ChangedBrush += Brushselector_ChangedBrush;

        }


        private void AlignmentSelector_ChangedAlign(HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            cont.HorizontalContentAlignment = horizontal;
            cont.VerticalContentAlignment = vertical;
        }

        private void Brushselector_ChangedBrush(BrushSelector brushSelector, Brush Sbrush)
        {
            cont.Background = Sbrush;
        }

        private void Content_Click(object sender, RoutedEventArgs e)
        {
            DWindow_Content window_Content = new DWindow_Content(data, cont.contentCreator);
            window_Content.Owner = Window.GetWindow(this);
            window_Content.ShowDialog();

            if (window_Content.IsOK == true)
            {
                cont.contentCreator = window_Content.content;
                cont.Content = "";
                cont.Content = cont.contentCreator.Create(data);
                ChangedContentViewer?.Invoke();
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

        private void Button_MakeRepeating_Click(object sender, RoutedEventArgs e)
        {
            if (data.pages.Count > 0)
            {
                Window_Repeating window_Repeating = new Window_Repeating(data,cont);
                window_Repeating.Owner = Window.GetWindow(this);


                cont.Content = cont.contentCreator.Create(data);

                window_Repeating.ShowDialog();
            }
        }

    }
}
