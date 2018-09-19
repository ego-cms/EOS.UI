namespace EOS.UI.Droid.Interfaces
{
    interface ICircleMenuClickable
    {
        void PerformClick(int id, bool isSubMenu = false, bool isOpened = false);
        void PerformSwipe(bool isForward);
        bool Locked { get; }
    }
}
