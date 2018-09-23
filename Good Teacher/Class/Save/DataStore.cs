using System.Collections.Generic;
using System.Windows.Media;
using Good_Teacher.Class;
using Good_Teacher.Class.Save;
using Good_Teacher.Class.TestClass;


namespace Good_Teacher
{
    public class DataStore
    {
        public List<Test_DefaultAbstract> pages = new List<Test_DefaultAbstract>();
        public List<RepeatingData> RepeatingControls = new List<RepeatingData>();
        public string AllBackground = "";
        public ImageRepresentation ImageBrush;
        public bool IsImageBrush = false;
        public BitmapScalingMode BitmapScalingMode = BitmapScalingMode.Linear;
        public ResourcesArchive archive = new ResourcesArchive();
        public double CanvasW = 1280, CanvasH = 720;
        public bool BlockPresentationInput = false;
        public Brush OutsideBrush = new SolidColorBrush(Color.FromRgb(44, 62, 80));
        public long LastAddedImageID = 0;
        public bool OptimizedMode = true;
        public bool SaveOutput = false;
        public bool SaveTemporaryData = true;
        public bool ClickToNext = false;
        public bool CacheCanvas = false;
        public bool HideInput = false;
        public bool AreScriptsAllowed = false;
        public bool ScriptDebug = false;
        public string ScriptWarningMessage = "";
        public List<FontPackage> FontManager = new List<FontPackage>();

        public bool UploadWholeFile = false;
        public string UploadWholeFileAddress = "";

        public float SavedVersionCode = 0;

    }
}
