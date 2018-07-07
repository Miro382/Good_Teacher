using System;
using System.Collections.Generic;

namespace Good_Teacher.Class.Save.Output
{
    public class PresentationOutput
    {
        public Dictionary<int, OutputPage> Pages = new Dictionary<int, OutputPage>();
        public double W = 1280, H= 720;
        public DateTime CreatedTime;
        public int PresentationPagesCount = 0;
        public bool HaveScripts = false;
        public bool ScriptsAllowed = false;
    }
}
