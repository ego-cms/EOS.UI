using Android.Content;
using Android.Views;
using Java.Lang;

namespace EOS.UI.Android.Components
{
    internal class NormalizationEndRunnable: View, IRunnable
    {
        private CircleMenu _menu;

        internal NormalizationEndRunnable(Context context, CircleMenu menu) : base(context)
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
