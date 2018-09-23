using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher.Controls
{
    public class NumberBox : TextBox
    {
        public bool Success { get { return success; } }
        private bool success = false;

        public NumberBox()
        {
            PreviewTextInput += NumberBox_PreviewTextInput;
            DataObject.AddPastingHandler(this, OnPaste);
        }

        public double GetDouble(double FailDefaultValue = 0)
        {
            double val = 0;
            if(double.TryParse(Text,out val))
            {
                success = true;
                return val;
            }
            else
            {
                success = false;
                Text = "" + FailDefaultValue;
                return FailDefaultValue;
            }
        }


        public int GetInt(int FailDefaultValue = 0)
        {
            int val = 0;
            if (int.TryParse(Text, out val))
            {
                success = true;
                return val;
            }
            else
            {
                success = false;
                Text = "" + FailDefaultValue;
                return FailDefaultValue;
            }
        }


        public float GetFloat(float FailDefaultValue = 0)
        {
            float val = 0;
            if (float.TryParse(Text, out val))
            {
                success = true;
                return val;
            }
            else
            {
                success = false;
                Text = "" + FailDefaultValue;
                return FailDefaultValue;
            }
        }


        private void NumberBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9,-]+");
            return !regex.IsMatch(text);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
