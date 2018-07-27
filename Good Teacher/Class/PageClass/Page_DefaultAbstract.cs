using Good_Teacher.Class.Animations;
using Good_Teacher.Class.Enumerators;
using System.Collections.Generic;

namespace Good_Teacher.Class.TestClass
{
    public abstract class Test_DefaultAbstract
    {
        public int Position;
        public string canvas;
        public string canvasbrush;
        public ImageRepresentation ImageBrush;
        public bool IsImageBrush;
        public bool CustomBrush;
        public bool isUnlocked = true; //Locked
        public bool isHidden = false; //Hidden - Secret page
        public List<object> CustomControls = new List<object>();
        public List<IAnimation> AnimationList = new List<IAnimation>();

        public SoundAction.SoundActionType soundActionType = SoundAction.SoundActionType.NoAction;
        public string PathToPlaySound = "";
        public bool SoundLoop = true;

        public string ScriptCode = "";
        public int LastID = 0;

        public int TransitionMove = 0;
        public TransitionTypeEnum.TransitionType transitionType = TransitionTypeEnum.TransitionType.Manual;

        abstract public TestTypeID.Test_Type TestType { get; }

    }
}
