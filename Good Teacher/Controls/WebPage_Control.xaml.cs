using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Good_Teacher.Controls
{
    /// <summary>
    /// Interaction logic for WebPage_Control.xaml
    /// </summary>
    public partial class WebPage_Control : UserControl
    {

        public static readonly DependencyProperty ControlPanelBackground =
        DependencyProperty.Register("ControlPanelBackground", typeof(Brush), typeof(WebPage_Control), new PropertyMetadata(new LinearGradientBrush(Color.FromRgb(222, 222, 222), Colors.White, 90)));

        public Brush ControlPanelBack
        {
            get { return (Brush)GetValue(ControlPanelBackground); }
            set { SetValue(ControlPanelBackground, value); }
        }

        public string PathToCPImage = "";
        public Stretch CPStretch = Stretch.Uniform;


        public static readonly DependencyProperty BFVisibilityProperty =
            DependencyProperty.Register("BackForwardVisibility", typeof(Visibility), typeof(WebPage_Control), new PropertyMetadata(Visibility.Collapsed));


        public Visibility BackForwardVisibility
        {
            get { return (Visibility)GetValue(BFVisibilityProperty); }
            set { SetValue(BFVisibilityProperty, value); }
        }

        public string WebUrl
        {
            get { return WebUrls; }
            set { WebUrls = value; }
        }

        public System.Windows.Forms.WebBrowser webBrowser;

        private string WebUrls = "";


        public WebPage_Control()
        {
            InitializeComponent();

            System.Windows.Forms.WebBrowser webbr = new System.Windows.Forms.WebBrowser();
            webBrowser = webbr;
            webbr.ScriptErrorsSuppressed = true;
            webbr.Navigated += Webbr_Navigated;
            webbr.Navigating += Webbr_Navigating;
            FormHost.Child = webbr;
        }

        private void Webbr_Navigating(object sender, System.Windows.Forms.WebBrowserNavigatingEventArgs e)
        {
            Loading.Visibility = Visibility.Visible;
        }

        private void Webbr_Navigated(object sender, System.Windows.Forms.WebBrowserNavigatedEventArgs e)
        {

            Loading.Visibility = Visibility.Collapsed;

            if (webBrowser.Url != null)
                URL_Label.Content = webBrowser.Url.OriginalString;

            if (webBrowser.CanGoBack)
            {
                Undo.IsEnabled = true;
                Undo.Opacity = 1f;
            }
            else
            {
                Undo.IsEnabled = false;
                Undo.Opacity = 0.7f;
            }

            if (webBrowser.CanGoForward)
            {
                Redo.IsEnabled = true;
                Redo.Opacity = 1f;
            }
            else
            {
                Redo.IsEnabled = false;
                Redo.Opacity = 0.7f;
            }
            

        }


        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (webBrowser.CanGoBack)
                webBrowser.GoBack();
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            if (webBrowser.CanGoForward)
                webBrowser.GoForward();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            webBrowser.Dispose();
            webBrowser = null;
        }

    }
}
