using Android.Content;
using Android.Views;
using Java.Lang;

namespace EOS.UI.Droid.Components
{
    internal class UpdateMenuItemsVisibilityRunnable: View, IRunnable
    {
        private CircleMenu _menu;

        internal UpdateMenuItemsVisibilityRunnable(Context context, CircleMenu menu) : base(context)
        {
            _menu = menu;
        }

        #region IRunnable implementation

        public void Run()
        {
            _menu.HandleUpdateMenuItemsVisibility();
        }

        #endregion
    }
}
