using System.Windows.Media;

namespace Good_Teacher.Class.Serialization
{
    public class ModelPath
    {
        public string LocalPathToModel = "";
        public bool LoadTexture = true;
        public Color DefaultColor = Colors.LightGray;

        public ModelPath()
        {

        }


        public ModelPath(string Lpath, bool texture, Color defcolor)
        {
            LocalPathToModel = Lpath;
            LoadTexture = texture;
            DefaultColor = defcolor;
        }


    }
}
