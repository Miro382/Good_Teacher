using Good_Teacher.Windows.Special;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using WpfMath.Controls;

namespace Good_Teacher.Pages
{
    /// <summary>
    /// Interaction logic for Value_Formula.xaml
    /// </summary>
    public partial class Value_Formula : System.Windows.Controls.Page
    {
        FormulaControl cont;
        DataStore data;
        
        public Value_Formula(DataStore dataStore,FormulaControl formula)
        {
            InitializeComponent();

            cont = formula;

            data = dataStore;

            positionselector.SetData(cont);
            positionselector.LoadData();

            effectselector.SetData(cont);
            effectselector.LoadData();

            brushselector.SetData(cont, data, true);
            brushselector.LoadData(cont.Background);
            brushselector.ChangedBrush -= Brushselector_ChangedBrush;
            brushselector.ChangedBrush += Brushselector_ChangedBrush;
            FormulaText.Text = cont.Formula;

            cont.HorizontalAlignment = HorizontalAlignment.Stretch;
            cont.VerticalAlignment = VerticalAlignment.Stretch;

            TB_Size.Text = ""+cont.Scale;
        }

        private void Brushselector_ChangedBrush(Controls.Special.BrushSelector brushSelector, Brush Sbrush)
        {
            cont.Background = Sbrush;
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

        private void SetFormula_Click(object sender, RoutedEventArgs e)
        {

            cont.Width = Double.NaN;
            cont.Height = Double.NaN;

            cont.Formula = FormulaText.Text;

            cont.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            cont.Width = cont.DesiredSize.Width;
            cont.Height = cont.DesiredSize.Height;
        }

        private void TB_Size_LostFocus(object sender, RoutedEventArgs e)
        {
            SetSize();
        }

        private void TB_Size_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetSize();
            }
            else if (e.Key == Key.Escape)
            {
                Keyboard.ClearFocus();
                SetSize();
            }
        }


        void SetSize()
        {
            double size = 0;

            if(double.TryParse(TB_Size.Text,out size))
            {
                cont.Scale = size;

                cont.Width = Double.NaN;
                cont.Height = Double.NaN;

                cont.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                cont.Width = cont.DesiredSize.Width;
                cont.Height = cont.DesiredSize.Height;
            }
            else
            {
                TB_Size.Text = ""+cont.Scale;
            }
        }


        private void WriteFormula_Click(object sender, RoutedEventArgs e)
        {
            Window_FormulaWriter formulaWriter = new Window_FormulaWriter(cont.Formula);
            formulaWriter.Owner = Window.GetWindow(this);

            formulaWriter.ShowDialog();

            if(formulaWriter.IsOK)
            {
                FormulaText.Text = formulaWriter.formula;

                cont.Formula = FormulaText.Text;

                cont.Width = Double.NaN;
                cont.Height = Double.NaN;

                cont.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                cont.Width = cont.DesiredSize.Width;
                cont.Height = cont.DesiredSize.Height;
            }
        }


    }
}
