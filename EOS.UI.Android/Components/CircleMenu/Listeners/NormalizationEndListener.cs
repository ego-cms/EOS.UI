using Android.Content;
using Android.Views;
using Java.Lang;

namespace EOS.UI.Android.Components
{
    internal class NormalizationEndListener: View, IRunnable
    {
        private CircleMenu _menu;

        internal NormalizationEndListener(Context context, CircleMenu menu) : base(context)
        {
            _menu = menu;
        }

        #region IRunnable implementation

        public void Run()
        {
            _menu.HandleNormalizationEnd();
        }

        #endregion
    }
}
