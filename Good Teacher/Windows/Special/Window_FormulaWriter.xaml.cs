using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Good_Teacher.Windows.Special
{
    /// <summary>
    /// Interaction logic for Window_FormulaWriter.xaml
    /// </summary>
    public partial class Window_FormulaWriter : Window
    {

        public string formula = "";
        public bool IsOK = false;

        public Window_FormulaWriter(string Formula)
        {
            InitializeComponent();

            TB_Formula.Text = Formula;

            formulacontrol.Formula = TB_Formula.Text;
        }


        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            IsOK = true;
            formula = TB_Formula.Text;
            Close();
        }


        private void AddToFormula(string addtext)
        {
            int carin = TB_Formula.CaretIndex + addtext.Length;
            if (TB_Formula.SelectionLength > 0)
            {
                int selstart = TB_Formula.SelectionStart;
                TB_Formula.Text = TB_Formula.Text.Remove(selstart, TB_Formula.SelectionLength);
                TB_Formula.Text = TB_Formula.Text.Insert(selstart, addtext);
            }
            else
            {
                TB_Formula.Text = TB_Formula.Text.Insert(TB_Formula.CaretIndex, addtext);
            }
            TB_Formula.Focus();
            TB_Formula.CaretIndex = carin;
        }


        private void ButtonFastAdd_Click(object sender, RoutedEventArgs e)
        {
            int fadd = int.Parse(((Control)sender).Tag.ToString());

            switch (fadd)
            {
                case 0:
                    AddToFormula("x^{2} ");

                    break;
                case 1:
                    AddToFormula("x_{y} ");

                    break;
                case 2:
                    AddToFormula("\\sqrt{x} ");

                    break;
                case 3:
                    AddToFormula("\\sqrt[3]{x} ");

                    break;
                case 4:
                    AddToFormula("f(x) = x^{2} ");

                    break;
                case 5:
                    AddToFormula("\\frac{x}{y} ");

                    break;
                case 6:
                    AddToFormula("\\pi ");

                    break;
                case 7:
                    AddToFormula("\\infty ");

                    break;
                case 8:
                    AddToFormula("\\leftarrow ");

                    break;
                case 9:
                    AddToFormula("\\rightarrow ");

                    break;
                case 10:
                    AddToFormula("\\int^a_b ");

                    break;
                case 11:
                    AddToFormula("\\Delta ");

                    break;
                case 12:
                    AddToFormula("\\; ");

                    break;
                case 13:
                    AddToFormula("\\lim_{x \\to 2 }");

                    break;
                case 14:
                    AddToFormula("\\cdot ");

                    break;

            }
            formulacontrol.Formula = TB_Formula.Text;
        }

        private void TB_Formula_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                formulacontrol.Formula = TB_Formula.Text;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
