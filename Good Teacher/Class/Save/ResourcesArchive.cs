using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Good_Teacher.Class
{
    public class ResourcesArchive
    {
        public Dictionary<string, ResourceData> Res = new Dictionary<string, ResourceData>();

        public byte[] GetDataOnly(string Key)
        {
            if (Res.ContainsKey(Key))
            {
                return Res[Key].Data;
            }
            else
                return null;
        }

        public void StoreImage(string Path, string keyname)
        {
            if (!Res.ContainsKey(keyname))
            {
                if(File.Exists(Path))
                Res[keyname] = new ResourceData(ImageWorker.ToByteData(Path));
            }else
            {
                StoreImage(Path, keyname + "(C)");
            }
        }

        public void StoreImageFromWeb(string url, string keyname)
        {
            byte[] img;

            if (ImageWorker.ImageFromWebToBase64Data(url, out img))
            {
                if (!Res.ContainsKey(keyname))
                {
                    Res[keyname] = new ResourceData(img);
                }
                else
                {
                    Res[keyname+"(C)"] = new ResourceData(img);
                }
            }
            else
                MessageBox.Show(Strings.ResStrings.WrongUrl, Strings.ResStrings.Error);
        }


        public void StoreImage(BitmapSource bitmap, string keyname)
        {
            if(!Res.ContainsKey(keyname))
            Res[keyname] = new ResourceData(ImageWorker.BitmapSourceToByteData(bitmap));
            else
                StoreImage(bitmap, keyname + "(C)");
        }


        public ImageSource GetImage(string Key)
        {
            if (Res.ContainsKey(Key))
                return ImageWorker.ByteDataToImage(Res[Key].Data);
            else
                return null;
        }


        public int GetImageW(string Key)
        {
            if (Res.ContainsKey(Key))
                return Res[Key].W;
            else
                return 0;
        }

        public int GetImageH(string Key)
        {
            if (Res.ContainsKey(Key))
                return Res[Key].H;
            else
                return 0;
        }

        public bool GetImageSize(string Key, out int w, out int h)
        {
            if (Res.ContainsKey(Key))
            {
                w = Res[Key].W;
                h = Res[Key].H;
                return true;
            }
            else
            {
                w = 0;
                h = 0;
                return false;
            }
        }


        public static Stream FromBase64(string content)
        {
            return new MemoryStream(Convert.FromBase64String(content));
        }

        public ImageSource GetImage(string Key, int width)
        {
            if (Res.ContainsKey(Key))
            {

                using (var ms = new System.IO.MemoryStream(Res[Key].Data))
                {
                    BitmapImage src = new BitmapImage();
                    src.BeginInit();
                    src.DecodePixelWidth = width;
                    src.CacheOption = BitmapCacheOption.OnLoad;
                    src.StreamSource = ms;
                    src.EndInit();
                    return src;
                }

            }
            else
                return null;
        }


        public ImageSource GetImageOptimal(string Key, int width, int height)
        {
            if (Res.ContainsKey(Key))
            {

                using (var ms = new System.IO.MemoryStream(Res[Key].Data))
                {
                    BitmapImage src = new BitmapImage();
                    src.BeginInit();

                    if(width>height)
                        src.DecodePixelWidth = width;
                    else
                        src.DecodePixelHeight = height;

                    src.CacheOption = BitmapCacheOption.OnLoad;
                    src.StreamSource = ms;
                    src.EndInit();

                    return src;
                }

            }
            else
                return null;
        }


        public ImageSource GetImageByHeight(string Key, int height)
        {
            if (Res.ContainsKey(Key))
            {

                using (var ms = new System.IO.MemoryStream(Res[Key].Data))
                {
                    BitmapImage src = new BitmapImage();
                    src.BeginInit();
                    src.DecodePixelHeight = height;
                    src.CacheOption = BitmapCacheOption.OnLoad;
                    src.StreamSource = ms;
                    src.EndInit();
                    return src;
                }

            }
            else
                return null;
        }

        public bool ImageExists(string Key)
        {
            return Res.ContainsKey(Key);
        }

        public void RemoveData(string Key)
        {
            if (Res.ContainsKey(Key))
            {
                Res.Remove(Key);
            }
        }


    }
}
