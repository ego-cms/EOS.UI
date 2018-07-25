namespace EOS.UI.Droid.Interfaces
{
    interface ICircleMenuClickable
    {
        void PerformClick(int id, bool isSubMenu = false, bool isOpened = false);
        bool Locked { get; }
    }
}
