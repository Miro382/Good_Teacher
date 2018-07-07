using Good_Teacher.Class.Save.Output;
using Good_Teacher.Class.Workers;
using System.Windows;

namespace Good_Teacher_Repairo.Windows
{
    /// <summary>
    /// Interaction logic for Window_Info.xaml
    /// </summary>
    public partial class Window_Info : Window
    {
        public Window_Info(PresentationOutput output)
        {
            InitializeComponent();

            LCreated.Content = output.CreatedTime.ToShortDateString()+"   "+output.CreatedTime.ToLongTimeString();
            LPages.Content = output.PresentationPagesCount;
            LBPages.Content = output.Pages.Count;
            LCX.Content = ""+output.W;
            LCY.Content = ""+output.H;
            LScripts.Content = LocalizationWorker.BoolToYesNo(output.HaveScripts);
            LAllowedScripts.Content = LocalizationWorker.BoolToYesNo(output.ScriptsAllowed);
        }
    }
}
