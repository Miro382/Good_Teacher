using System.Windows;

namespace Good_Teacher.Class.Animations
{
    public interface IAnimation
    {
        void MakeAnimation(FrameworkElement elm);
        int GetID();
    }
}
