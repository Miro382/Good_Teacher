using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;

namespace Good_Teacher.Class.Workers
{
    public class WebWorker
    {

        public static string ReadText(string URL)
        {
            string content = "";
            using (WebClient client = new WebClient())
            {
                Stream stream = client.OpenRead(URL);
                StreamReader reader = new StreamReader(stream);
                content = reader.ReadToEnd();
            }

            return content;
        }


        public static void GetVersionInfo(out float vercode, out float Imprt, out string VerText, out DateTime Date)
        {
            vercode = 0;
            Imprt = 0;
            VerText = "-";
            Date = DateTime.Now;

            string GVer = ReadText("http://goodteacher.diodegames.eu/internal/Version.txt");
            if (!String.IsNullOrWhiteSpace(GVer))
            {
                JToken token = JObject.Parse(GVer);

                vercode = (float)token.SelectToken("Version");
                Imprt = (float)token.SelectToken("Important");
                VerText = (string)token.SelectToken("NewVersionText");
                try
                {
                    Date = DateTime.ParseExact((string)token.SelectToken("Date"), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                    Date = DateTime.Now;
                }
            }
        }

    }
}
