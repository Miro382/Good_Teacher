using Good_Teacher.Class.Serialization.Ser_Controls;
using Good_Teacher.Controls;
using System.Collections.Generic;

namespace Good_Teacher.Class.Save
{
    public class RepeatingData
    {
        public ContentViewer_Serialization contentViewer = new ContentViewer_Serialization();
        public string Name = "";
        public List<int> IgnorePages = new List<int>();

        public RepeatingData()
        {

        }

        public RepeatingData(ContentViewer cCV, string name, DataStore data)
        {
            contentViewer = new ContentViewer_Serialization(cCV, data);
            Name = name;
        }

    }
}
