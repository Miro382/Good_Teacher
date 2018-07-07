using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Markup;
using System.Xml;
using ZstdNet;

namespace Good_Teacher.Class
{

    public class SaveEditor
    {

        public static Stream StreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public bool SaveWithCompression(string Path, DataStore Data)
        {
            try
            {
                string ser = Serialize(Data);

                File.WriteAllBytes(Path, ZipStr(ser));

                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public bool SaveWithCompressionO(string Path, object Data)
        {
            try
            {
                string ser = SerializeObject(Data);

                File.WriteAllBytes(Path, ZipStr(ser));

                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }


        public static byte[] ZipStr(String str)
        {
            using (Compressor compressor = new Compressor())
            {
                return compressor.Wrap(Encoding.UTF8.GetBytes(str));
            }
            
        }


        public static string UnZipStr(byte[] input)
        {
            using (var decompressor = new Decompressor())
            {
               byte[] decompressedData = decompressor.Unwrap(input);
                return Encoding.UTF8.GetString(decompressedData);
            }
            
        }


        public DataStore LoadWithCompression(string Path)
        {
            byte[] flt = File.ReadAllBytes(Path);

            string obj = UnZipStr(flt);

            return Deserialize(obj);
        }

        public Object LoadWithCompressionO(string Path)
        {
            byte[] flt = File.ReadAllBytes(Path);

            string obj = UnZipStr(flt);

            return DeserializeObject(obj);
        }

        public string LoadWithCompressionJson(string Path)
        {
            byte[] flt = File.ReadAllBytes(Path);

            string obj = UnZipStr(flt);

            return obj;
        }


        public bool SaveWithEncoding(string Path, DataStore Data)
        {
            try
            {
                string ser = Serialize(Data);


                ser = Base64Encode(ser);
                File.WriteAllText(Path, ser);

                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }


        public DataStore LoadWithEncoding(string Path)
        {
            string flt = File.ReadAllText(Path);

            flt = Base64Decode(flt);

            return Deserialize(flt);
        }


        public bool Save(string Path, DataStore Data)
        {
            try
            {
                File.WriteAllText(Path, Serialize(Data));

                return true;

            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }


        public DataStore Load(string Path)
        {
            string flt = File.ReadAllText(Path);

            return Deserialize(flt);
        }



        public static string Base64Encode(string plainText)
        {
            if (!string.IsNullOrEmpty(plainText))
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            else
            {
                return "";
            }
        }

        public static string Base64Decode(string EncodedText)
        {
            if (!string.IsNullOrEmpty(EncodedText))
            {
                byte[] base64EncodedBytes = System.Convert.FromBase64String(EncodedText);
                return Encoding.UTF8.GetString(base64EncodedBytes);
            }
            else
            {
                return "";
            }
        }


        
        public static string SerializeObject(Object obj)
        {
            string ser = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });

            return ser;
        }


        public static object DeserializeObject(string json)
        {
            Object obj = JsonConvert.DeserializeObject(json, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });
            return obj;
        }


        private string Serialize(DataStore dts)
        {
            string ser = JsonConvert.SerializeObject(dts, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });

            return ser;
        }


        private DataStore Deserialize(string dts)
        {
            DataStore dt = (DataStore)JsonConvert.DeserializeObject(dts, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });

            return dt;
        }
        
        public static string XMLSerialize(object toSerialize)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = false;
            settings.NewLineOnAttributes = true;
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb, settings);
            XamlDesignerSerializationManager manager = new XamlDesignerSerializationManager(writer);
            manager.XamlWriterMode = XamlWriterMode.Expression;
            XamlWriter.Save(toSerialize, manager);

            return sb.ToString();
        }

        public static object XMLDeserialize(string xmlText)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);
            return XamlReader.Load(new XmlNodeReader(doc));
        }




    }
}
