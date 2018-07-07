using System;
using System.Collections.Generic;

namespace Good_Teacher.Class.Save.Output
{
    public class OutputPage : IComparable<OutputPage>
    {
        public byte[] Image;
        public int Page = 0;
        public int GoodAnswers = 0, WrongAnswers = 0, AllAnswers = 0;
        public List<InputType> InputList = new List<InputType>();
        public byte[] strokeCollection;


        public int CompareTo(OutputPage other)
        {
            if (other != null)
                return Page - other.Page;
            else
                return 1;
        }

    }
}
