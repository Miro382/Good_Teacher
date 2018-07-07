using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Windows
{
    /// <summary>
    /// Interaction logic for Window_ColorPicker.xaml
    /// </summary>
    public partial class Window_ColorPicker : Window
    {

        Bitmap image,brg;
        bool clicked = false,brchan=false;


        private bool OK = false;
        private System.Windows.Media.Color Newcolor;

        public Window_ColorPicker()
        {
            InitializeComponent();

            hexadecimal.Text = "#FF000000";
            SetColorFromHex();

            image = new Bitmap(Properties.Resources.palette, new System.Drawing.Size(300, 300));
            ChangeSliders();
        }


        public Window_ColorPicker(System.Windows.Media.Color color)
        {
            InitializeComponent();

            OldColor.Fill = new SolidColorBrush(color);
            hexadecimal.Text = ColorToHexConverter(color);
            SetColorFromHex();

            image = new Bitmap(Properties.Resources.palette, new System.Drawing.Size(300, 300));
            ChangeSliders();
        }



        public Window_ColorPicker(System.Windows.Media.Brush brush)
        {
            InitializeComponent();
            System.Windows.Media.Color color = Colors.White;
            if(brush!=null)
            {
                if (brush is SolidColorBrush)
                    color = ((SolidColorBrush)brush).Color;
            }

            OldColor.Fill = new SolidColorBrush(color);
            hexadecimal.Text = ColorToHexConverter(color);
            SetColorFromHex();

            image = new Bitmap(Properties.Resources.palette, new System.Drawing.Size(300, 300));
            ChangeSliders();
        }


        public Window_ColorPicker(string color)
        {
            InitializeComponent();

            if (String.IsNullOrWhiteSpace(color))
            {
                hexadecimal.Text = "#FF000000";
                SetColorFromHex();
            }
            else
            {
                try
                {
                    hexadecimal.Text = color;
                    SetColorFromHex();
                    OldColor.Fill = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(color));
                }
                catch
                {
                    hexadecimal.Text = "#FF000000";
                    SetColorFromHex();
                }
            }


            image = new Bitmap(Properties.Resources.palette, new System.Drawing.Size(300, 300));
            ChangeSliders();
        }


        public bool IsOK()
        {
            return OK;
        }

        public System.Windows.Media.Color GetColor()
        {
            return Newcolor;
        }

        public string GetHexadecimalColor()
        {
            return hexadecimal.Text;
        }

        void ChoosedNewColor(System.Windows.Point p)
        {
            try
            {
                int x = (int)p.X;
                int y = (int)p.Y;

                if (x < image.Width && y < image.Height)
                {

                    if (image.GetPixel(x, y).A != 0)
                    {
                        byte r = image.GetPixel(x, y).R;
                        byte g = image.GetPixel(x, y).G;
                        byte b = image.GetPixel(x, y).B;
                        SliderBlue.Value = b;
                        SliderGreen.Value = g;
                        SliderRed.Value = r;
                        SliderAlpha.Value = 255;
                        Alpha.Text = "255";
                        Red.Text = "" + r;
                        Green.Text = "" + g;
                        Blue.Text = "" + b;
                        hexadecimal.Text = ColorToHexConverter(System.Windows.Media.Color.FromArgb(255,r,g,b));
                        ChangeBrightnessImage(r,g,b);


                        CurColor.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, r, g, b));
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }




        void ChoosedNewColorByBrightness(System.Windows.Point p)
        {
            try
            {
                int x = (int)p.X;
                int y = (int)p.Y;

                if (x < brg.Width && y < brg.Height)
                {

                    if (brg.GetPixel(x, y).A != 0)
                    {
                        byte r = brg.GetPixel(x, y).R;
                        byte g = brg.GetPixel(x, y).G;
                        byte b = brg.GetPixel(x, y).B;
                        SliderBlue.Value = b;
                        SliderGreen.Value = g;
                        SliderRed.Value = r;
                        SliderAlpha.Value = 255;
                        Alpha.Text = "255";
                        Red.Text = "" + r;
                        Green.Text = "" + g;
                        Blue.Text = "" + b;
                        hexadecimal.Text = ColorToHexConverter(System.Windows.Media.Color.FromArgb(255, r, g, b));

                        CurColor.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, r, g, b));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }




        void ChangeBrightnessImage(int r,int g, int b)
        {
            System.Drawing.Color color = System.Drawing.Color.FromArgb(255,r,g,b);

            brg = new Bitmap(30, 200);
            for (int j = 0; j < 200; j++)
            {
                for (int i = 0; i < 30; i++)
                {
                    if (j<100)
                    brg.SetPixel(i, j, SetBrightness(color, ((float)j / 100) ));
                    else
                    brg.SetPixel(i, j, SetBrightnessUP(color, ((float)(j-100) / 100)));

                }
            }
            brightness.Source = BitmapToImageSource(brg);
        }


        System.Drawing.Color SetBrightness(System.Drawing.Color color, float pow)
        {
            int r = color.R;
            int g = color.G;
            int b = color.B;

            r = (int)(r * pow);
            g = (int)(g * pow);
            b = (int)(b * pow);

            if (r>255)
                r = 255;
            if (g > 255)
                g = 255;
            if (b > 255)
                b = 255;

            if (r < 0)
                r = 0;
            if (g < 0)
                g = 0;
            if (b < 0)
                b = 0;

            return System.Drawing.Color.FromArgb(255,r,g,b);
        }



        System.Drawing.Color SetBrightnessUP(System.Drawing.Color color, float pow)
        {
            int r = color.R;
            int g = color.G;
            int b = color.B;


            int sm = r;

            if (g < sm)
                sm = g;

            if (b < sm)
                sm = b;



            float change = 255 - sm;
            change /= 100;
            pow += 0.02f;

            r = (int)(r + (100*(change * pow)));
            g = (int)(g + (100 * (change * pow)));
            b = (int)(b + (100 * (change * pow)));

            if (r > 255)
                r = 255;
            if (g > 255)
                g = 255;
            if (b > 255)
                b = 255;

            if (r < 0)
                r = 0;
            if (g < 0)
                g = 0;
            if (b < 0)
                b = 0;

            return System.Drawing.Color.FromArgb(255, r, g, b);
        }


        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }


        void NewColorFromRGB()
        {
            try
            {
                System.Windows.Media.Color color = System.Windows.Media.Color.FromArgb(byte.Parse(Alpha.Text), byte.Parse(Red.Text), byte.Parse(Green.Text), byte.Parse(Blue.Text));

                CurColor.Fill = new SolidColorBrush(color);
                hexadecimal.Text = ColorToHexConverter(color);

            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                Alpha.Text = "255";
                Red.Text = "255";
                Green.Text = "255";
                Blue.Text = "255";
               CurColor.Fill = new SolidColorBrush(Colors.White);
            }
        }

        private void palette_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point p = e.GetPosition(palette);
            ChoosedNewColor(p);
            clicked = true;
        }

        private void palette_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicked)
            {
                System.Windows.Point p = e.GetPosition(palette);
                ChoosedNewColor(p);
            }
        }

        private void palette_MouseUp(object sender, MouseButtonEventArgs e)
        {
            clicked = false;
        }

        private void palette_MouseLeave(object sender, MouseEventArgs e)
        {
            clicked = false;
        }


        void ChangeSliders()
        {
            try
            {
                SliderBlue.Value = int.Parse(Blue.Text);
                SliderGreen.Value = int.Parse(Green.Text);
                SliderRed.Value = int.Parse(Red.Text);
                SliderAlpha.Value = int.Parse(Alpha.Text);
            }
            catch { }
        }


        private void Control_LostFocus(object sender, RoutedEventArgs e)
        {
            NewColorFromRGB();
            ChangeSliders();
            ChangeBrightnessImage(byte.Parse(Red.Text), byte.Parse(Green.Text), byte.Parse(Blue.Text));
        }


        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter)
            {
                NewColorFromRGB();
                ChangeSliders();
                ChangeBrightnessImage(byte.Parse(Red.Text), byte.Parse(Green.Text), byte.Parse(Blue.Text));
            }
            else if (e.Key == Key.Escape)
            {
                Keyboard.ClearFocus();
                NewColorFromRGB();
                ChangeSliders();
                ChangeBrightnessImage(byte.Parse(Red.Text), byte.Parse(Green.Text), byte.Parse(Blue.Text));
            }
            
        }



        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            if (!clicked)
            {
                if (sender == SliderAlpha)
                {
                    Alpha.Text = "" + (int)SliderAlpha.Value;
                }
                else if (sender == SliderRed)
                {
                    Red.Text = "" + (int)SliderRed.Value;
                }
                else if (sender == SliderGreen)
                {
                    Green.Text = "" + (int)SliderGreen.Value;
                }
                else if (sender == SliderBlue)
                {
                    Blue.Text = "" + (int)SliderBlue.Value;
                }

                NewColorFromRGB();
            }

            if(!brchan)
                ChangeBrightnessImage(byte.Parse(Red.Text), byte.Parse(Green.Text), byte.Parse(Blue.Text));
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            OK = true;
            Newcolor = ((SolidColorBrush)CurColor.Fill).Color;
            Close();
        }

        void SetColorFromHex()
        {
            try
            {
                System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(hexadecimal.Text);
                Red.Text = "" + color.R;
                Green.Text = "" + color.G;
                Blue.Text = "" + color.B;
                Alpha.Text = "" + color.A;
            }catch(Exception ex)
            {
                hexadecimal.Text = "#FF000000";
                Debug.WriteLine(""+ex);
            }
        }



        private void hexadecimal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetColorFromHex();
                NewColorFromRGB();
                ChangeSliders();
            }
            else if (e.Key == Key.Escape)
            {
                Keyboard.ClearFocus();
                SetColorFromHex();
                NewColorFromRGB();
                ChangeSliders();
            }
        }


        private void hexadecimal_LostFocus(object sender, RoutedEventArgs e)
        {
            SetColorFromHex();
            NewColorFromRGB();
            ChangeSliders();
        }

        private static String ColorToHexConverter(System.Windows.Media.Color c)
        {
            return "#" + c.A.ToString("X2") + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }


        private void brightness_MouseDown(object sender, MouseButtonEventArgs e)
        {
            brchan = true;
            System.Windows.Point p = e.GetPosition(brightness);
            ChoosedNewColorByBrightness(p);
            clicked = true;
        }

        private void brightness_MouseLeave(object sender, MouseEventArgs e)
        {
            clicked = false;
            brchan = false;
        }

        private void ButtonColor_Click(object sender, RoutedEventArgs e)
        {
            string tag = (string)((Button)sender).Tag;

            hexadecimal.Text = tag;
            SetColorFromHex();
            ChangeSliders();
        }

        private void brightness_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicked)
            {
                System.Windows.Point p = e.GetPosition(brightness);
                ChoosedNewColorByBrightness(p);
            }
        }

        private void brightness_MouseUp(object sender, MouseButtonEventArgs e)
        {
            clicked = false;
            brchan = false;
        }

    }
}
