using Jint;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Good_Teacher.Class.Workers
{
    public class ScriptingWorker
    {
        private int cpage = 0;

        public void DoScript(string script, Canvas canvas,int currpage, bool SCRdebug)
        {
            Engine
                engine = new Engine(cfg => { cfg.AllowClr(); cfg.CatchClrExceptions(); })
                    .SetValue("Alert", new Action<string, string>(Alert))
                    .SetValue("alert", new Action<string>(alert))
                    .SetValue("SetX", new Action<UIElement, double>(SetX))
                    .SetValue("SetY", new Action<UIElement, double>(SetY))
                    .SetValue("SetW", new Action<FrameworkElement, double>(SetW))
                    .SetValue("SetH", new Action<FrameworkElement, double>(SetH))
                    .SetValue("GetX", new Func<UIElement, double>(GetX))
                    .SetValue("GetY", new Func<UIElement, double>(GetY))
                    .SetValue("GetW", new Func<FrameworkElement, double>(GetW))
                    .SetValue("GetH", new Func<FrameworkElement, double>(GetH))
                    .SetValue("FindByID", new Func<long,FrameworkElement, FrameworkElement>(GetControlByID))
                    .SetValue("Canvas", canvas)
                    .SetValue("Element", canvas.Children)
                    .SetValue("CurrentPage", new Func<int>(CurrentPage))
                    .SetValue("Visible", 0)
                    .SetValue("Collapsed", 2)
                    ;

            cpage = currpage;
            try
            {
                engine.Execute(script);

            }
            catch(Jint.Runtime.JavaScriptException jsrex)
            {
                if (SCRdebug)
                {
                    MessageBox.Show( jsrex.Error + Environment.NewLine + Strings.ResStrings.LineNumber + ": " + jsrex.LineNumber);
                }
            }
            catch (Jint.Parser.ParserException Pex)
            {
                if (SCRdebug)
                {
                    MessageBox.Show(Pex.Message + Environment.NewLine + Strings.ResStrings.LineNumber + ": " + Pex.LineNumber);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
            }
        }

        public int CurrentPage()
        {
            return cpage;
        }

        public void Alert(string caption, string text)
        {
            MessageBox.Show(text, caption);
        }

        public void alert(string text)
        {
            MessageBox.Show(text);
        }

        public double GetX(UIElement element)
        {
            return Canvas.GetLeft(element);
        }

        public double GetY(UIElement element)
        {
            return Canvas.GetTop(element);
        }

        public void SetX(UIElement element, double posx)
        {
            Canvas.SetLeft(element, posx);
        }

        public void SetY(UIElement element, double posy)
        {
            Canvas.SetTop(element, posy);
        }

        public void SetW(FrameworkElement element, double w)
        {
            element.Width = w;
        }

        public void SetH(FrameworkElement element, double h)
        {
            element.Height = h;
        }

        public double GetW(FrameworkElement element)
        {
            return element.Width;
        }

        public double GetH(FrameworkElement element)
        {
            return element.Height;
        }

        public FrameworkElement GetControlByID(long ID, FrameworkElement parent)
        {
            return ControlWorker.FindChild<FrameworkElement>(parent, "ID_" + ID);
        }

    }
}
