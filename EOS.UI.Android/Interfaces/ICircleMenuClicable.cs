namespace EOS.UI.Android.Interfaces
{
    interface ICircleMenuClicable
    {
        void PerformClick(int id, bool isSubMenu = false, bool isOpened = false);
        bool Locked { get; }
    }
}
