namespace EOS.UI.Droid.Interfaces
{
    /// <summary>
    /// Interface wich implements show/hide menu items logic
    /// </summary>
    internal interface IMenuStateCommutator
    {
        void ShowMenuItems(int iteration);

        void HideMenuItems(int iteration);
    }
}
