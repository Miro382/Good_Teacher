using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Good_Teacher.Class
{
    public class RichTextBoxWorker
    {

        public static void LoadFromRTF(RichTextBox txt, string pathtoRTF)
        {
            byte[] documentBytes = Encoding.UTF8.GetBytes(File.ReadAllText(pathtoRTF));
            using (MemoryStream reader = new MemoryStream(documentBytes))
            {
                reader.Position = 0;
                txt.SelectAll();
                txt.Selection.Load(reader, DataFormats.Rtf);
            }
        }


        public static void SaveToRTF(RichTextBox txt, string pathtoSave)
        {
            txt.SelectAll();
            txt.Selection.Save(new FileStream(pathtoSave, FileMode.OpenOrCreate, FileAccess.Write), DataFormats.Rtf);
            txt.Selection.Select(txt.Selection.Start, txt.Selection.Start);
        }


        public static string SaveRTFToString(RichTextBox txt)
        {
            TextRange tr = new TextRange(txt.Document.ContentStart,
                        txt.Document.ContentEnd);
            MemoryStream ms = new MemoryStream();
            tr.Save(ms, DataFormats.Rtf);
            return ASCIIEncoding.Default.GetString(ms.ToArray());
        }


        public static void LoadRTFFromString(RichTextBox txt, string str)
        {
            using (Stream s = StreamFromString(str))
            {
                txt.SelectAll();
                txt.Selection.Load(s, DataFormats.Rtf);
            }
        }


        public static Stream StreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


    }
}
