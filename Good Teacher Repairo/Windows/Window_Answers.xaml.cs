using Good_Teacher.Class.Save.Output;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher_Repairo.Windows
{
    /// <summary>
    /// Interaction logic for Window_Answers.xaml
    /// </summary>
    public partial class Window_Answers : Window
    {
        public Window_Answers(PresentationOutput output)
        {
            InitializeComponent();

            int allgood = 0;
            int allwrong = 0;
            int allGW = 0;

            foreach (KeyValuePair<int, OutputPage> pair in output.Pages)
            {
                Label page = new Label();
                page.Content = (pair.Key+1)+".";
                page.HorizontalContentAlignment = HorizontalAlignment.Center;
                page.FontSize = 14;

                Label good = new Label();
                good.Content = ""+output.Pages[pair.Key].GoodAnswers;
                good.HorizontalContentAlignment = HorizontalAlignment.Center;
                good.FontSize = 14;

                Label wrong = new Label();
                wrong.Content = "" + output.Pages[pair.Key].WrongAnswers;
                wrong.HorizontalContentAlignment = HorizontalAlignment.Center;
                wrong.FontSize = 14;

                Label all = new Label();
                all.Content = "" + output.Pages[pair.Key].AllAnswers;
                all.HorizontalContentAlignment = HorizontalAlignment.Center;
                all.FontSize = 14;

                allgood += output.Pages[pair.Key].GoodAnswers;
                allwrong += output.Pages[pair.Key].WrongAnswers;
                allGW += output.Pages[pair.Key].AllAnswers;

                PageP.Children.Add(page);
                GoodP.Children.Add(good);
                WrongP.Children.Add(wrong);
                AllP.Children.Add(all);
            }

            {
                Label page = new Label();
                page.Content = Good_Teacher.Strings.ResStrings.All;
                page.HorizontalContentAlignment = HorizontalAlignment.Center;
                page.FontSize = 14;
                page.FontWeight = FontWeights.Bold;

                Label good = new Label();
                good.Content = allgood;
                good.HorizontalContentAlignment = HorizontalAlignment.Center;
                good.FontSize = 14;
                good.FontWeight = FontWeights.Bold;

                Label wrong = new Label();
                wrong.Content = allwrong;
                wrong.HorizontalContentAlignment = HorizontalAlignment.Center;
                wrong.FontSize = 14;
                wrong.FontWeight = FontWeights.Bold;

                Label all = new Label();
                all.Content = allGW;
                all.HorizontalContentAlignment = HorizontalAlignment.Center;
                all.FontSize = 14;
                all.FontWeight = FontWeights.Bold;

                PageP.Children.Add(page);
                GoodP.Children.Add(good);
                WrongP.Children.Add(wrong);
                AllP.Children.Add(all);
            }
        }


    }
}
