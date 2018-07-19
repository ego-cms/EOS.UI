using Android.Content;
using Android.Views;
using Java.Lang;

namespace EOS.UI.Android.Components
{
    internal class UpdateMenuItemsVisibilityListener: View, IRunnable
    {
        private CircleMenu _menu;

        internal UpdateMenuItemsVisibilityListener(Context context, CircleMenu menu) : base(context)
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
